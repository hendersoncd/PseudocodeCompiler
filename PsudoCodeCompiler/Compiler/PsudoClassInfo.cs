using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoCodeCompiler.Compiler
{
    public class PsudoClassInfo
    {
        public int StartLine { get; set; }
        public int EndLine { get; set; }
        public string Name { get; set; }

        public PsudoClassInfo(string name, int start)
        {
            this.Name = name;
            this.StartLine = start;
            this.EndLine = -1;
        }
    }
}
