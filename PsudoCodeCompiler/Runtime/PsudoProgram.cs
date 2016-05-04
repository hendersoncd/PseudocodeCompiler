using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using PseudoCodeCompiler.Runtime.Instructions;

namespace PseudoCodeCompiler.Runtime
{
    public class Output_EventArgs : EventArgs
    {
        public string Message { get; set; }

        public Output_EventArgs(string message)
        {
            this.Message = message;
        }
    }

    public class Instruction_EventArgs : EventArgs
    {
        public int NextLine { get; set; }

        public Instruction_EventArgs(int nextLine)
        {
            this.NextLine = nextLine;
        }
    }

    public delegate bool EOFDelegate();
    public delegate string[] ReadLineDelegate();
    public delegate string GetColumnDelegate(int i);
    public class PsudoProgram : ICustomTypeDescriptor
    {
        public EventHandler<Output_EventArgs> StandardOutput;
        public EventHandler<Output_EventArgs> ErrorOutput;
        public EventHandler<Output_EventArgs> DebugOutput;
        public EventHandler<Instruction_EventArgs> AfterInstruction;
        public EventHandler<Instruction_EventArgs> BeforeInstruction;
        public EventHandler ProgramComplete;
        public ReadLineDelegate DoReadLine { get; set; }
        public GetColumnDelegate DoGetColumn { get; set; }
        public EOFDelegate DoEOF { get; set; }

        public Dictionary<string, PsudoClass> Classes { get; set; }
        public Dictionary<string, PsudoMethod> GlobalMethods { get; set; }
        public Dictionary<string, object> GlobalVariables { get; set; }
        public PsudoMethod MainMethod { get; set; }
        bool abortRun = false;

        public Stack<PsudoMethod> callStack = new Stack<PsudoMethod>();
        public PsudoMethod CurrentMethod
        {
            get
            {
                if (this.callStack.Count > 0)
                    return this.callStack.Peek();
                return null;
            }
        }

        public PsudoProgram()
        {
            this.Classes = new Dictionary<string, PsudoClass>();
            this.GlobalMethods = new Dictionary<string, PsudoMethod>();
            this.GlobalVariables = new Dictionary<string, object>();
            this.MainMethod = null;
        }

        public void Abort()
        {
            this.abortRun = true;
        }

        public void RunMain()
        {
            if (this.MainMethod == null)
            {
                this.OnError("No Main Method Defined");
                return;
            }

            Thread runThread = new Thread(new ThreadStart(this.RunMainThreadStart));
            runThread.Start();
        }

        public string[] ReadLine()
        {
            return this.DoReadLine();
        }

        public bool EOF
        {
            get
            {
                return this.DoEOF();
            }
        }

        public void RunMainThreadStart()
        {
            foreach (PsudoMethod meth in this.GlobalMethods.Values)
                meth.Program = this;

            this.callStack.Push(this.MainMethod);
            try
            {
                this.MainMethod.Run(this);
            }
            catch (Exception e)
            {
                this.OnError(e.Message);
            }

            this.OnProgramComplete();
        }

        protected internal void OnDebug(string message)
        {
            if (this.DebugOutput != null)
                this.DebugOutput(this, new Output_EventArgs(message));
        }

        protected internal void OnStandard(string message)
        {
            if (this.StandardOutput != null)
                this.StandardOutput(this, new Output_EventArgs(message));
        }

        protected internal void OnError(string message)
        {
            if (this.ErrorOutput != null)
                this.ErrorOutput(this, new Output_EventArgs(message));
        }

        protected internal void OnProgramComplete()
        {
            if (this.ProgramComplete != null)
                this.ProgramComplete(this, new EventArgs());
        }

        #region ICustomTypeDescriptor Members

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }

        public PropertyDescriptorCollection GetProperties()
        {
            List<PropertyDescriptor> propList = new List<PropertyDescriptor>();
            // Get Global Variables
            propList.AddRange(GetGlobalVarProps());
            propList.AddRange(GetLocalMethodProps());

            PropertyDescriptorCollection props =
                new PropertyDescriptorCollection(propList.ToArray());

            return props;
        }

        protected List<PropertyDescriptor> GetGlobalVarProps()
        {
            List<PropertyDescriptor> propList = new List<PropertyDescriptor>();

            foreach (var variable in this.GlobalVariables)
            {
                PropertyDescriptor desc = TypeDescriptor.CreateProperty(
                    this.GetType(), variable.Key, variable.Value.GetType());
                propList.Add(new ProgramPropertyDescriptor(desc, "Global Variables", variable.Value));

            }
            return propList;
        }

        protected List<PropertyDescriptor> GetLocalMethodProps()
        {
            List<PropertyDescriptor> propList = new List<PropertyDescriptor>();

            if (this.CurrentMethod == null)
                return propList;

            if (this.CurrentMethod.ParentClass != null)
            {
                foreach (var variable in this.CurrentMethod.ParentClass.ClassVariables)
                {
                    PropertyDescriptor desc = TypeDescriptor.CreateProperty(
                        this.GetType(), variable.Key, variable.Value.GetType());
                    propList.Add(new ProgramPropertyDescriptor(desc, "Class Variables", variable.Value));
                }
            }

            //foreach (var variable in this.CurrentMethod.LocalVariables)
            //{
            //    PropertyDescriptor desc = TypeDescriptor.CreateProperty(
            //        this.GetType(), variable.Key, variable.Value==null?typeof(string):variable.Value.GetType());
            //    propList.Add(new ProgramPropertyDescriptor(desc, "Local Variables", variable.Value));

            //}

            return propList;
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        #endregion

        internal void OnAfterInstruction(PsudoInstruction instruction)
        {
            if (this.abortRun)
                throw new Exception("Run Aborted. Exiting.");

            if (instruction == null)
                return;

            if (this.AfterInstruction != null)
                this.AfterInstruction(this, new Instruction_EventArgs(instruction.Line));
        }

        internal void OnBeforeInstruction(PsudoInstruction instruction)
        {
            if (this.abortRun)
                throw new Exception("Run Aborted. Exiting.");

            if (instruction == null)
                return;

            if (this.BeforeInstruction != null)
                this.BeforeInstruction(this, new Instruction_EventArgs(instruction.Line));
        }

        internal string GetColumnName(int i)
        {
            return DoGetColumn(i);
        }
    }

    public class ProgramPropertyDescriptor : PropertyDescriptor
    {
        PropertyDescriptor sourceProperty;
        string category;
        object dataValue;

        public ProgramPropertyDescriptor(PropertyDescriptor sourceProperty, string category, object dataValue)
            : base(sourceProperty)
        {
            this.sourceProperty = sourceProperty;
            this.category = category;
            this.dataValue = dataValue;
        }

        public override bool CanResetValue(object component)
        {
            return sourceProperty.CanResetValue(component);
        }

        public override Type ComponentType
        {
            get { return sourceProperty.ComponentType; }
        }

        public override object GetValue(object component)
        {
            return this.dataValue;
        }

        public override bool IsReadOnly
        {
            get { return true; }
        }

        public override Type PropertyType
        {
            get { return this.dataValue==null?typeof(string):this.dataValue.GetType(); }
        }

        public override void ResetValue(object component)
        {
        }

        public override void SetValue(object component, object value)
        {
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }

        public override string Category
        {
            get
            {
                return this.category;
            }
        }
    }
}
