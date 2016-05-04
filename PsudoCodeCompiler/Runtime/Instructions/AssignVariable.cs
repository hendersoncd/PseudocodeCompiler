using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoCodeCompiler.Runtime.Instructions
{
    public class AssignVariable : AbstractAssignmentInstruction
    {
        string value;
        string variable;


        public AssignVariable(int lineNumber, PsudoMethod method, string variable, string value)
            : base(lineNumber, method)
        {
            this.variable = variable;
            this.value = value;
        }

        public override PsudoInstruction Run()
        {
            ParseVariable(this.variable, this.value);

            return null;
        }

        public override string ToString()
        {
            return variable + "=" + value;
        }
    }
}
