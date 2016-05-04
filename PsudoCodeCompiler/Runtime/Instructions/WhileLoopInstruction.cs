using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using PseudoCodeCompiler.ExpressionTree;

namespace PseudoCodeCompiler.Runtime.Instructions
{
    public class WhileLoopInstruction : PsudoInstruction
    {
        protected string expression;
        protected PseudoCodeCompiler.ExpressionTree.ExpressionTree expTree;
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

        public WhileLoopInstruction(int lineNumber, PsudoMethod method, string exp)
            : base(lineNumber, method)
        {
            this.expression = InstructionFactory.CleanupExpression(exp);
            expTree = new PseudoCodeCompiler.ExpressionTree.ExpressionTree(this.expression);
        }

        public override PsudoInstruction Run()
        {

            if (this.expression.ToLower().Contains("eof"))
            {
                if (this.expression.ToLower().Contains("!"))
                {
                    while (!this.Method.Program.EOF)
                    {
                        this.CodeBlock.Run(this.Method.Program);
                    }
                }
                else
                {
                    while (this.Method.Program.EOF)
                    {
                        this.CodeBlock.Run(this.Method.Program);
                    }
                }
            }
            else
            {
                while (Convert.ToBoolean(expTree.Evaluate(this.Method.GetVariable)))
                {
                    this.Method.Program.OnAfterInstruction(this);
                    this.CodeBlock.Run(this.Method.Program);
                }
            }

            return null;
        }

        public override PsudoInstruction CopyTo(PsudoMethod newMethod)
        {
            WhileLoopInstruction clone = new WhileLoopInstruction(this.Line, newMethod, this.expression);
            clone.Instructions = new List<PsudoInstruction>();
            foreach (PsudoInstruction ins in this.Instructions)
                clone.Instructions.Add(ins.CopyTo(newMethod));
            return clone;
        }

        public override string ToString()
        {
            return "WHILE " + this.expression.Replace('#', '(').Replace('@', ')');
        }
    }
}
