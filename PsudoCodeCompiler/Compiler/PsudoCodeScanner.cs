using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PseudoCodeCompiler.Compiler
{
    public class PsudoCodeScanner
    {
        PsudoCompiler compiler;
        protected string[] codeLines
        {
            get { return compiler.CodeLines; }
        }
        public PsudoLineType[] lineTypes;

        public List<CompileError> errors;
        public List<PsudoClassInfo> classInfo;
        public List<PsudoMethodInfo> methodInfo;
        public List<PsudoVariableInfo> variableInfo;
        //public PsudoMethodInfo mainMethod;

        public PsudoCodeScanner(PsudoCompiler compiler)
        {
            this.compiler = compiler;
        }

        public bool ScanCode()
        {
            errors = new List<CompileError>();
            lineTypes = new PsudoLineType[codeLines.Length];

            for (int i = 0; i < codeLines.Length; i++)
            {
                try
                {
                    lineTypes[i] = CategorizeLine(codeLines[i]);
                }
                catch (Exception e)
                {
                    this.errors.Add(new CompileError(e.Message, i + 1));
                    break;
                }
            }
            if (errors.Count > 0)
                return false;

            //this.FindMain();
            this.FindClasses();
            this.FindMethods();
            this.FindVariables();
            
            return errors.Count == 0;
        }

        private void FindVariables()
        {
            this.variableInfo = new List<PsudoVariableInfo>();

            for (int i = 0; i < lineTypes.Length; i++)
            {
                switch (lineTypes[i])
                {
                    case PsudoLineType.Variable:
                        Match typeMatch = Regex.Match(codeLines[i],
                            @"(string|num)",
                            RegexOptions.IgnoreCase);
                            Match match = Regex.Match(codeLines[i],
                                @"(?<=(string|num))[\s]*[a-zA-Z0-9_-]*",
                                RegexOptions.IgnoreCase);
                            PsudoVariableInfo var = new PsudoVariableInfo(
                                match.Value.Trim().ToLower(), typeMatch.Value.Trim().ToLower(), i);

                            Match value = Regex.Match(codeLines[i],
                                               @"(?<=(=[\s]*))[a-zA-Z0-9_-]*[\s]*$",
                                               RegexOptions.IgnoreCase);
                            if (value.Success)
                                var.Default = value.Value;
                            this.variableInfo.Add(var);
                        break;
                    default:
                        break;
                }
            }
        }

        private void FindClasses()
        {
            this.classInfo = new List<PsudoClassInfo>();

            PsudoClassInfo curClass = null;
            for (int i = 0; i < lineTypes.Length; i++)
            {
                switch (lineTypes[i])
                {
                    case PsudoLineType.StartClass:
                        if (curClass != null)
                        {
                            this.errors.Add(new CompileError(
                                "Cannot Start Class Inside Body of another class", i));
                        }
                        else
                        {
                            Match match = Regex.Match(codeLines[i],
                                @"(?<=(class))[\s]*[a-zA-Z0-9_-]*",
                                RegexOptions.IgnoreCase);
                            curClass = new PsudoClassInfo(match.Value.Trim().ToLower(), i);
                        }

                        break;
                    case PsudoLineType.EndClass:
                        if (curClass == null)
                        {
                            this.errors.Add(new CompileError(
                                "End Class Found without Start Class", i));
                        }
                        else
                        {
                            curClass.EndLine = i;
                            this.classInfo.Add(curClass);
                            curClass = null;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void FindMethods()
        {
            this.methodInfo = new List<PsudoMethodInfo>();

            PsudoMethodInfo curMethod = null;
            for (int i = 0; i < lineTypes.Length; i++)
            {
                switch (lineTypes[i])
                {
                    case PsudoLineType.StartMethod:
                        if (curMethod != null)
                        {
                            this.errors.Add(new CompileError(
                                "Cannot Start Method Inside Body of another method", i));
                        }
                        else
                        {
                            //Match match = Regex.Match(codeLines[i],
                            //    @"(?<=(void|string|num))[\s]*[a-zA-Z0-9_-]*[\s]*(?=\()*",
                            //    RegexOptions.IgnoreCase);
                            curMethod = new PsudoMethodInfo(codeLines[i].Trim().ToLower(), i);
                        }

                        break;
                    case PsudoLineType.EndMethod:
                        if (curMethod == null)
                        {
                            this.errors.Add(new CompileError(
                                "Return statement Found outside a method", i));
                        }
                        else
                        {
                            curMethod.EndLine = i;
                            this.methodInfo.Add(curMethod);
                            curMethod = null;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        //private void FindMain()
        //{
        //    mainMethod = new PsudoMethodInfo("Main", -1);

        //    for (int i = 0; i < lineTypes.Length; i++)
        //    {
        //        switch (lineTypes[i])
        //        {
        //            case PsudoLineType.StartMain:
        //                if (mainMethod.StartLine < 0)
        //                {
        //                    mainMethod.StartLine = i;
        //                }
        //                else
        //                {
        //                    this.errors.Add(new CompileError(
        //                        "Multiple Main Method Starts", i));
        //                }
        //                break;

        //            case PsudoLineType.EndMethod:
        //                if (mainMethod.StartLine < 0 || mainMethod.EndLine >= 0)
        //                {
        //                    // Not the correct end
        //                    continue;
        //                }
        //                else
        //                {
        //                    mainMethod.EndLine = i;
        //                }
        //                break;
        //            // Check for types that cannot be in main
        //            case PsudoLineType.StartMethod:
        //            case PsudoLineType.EndClass:
        //            case PsudoLineType.StartClass:
        //                if (mainMethod.StartLine >= 0 && mainMethod.EndLine < 0) // If we are in the main method these are bad
        //                {
        //                    this.errors.Add(new CompileError(
        //                        "Invalid Construct in Main Method", i));
        //                }
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    if (mainMethod.StartLine < 0)
        //    {
        //        this.errors.Add(new CompileError(
        //            "No Main Method Start Found", -1));
        //    }
        //    if (mainMethod.EndLine < 0)
        //    {
        //        this.errors.Add(new CompileError(
        //            "No Main Method End Found", -1));
        //    }

        //}

        private PsudoLineType lastLine = PsudoLineType.Comment;

        private PsudoLineType CategorizeLine(string line)
        {
            lastLine = RealCategorizeLine(line);
            return lastLine;
        }

        bool inMethod = false;

        private PsudoLineType RealCategorizeLine(string line)
        {
            if (lastLine == PsudoLineType.StartStartDecision || lastLine == PsudoLineType.MidStartDecision)
            {
                if (Regex.IsMatch(line, @"\b(then)\b", RegexOptions.IgnoreCase))
                    return PsudoLineType.EndStartDecision;
                if (Regex.IsMatch(line, @"[\s]*\b(elseif|else|endif|end)\b[\s]*", RegexOptions.IgnoreCase))
                    throw new Exception("IF found without THEN");

                return PsudoLineType.MidStartDecision;
            }

            if (Regex.IsMatch(line, @"^[\s]*#.*$"))
            {
                return PsudoLineType.Comment;
            }
            else if (Regex.IsMatch(line, @"^[\s]*$"))
            {
                return PsudoLineType.Whitespace;
            }
            //else if (Regex.IsMatch(line, @"^[\s]*(start|main|mainline)[\s]*$", RegexOptions.IgnoreCase))
            //{
            //    inMethod = true;
            //    return PsudoLineType.StartMain;
            //}
            else if (Regex.IsMatch(line, @"^[\s]*(stop|end|endmain)[\s]*$", RegexOptions.IgnoreCase))
            {
                inMethod = false;
                return PsudoLineType.EndMethod;
            }
            else if (Regex.IsMatch(line, @"^[\s]*\b(string|num)\b[\s]*[a-zA-Z0-9_]*[\s]*", RegexOptions.IgnoreCase))
            {
                return PsudoLineType.Variable;
            }
            else if (Regex.IsMatch(line, @"^[\s]*\b(while|foreach|for|dowhile|repeat|do)\b[\s]*[a-zA-Z0-9_()><=!|& -]*$", RegexOptions.IgnoreCase))
            {
                return PsudoLineType.StartLoop;
            }
            else if (Regex.IsMatch(line, @"^[\s]*\b(endwhile|endforeach|endfor|enddo|endloop|until)\b[\s]*", RegexOptions.IgnoreCase))
            {
                return PsudoLineType.EndLoop;
            }
            else if (Regex.IsMatch(line, @"^[\s]*\b(if|elseif)\b[\s]*[a-zA-Z0-9._=()$," + "\"><=!|& -]*$", RegexOptions.IgnoreCase))
            {
                if (Regex.IsMatch(line, @"\b(then)\b", RegexOptions.IgnoreCase))
                    return PsudoLineType.StartDecision;
                else
                    return PsudoLineType.StartStartDecision;
            }
            else if (Regex.IsMatch(line, @"^[\s]*\b(case)\b[\s]*[a-zA-Z0-9_=()" + "\"><=!|& -]*$", RegexOptions.IgnoreCase))
            {
                return PsudoLineType.StartDecision;
            }
            else if (Regex.IsMatch(line, @"^[\s]*\b(else)\b", RegexOptions.IgnoreCase))
            {
                return PsudoLineType.Instruction;
            }
            else if (Regex.IsMatch(line, @"^[\s]*\b(endif|endcase)\b[\s]*$", RegexOptions.IgnoreCase))
            {
                return PsudoLineType.EndDecision;
            }
            else if (Regex.IsMatch(line, @"^[\s]*\b(class)\b[\s]*[a-zA-Z0-9_]*[\s]*$", RegexOptions.IgnoreCase))
            {
                return PsudoLineType.StartClass;
            }
            else if (Regex.IsMatch(line, @"^[\s]*\b(endclass)\b[\s]*$", RegexOptions.IgnoreCase))
            {
                return PsudoLineType.EndClass;
            }
            else if (
                /*Regex.IsMatch(line, @"^[\s]*\b(void|string|num)\b[\s]*[a-zA-Z0-9_]*[\s]*[\(][a-zA-Z0-9_ ,-]*[)][\s]*$", RegexOptions.IgnoreCase)
                || */
                Regex.IsMatch(line, @"^[\s]*[\s]*[a-zA-Z0-9_]*[\s]*$", RegexOptions.IgnoreCase)
                )
            {
                if (inMethod)
                {
                    return PsudoLineType.Instruction;
                }
                else
                {
                    inMethod = true;
                    return PsudoLineType.StartMethod;
                }
            }

            return PsudoLineType.Instruction;
        }
    }
}
