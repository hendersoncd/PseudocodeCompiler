using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Text.RegularExpressions;

namespace PseudoCodeCompiler.Runtime.Instructions
{
    class ReadInstruction : AbstractAssignmentInstruction
    {
        string[] variables = null;

        public ReadInstruction(int lineNumber, PsudoMethod method, params string[] variables)
            : base(lineNumber, method)
        {
            this.variables = variables;
        }

        public override PsudoInstruction Run()
        {
            string[] readLine = this.Method.Program.ReadLine();

            if (readLine == null)
            {
                this.Method.Program.OnError("Error Reading: No records were returned\n");
                return null;
            }
                        
            if (readLine.Length != variables.Length)
            {
                if (variables.Length == 1 && System.Text.RegularExpressions.Regex.IsMatch(variables[0], @"[*][\s]*[)]"))
                {
                    for (int i = 1; i <= readLine.Length; i++)
                    {
                        string variable = variables[0].Replace("*", i.ToString());

                        string value = readLine[i - 1];
                        ParseVariable(variable, value);
                    }
                }
                else if (variables.Length == 1)
                {
                    for (int i = 0; i < readLine.Length; i++)
                    {
                        string variable = this.Method.Program.GetColumnName(i);
                        variable = variable.Replace(' ', '_');

                        string value = readLine[i];
                        ParseVariable(variable, value);
                    }
                }
                else
                {
                    this.Method.Program.OnError("Error Reading: Incorrect Number of Variables\n");
                }
                return null;
            }

            for (int i = 0; i < variables.Length; i++)
            {
                string variable = variables[i];
                string value = readLine[i];
                ParseVariable(variable, value);
            }

            return null;
        }
    }
}
