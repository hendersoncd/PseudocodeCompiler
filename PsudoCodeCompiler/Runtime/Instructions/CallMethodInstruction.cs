using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoCodeCompiler.Runtime.Instructions
{
    class CallMethodInstruction : PsudoInstruction
    {
        string methodName;
        public List<string> Arguments {get; set;}

        public CallMethodInstruction(int lineNumber, PsudoMethod method, string methodName)
            : base(lineNumber, method)
        {
            this.methodName = methodName;
            this.Arguments = new List<string>();
        }

        public override PsudoInstruction Run()
        {
            PsudoMethod method = this.Method.GetMethod(this.methodName);
            method.Program = this.Method.Program;
            this.Method.Program.callStack.Push(method);
            if (Arguments.Count != method.ArgumentNames.Count)
            {
                this.Method.Program.OnError("Incorrect number of parameters");
                return null;
            }
            for(int i = 0; i < Arguments.Count; i++)
            {
                method.CreateVariable(method.ArgumentNames[i], this.Method.GetVariable(Arguments[i].Trim()));
                if (method.ArgumentTypes[i] == "num" && method.GetVariable(method.ArgumentNames[i]) is string)
                {
                    method.SetVariable(method.ArgumentNames[i], Convert.ToDouble(Arguments[i]));
                }
                else if (method.ArgumentTypes[i] == "string" && method.GetVariable(method.ArgumentNames[i]) is double)
                {
                    method.SetVariable(method.ArgumentNames[i], this.Method.GetVariable(Arguments[i]).ToString());
                }
            }
            method.Run(this.Method.Program);

            this.Method.Program.callStack.Pop();
            return null;
        }

        public override PsudoInstruction CopyTo(PsudoMethod newMethod)
        {
            CallMethodInstruction clone = new CallMethodInstruction(this.Line, newMethod, this.methodName);
            clone.Arguments = this.Arguments;
            return clone;
        }

        public override string ToString()
        {
            return "CALL " + methodName;
        }
    }
}
