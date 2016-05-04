using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using ExpEvalTS2;
using System.Collections;
using System.Text.RegularExpressions;
using PseudoCodeCompiler.ExpressionTree;

namespace PseudoCodeCompiler.Runtime.Instructions
{
    class MathmaticAssignmentInstruction : AbstractAssignmentInstruction
    {
        string expression;
        string variable;
        PseudoCodeCompiler.ExpressionTree.ExpressionTree expTree;

        public MathmaticAssignmentInstruction(int lineNumber, PsudoMethod method, string variable, string exp)
            : base(lineNumber, method)
        {
            this.variable = variable;

            this.expression = InstructionFactory.CleanupExpression(exp);

            expTree = new PseudoCodeCompiler.ExpressionTree.ExpressionTree(this.expression);
        }

        public override PsudoInstruction Run()
        {
            try
            {
                object value = expTree.Evaluate(this.Method.GetVariable);
                CreateOrSetVariable(variable, value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

        public override string ToString()
        {
            return this.variable + "=" + this.expression.Replace('#', '(').Replace('@',')');
        }
    }
}
