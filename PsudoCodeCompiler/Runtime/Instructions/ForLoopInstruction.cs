using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoCodeCompiler.Runtime.Instructions
{
    public class ForLoopInstruction : WhileLoopInstruction
    {
        public PsudoInstruction InitInstruction {get; set;}
        public PsudoInstruction UpdateInstruction { get; set; }

        public ForLoopInstruction(int lineNumber, PsudoMethod method, string variable, string initVal, string endVal)
            : base(lineNumber, method, variable + " <= " + endVal)
        {
            InitInstruction = new AssignVariable(lineNumber, method, variable, initVal);
            UpdateInstruction = new MathmaticAssignmentInstruction(lineNumber, method, variable, variable + " + 1");
        }

        private ForLoopInstruction(int lineNumber, PsudoMethod method, string expression, PsudoInstruction init)
            : base(lineNumber, method, expression)
        {
            InitInstruction = init.CopyTo(method);
        }

        public override PsudoInstruction Run()
        {
            this.Method.Program.OnBeforeInstruction(InitInstruction);
            InitInstruction.Run();
            this.Method.Program.OnAfterInstruction(InitInstruction);

            // Parse and write the result
            while (Convert.ToBoolean(expTree.Evaluate(this.Method.GetVariable)))
            {
                this.CodeBlock.Run(this.Method.Program);

                this.Method.Program.OnBeforeInstruction(UpdateInstruction);
                UpdateInstruction.Run();
                this.Method.Program.OnAfterInstruction(UpdateInstruction);
            }

            return base.Run();
        }

        public override PsudoInstruction CopyTo(PsudoMethod newMethod)
        {
            if (!this.Instructions.Contains(this.InitInstruction))
                this.Instructions.Add(this.InitInstruction);

            ForLoopInstruction clone = new ForLoopInstruction(this.Line, newMethod, this.expression, this.InitInstruction);
            clone.InitInstruction = this.InitInstruction.CopyTo(newMethod);
            clone.Instructions = new List<PsudoInstruction>();
            foreach (PsudoInstruction ins in this.Instructions)
                clone.Instructions.Add(ins.CopyTo(newMethod));
            return clone;
        }
    }
}
