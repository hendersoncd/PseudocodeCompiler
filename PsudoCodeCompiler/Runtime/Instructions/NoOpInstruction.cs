using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoCodeCompiler.Runtime.Instructions
{
    class NoOpInstruction : PsudoInstruction
    {
        public NoOpInstruction(int lineNumber, PsudoMethod method)
            : base(lineNumber, method)
        {
        }

        public override PsudoInstruction Run()
        {
            return null;
        }

        public override string ToString()
        {
            return "No Op";
        }
    }
}
