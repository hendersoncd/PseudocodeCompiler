using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoCodeCompiler.ExpressionTree
{
    public abstract class ExpressionOperatorBase
    {
        public abstract string[] Symbols { get; }
        public abstract int Priority { get; }
        public abstract object ApplyOperator(object left, object right);

        public virtual bool ContainsSymbol(string symbol)
        {
            return this.Symbols.Contains(symbol);
        }
    }

    #region Math Operators

    public class AddOperator : ExpressionOperatorBase
    {
        #region IExpressionOperator Members

        public override string[] Symbols
        {
            get { return new string[] { "+" }; }
        }

        public override int Priority
        {
            get { return 10; }
        }

        public override object ApplyOperator(object left, object right)
        {
            if (left is string || right is string)
            {
                return left.ToString() + right.ToString();
            }

            //If not strings treat them as numbers
            return Convert.ToDouble(left) + Convert.ToDouble(right);
        }

        #endregion
    }

    public class SubtractOperator : ExpressionOperatorBase
    {
        #region IExpressionOperator Members

        public override string[] Symbols
        {
            get { return new string[] { "-" }; }
        }

        public override int Priority
        {
            get { return 10; }
        }

        public override object ApplyOperator(object left, object right)
        {
            if (left is string || right is string)
            {
                throw new NotSupportedException("Subtracting strings not supported");
            }

            //If not strings treat them as numbers
            return Convert.ToDouble(left) - Convert.ToDouble(right);
        }

        #endregion
    }
    public class DivideOperator : ExpressionOperatorBase
    {
        #region IExpressionOperator Members

        public override string[] Symbols
        {
            get { return new string[] { "/" }; }
        }

        public override int Priority
        {
            get { return 20; }
        }

        public override object ApplyOperator(object left, object right)
        {
            if (left is string || right is string)
            {
                throw new NotSupportedException("Dividing strings not supported");
            }

            //If not strings treat them as numbers
            return Convert.ToDouble(left) / Convert.ToDouble(right);
        }

        #endregion
    }
    public class MultiplyOperator : ExpressionOperatorBase
    {
        #region IExpressionOperator Members

        public override string[] Symbols
        {
            get { return new string[] { "*" }; }
        }

        public override int Priority
        {
            get { return 20; }
        }

        public override object ApplyOperator(object left, object right)
        {
            if (left is string || right is string)
            {
                throw new NotSupportedException("Multiplying strings not supported");
            }

            //If not strings treat them as numbers
            return Convert.ToDouble(left) * Convert.ToDouble(right);
        }

        #endregion
    }
    public class PowOperator : ExpressionOperatorBase
    {
        #region IExpressionOperator Members

        public override string[] Symbols
        {
            get { return new string[] { "^" }; }
        }

        public override int Priority
        {
            get { return 30; }
        }

        public override object ApplyOperator(object left, object right)
        {
            if (left is string || right is string)
            {
                throw new NotSupportedException("Exponents with strings not supported");
            }

            //If not strings treat them as numbers
            return Math.Pow(Convert.ToDouble(left), Convert.ToDouble(right));
        }

        #endregion
    }

    #endregion Math Operators

    #region Comparison Operators

    public class LessOperator : ExpressionOperatorBase
    {
        #region IExpressionOperator Members

        public override string[] Symbols
        {
            get { return new string[] { "<" }; }
        }

        public override int Priority
        {
            get { return 5; }
        }

        public override object ApplyOperator(object left, object right)
        {
            if (left is string || right is string)
            {
                return left.ToString().ToLower().CompareTo(right.ToString().ToLower()) < 0;
            }

            //If not strings treat them as numbers
            return Convert.ToDouble(left) < Convert.ToDouble(right);
        }

        #endregion
    }
    public class GreaterOperator : ExpressionOperatorBase
    {
        #region IExpressionOperator Members

        public override string[] Symbols
        {
            get { return new string[] { ">" }; }
        }

        public override int Priority
        {
            get { return 5; }
        }

        public override object ApplyOperator(object left, object right)
        {
            if (left is string || right is string)
            {
                return left.ToString().ToLower().CompareTo(right.ToString().ToLower()) > 0;
            }

            //If not strings treat them as numbers
            return Convert.ToDouble(left) > Convert.ToDouble(right);
        }

        #endregion
    }
    public class LessEqualOperator : ExpressionOperatorBase
    {
        #region IExpressionOperator Members

        public override string[] Symbols
        {
            get { return new string[] { "<=" }; }
        }

        public override int Priority
        {
            get { return 5; }
        }

        public override object ApplyOperator(object left, object right)
        {
            if (left is string || right is string)
            {
                return left.ToString().ToLower().CompareTo(right.ToString().ToLower()) <= 0;
            }

            //If not strings treat them as numbers
            return Convert.ToDouble(left) <= Convert.ToDouble(right);
        }

        #endregion
    }
    public class GreaterEqualOperator : ExpressionOperatorBase
    {
        #region IExpressionOperator Members

        public override string[] Symbols
        {
            get { return new string[] { ">=" }; }
        }

        public override int Priority
        {
            get { return 5; }
        }

        public override object ApplyOperator(object left, object right)
        {
            if (left is string || right is string)
            {
                return left.ToString().ToLower().CompareTo(right.ToString().ToLower()) >= 0;
            }

            //If not strings treat them as numbers
            return Convert.ToDouble(left) >= Convert.ToDouble(right);
        }

        #endregion
    }
    public class EqualOperator : ExpressionOperatorBase
    {
        #region IExpressionOperator Members

        public override string[] Symbols
        {
            get { return new string[] { "==", "=" }; }
        }

        public override int Priority
        {
            get { return 5; }
        }

        public override object ApplyOperator(object left, object right)
        {
            if (left is string || right is string)
            {
                return left.ToString().ToLower().CompareTo(right.ToString().ToLower()) == 0;
            }
            if (left is bool && right is bool)
            {
                return left == right;
            }

            //If not strings treat them as numbers
            return Convert.ToDouble(left) == Convert.ToDouble(right);
        }

        #endregion
    }
    public class NotEqualOperator : ExpressionOperatorBase
    {
        #region IExpressionOperator Members

        public override string[] Symbols
        {
            get { return new string[] { "<>", "!=" }; }
        }

        public override int Priority
        {
            get { return 5; }
        }

        public override object ApplyOperator(object left, object right)
        {
            if (left is string || right is string)
            {
                return left.ToString().ToLower().CompareTo(right.ToString().ToLower()) != 0;
            }
            if (left is bool && right is bool)
            {
                return left != right;
            }

            //If not strings treat them as numbers
            return Convert.ToDouble(left) != Convert.ToDouble(right);
        }

        #endregion
    }

    #endregion Comparison Operators

    #region Boolean Operators

    public class AndOperator : ExpressionOperatorBase
    {
        #region IExpressionOperator Members

        public override string[] Symbols
        {
            get { return new string[] { "&&", @"\sAND\s" }; }
        }

        public override int Priority
        {
            get { return 2; }
        }

        public override object ApplyOperator(object left, object right)
        {
            return Convert.ToBoolean(left) && Convert.ToBoolean(right);
        }

        public override bool ContainsSymbol(string symbol)
        {
            if (symbol.Trim().ToLower() == "and")
                return true;

            return base.ContainsSymbol(symbol);
        }

        #endregion
    }

    public class OrOperator : ExpressionOperatorBase
    {
        #region IExpressionOperator Members

        public override string[] Symbols
        {
            get { return new string[] { "||", @"\sOR\s" }; }
        }

        public override int Priority
        {
            get { return 2; }
        }

        public override bool ContainsSymbol(string symbol)
        {
            if (symbol.Trim().ToLower() == "or")
                return true;

            return base.ContainsSymbol(symbol);
        }

        public override object ApplyOperator(object left, object right)
        {
            return Convert.ToBoolean(left) || Convert.ToBoolean(right);
        }

        #endregion
    }

    public class NotOperator : ExpressionOperatorBase
    {
        #region IExpressionOperator Members

        public override string[] Symbols
        {
            get { return new string[] { "!", @"NOT\s" }; }
        }

        public override bool ContainsSymbol(string symbol)
        {
            if (symbol.Trim().ToLower() == "not")
                return true;

            return base.ContainsSymbol(symbol);
        }

        public override int Priority
        {
            get { return 1; }
        }

        public override object ApplyOperator(object left, object right)
        {
            //If not strings treat them as numbers
            return !Convert.ToBoolean(right);
        }

        #endregion
    }

    #endregion Boolean Operators
}
