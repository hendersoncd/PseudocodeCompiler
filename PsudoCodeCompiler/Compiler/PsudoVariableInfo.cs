using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoCodeCompiler.Compiler
{
    public class PsudoVariableInfo
    {
        public int Line { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Default { get; set; }
        public bool Processed { get; set; }
        public PsudoClassInfo ParentClass { get; set; }
        public PsudoMethodInfo ParentMethod { get; set; }

        public PsudoVariableInfo(string name, string type, int line)
        {
            this.Name = name;
            this.Type = type;
            this.Line = line;
            this.Processed = false;
            this.ParentClass = null;
            this.ParentMethod = null;
            this.Default = null;
        }
    }
}
