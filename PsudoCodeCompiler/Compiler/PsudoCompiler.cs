using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using PseudoCodeCompiler.Runtime;
using PseudoCodeCompiler.Runtime.Instructions;

namespace PseudoCodeCompiler.Compiler
{
    public class PsudoCompiler
    {
        public string[] codeLines;
        public PsudoCodeScanner scanner;
        public PsudoProgram program;
        public List<CompileError> Errors = new List<CompileError>();

        internal string[] CodeLines
        {
            get { return codeLines; }
        }


        public PsudoCompiler(string code)
        {
            codeLines = code.Split('\n');
        }

        public bool CheckSyntax()
        {
            scanner = new PsudoCodeScanner(this);
            if (!scanner.ScanCode())
                return false;

            return true;
        }

        public bool Compile()
        {
            try
            {
                this.program = new PsudoProgram();

                foreach (PsudoClassInfo pClassInfo in this.scanner.classInfo)
                {
                    PsudoClass pClass = new PsudoClass(pClassInfo.Name);
                    this.program.Classes.Add(pClass.Name, pClass);

                    var classMethods = this.scanner.methodInfo.FindAll(
                        item => item.StartLine > pClassInfo.StartLine && item.StartLine < pClassInfo.EndLine);

                    foreach (PsudoMethodInfo mInfo in classMethods)
                    {
                        pClass.Methods.Add(mInfo.Name, CompileMethod(mInfo, pClassInfo, pClass));
                    }

                    var classVars = this.scanner.variableInfo.FindAll(
                        item => item.Line > pClassInfo.StartLine && item.Line < pClassInfo.EndLine && !item.Processed);

                    foreach (PsudoVariableInfo vInfo in classVars)
                    {
                        vInfo.ParentClass = pClassInfo;
                        vInfo.Processed = true;
                        pClass.ClassVariables.Add(vInfo.Name, CreateVariable(vInfo));
                    }
                }

                var globalMethods = this.scanner.methodInfo.FindAll(item => !item.Processed);

                foreach (PsudoMethodInfo mInfo in globalMethods)
                {
                    this.program.GlobalMethods.Add(mInfo.Name, CompileMethod(mInfo, null, null));
                }

                if (this.program.GlobalMethods.Count == 1)
                {
                    this.program.MainMethod = this.program.GlobalMethods.Values.First();
                }
                else 
                {
                    if (this.program.GlobalMethods.ContainsKey("main"))
                    {
                        this.program.MainMethod = this.program.GlobalMethods["main"];
                    }
                    else if (this.program.GlobalMethods.ContainsKey("mainline"))
                    {
                        this.program.MainMethod = this.program.GlobalMethods["mainline"];
                    }
                    else if (this.program.GlobalMethods.ContainsKey("start"))
                    {
                        this.program.MainMethod = this.program.GlobalMethods["start"];
                    }
                    else
                    {
                        // We don't have a designated Main. Will have to be chosen later.
                        this.program.MainMethod = null;
                    }
                }

                var globalVars = this.scanner.variableInfo.FindAll(item => !item.Processed);

                foreach (PsudoVariableInfo vInfo in globalVars)
                {
                    vInfo.Processed = true;
                    this.program.GlobalVariables.Add(vInfo.Name, CreateVariable(vInfo));
                }

                return this.Errors.Count == 0;
            }
            catch (Exception e)
            {
                this.Errors.Add(new CompileError(e.Message, -1));
                return false;
            }
        }

        protected object CreateVariable(PsudoVariableInfo vInfo)
        {
            switch (vInfo.Type)
            {
                case "string":
                    if (vInfo.Default != null)
                        return vInfo.Default;
                    return "";
                case "num":
                    if (vInfo.Default != null)
                        return Convert.ToDouble(vInfo.Default);
                    return 0.0;
            }

            return null;
        }

        protected PsudoInstruction CompileDecision(PsudoMethod method, int line, out int endLine)
        {
            if (Regex.IsMatch(codeLines[line], @"^[\s]*\b(if|elseif)\b[\s]*", RegexOptions.IgnoreCase))
            {
                string decision = codeLines[line].Trim();
                if (decision.ToLower().Contains("elseif"))
                    decision = decision.Substring(6);
                else
                    decision = decision.Substring(2);

                while (!decision.ToLower().Contains("then"))
                {
                    if (line + 1 == codeLines.Length || 
                        Regex.IsMatch(codeLines[line + 1], @"^[\s]*\b(else|elseif|endif)\b[\s]*", RegexOptions.IgnoreCase)
                        )
                    {
                        throw new Exception("IF found without THEN");
                    }
                    decision += " " + codeLines[line + 1];
                    line += 1;
                }

                decision = decision.Trim();
                decision = decision.Substring(0, decision.Length - 4).Trim();

                DecisionInstruction ins = new DecisionInstruction(line, method, decision);
                int next = -1;
                int end = -1;
                int nested = 0;
                for(int num = line + 1; num < codeLines.Length; num++)
                {
                    string item = codeLines[num];
                    if (Regex.IsMatch(item, @"^[\s]*\b(if)\b[\s]*", RegexOptions.IgnoreCase))
                        nested++;
                    if (Regex.IsMatch(item, @"^[\s]*\b(endif)\b[\s]*", RegexOptions.IgnoreCase))
                    {
                        if (nested == 0)
                        {
                            if (next < 0)
                                next = num;
                            end = num;
                        }
                        else
                        {
                            nested--;
                        }
                    }
                    if (nested == 0 && next < 0)
                    {
                        if (Regex.IsMatch(item, @"^[\s]*\b(elseif|else)\b[\s]*", RegexOptions.IgnoreCase))
                        {
                            next = num;
                        }
                    }

                }

                if (end < 0)
                    throw new Exception("IF found with no ENDIF");

                ins.Instructions = CompileBlock(method, line + 1, next - 1);
                int temp;
                ins.FalseInstruction = CompileDecision(method, next, out temp);
                ins.EndInstruction = CompileDecision(method, end, out temp);

                endLine = end;
                return ins;
            }
            else if (Regex.IsMatch(codeLines[line], @"^[\s]*\b(else)\b[\s]*", RegexOptions.IgnoreCase))
            {
                BlockInstruction ins = new BlockInstruction(line, method);
                int next = codeLines.ToList().FindIndex(line, item => Regex.IsMatch(item, @"^[\s]*\b(endif)\b[\s]*", RegexOptions.IgnoreCase));

                ins.Instructions = CompileBlock(method, line + 1, next - 1);
                endLine = next;
                return ins;
            }
            else if (Regex.IsMatch(codeLines[line], @"^[\s]*\b(endif)\b[\s]*", RegexOptions.IgnoreCase))
            {
                endLine = line;
                return new NoOpInstruction(line, method);
            }
            else if (Regex.IsMatch(codeLines[line], @"^[\s]*\b(case of|case)\b[\s]*", RegexOptions.IgnoreCase))
            {
                // Trim of case/case of
                string variable = Regex.Replace(codeLines[line], @"\b(case of|case)\b", "").Trim();            

                int start = codeLines.ToList().FindIndex(line+1, item => item.Contains(":"));
                if (start < 0)
                    throw new Exception("CASE Statement contains no options or options are formmed incorrectly");
                return CompileCase(method, variable, start, out endLine);
            }

            endLine = line;
            return null;
        }

        private PsudoInstruction CompileCase(PsudoMethod method, string variable, int start, out int endLine)
        {
            int nextStart = codeLines.ToList().FindIndex(start+1, 
                item => item.Contains(":") ||
                    Regex.IsMatch(item, @"^[\s]*\b(endcase)\b[\s]*", RegexOptions.IgnoreCase)
                );

            if (nextStart < 0)
                throw new Exception("CASE found with no ENDCASE");

            string[] parts = codeLines[start].Split(':');
            string lineBackup = codeLines[start];
            // Do this to help the compiler
            codeLines[start] = parts[1].Trim();

            if (parts[0].Trim().ToLower().Contains("other"))
            {
                BlockInstruction ins = new BlockInstruction(start, method);

                ins.Instructions = CompileBlock(method, start, nextStart - 1);
                endLine = nextStart;

                codeLines[start] = lineBackup;
                return ins;
            }
            else
            {
                string decision = variable + "==" + parts[0].Trim();
                DecisionInstruction ins = new DecisionInstruction(start, method, decision);

                ins.Instructions = CompileBlock(method, start, nextStart - 1);

                if (codeLines[nextStart].ToLower().Contains("endcase"))
                {
                    endLine = nextStart;
                    ins.FalseInstruction = new NoOpInstruction(endLine, method);
                }
                else
                {
                    ins.FalseInstruction = CompileCase(method, variable, nextStart, out endLine);
                }
                ins.EndInstruction = new NoOpInstruction(endLine, method);

                codeLines[start] = lineBackup;
                return ins;
            }
        }

        protected int FindNestedEnd(int line, string startMatch, string endMatch)
        {
            int end = -1;
            int nested = 0;
            for (int num = line + 1; num < codeLines.Length; num++)
            {
                string item = codeLines[num];
                if (Regex.IsMatch(item, @"^[\s]*\b(" + startMatch + @")\b[\s]*", RegexOptions.IgnoreCase))
                    nested++;
                if (Regex.IsMatch(item, @"^[\s]*\b(" + endMatch + @")\b[\s]*", RegexOptions.IgnoreCase))
                {
                    if (nested == 0)
                    {
                        end = num;
                        break;
                    }
                    else
                    {
                        nested--;
                    }
                }
            }

            return end;
        }

        protected PsudoInstruction CompileLoop(PsudoMethod method, int line, out int endLine)
        {
            if (Regex.IsMatch(codeLines[line], @"^[\s]*\b(while|dowhile)\b[\s]*", RegexOptions.IgnoreCase))
            {
                string decision = codeLines[line].Trim();
                if (decision.ToLower().StartsWith("dowhile"))
                    decision = decision.Substring(7).Trim();
                else
                    decision = decision.Substring(5).Trim();

                WhileLoopInstruction ins = new WhileLoopInstruction(line, method, decision);

                //int end = codeLines.ToList().FindIndex(line, item => Regex.IsMatch(item, @"^[\s]*\b(endwhile|enddo)\b[\s]*", RegexOptions.IgnoreCase));
                int end = FindNestedEnd(line, "(while|dowhile|do)", "(endwhile|enddo)");
                if (end < 0)
                    throw new Exception("WHILE found with no ENDWHILE");

                ins.Instructions = CompileBlock(method, line + 1, end - 1);

                endLine = end;
                return ins;
            }
            if (Regex.IsMatch(codeLines[line], @"^[\s]*\b(repeat)\b[\s]*", RegexOptions.IgnoreCase))
            {
                //int end = codeLines.ToList().FindIndex(line, item => Regex.IsMatch(item, @"^[\s]*\b(until)\b[\s]*", RegexOptions.IgnoreCase));
                int end = FindNestedEnd(line, "(repeat)", "(until)");
                if (end < 0)
                    throw new Exception("REPEAT found with no UNTIL");

                string decision = codeLines[end].Trim();
                decision = decision.Substring(5).Trim();

                DoLoopInstruction ins = new DoLoopInstruction(line, method, decision);

                ins.Instructions = CompileBlock(method, line + 1, end - 1);

                endLine = end;
                return ins;
            }
            if (Regex.IsMatch(codeLines[line], @"^[\s]*\b(do)\b[\s]*", RegexOptions.IgnoreCase))
            {
                string decision = codeLines[line].Trim();

                Match variable = Regex.Match(decision, @"(?<=do[\s]+)[a-zA-Z0-9_()-]+", RegexOptions.IgnoreCase);
                Match initVal = Regex.Match(decision, @"(?<=[=][\s]*)[a-zA-Z0-9_()-]+(?=[\s]*(to))", RegexOptions.IgnoreCase);
                Match endVal = Regex.Match(decision, @"(?<=\b(to)\b[\s]*)[a-zA-Z0-9_()-]+", RegexOptions.IgnoreCase);

                if (!variable.Success || !initVal.Success || !endVal.Success)
                    throw new Exception("Error in Do Loop Formatting");

                //int end = codeLines.ToList().FindIndex(line, item => Regex.IsMatch(item, @"^[\s]*\b(enddo)\b[\s]*", RegexOptions.IgnoreCase));
                int end = FindNestedEnd(line, "(do|while|dowhile|do)", "(enddo)");
                if (end < 0)
                    throw new Exception("DO found with no ENDDO");

                ForLoopInstruction ins = new ForLoopInstruction(line, method, variable.Value.Trim(), initVal.Value.Trim(), endVal.Value.Trim());

                ins.Instructions = CompileBlock(method, line + 1, end - 1);

                endLine = end;
                return ins;
            }

            endLine = line;
            return null;
        }

        protected PsudoMethod CompileMethod(PsudoMethodInfo mInfo, PsudoClassInfo pClassInfo, PsudoClass pClass)
        {
            PsudoMethod method = new PsudoMethod(mInfo.Name, pClass);
            mInfo.ParentClass = pClassInfo;
            mInfo.Processed = true;

            if (mInfo.Name != "Main")
            {
                Match value = Regex.Match(codeLines[mInfo.StartLine],
                                   @"(?<=[(])[a-zA-Z0-9_,\s -]*(?=[)])",
                                   RegexOptions.IgnoreCase);

                string[] args = value.Value.Split(',');
                foreach (string arg in args)
                {
                    if (arg.Trim().Length > 0)
                    {
                        Match typeMatch = Regex.Match(arg,
                                  @"(string|num)",
                                  RegexOptions.IgnoreCase);
                        Match match = Regex.Match(arg,
                            @"(?<=(string|num))[\s]*[a-zA-Z0-9_-]*",
                            RegexOptions.IgnoreCase);
                        method.ArgumentTypes.Add(typeMatch.Value.Trim().ToLower());
                        method.ArgumentNames.Add(match.Value.Trim().ToLower());
                    }
                }
            }

            method.Instructions = CompileBlock(method, mInfo.StartLine, mInfo.EndLine);
            return method;
        }

        protected List<PsudoInstruction> CompileBlock(PsudoMethod method, int startLine, int endLine)
        {
            List<PsudoInstruction> list = new List<PsudoInstruction>();
            for (int i = startLine; i <= endLine; i++)
            {
                try
                {
                    switch (this.scanner.lineTypes[i])
                    {
                        case PsudoLineType.Variable:
                            PsudoVariableInfo vInfo = this.scanner.variableInfo.Find(
                                item => item.Line == i);
                            vInfo.Processed = true;
                            list.Add(new CreateLocalVariable(i, method, vInfo.Type, vInfo.Name, vInfo.Default));
                            break;
                        case PsudoLineType.StartDecision:
                        case PsudoLineType.StartStartDecision:
                            int end;
                            list.Add(CompileDecision(method, i, out end));
                            i = end;
                            break;
                        case PsudoLineType.StartLoop:
                            int end2;
                            list.Add(CompileLoop(method, i, out end2));
                            i = end2;
                            break;
                        case PsudoLineType.Instruction:
                            list.Add(InstructionFactory.CompileInstruction(
                                this.codeLines[i], i, method));
                            break;
                        // Nothing to do for the following
                        case PsudoLineType.StartMain:
                        case PsudoLineType.StartMethod:
                        case PsudoLineType.Whitespace:
                        case PsudoLineType.Comment:
                        case PsudoLineType.EndMethod:
                            break;
                        case PsudoLineType.EndDecision:
                            throw new Exception("Unexpected End Decision");
                        case PsudoLineType.EndLoop:
                            throw new Exception("Unexpected End Loop");
                        case PsudoLineType.EndClass:
                        case PsudoLineType.StartClass:
                            throw new Exception("Classes Not Supported Currently");
                        default:
                            throw new Exception(string.Format(
                                "Error Compiling Method \"{0}\" on Line #{1}",
                                method.Name, i+1));
                    }
                }
                catch (Exception e)
                {
                    this.Errors.Add(new CompileError(e.Message, i+1));
                }
            }

            return list;
        }
    }
}
