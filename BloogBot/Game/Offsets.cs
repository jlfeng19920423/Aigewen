using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BloogBot.Game
{
    public class Offsets
    {

        public class Guids
        {

            public const int Mouseover_Guid = 0x30701E8;

            public const int Pet_Guid = 0x30A40E8;

            public const int Player_Guid = 0x2FBECD0;

            public const int Target_Guid = 0x2D4DC60;

            public const int Last_Target_Guid = 0x2D4DC70;

            public const int Last_Enemy_Guid = 0x2D4DC80;

            public const int Last_Friendly_Guid = 0x2D4DC90;

            public const int Focus_Guid = 0x2D4DCA0;

            public const int DialogWindowOwner_Guid = 0x2D4DCB0;

            public const int Bag_Guid = 0x30BF5B0;
        }

        public class Global_Data
        {

            public const int In_Game_Status = 0x3070194;

            public const int Player_Name = 0x2FBECE8;

            public const int Corpse_Position = 0x2C055A0;

            public const int Last_Message = 0x306EFC0;

            public const int Loot_Window = 0x30A4278;
        }

        public class Quests
        {

            public const int Base = 0x30B4620;

            public const int NumQuests = 0x30B4460;

            public const int CurrentQuest = 0x30DDE64;

            public const int QuestTitle = 0x30E8FB0;

            public const int GossipQuests = 0x30AE808;

            public const int NumQuestChoices = 0x30ED460;

            public const int QuestReward = 0x30ED468;
        }

        public class Auto_Loot
        {

            public const int Base = 0x306FCE0;

            public const int Offset = 0x4C;
        }

        public class Click_To_Move
        {

            public const int Base = 0x306FC98;

            public const int Offset = 0x4C;
        }

        public class Chat
        {

            public const int Open = 0x2F54144;

            public const int Start = 0x3070870;

            public const int Offset = 0xCB8;

            public const int Message = 0xE6;
        }

        public class Key_Bindings
        {

            public const int Base = 0x2FBDF58;
        }

        public class Add_On
        {

            public const int Count = 0x30F2AC8;

            public const int List = 0x30F3010;
        }

        public class Spellbooks
        {

            public const int Count = 0x30A37A0;

            public const int Base = 0x30A37A8;

            public const int PetBase = 0x30A37D0;

            public const int PetCount = 0x30A37C8;
        }

        public class Object_Manager
        {

            public const int Zone_ID = 0x306FBC8;

            public const int Names = 0x2CF5A20;

            public const int Base = 0x306EEC0;

            public const int Cooldown = 0x300A090;
        }

        public class Battlegrounds
        {

            public const int Finished = 0x30A6174;

            public const int Winner = 0x30A6178;

            public const int Info = 0x2C0F288;
        }

        public class Camera
        {

            public const int Base = 0x2F9B198;

            public const int Offset = 0x38E8;
        }

        public class Misc_Junk
        {

            public const int AddOnsLoaded = 0x30F2AF8;

            //public const int dword-2BE7798 = 0x2D4B370;
        
            public const int Frame_Base = 0x2D4B370;

            public const int Unk1 = 0x905A51;

            public const int Macro_Manager = 0x2C120F8;

            public const int Addon_Count = 0x30F2AC8;

            public const int Addon_Collection = 0x2C17EB0;

            public const int CGParyInfoGetActiveParty = 0x30A4010;

            public const int Power_Table = 0x3ACC873;

            public const int Unknown = 0x30EDD0F;

            public const int Unknown2 = 0x2F54750;

            public const int CombatLogEvents = 0x2C00D00; // Should be good

            public const int CombatLogEventType = 0x3FD9235; // probably wrong

            public const int CombatLogMissType = 0x4137524; // probably wrong

            public const int CombatLogEnvironmentType = 0x3C8F986; // probably wrong

            public const int ScreenSize = 0x2BAFB9C;

            public const int ScreenSize2 = 0x2BAFBA0;
        }

        public static IntPtr MemBase = Process.GetCurrentProcess().MainModule.BaseAddress;

        public const int array = 0x8;
        public const int objGuid = 0x18;
        public const int entptr = 0x18;
        public const int objType = 0x10;
        public const int CorpsePositionOffset = 0x40;
        public const int ChatHistoryNextOffset = 0xCB8;
        public const int ChatHistorySenderGuid = 0x0;  //checked
        public const int ChatHistorySenderName = 0x34; //checked
        public const int ChatHistoryFullMessage = 0xE6; //checked
        public const int ChatHistoryChatType = 0xCA0;  //checked
        public const int ChatHistoryChannelNum = 0xCA4;

        public static int FuncAccessAuraoffset = 0x134D9C0;
        public static int FuncAccessHealth = 0x14293A0;
        public static int ClntObjMgrEnumVisibleObjects = 0x15B24A0;
        public static int GameObjectIsLocked = 0x149CD30;
        public static int GameObjectCanUse = 0x1494030;
        public static int GameObjectCanUseNow = 0x1494450;
        public static int UnitCanAttack = 0x1405450;
        public static int UnitFlight = 0x142E5B0;
        public static int UnitSwimStart = 0x1432250;
        public static int UnitJump = 0x142E610;
        public static int UnitDismount = 0x14155F0;
        public static int UnitIsOutdoors = 0x1424E20;
        public static int ItemGetName = 0x14706B0;
        public static int ItemUse = 0x14771A0;
        public static int ItemGetUseSpell = 0x15E9CB0;
        public static int ItemGetCooldown = 0x135B4B0;
        public static int ItemSell = 0x13AAD20;
        public static int ItemIsBinded = 0x1474510;
        public static int ItemIsBindedForTrade = 0x1474650;
        public static int ItemAutoEquipToSlot = 0x13BFAF0;
        public static int PlayerSwapItem = 0x13CD7D0;
        public static int ClickToMoveFacing = 0x1439EE0;
        public static int ClickToMoveObjectInteract = 0x1439FC0;
        public static int ClickToMoveMoveTo = 0x143A160;
        public static int ClickToMoveTargetTo = 0x143A200;
        public static int ResetControl = 0x116E590;
        public static int SetControlBit = 0x116EF30;
        public static int UnSetControlBit = 0x11702C0;
        public static int MovementCancelSpells = 0x1354A50;
        public static int UpdatePlayer = 0x1170A00;
        public static int GameUIClearCursor = 0x15E28D0;
        public static int GameUIOnSpriteLeftClick = 0x15F3D60;
        public static int GameUIOnSpriteRightClick = 0x15F4190;
        public static int GameUIClearInteractTarget = 0x15E3D60;
        public static int GameUITarget = 0x5D1000;
        public static int GameUISetCursorItem = 0x15FC8E0;
        public static int IsSpellKnown = 0x1661AB0;
        public static int SpellBookCastSpell = 0x1658D10;
        public static int FindSlotBySpellId = 0x165ADA0;
        public static int CastSpellBySlot = 0x1658D10;
        public static int IsCurrentSpell = 0x1367AC0;
        public static int SpellBookFindSpellByName = 0x165AFB0;
        public static int SpellIsUsableAction = 0x13680F0;
        public static int SpellHavePower = 0x1363860;
        public static int SpellCastSpell = 0x1356670;
        public static int SpellGetMinMaxRange = 0x135CF40;
        public static int SpellRangeCheck = 0x136A690;
        public static int IsSpellCoolDown = 0x135F5F0;
        public static int CancelActiveSpell = 0x1354220;
        public static int SpellCancelChannel = 0x1354B80;
        public static int GetCastingSpellCast = 0x138C3D0;
        public static int RepopRequest = 0x13AACE0;
        public static int ResurrectRequest = 0x139A8D0;
        public static int MacroSetBody = 0x1785260;
        public static int ClearAFK = 0x13A01F0;
        public static int Script_RetrieveCorpse = 0x15DE9F0;
        public static int Script_AcceptGroup = 0x1686310;
        public static int Script_BuyMerchantItem = 0x1737DF0;
        public static int Script_CloseLoot = 0x16AA590;
        public static int Script_CanLootUnit = 0x15C4B30;
        public static int Script_LootSlot = 0x16AA430;
        public static int Script_LootSlotHasItem = 0x16AA2F0;
        public static int Script_GetNumLootItems = 0x16A9C90;
        public static int Script_SendChatMessage = 0x1604AE0;
        public static int Script_SendSystemMessage = 0x160F2A0;
        //offets
        public static int CurMgrPointer = 0x306EEC0;
        public static int InputControlPointer = 0x2BEB3E0;
        public static int PetGuid = 0x30A40E8;
        public static int PlayerGuid = 0x2FBECD0;
        public static int TargetGuid = 0x2D4DC60;
        public static int LastTargetGuid = 0x2D4DC70;
        public static int LastEnermyGuid = 0x2D4DC80;
        public static int FocusGuid = 0x2D4DCA0;
        public static int DialogWindowOwnerGuid = 0x2D4DCB0;
        public static int BagGuid = 0x30BF5B0;
        public static int CorpseGuid = 0x2C05578;
        public static int CorpseCorpseMapId = 0x2C05560;
        public static int LootWindows = 0x30A4278;
        public static int InWorld = 0x3070194;
        public static int IsLootWindowOpen = 0x30A4250;
        public static int LootInfoLoot = 0x30A4280;
        public static int LootInfoLootPending = 0x30A4279;
        public static int EvenTime = 0x2D4B378;
        public static int CurrentTimeMessage = 0x2D4DB60;
        public static int ChatHistoryInitialized = 0x30A03B0;
        public static int ChatHistory = 0x3070870;
        public static int ChatHistoryCurrentIndex = 0x307086C;
    }
}
