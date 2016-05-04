using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PseudoCodeCompiler.ExpressionTree
{
    public class ExpressionParser
    {
        public enum Token { Literal, Symbol, Operator, InValid, Eof };

        public List<ExpressionOperatorBase> Operators { get; set; }
        Regex reOps;		// Operators regular expression 
        Regex reLit;		// Litrels regular expression 
        Regex reSym;		// Symbols regular expression  

        Stack<ExpressionNode> OpStack;
        Stack<ExpressionNode> NodeStack;
        string _exp;


        //Type of the Current Token
        Token current;
        //Current Sysmboool
        string symbol;
        string Curr_op;		// current operator
        object literal;		// current literal

        int Curr_idx;			// subExpression Start position

        public ExpressionParser()
        {
            this.Operators = new List<ExpressionOperatorBase>();

            this.Operators.Add(new AddOperator());
            this.Operators.Add(new SubtractOperator());
            this.Operators.Add(new MultiplyOperator());
            this.Operators.Add(new DivideOperator());
            this.Operators.Add(new PowOperator());

            this.Operators.Add(new AndOperator());
            this.Operators.Add(new OrOperator());
            this.Operators.Add(new NotOperator());

            this.Operators.Add(new LessOperator());
            this.Operators.Add(new GreaterOperator());
            this.Operators.Add(new LessEqualOperator());
            this.Operators.Add(new GreaterEqualOperator());
            this.Operators.Add(new NotEqualOperator());
            this.Operators.Add(new EqualOperator());

            string opString = @"^\s*(";
            string singleChars = "";

            foreach (ExpressionOperatorBase op in this.Operators)
            {
                foreach(string sym in op.Symbols)
                {
                    string cleanSym = sym.Replace("|",@"\|").Replace("-",@"\-");
                    if (sym.Length == 1)
                        singleChars += cleanSym;
                    else
                        opString += cleanSym + "|";
                }
            }

            opString = opString + "[" + singleChars + "()])";

            reOps = new Regex(opString, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            reSym = new Regex(@"^\s*(\-?\b*[_a-zA-Z]+[_a-zA-Z0-9,#@]*)", RegexOptions.Compiled);
            reLit = new Regex(@"^\s*(([$]*[0-9,]+(\.[0-9]+)?)|(" + "[\"'][_a-zA-Z0-9-\\s]+[\"']))", RegexOptions.Compiled);
            //reLit = new Regex(@"^\s*(([0-9]+(\.[0-9]+)?)|(" + "[\"'][_a-zA-Z0-9-]+[\"']))", RegexOptions.Compiled);

            OpStack = new Stack<ExpressionNode>();
            NodeStack = new Stack<ExpressionNode>();
        }


        public ExpressionNode ParseExpression(string exp)
        {
            if (!CheckValidity(exp))
            {
                throw new FormatException("Parenthesis Error!");
            }

            this._exp = exp;

            while (NextToken() != Token.Eof)
            {
                //The errors that might come would be from stack 
                //If that went wrong -- gone case -- What use rwould do
                //No USer specific Errors from this funtion
                switch (current)
                {
                    case Token.Literal:
                        //CreateLeafNode and push it on the stack
                        ExpressionNode LNode = new ExpressionNode(literal, NodeType.Literal);
                        NodeStack.Push(LNode);
                        break;
                    case Token.Operator:
                        object op = this.Operators.Find(item => item.ContainsSymbol(Curr_op));
                        if (op == null)
                            op = Curr_op;
                        ExpressionNode ONode = new ExpressionNode(op, NodeType.Operator);

                        if (Curr_op == "(")
                        {
                            OpStack.Push(ONode);
                            break;
                        }
                        if (Curr_op == ")")
                        {
                            while ("(" != (string)OpStack.Peek().Value.ToString())
                            {
                                PopConnectPush();
                            }
                            OpStack.Pop(); //Throw away {{
                            break;
                        }

                        if (OpStack.Count == 0)
                        {
                            OpStack.Push(ONode);
                        }
                        else if ((string)OpStack.Peek().Value.ToString() == "(")
                        {
                            OpStack.Push(ONode);
                        }
                        else if (OpStack.Peek().Operator.Priority < ONode.Operator.Priority)
                        {
                            OpStack.Push(ONode);
                        }
                        else
                        {
                            while (true)
                            {
                                PopConnectPush();
                                if (OpStack.Count == 0 || (string)OpStack.Peek().Value.ToString() == "(" 
                                    || OpStack.Peek().Operator.Priority < ONode.Operator.Priority)
                                    break;
                            }
                            OpStack.Push(ONode);
                        }
                        break;
                    case Token.Symbol:
                        {
                            ExpressionNode SNode = new ExpressionNode(symbol, NodeType.Symbol);
                            NodeStack.Push(SNode);
                        }
                        break;
                    default:
                        break;
                }

            }

            while (OpStack.Count > 0)
            {
                PopConnectPush();
            }

            if (NodeStack.Count != 1)
                throw new Exception("Expression Syntax Error");

            return NodeStack.Pop();
        }

        private void PopConnectPush()
        {
            ExpressionNode temp = OpStack.Pop(); 
            temp.Right = NodeStack.Pop(); 
            if (!temp.Operator.Symbols.Contains("!")) // if its the Not Operator we dont need left
                temp.Left = NodeStack.Pop();
            NodeStack.Push(temp); 
        }

        //Gets the Next Token
        protected Token NextToken()
        {
            Match m;

            m = reOps.Match(_exp);
            if (m.Length != 0)
            {
                Curr_op = m.Groups[1].Value;
                Curr_idx += m.Length;
                _exp = _exp.Substring(m.Length);
                current = Token.Operator;
            }
            else
            {           
                m = reSym.Match(_exp);
                if (m.Length != 0)
                {
                    symbol = m.Groups[1].Value;
                    Curr_idx += m.Length;
                    _exp = _exp.Substring(m.Length);
                    current = Token.Symbol;
                }
                else
                {
                    m = reLit.Match(_exp);
                    if (m.Length != 0)
                    {
                        string val = m.Groups[1].Value.Trim();
                        if (val[0] == '"' || val[0] == '\'')
                            literal = val.Trim('"', '\'');
                        else
                        {
                            literal = Double.Parse(m.Groups[1].Value, System.Globalization.NumberStyles.Any);
                        }

                        Curr_idx += m.Length;
                        _exp = _exp.Substring(m.Length);
                        current = Token.Literal;
                    }
                    else
                        current = Token.Eof;
                }
            }
            return current;
        }

        private bool CheckValidity(string s)
        {
            int opens = 0;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                    opens++;
                if (s[i] == ')')
                    opens--;
                if (opens < 0)
                    return false;
            }

            return opens == 0;
        }
    }
}
