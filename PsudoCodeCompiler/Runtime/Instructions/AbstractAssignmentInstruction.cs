using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoCodeCompiler.Runtime.Instructions
{
    public abstract class AbstractAssignmentInstruction : PsudoInstruction
    {
        public AbstractAssignmentInstruction(int lineNumber, PsudoMethod method)
            : base(lineNumber, method)
        {
        }

        protected void CreateOrSetVariable(string variable, object value)
        {
            object obj = this.Method.GetVariable(variable);

            // if variable doesn't exist yet
            if (obj == (object)variable)
            {
                this.Method.CreateVariable(variable, value);
            }
            else
            {
                this.Method.SetVariable(variable, value);
            }
        }

        protected void ParseVariable(string variable, string value)
        {
            if (value == null)
                value = "";

            object valueObj = this.Method.GetVariable(value);

            // If its a value, not a variable
            if (valueObj.Equals(value))
            {
                if (value.ToLower() == "true")
                {
                    CreateOrSetVariable(variable, true);
                }
                else if (value.ToLower() == "false")
                {
                    CreateOrSetVariable(variable, false);
                }
                else
                {
                    try
                    {
                        CreateOrSetVariable(variable, double.Parse(value, System.Globalization.NumberStyles.Any));
                    }
                    catch
                    {
                        CreateOrSetVariable(variable, value.Trim('"').Trim('\''));
                    }
                }
            }
            else
            {
                CreateOrSetVariable(variable, valueObj);
            }
        }
    }
}
