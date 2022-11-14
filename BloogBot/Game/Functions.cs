using BloogBot.Game.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Diagnostics;

namespace BloogBot.Game
{
    static public class Functions
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(
            out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(
            out long lpFrequency);
        public static long currenttime()
        {
            IntPtr hardwareEventPtr = IntPtr.Add(Offsets.MemBase, Offsets.EvenTime);

            long perfCount;
            long freq;

            QueryPerformanceFrequency(out freq);
            QueryPerformanceCounter(out perfCount);

            long currentTime = (perfCount * 1000) / freq;
            return currentTime;
        }

        //FindSlotBySpellId: return the slot of spellid for "false" local player ("true" pet)
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate int FindSlotBySpellIdDelegate(Int32 SpellId, bool isPet);

        static readonly FindSlotBySpellIdDelegate FindSlotBySpellIdFunction =
            Marshal.GetDelegateForFunctionPointer<FindSlotBySpellIdDelegate>(IntPtr.Add(MemoryAddresses.MemBase, Offsets.FindSlotBySpellId)); 

        static public int FindSlotBySpellId(Int32 SpellId, bool isPet)
        {
            return FindSlotBySpellIdFunction(SpellId, isPet);
        }
        // end of FindSlotBySpellId  (tested)

        //CastSpellBySlot: cast a spell by the slot, the return is unkown
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate int CastSpellBySlotDelegate(Int32 SpellSlot, Int32 param1, IntPtr TargetGuidPointer, byte param2, byte param3);


        static readonly CastSpellBySlotDelegate CastSpellBySlotFunction =
            Marshal.GetDelegateForFunctionPointer<CastSpellBySlotDelegate>(IntPtr.Add(MemoryAddresses.MemBase, Offsets.CastSpellBySlot)); //linfeng

        static public int CastSpellBySlot(Int32 SpellSlot, IntPtr TargetGuidPtr)
        {
            return CastSpellBySlotFunction(SpellSlot, 0, TargetGuidPtr, 0, 0);
        }
        //end of CastSpellBySlot (tested)

        //Dismount: dismount local player
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate void DismountDelegate();


        static readonly DismountDelegate DismountFunction =
            Marshal.GetDelegateForFunctionPointer<DismountDelegate>(IntPtr.Add(MemoryAddresses.MemBase, MemoryAddresses.Dismount_Ptr)); //linfeng

        static public void Dismount()
        {
            DismountFunction();
        }
        //end of Dismount (tested)

        //PetStopAttack: Stop Pet Attacking state
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate void PetStopAttackDelegate();


        static readonly PetStopAttackDelegate PetStopAttackFunction =
            Marshal.GetDelegateForFunctionPointer<PetStopAttackDelegate>(IntPtr.Add(MemoryAddresses.MemBase, MemoryAddresses.PetStopAttack_Ptr)); //linfeng

        static public void PetStopAttack()
        {
            PetStopAttackFunction();
        }
        //end of PetStopAttack (tested)

        //ClickToMoveMoveTo: CTM to Position
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate void ClickToMoveMoveToDelegate(IntPtr LocalPlayer, IntPtr position);


        static readonly ClickToMoveMoveToDelegate ClickToMoveMoveToFunction =
            Marshal.GetDelegateForFunctionPointer<ClickToMoveMoveToDelegate>(IntPtr.Add(MemoryAddresses.MemBase, Offsets.ClickToMoveMoveTo));

        static public void ClickToMoveMoveTo(IntPtr LocalPlayer, IntPtr position)
        {
            ClickToMoveMoveToFunction(LocalPlayer,  position);
        }
        //end of ClickToMoveMoveTo (tested)

        //ClickToMoveFacing: face to 
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate void FaceToDelegate(IntPtr LocalPlayer, float facing);


        static readonly FaceToDelegate FaceToFunction =
            Marshal.GetDelegateForFunctionPointer<FaceToDelegate>(IntPtr.Add(MemoryAddresses.MemBase, Offsets.ClickToMoveFacing));

        static public void FaceTo(IntPtr LocalPlayer, float facing)
        {
            FaceToFunction(LocalPlayer, facing);
        }
        //end of ClickToMoveMoveTo (tested)

        //AcceptResurrect: accept resurrect
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate void RetrieveCorpseDelegate();


        static readonly RetrieveCorpseDelegate RetrieveCorpseFunction =
            Marshal.GetDelegateForFunctionPointer<RetrieveCorpseDelegate>(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Script_RetrieveCorpse));

        static public void RetrieveCorpse()
        {
            RetrieveCorpseFunction();
        }
        //end of AcceptResurrect (tested)

        //ReleaseCorpse: release the corpse 
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate void ReleaseCorpseDelegate(IntPtr LocalPlayer, bool checkInstance/*=0*/);


        static readonly ReleaseCorpseDelegate ReleaseCorpseFunction =
            Marshal.GetDelegateForFunctionPointer<ReleaseCorpseDelegate>(IntPtr.Add(MemoryAddresses.MemBase, Offsets.RepopRequest));

        static public void ReleaseCorpse(IntPtr LocalPlayer)
        {
            ReleaseCorpseFunction(LocalPlayer, false);
        }
        //end of ReleaseCorpse (tested)

        //ClearAFK: clear AFK state
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate void ClearAFKDelegate(IntPtr LocalPlayer, int afk/*=0*/);


        static readonly ClearAFKDelegate ClearAFKFunction =
            Marshal.GetDelegateForFunctionPointer<ClearAFKDelegate>(IntPtr.Add(MemoryAddresses.MemBase, Offsets.ClearAFK));

        static public void ClearAFK(IntPtr LocalPlayer)
        {
            ClearAFKFunction(LocalPlayer, 0);
        }
        //end of ClearAFK (not tested)

        //IsSpellKnow: check if I learn the spell
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate bool IsSpellKnowDelegate(int spellId, bool isPet);


        static readonly IsSpellKnowDelegate IsSpellKnowFunction =
            Marshal.GetDelegateForFunctionPointer<IsSpellKnowDelegate>(IntPtr.Add(MemoryAddresses.MemBase, Offsets.IsSpellKnown));

        static public bool IsSpellKnow(int spellId, bool isPet)
        {
            return IsSpellKnowFunction(spellId, isPet);
        }
        //end of IsSpellKnow (not tested)

        //GameUIOnSpriteRightClick: Right click object
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate UInt64 GameUIOnSpriteRightClickDelegate(IntPtr guidPtr);


        static readonly GameUIOnSpriteRightClickDelegate GameUIOnSpriteRightClickFunction =
            Marshal.GetDelegateForFunctionPointer<GameUIOnSpriteRightClickDelegate>(IntPtr.Add(MemoryAddresses.MemBase, Offsets.GameUIOnSpriteRightClick));

        static public UInt64 GameUIOnSpriteRightClick(IntPtr guidPtr)
        {
            return GameUIOnSpriteRightClickFunction(guidPtr);
        }
        //end of GameUIOnSpriteRightClick (tested)
    }
}
