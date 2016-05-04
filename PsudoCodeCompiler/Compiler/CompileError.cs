using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoCodeCompiler.Compiler
{
    public class CompileError
    {
        public string Details { get; set; }
        public int Line { get; set; }

        public CompileError(string details, int line)
        {
            this.Details = details;
            this.Line = line;
        }
    }
}
