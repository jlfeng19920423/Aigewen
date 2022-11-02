using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using BloogBot.Game.Enums;
using BloogBot.Game;

namespace BloogBot
{
    public static unsafe class MemoryManager
    {
        [DllImport("kernel32.dll")]
        static extern bool WriteProcessMemory(
        IntPtr hProcess,
        IntPtr lpBaseAddress,
        byte[] lpBuffer,
        int dwSize,
        ref int lpNumberOfBytesWritten);

        //read byte
        [HandleProcessCorruptedStateExceptions]
        internal static byte ReadByte(IntPtr address)
        {
            try
            {
                return *(byte*)address;
            }
            catch (AccessViolationException)
            {
                Console.WriteLine("Access Violation on " + address + " with type Byte");
                return default;
            }
        }

        // read int
        [HandleProcessCorruptedStateExceptions]
        internal static int ReadInt(IntPtr address)
        {
            try
            {
                return *(int*)address;
            }
            catch (AccessViolationException)
            {
                Console.WriteLine("Access Violation on " + address + " with type Int");
                return default;
            }
        }

        [HandleProcessCorruptedStateExceptions]
        static internal uint ReadUint(IntPtr address)
        {
            try
            {
                return *(uint*)address;
            }
            catch (AccessViolationException)
            {
                Console.WriteLine("Access Violation on " + address + " with type Uint");
                return default;
            }
        }

        [HandleProcessCorruptedStateExceptions]
        static internal ulong ReadUlong(IntPtr address)
        {
            try
            {
                return *(ulong*)address;
            }
            catch (AccessViolationException)
            {
                Console.WriteLine("Access Violation on " + address + " with type Ulong");
                return default;
            }
        }

        //read intptr
        [HandleProcessCorruptedStateExceptions]
        static internal IntPtr ReadIntPtr(IntPtr address)
        {
            try
            {
                return *(IntPtr*)address;
            }
            catch (AccessViolationException ex)
            {
                Console.WriteLine("Access Violation on " + address + " with type IntPtr");
                return default;
            }
        }

        [HandleProcessCorruptedStateExceptions]
        internal static float ReadFloat(IntPtr address)
        {
            try
            {
                return *(float*)address;
            }
            catch (AccessViolationException ex)
            {
                Console.WriteLine("Access Violation on " + address + " with type Float");
                return default;
            }
        }

        //read string
        [HandleProcessCorruptedStateExceptions]
        internal static string ReadString(IntPtr address)
        {
            var buffer = ReadBytes(address, 512);
            if (buffer.Length == 0)
                return default;

            var ret = Encoding.ASCII.GetString(buffer);

            if (ret.IndexOf('\0') != -1)
                ret = ret.Remove(ret.IndexOf('\0'));

            return ret;
        }

        //read stringname
        [HandleProcessCorruptedStateExceptions]
        internal static string ReadStringName(IntPtr address, Encoding encoding)
        {
            var buffer = ReadBytes(address, 512);
            if (buffer.Length == 0)
                return default;
            
            var ret = encoding.GetString(buffer);

            if (ret.IndexOf('\0') != -1)
                ret = ret.Remove(ret.IndexOf('\0'));

            return ret;
        }

        [HandleProcessCorruptedStateExceptions]
        internal static byte[] ReadBytes(IntPtr address, int count)
        {
            try
            {
                var ret = new byte[count];
                var ptr = (byte*)address;

                for (var i = 0; i < count; i++)
                    ret[i] = ptr[i];

                return ret;
            }
            catch (AccessViolationException ex)
            {
                Console.WriteLine("Access Violation on " + address + " with type Byte[]");
                return default;
            }
        }

        [HandleProcessCorruptedStateExceptions]
        static internal CGGuid ReadGuid(IntPtr address)
        {
            try
            {
                return *(CGGuid*)address;
            }
            catch (AccessViolationException)
            {
                Console.WriteLine("Access Violation on " + address + " with type Ulong");
                return default;
            }
        }

        [HandleProcessCorruptedStateExceptions]
        static internal Aura ReadAura(IntPtr address)
        {
            try
            {
                return *(Aura*)address;
            }
            catch (AccessViolationException)
            {
                Console.WriteLine("Access Violation on " + address + " with type Ulong");
                return default;
            }
        }

        internal static void WriteBytes(IntPtr address, byte[] bytes)
        {
            //var process = Process.GetProcessesByName("WoW")[0].Handle;
            var processHandle = Process.GetProcessById(Process.GetCurrentProcess().Id).Handle;
            int ret = 0;
            WriteProcessMemory(processHandle, address, bytes, bytes.Length, ref ret);
        }

        internal static IntPtr GetPosAddr(ref float[] pos)
        {
            try
            {
                fixed (float* ptr = &pos[0])
                {
                    IntPtr address = (IntPtr)ptr;
                    return address;
                }
            }
            catch (AccessViolationException)
            {
                Console.WriteLine("Access Violation on " + pos + " with type Ulong");
                return default;
            }
        }
    }
}
