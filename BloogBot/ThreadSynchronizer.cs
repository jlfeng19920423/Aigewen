using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace BloogBot
{
    static public class ThreadSynchronizer
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        
        [DllImport("user32.dll")]
        static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        [DllImport("user32.dll")]
        static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("kernel32.dll")]
        static extern uint GetCurrentThreadId();

        delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        delegate int WindowProc(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        static readonly Queue<Action> actionQueue = new Queue<Action>();
        static readonly Queue<Delegate> delegateQueue = new Queue<Delegate>();
        static readonly Queue<object> returnValueQueue = new Queue<object>();

        const int GWL_WNDPROC = -4;
        const int WM_USER = 0x0400;
        static IntPtr oldCallback;
        static WindowProc newCallback;
        static IntPtr windowHandle;
        
        
        static ThreadSynchronizer()
        {
            //windowHandle = Process.GetCurrentProcess().Handle;
            EnumWindows(FindWindowProc, IntPtr.Zero);
            newCallback = WndProc;
            oldCallback = SetWindowLongPtr((IntPtr)windowHandle, GWL_WNDPROC, Marshal.GetFunctionPointerForDelegate(newCallback));
        }

        static public void RunOnMainThread(Action action)
        {
            if (GetCurrentThreadId() == Process.GetCurrentProcess().Threads[0].Id)
            {
                action();
                return;
            }
            actionQueue.Enqueue(action);
            SendUserMessage();
        }

        static public T RunOnMainThread<T>(Func<T> function)
        {
            if (GetCurrentThreadId() == Process.GetCurrentProcess().Threads[0].Id)
                return function();

            delegateQueue.Enqueue(function);
            SendUserMessage();
            return (T)returnValueQueue.Dequeue();
        }

        static int WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            try
            {
                if (msg != WM_USER) return CallWindowProc(oldCallback, hWnd, msg, wParam, lParam);

                while (actionQueue.Count > 0)
                    actionQueue.Dequeue()?.Invoke();
                while (delegateQueue.Count > 0)
                {
                    var invokeTarget = delegateQueue.Dequeue();
                    returnValueQueue.Enqueue(invokeTarget?.DynamicInvoke());
                }
                return 0;
            }
            catch (Exception e)
            {
               // Logger.Log(e);
            }

            return CallWindowProc(oldCallback, hWnd, msg, wParam, lParam);
        }

        static bool FindWindowProc(IntPtr hWnd, IntPtr lParam)
        {
            GetWindowThreadProcessId(hWnd, out int procId);
            if (procId != Process.GetCurrentProcess().Id) return true;
            if (!IsWindowVisible(hWnd)) return true;
            var l = GetWindowTextLength(hWnd);
            if (l == 0) return true;
            var builder = new StringBuilder(l + 1);
            GetWindowText(hWnd, builder, builder.Capacity);
            if (builder.ToString() == "魔兽世界")
                windowHandle = hWnd;
            return true;
        }

        static void SendUserMessage() => SendMessage(windowHandle, WM_USER, IntPtr.Zero, IntPtr.Zero);
    }
}
