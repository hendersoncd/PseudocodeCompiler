using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoCodeCompiler.ExpressionTree
{
    public delegate object GetVariableDelegate(string variable);

    public class ExpressionTree
    {
        ExpressionNode rootNode = null;

        public ExpressionTree()
        {
        }

        public ExpressionTree(string expression)
        {
            this.ParseExpression(expression);
        }

        public bool ParseExpression(string expression)
        {
            ExpressionParser parser = new ExpressionParser();
            this.rootNode = parser.ParseExpression(expression);
            return this.rootNode != null;
        }

        public object Evaluate(Dictionary<string, object> variables)
        {
            if (variables == null)
                variables = new Dictionary<string, object>();
            return rootNode.Evaluate(variables);
        }

        public object Evaluate(GetVariableDelegate varDelegate)
        {
            if (varDelegate == null)
                throw new ArgumentNullException("varDelegate");

            return rootNode.Evaluate(varDelegate);
        }
    }
}
