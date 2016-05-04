using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoCodeCompiler.Runtime.Instructions
{
    class BlockInstruction : PsudoInstruction
    {
        public List<PsudoInstruction> Instructions
        {
            get
            {
                if (this.CodeBlock == null)
                    this.CodeBlock = new PsudoCodeBlock();
                return this.CodeBlock.Instructions;
            }
            set
            {
                if (this.CodeBlock == null)
                    this.CodeBlock = new PsudoCodeBlock();
                this.CodeBlock.Instructions = value;
            }
        }
        public PsudoCodeBlock CodeBlock { get; set; }

        public BlockInstruction(int lineNumber, PsudoMethod method)
            : base(lineNumber, method)
        {
        }

        public override PsudoInstruction Run()
        {
            this.CodeBlock.Run(this.Method.Program);

            return null;
        }

        public override PsudoInstruction CopyTo(PsudoMethod newMethod)
        {
            BlockInstruction clone = new BlockInstruction(this.Line, newMethod);
            clone.Instructions = new List<PsudoInstruction>();
            foreach (PsudoInstruction ins in this.Instructions)
                clone.Instructions.Add(ins.CopyTo(newMethod));
            return clone;
        }
    }
}
