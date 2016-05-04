using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PseudoCodeCompiler.Runtime.Instructions
{
    class PrintInstruction : PsudoInstruction
    {
        string[] variables = null;

        public PrintInstruction(int lineNumber, PsudoMethod method, params string[] variables)
            : base(lineNumber, method)
        {
            this.variables = variables;
        }

        private string GetString(string variable)
        {
            if (variable.Contains('"') || variable.Contains('\''))
            {
                return variable.Trim('"', '\'');
            }
            else if (Regex.IsMatch(variable, @"[*][\s]*[)]"))
            {
                // X Dimensional Array
                int index = 0;

                string list = "";
                string curVarName = variable.Replace("*", index.ToString());
                if (this.Method.GetVariable(curVarName) == (object)curVarName)
                    index++;

                curVarName = variable.Replace("*", index.ToString());
                while (this.Method.GetVariable(curVarName) != (object)curVarName)
                {
                    list += this.Method.GetVariable(curVarName) + " ";
                    index++;
                    curVarName = variable.Replace("*", index.ToString());
                }
                return list;
            }
            else
            {
                return this.Method.GetVariable(variable).ToString() + " ";
            }
        }

        public override PsudoInstruction Run()
        {
            string mess = "";
            for (int i = 0; i < variables.Length - 1; i++)
            {
                mess += GetString(variables[i]) + " ";
            }
            mess += GetString(variables[variables.Length - 1]);

            this.Method.Program.OnStandard(mess + "\n");

            return null;
        }

        public override string ToString()
        {
            string text = "PRINT ";
            foreach (string var in this.variables)
                text += var + ", ";
            text = text.Trim(' ', ',');
            return text;
        }
    }
}
