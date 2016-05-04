using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PseudoCodeCompiler.Runtime.Instructions
{
    static class InstructionFactory
    {
        public const string VariableRegexString = @"([a-zA-Z0-9_]+|[a-zA-Z0-9_]+[(][a-zA-Z0-9_ ,]+[)])";
        public static Regex VariableRegex = new Regex(VariableRegexString, RegexOptions.Compiled);

        private static string CleanupPartialExpression(string exp)
        {
            string expression = exp.ToLower();
            expression = Regex.Replace(expression, @"\sand\s", "&&"); //expression.Replace(" and ", "&&");
            expression = Regex.Replace(expression, @"\sor\s", "||");
            expression = Regex.Replace(expression, @"(\s|^)not\s", "!");
            expression = expression.Replace(" ", "");
            char[] exprArray = expression.ToCharArray();
            MatchCollection matches = Regex.Matches(expression, @"[(][a-zA-Z0-9 ,]+[)]");
            foreach (Match match in matches)
            {
                exprArray[match.Index] = '#';
                exprArray[match.Index + match.Length - 1] = '@';
            }
            expression = new string(exprArray);

            return expression;
        }

        public static string CleanupExpression(string exp)
        {
            string expression = exp;
            MatchCollection quotes = Regex.Matches(expression, "[\"].*[\"]");

            if (quotes.Count == 0)
            {
                expression = CleanupPartialExpression(expression);
            }
            else
            {
                int i = 0;
                string tempExp = CleanupPartialExpression(expression.Substring(0, quotes[i].Index));
                for (; i < quotes.Count-1; i++)
                {
                    tempExp += quotes[i].Value;
                    int start = quotes[i].Index+quotes[i].Length;
                    int length = quotes[i+1].Index - start;
                    tempExp += CleanupPartialExpression(expression.Substring(start, length));
                }
                tempExp += quotes[i].Value;
                tempExp += CleanupPartialExpression(expression.Substring(quotes[i].Index + quotes[i].Length));

                expression = tempExp;
            }

            return expression;
        }
        public static bool CheckSimpleCommand(string line, string command, out string args)
        {
            if (Regex.IsMatch(line, @"^[\s]*\b(" + command + @")\b[\s]*", RegexOptions.IgnoreCase))
            {
                args = line.Trim().Substring(command.Length).Trim();
                return true;
            }

            args = "";
            return false;
        }

        private static PsudoInstruction lastInstruction = null;

        public static PsudoInstruction CompileInstruction(string line, int lineNum, PsudoMethod method)
        {
            lastInstruction = RealCompileInstruction(line, lineNum, method);
            return lastInstruction;
        }

        private static string[] SplitVariableLiteralList(string args)
        {
            string splitPattern = @"(?<![(][\s]*[a-zA-Z0-9_]+[\s]*)[,]";
            string[] vars = Regex.Split(args, splitPattern, RegexOptions.IgnoreCase);
            
            for (int i = 0; i < vars.Length; i++)
                vars[i] = vars[i].Trim();

            return vars;
        }

        private static PsudoInstruction RealCompileInstruction(string line, int lineNum, PsudoMethod method)
        {
            string args = "";
            string variableRegex = VariableRegexString;

            if (CheckSimpleCommand(line, "read", out args) )
            {
                string[] vars = SplitVariableLiteralList(args);

                return new ReadInstruction(lineNum, method, vars);
            }
            else if ( CheckSimpleCommand(line, "get", out args))
            {
                if (lastInstruction is PromptInstruction)
                {
                    return new NoOpInstruction(lineNum, method);
                }
                throw new Exception("Cannot use get without a prompt command");
            }
            else if (CheckSimpleCommand(line, "print", out args)
                || CheckSimpleCommand(line, "write", out args)
                || CheckSimpleCommand(line, "output", out args)
                || CheckSimpleCommand(line, "put", out args)
                || CheckSimpleCommand(line, "display", out args)
                )
            {
                string[] vars = SplitVariableLiteralList(args);
                
                return new PrintInstruction(lineNum, method, vars);
            }
            else if (CheckSimpleCommand(line, "prompt", out args)
                || CheckSimpleCommand(line, "input", out args))
            {
                string withoutFor = args.Trim();
                if (args.Trim().StartsWith("for"))
                    withoutFor = withoutFor.Substring(3).Trim();

                string[] vars = SplitVariableLiteralList(withoutFor);

                return new PromptInstruction(lineNum, method, vars);
            }
            else if (CheckSimpleCommand(line, "add", out args))
            {
                string oper = "+";
                // This should be in the format "add number to total"
                Match number = Regex.Match(args,
                                   @"^[\s]*\b" + variableRegex + @"\b[\s]+(?=([\s]*\b(to)\b[\s]*))",
                                   RegexOptions.IgnoreCase);
                Match total = Regex.Match(args,
                                   @"(?<=([\s]*\b(to)\b[\s]*))\b" + variableRegex + @"\b[\s]*",
                                   RegexOptions.IgnoreCase);

                if (!number.Success || !total.Success)
                {
                    throw new Exception("Invalid Operation Arguments on " + lineNum);
                }

                string expression = total.Value + oper + number.Value;
                return new MathmaticAssignmentInstruction(lineNum, method, total.Value.Trim(), expression);
            }
            else if (CheckSimpleCommand(line, "subtract", out args))
            {
                string oper = "-";
                // This should be in the format "add number to total"
                Match number = Regex.Match(args,
                                   @"^[\s]*\b" + variableRegex + @"\b[\s](?=([\s]*\b(from)\b[\s]*))",
                                   RegexOptions.IgnoreCase);
                Match total = Regex.Match(args,
                                   @"(?<=([\s]*\b(from)\b[\s]*))\b" + variableRegex + @"\b[\s]*",
                                   RegexOptions.IgnoreCase);

                if (!number.Success || !total.Success)
                {
                    throw new Exception("Invalid Operation Arguments on " + lineNum);
                }

                string expression = total.Value + oper + number.Value;
                return new MathmaticAssignmentInstruction(lineNum, method, total.Value.Trim(), expression);
            }
            else if (CheckSimpleCommand(line, "multiply", out args))
            {
                string oper = "*";
                // This should be in the format "add number to total"
                Match total = Regex.Match(args,
                                       @"^[\s]*\b" + variableRegex + @"\b[\s](?=([\s]*\b(by)\b[\s]*))",
                                       RegexOptions.IgnoreCase);
                Match number = Regex.Match(args,
                                       @"(?<=([\s]*\b(by)\b[\s]*))\b" + variableRegex + @"\b[\s]*",
                                       RegexOptions.IgnoreCase);

                if (!number.Success || !total.Success)
                {
                    throw new Exception("Invalid Operation Arguments on " + lineNum);
                }

                string expression = total.Value + oper + number.Value;
                return new MathmaticAssignmentInstruction(lineNum, method, total.Value.Trim(), expression);
            }
            else if (CheckSimpleCommand(line, "divide", out args))
            {
                string oper = "/";
                // This should be in the format "add number to total"
                Match total = Regex.Match(args,
                                       @"^[\s]*\b" + variableRegex + @"\b[\s](?=([\s]*\b(by)\b[\s]*))",
                                       RegexOptions.IgnoreCase);
                Match number = Regex.Match(args,
                                       @"(?<=([\s]*\b(by)\b[\s]*))\b" + variableRegex + @"\b[\s]*",
                                       RegexOptions.IgnoreCase);

                if (!number.Success || !total.Success)
                {
                    throw new Exception("Invalid Operation Arguments on " + lineNum);
                }

                string expression = total.Value + oper + number.Value;
                return new MathmaticAssignmentInstruction(lineNum, method, total.Value.Trim(), expression);
            }
            else if (CheckSimpleCommand(line, "initialize", out args)
                || CheckSimpleCommand(line, "set", out args))
            {
                // This should be in the format "set number to total"
                Match name = Regex.Match(args,
                                   @"^[\s]*\b" + variableRegex + @"\b[\s](?=([\s]*\b(to)\b[\s]*))",
                                   RegexOptions.IgnoreCase);
                Match value = Regex.Match(args,
                                   @"(?<=([\s]*to[\s]*))\b" + variableRegex + @"\b[\s]*",
                                   RegexOptions.IgnoreCase);

                if (!value.Success || !name.Success)
                    throw new Exception("Invalid Operation Arguments on " + lineNum);

                return new AssignVariable(lineNum, method, name.Value.Trim(), value.Value.Trim());
            }
            // Simple Assignment Instructions
            else if (
                // check for normal assignment
                Regex.IsMatch(line, @"^[\s]*" + variableRegex + @"[\s]*=[\s]*[$]*[a-zA-Z0-9._ ,]*[\s]*$", RegexOptions.IgnoreCase)
                // check for string literals
                || Regex.IsMatch(line, @"^[\s]*" + variableRegex + @"[\s]*=[\s]*" + "[\\'\"]" + @"[a-zA-Z0-9_' -]*" + "[\\'\"]" + @"[\s]*$", RegexOptions.IgnoreCase)
                )
            {
                Match name = Regex.Match(line,
                                   @"^[\s]*" + variableRegex + @"[\s]*=",
                                   RegexOptions.IgnoreCase);
                Match value = Regex.Match(line,
                                   @"(?<=(=[\s]))*[$]*[a-zA-Z0-9._," + '"' + @"' -]*[\s]*$",
                                   RegexOptions.IgnoreCase);
                // This is a simple assignment to either a variable or literal
                return new AssignVariable(lineNum, method, name.Value.Trim('=').Trim(), value.Value.Trim());
            }
            // Mathmatic assignment
            else if (Regex.IsMatch(line, @"^[\s]*" + variableRegex + @"[\s]*=[\s]*[a-zA-Z0-9.()\s" + "\"" + @"_+-/*,]*[\s]*$", RegexOptions.IgnoreCase))
            {
                Match name = Regex.Match(line,
                                   @"^[\s]*" + variableRegex + @"[\s]",
                                   RegexOptions.IgnoreCase);
                Match value = Regex.Match(line,
                                   @"(?<=(=[\s]))*[a-zA-Z0-9._ ()" + "\"" + @"+-/*,]*[\s]*$",
                                   RegexOptions.IgnoreCase);

                // This is a simple assignment to either a variable or literal
                return new MathmaticAssignmentInstruction(lineNum, method, name.Value.Trim(), value.Value.Trim());
            }
            // Simple Call Method
            else if (Regex.IsMatch(line, @"^[\s]*[\s]*[a-zA-Z0-9_]*[\s]*$", RegexOptions.IgnoreCase))
            {
                string name = line.Trim();
                CallMethodInstruction ins = new CallMethodInstruction(lineNum, method, name.Trim());
                return ins;
            }

            throw new Exception(string.Format(
                "Invalid Instruction in Method \"{0}\"",
                method.Name));
        }
    }
}
