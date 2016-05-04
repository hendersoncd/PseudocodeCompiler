using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoCodeCompiler.Runtime.Instructions
{
    public class ReturnInstruction : PsudoInstruction
    {
        public ReturnInstruction(int lineNumber, PsudoMethod method)
            : base(lineNumber, method)
        {
        }

        public override PsudoInstruction Run()
        {
            return null;
        }
    }
}
