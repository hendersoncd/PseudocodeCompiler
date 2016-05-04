using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PseudoCodeCompiler.Runtime.Instructions;

namespace PseudoCodeCompiler.Runtime
{
    public class PsudoCodeBlock
    {
        public List<PsudoInstruction> Instructions { get; set; }

        public PsudoCodeBlock()
        {
        }

        internal void Run(PsudoProgram psudoProgram)
        {
            // Handle empty blocks of code cleanly
            if (this.Instructions.Count == 0)
                return;

            PsudoInstruction curIns = this.Instructions[0];

            while(curIns != null)
            {
                psudoProgram.OnBeforeInstruction(curIns);

                PsudoInstruction jump = curIns.Run();

                psudoProgram.OnAfterInstruction(curIns);

                // if returning break out of the loop
                if (curIns is ReturnInstruction)
                {
                    curIns = null;
                }
                else if (jump == null)
                {
                    curIns = this.Instructions.Find(item => item != null && item.Line > curIns.Line);
                }
                else
                {
                    curIns = jump;
                }
            }
        }

        internal void OldRun(PsudoProgram psudoProgram)
        {
            for (int i = 0; i < this.Instructions.Count; i++)
            {
                psudoProgram.OnBeforeInstruction(this.Instructions[i]);

                if (this.Instructions[i] == null)
                {
                    psudoProgram.OnError("Null Instruction on Line " + i + "\n");
                    continue;
                }
                PsudoInstruction jump = this.Instructions[i].Run();

                psudoProgram.OnAfterInstruction(this.Instructions[i]);

                if (jump != null)
                {
                    int jumpLine = this.Instructions.FindIndex(item => item != null && item.Line >= jump.Line) - 1;
                   // if (jumpLine >= 0)
                        i = jumpLine;
                }

                // if returning break out of the loop
                if (this.Instructions[i] is ReturnInstruction)
                {
                    break;
                }
            }
        }
    }
}
