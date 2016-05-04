using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PseudoCodeCompiler.Runtime.Instructions
{
    public class PromptInstruction : AbstractAssignmentInstruction
    {
        public const string VariableRegexString = @"^([a-zA-Z0-9_]+|[a-zA-Z0-9_]+[(][a-zA-Z0-9_ ,]+[)])$";
        public static Regex VariableRegex = new Regex(VariableRegexString, RegexOptions.Compiled);

        public string[] variables = null;

        public PromptInstruction(int lineNumber, PsudoMethod method, params string[] variables)
            : base(lineNumber, method)
        {
            this.variables = variables;
            foreach (string var in variables)
            {
                if (!VariableRegex.IsMatch(var))
                    throw new Exception("Illegal Variable Name: \""+var+"\"");
            }
        }

        public override PsudoInstruction Run()
        {
            foreach (string variable in this.variables)
            {
                string varName = this.Method.ResolveName(variable);
                this.Method.Program.OnStandard("Enter a Value for " + varName + ": ");
                string value = Program.MainForm.PromptForUserInput("Enter a Value for " + varName);
                ParseVariable(variable, value);
                this.Method.Program.OnStandard(value + "\n");
            }
            return null;
        }

        public override string ToString()
        {
            string text = "PROMPT FOR ";

            foreach (string variable in this.variables)
            {
                text += variable + ", ";
            }
            return text.Trim(' ', ',');
        }
    }
}
