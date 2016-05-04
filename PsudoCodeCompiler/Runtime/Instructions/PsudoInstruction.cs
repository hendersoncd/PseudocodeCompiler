using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PseudoCodeCompiler.Runtime.Instructions
{
    public abstract class PsudoInstruction
    {
        public int Line { get; set; }
        public PsudoMethod Method { get; set; }

        public PsudoInstruction(int lineNumber, PsudoMethod method)
        {
            this.Line = lineNumber;
            this.Method = method;
        }

        /// <summary>
        /// Run the instruction, Return the next instruction to run.
        /// If null is returned then it is assumed the next instruction contained
        /// in the method will be executed.
        /// </summary>
        /// <returns></returns>
        public abstract PsudoInstruction Run();

        public virtual PsudoInstruction CopyTo(PsudoMethod newMethod)
        {
            // by default this will work but where needed we can override.
            PsudoInstruction copy = this.MemberwiseClone() as PsudoInstruction;
            copy.Method = newMethod;
            return copy;
        }

        public override string ToString()
        {
            return this.Line.ToString() + "-" + this.GetType().Name;
            //return base.ToString();
        }
    }
}
