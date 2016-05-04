using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoCodeCompiler.ExpressionTree
{
    public enum NodeType { Literal, Symbol, Operator };

    public class ExpressionNode
    {
        public object Value { get; set; }
        public NodeType Type { get; set; }
        public ExpressionOperatorBase Operator
        {
            get
            {
                return this.Value as ExpressionOperatorBase;
            }
        }

        public ExpressionNode Left { get; set; }
        public ExpressionNode Right { get; set; }

        public ExpressionNode(object value, NodeType type)
        {
            this.Value = value;
            this.Type = type;
        }

        internal object Evaluate(Dictionary<string, object> variables)
        {
            switch (this.Type)
            {
                case NodeType.Literal:
                    return this.Value;
                case NodeType.Operator:
                    object left = null;
                    if (this.Left != null)
                        left = this.Left.Evaluate(variables);

                    object right = this.Right.Evaluate(variables);
                    return this.Operator.ApplyOperator(left, right);
                case NodeType.Symbol:
                    string key = this.Value.ToString().ToLower();
                    if (variables.ContainsKey(key))
                        return variables[key];
                    throw new Exception("Symbol Undefined: \"" + this.Value + "\"");
            }
            throw new Exception("Invalid Node Type");
        }

        internal object Evaluate(GetVariableDelegate varDelegate)
        {
            switch (this.Type)
            {
                case NodeType.Literal:
                    return this.Value;
                case NodeType.Operator:
                    object left = null;
                    if (this.Left != null)
                        left = this.Left.Evaluate(varDelegate);

                    object right = this.Right.Evaluate(varDelegate);
                    return this.Operator.ApplyOperator(left, right);
                case NodeType.Symbol:
                    string key = this.Value.ToString().ToLower();
                    object value = varDelegate(key);
                    if (value != (object)key)
                        return value;
                    throw new Exception("Symbol Undefined: \"" + this.Value + "\"");
            }
            throw new Exception("Invalid Node Type");
        }
    }
}
