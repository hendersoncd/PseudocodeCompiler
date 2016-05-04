using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoCodeCompiler.Compiler
{
    public enum PsudoLineType
    {
        Whitespace,
        Comment,
        StartClass,
        EndClass,
        StartMethod,
        EndMethod,
        StartMain,
        StartLoop,
        EndLoop,
        StartDecision,
        
        StartStartDecision,
        MidStartDecision,
        EndStartDecision,

        EndDecision,
        Variable,
        Instruction
    }
}
