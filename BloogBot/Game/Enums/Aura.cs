using System;
using System.Runtime.InteropServices;

namespace BloogBot.Game.Enums
{
	[StructLayout(LayoutKind.Sequential)]
	
	public unsafe struct Aura
	{
		fixed Char pad_0000[32]; //0x0000
		CGGuid OwnerGuid; //0x0020
		fixed Char pad_0028[16]; //0x0030
		uint TimeLeft; //0x0040
		fixed Char pad_0044[68]; //0x0044
		int SpellID; //0x0088
		fixed Char pad_008C[4]; //0x008C
		UInt16 Flags; //0x0090
		byte Level; //0x0092
					//char pad_0094[240]; //0x0094
	}; //Size: 0x0184
	
	/*
	public class Aura
	{
		Char[] pad_0000 = new Char[32]; //0x0000
		CGGuid OwnerGuid; //0x0020
		Char[] pad_0028 = new Char[16]; //0x0030
		uint TimeLeft; //0x0040
		Char[] pad_0044 = new Char[68]; //0x0044
		int SpellID; //0x0088
		Char[] pad_008C = new Char[4]; //0x008C
		UInt16 Flags; //0x0090
		byte Level; //0x0092
					//char pad_0094[240]; //0x0094
	}; //Size: 0x0184
	*/
}
