using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoCodeCompiler.Compiler
{
    public class PsudoMethodInfo
    {
        public int StartLine { get; set; }
        public int EndLine { get; set; }
        public string Name { get; set; }
        public bool Processed { get; set; }
        public PsudoClassInfo ParentClass { get; set; }

        public PsudoMethodInfo(string name, int start)
        {
            this.Name = name;
            this.StartLine = start;
            this.EndLine = -1;
            this.Processed = false;
            this.ParentClass = null;
        }
    }
}
