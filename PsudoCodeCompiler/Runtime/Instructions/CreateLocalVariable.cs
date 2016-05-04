using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoCodeCompiler.Runtime.Instructions
{
    public class CreateLocalVariable : PsudoInstruction
    {
        string type;
        string name;
        string defaultValue;

        public CreateLocalVariable(int lineNumber, PsudoMethod method, string type, string name, string defaultValue)
            : base(lineNumber, method)
        {
            this.type = type;
            this.name = name;
            this.defaultValue = defaultValue;
        }

        public override PsudoInstruction Run()
        {
            this.Method.CreateVariable(this.name.ToLower(), this.CreateVariable());
            return null;
        }

        protected object CreateVariable()
        {
            switch (this.type)
            {
                case "string":
                    if (this.defaultValue != null)
                        return this.defaultValue;
                    return "";
                case "num":
                    if (this.defaultValue != null)
                        return Convert.ToDouble(this.defaultValue);
                    return 0.0;
            }

            return null;
        }
    }
}
