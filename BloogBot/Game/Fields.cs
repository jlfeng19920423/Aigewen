using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloogBot.Game
{
    class Fields
    {
        public class Inventory
        {
            public const int EquipmentFirst = 0x010; // Updated
            public const int EquipmentLast = 0x120; // Updated


            public const int BagSlot1 = 0x140; // Updated
            public const int BagSlot2 = 0x150; // Updated
            public const int BagSlot3 = 0x160; // Updated
            public const int BagSlot4 = 0x170; // Updated


            public const int BackpackFirst = 0x170; // Updated
            public const int BackpackLast = 0x270; // Updated


            public const int BankSlotFirst = 0x300; // Updated
            public const int BankSlotLast = 0x4B0; // Updated


            public const int BankBag1 = 0x130; // Not Used
            public const int BankBag2 = 0x140; // Not Used
            public const int BankBag3 = 0x150; // Not Used
            public const int BankBag4 = 0x160; // Not Used
            public const int BankBag5 = 0x130; // Not Used
            public const int BankBag6 = 0x140; // Not Used
            public const int BankBag7 = 0x150; // Not Used
        }


        public class Unit
        {
            public const int UnitId = 0x0; // ?
            public const int Master = 0x0; // ?
            public const int Charmer = 0x0; // ?


            public const int DisplayId = 0x388; // Updated


            public const int CastID = 0x540; // Updated
            public const int ChanID = 0x598; // Updated


            public const int CastEnd = 0x594; // Updated
            public const int ChanEnd = 0x5A4; // Updated


            public const int CastStart = 0x590; // Updated
            public const int ChanStart = 0x5A0; // Updated


            public const int Info = 0x380; // tested
            public const int Type = 0x30; // 
            public const int Family = 0x34; //
            public const int Rank = 0x38; // 
            public const int GatherType = 0xE8; //
            public const int Name = 0xF8; // tested


            public const int UnitSkinningTypeOffset = 0xE8;
            public const int UnitSkinableHerb = 0x100;
            public const int UnitSkinableRock = 0x200;
            public const int UnitSkinableBolts = 0x8000;

            public const int UnitFlag1 = 0xD5F0; // Updated
            public const int UnitFlag2 = 0xD5F4; // Updated
            public const int UnitFlag3 = 0xD5F8; // Updated
            public const int DynamicFlag = 0xDC; // Updated


            public const int Summoner = 0xD540; // Updated => ?
            public const int Creator = 0xD560; // Updated => ?
            public const int NpcFlag = 0xD500; // Updated => ?
            public const int Faction = 0xD5EC; // Updated => ?


            public const int Level = 0xD5C8; // tested  player
            public const int Health = 0xD3A8; // tested player
            public const int MaxHealth = 0xD4D0; // tested player


            public const int Sex = 0xD5BF; // tested player
            public const int Race = 0xD5BC; // tested player 
            public const int Class = 0xD5BD; // tested player


            public const int Target = 0xD588; // tested player
            public const int MountID = 0xD618; // Updated


            public const int Movement = 0xF0; // tested player
            public const int Location = 0x20; // tested  player correct; [[entptr+Movement]+Location]:X [[entptr+Movement]+Location + 4]:Y [[entptr+Movement]+Location+ 8]:Z
            public const int RotationD = 0x2C; // tested player
            public const int RotationF = 0x30; // tested player
            public const int Pitch = 0x34; // tested    player
            public const int MoveFlag = 0x58; // tested player
            public const int UnitCurrentSpeed = 0xA4;
            public const int UnitWalkSpeed = 0xA8;
            public const int UnitRunSpeed = 0xAC;
            public const int UnitFlightSpeed = 0xBC;
            public const int UnitSwimSpeed = 0xB4;


            public const int MaxPower1 = 0xD7FC; // Updated player
            public const int MaxPower2 = 0xD800; // Updated player
            public const int MaxPower3 = 0xD804; // Updated
            public const int MaxPower4 = 0xD808; // Updated
            public const int MaxPower5 = 0xD80C; // Updated
            public const int MaxPower6 = 0xD810; // Updated
            public const int MaxPower7 = 0xD814; // Updated


            public const int Power1 = 0xD7E0; // Updated
            public const int Power2 = 0xD7E4; // Updated
            public const int Power3 = 0xD7E8; // Updated
            public const int Power4 = 0xD7EC; // Updated
            public const int Power5 = 0xD7F0; // Updated 
            public const int Power6 = 0xD7F4; // Updated
            public const int Power7 = 0xD7F8; // Updated
        }


        public class Player
        {
            public const int LootTarget = 0x0; // Not Used
            public const int Flags1 = 0xDB78; // Not Used
            public const int Flags2 = 0xDB7C; // Not Used
        }


        public class LocalPlayer
        {
            public const int Flags = 0x0; // Not Used

            public const int LootTargetGuid = 0xDB90;
            public const int PlayerFlags = 0xDBA0;
            public const int PlayerFlagsEx = 0xDBA4;
            public const int MapId = 0x160;
            public const int ComboTarget = 0xD798; // Updated
            public const int Money = 0xE3B0; // Updated
            public const int Inventory = 0xF5C8; // Updated
            public const int Experience = 0xE3B8; // Updated
            public const int NextLevelXP = 0xE3BC; // Updated
        }




        public class Item
        {
            public const int Creator = 0x0; // ?
            public const int Expiration = 0x0; // ?


            public const int ID = 0x150; // Updated
            public const int Flag = 0x1F8; // Out-dated
            public const int Owner = 0x190; // Updated
            public const int Container = 0x1A0; // Updated
            public const int StackCount = 0x1D0; // Updated


            public const int Enchant = 0x2A0; // Updated
            public const int Enchant_ID = 0x30; // Updated
            public const int Enchant_Expire = 0x34; // Updated


            public const int Durability = 0x1E4; // Updated
            public const int MaxDurability = 0x1E8; // Updated
        }


        public class Container
        {
            public const int Count = 0x430; // Out-dated
            public const int Items = 0x438; // Out-dated
        }
        public class Object
        {
            public const int Name1 = 0x148; // Updated
            public const int Name2 = 0xE0; // Updated


            public const int Flags = 0xA0; // Out-dated
            public const int Creator = 0x210; // Out-dated
            public const int ObjectID = 0xD0; // Out-dated
            public const int Location = 0x108; // Out-dated
        }


        public class Aura
        {
            public const int Size = 0xB0; // Updated
            public const int Table1 = 0x6A0; // Updated
            public const int Table2 = 0x6A8; // Updated
        }
    }
}
