using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PseudoCodeCompiler
{
    class KeyboardHooks
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;

        public delegate void KeyboardHookCallback(Keys keyPressed, Keys modifier);
        private Dictionary<Keys, Dictionary<Keys, List<KeyboardHookCallback>>> callbacks;

        public KeyboardHooks()
        {
	        callbacks = new Dictionary<Keys, Dictionary<Keys, List<KeyboardHookCallback>>>();
			
			if (!Program.MonoRuntime)
			{
	            _proc = HookCallback;
	            _hookID = SetHook(_proc);
			}
        }

        ~KeyboardHooks()
        {
			if (!Program.MonoRuntime)
			{
            	UnhookWindowsHookEx(_hookID);
			}
        }

        public void RegisterHook(Keys key, KeyboardHookCallback callback)
        {
            Keys modifiers = 0;

            if ((key & Keys.Alt) == Keys.Alt)
                modifiers = modifiers | Keys.Alt;

            if ((key & Keys.Control) == Keys.Control)
                modifiers = modifiers | Keys.Control;

            if ((key & Keys.Shift) == Keys.Shift)
                modifiers = modifiers | Keys.Shift;

            Keys k = key & ~Keys.Control & ~Keys.Shift & ~Keys.Alt;

            if (!callbacks.ContainsKey(modifiers))
                callbacks.Add(modifiers, new Dictionary<Keys, List<KeyboardHookCallback>>());
            if (!callbacks[modifiers].ContainsKey(k))
                callbacks[modifiers].Add(k, new List<KeyboardHookCallback>());

            callbacks[modifiers][k].Add(callback);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                Keys keyPressed = (Keys)Marshal.ReadInt32(lParam);
                if (this.callbacks.ContainsKey(Control.ModifierKeys))
                {
                    if (this.callbacks[Control.ModifierKeys].ContainsKey(keyPressed))
                    {
                        foreach (KeyboardHookCallback callback in this.callbacks[Control.ModifierKeys][keyPressed])
                        {
                            callback.Invoke(keyPressed, Control.ModifierKeys);
                        }
                    }
                }

            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}

class InterceptKeys
{


}
