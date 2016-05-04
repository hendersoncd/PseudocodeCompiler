using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PseudoCodeCompiler
{
    static class Program
    {
		public static bool MonoRuntime
		{
			get
			{
				return Type.GetType("Mono.Runtime") != null;
			}
		}
		
        public static PsudoRuntimeViewer MainForm
        {
            get
            {
                return mainForm;
            }
        }

        private static PsudoRuntimeViewer mainForm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainForm = new PsudoRuntimeViewer();
            Application.Run(mainForm);
        }
    }
}
