using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoCodeCompiler.Runtime.Instructions
{
    public class DoLoopInstruction : WhileLoopInstruction
    {
        public DoLoopInstruction(int lineNumber, PsudoMethod method, string exp)
            : base(lineNumber, method, exp)
        {
        }

        public override PsudoInstruction Run()
        {
            this.CodeBlock.Run(this.Method.Program);
            return base.Run();
        }

        public override PsudoInstruction CopyTo(PsudoMethod newMethod)
        {
            DoLoopInstruction clone = new DoLoopInstruction(this.Line, newMethod, this.expression);
            clone.Instructions = new List<PsudoInstruction>();
            foreach (PsudoInstruction ins in this.Instructions)
                clone.Instructions.Add(ins.CopyTo(newMethod));
            return clone;
        }
    }
}
