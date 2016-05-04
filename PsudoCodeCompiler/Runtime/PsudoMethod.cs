using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using PseudoCodeCompiler.Runtime.Instructions;
using System.Collections;
using System.Text.RegularExpressions;

namespace PseudoCodeCompiler.Runtime
{
    [TypeConverterAttribute(typeof(MethodObjectConverter))]
    public class PsudoMethod
    {
        public string Name { get; set; }
        //public Dictionary<string, object> LocalVariables { get; set; }
        public List<string> ArgumentTypes { get; set; }
        public List<string> ArgumentNames { get; set; }

        public List<PsudoInstruction> Instructions
        {
            get
            {
                if (this.CodeBlock == null)
                    this.CodeBlock = new PsudoCodeBlock();
                return this.CodeBlock.Instructions;
            }
            set
            {
                if (this.CodeBlock == null)
                    this.CodeBlock = new PsudoCodeBlock();
                this.CodeBlock.Instructions = value;
            }
        }
        public PsudoCodeBlock CodeBlock { get; set; }
        public PsudoClass ParentClass { get; set; }
        public PsudoProgram Program { get; set; }

        public PsudoMethod(string name, PsudoClass pClass)
        {
            this.Name = name;
            //this.LocalVariables = new Dictionary<string, object>();
            this.ParentClass = pClass;
            this.Instructions = new List<PsudoInstruction>();
            this.ArgumentNames = new List<string>();
            this.ArgumentTypes = new List<string>();
        }

        internal void Run(PsudoProgram psudoProgram)
        {
            this.Program = psudoProgram;
            this.CodeBlock.Run(psudoProgram);
        }

        public string ResolveName(string p)
        {
            if (p == null)
                return "";

            string name = p.ToLower();
            // Fix the temp characters
            name = name.Replace('#', '(');
            name = name.Replace('@', ')');

            // If this is a simple variable name, return it
            if (Regex.IsMatch(name, @"^[a-zA-Z0-9_]+$"))
                return name;

            // Is this a single dimension array index
            if (Regex.IsMatch(name, @"^[a-zA-Z0-9_]+[(][a-zA-Z0-9]+[)]$"))
            {
                // check if this is a simple variable
                if (!Regex.IsMatch(name, @"^[a-zA-Z0-9_]*[(]*[0-9]*[)]*$"))
                {
                    string indexPattern = @"(?<=[(])[a-zA-Z0-9_]*(?=[)])";
                    // If this is a variable lookup then lookup the index value and update
                    Match index = Regex.Match(name, indexPattern);
                    object value = GetVariable(index.Value);
                    // Convert the double to an integer index
                    int indexVal = (int)(double)value;

                    name = Regex.Replace(name, indexPattern, indexVal.ToString());
                }
            }

            // Is this a two dimension array index
            if (Regex.IsMatch(name, @"^[a-zA-Z0-9_]+[(][a-zA-Z0-9]+[\s]*[,][\s]*[a-zA-Z0-9]+[)]$"))
            {
                string indexPattern = @"(?<=[(])[a-zA-Z0-9_ ,]*(?=[)])";
                // pull the index information
                Match index = Regex.Match(name, indexPattern);

                string[] indexes = index.Value.Split(',');
                string resolvedIndexes = GetVariable(indexes[0].Trim()) + "," + GetVariable(indexes[1].Trim());
                name = name.Replace(index.Value, resolvedIndexes);
            }

            return name;
        }

        internal object GetVariable(string p)
        {
            string name = ResolveName(p);

            //if (this.LocalVariables.ContainsKey(name))
            //{
            //    return this.LocalVariables[name];
            //}
            //else 
            if (this.ParentClass != null && this.ParentClass.ClassVariables.ContainsKey(name))
            {
                return this.ParentClass.ClassVariables[name];
            }
            else if (this.Program.GlobalVariables.ContainsKey(name))
            {
                return this.Program.GlobalVariables[name];
            }

            return p;
        }

        internal void SetVariable(string p, object value)
        {
            string name = ResolveName(p);

            //if (this.LocalVariables.ContainsKey(name))
            //{
            //    this.LocalVariables[name] = value;
            //    return;
            //}
            //else 
            if (this.ParentClass != null && this.ParentClass.ClassVariables.ContainsKey(name))
            {
                this.ParentClass.ClassVariables[name] = value;
                return;
            }
            else if (this.Program.GlobalVariables.ContainsKey(name))
            {
                this.Program.GlobalVariables[name] = value;
                return;
            }

            throw new Exception("Use of Unknown Variable " + p);
        }

        internal void CreateVariable(string p, object value)
        {
            string name = ResolveName(p);
            this.Program.GlobalVariables[name] = value;
        }

        private string NameSanitize(string key)
        {
            return key.Replace('(', '_').Replace(')', '_');
        }

        private Dictionary<string, object> AllVariablesDictionary
        {
            get
            {
                Dictionary<string, object> h = new Dictionary<string, object>();

                //foreach (var variable in this.LocalVariables)
                //{
                //    h[NameSanitize(variable.Key)] = variable.Value;
                //}
                if (this.ParentClass != null)
                {
                    foreach (var variable in this.ParentClass.ClassVariables)
                    {
                        h[NameSanitize(variable.Key)] = variable.Value;
                    }
                }
                foreach (var variable in this.Program.GlobalVariables)
                {
                    h[NameSanitize(variable.Key)] = variable.Value;
                }
                return h;
            }
        }

        public override string ToString()
        {
            if (this.ParentClass != null)
                return this.ParentClass.Name + "." + this.Name;
            return this.Name;
        }

        internal PsudoMethod GetMethod(string p)
        {
            string name = p.ToLower();
            if (this.ParentClass != null && this.ParentClass.Methods.ContainsKey(name))
            {
                return this.ParentClass.Methods[name].CleanCopy();
            }
            else if (this.Program.GlobalMethods.ContainsKey(name))
            {
                return this.Program.GlobalMethods[name].CleanCopy();
            }

            throw new Exception("Method not found");
        }

        internal PsudoMethod CleanCopy()
        {
            PsudoMethod meth = new PsudoMethod(this.Name, this.ParentClass);
            //meth.LocalVariables = new Dictionary<string, object>();
            meth.Instructions = new List<PsudoInstruction>();
            foreach (PsudoInstruction ins in this.Instructions)
                meth.Instructions.Add(ins.CopyTo(meth));
            meth.Program = this.Program;
            meth.ArgumentNames = this.ArgumentNames;
            meth.ArgumentTypes = this.ArgumentTypes;

            return meth;
        }
    }

    public class MethodObjectConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(PsudoMethod))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return (value as PsudoMethod).Name;
            }
            
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
