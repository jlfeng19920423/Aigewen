using System.Runtime.InteropServices;

namespace BloogBot.Game.Enums
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CGGuid
    {
        public ulong low;
        public ulong high;
        
        public bool isEmpty() { return high == 0 && low == 0; }

        public bool isEqualTo(CGGuid guid)
        {
            if (high == guid.high && low == guid.low)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
