using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using PseudoCodeCompiler.ExpressionTree;

namespace PseudoCodeCompiler.Runtime.Instructions
{
    class DecisionInstruction : PsudoInstruction
    {
        string expression;
        PseudoCodeCompiler.ExpressionTree.ExpressionTree expTree;
        public PsudoInstruction FalseInstruction { get; set; }
        public PsudoInstruction EndInstruction { get; set; }
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

        public DecisionInstruction(int lineNumber, PsudoMethod method, string exp)
            : base(lineNumber, method)
        {
            this.expression = InstructionFactory.CleanupExpression(exp);

            expTree = new PseudoCodeCompiler.ExpressionTree.ExpressionTree(this.expression);
        }

        public override PsudoInstruction Run()
        {
            bool result = false;

            if (this.expression.ToLower().Contains("eof"))
            {
                if (this.expression.ToLower().Contains("not"))
                {
                    result = !this.Method.Program.EOF;
                }
                else
                {
                    result = this.Method.Program.EOF;
                }
            }
            else
            {
                result = Convert.ToBoolean(expTree.Evaluate(this.Method.GetVariable));
            }

            // if false need to branch
            if (result == false)
            {
                return this.FalseInstruction;
            }

            this.CodeBlock.Run(this.Method.Program);

            return this.EndInstruction;
        }

        public override PsudoInstruction CopyTo(PsudoMethod newMethod)
        {
            DecisionInstruction clone = new DecisionInstruction(this.Line, newMethod, this.expression);
            clone.FalseInstruction = this.FalseInstruction.CopyTo(newMethod);
            clone.EndInstruction = this.EndInstruction.CopyTo(newMethod);
            clone.Instructions = new List<PsudoInstruction>();
            foreach (PsudoInstruction ins in this.Instructions)
                clone.Instructions.Add(ins.CopyTo(newMethod));
            return clone;
        }

        public override string ToString()
        {
            return "IF " + this.expression.Replace('#', '(').Replace('@', ')');
        }
    }
}
