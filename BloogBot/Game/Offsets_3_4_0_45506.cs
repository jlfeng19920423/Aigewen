using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloogBot.Game
{
    internal class Offsets_3_4_0_45506
    {
        /// <summary>
        /// offset from oiramario
        /// </summary>
        public static int GameBuild = 0x264485C;    //search string "aWowVersion"
        public static int GameVersion = 0x25F5BA4;  //search string "aWowVersion"
        public static int GameReleaseDate = 0x2644850;

        public static int ServerTime = 0x11BFE30; // unknown  //48 83 EC 28 E8 ? ? ? ? 48 03 05 ? ? ? ? 48 83 C4 28 
        public static int EventTime = 0x2D61DF8;    // unknown
        public static int CurrentTimeMs = 0x2D645E0;
        public static int AreaSpiritHealerTime = 0x3086620;
        public static int CorpseReclaimDelay = 0x308662C;   //unknown
        public static int FrameRate = 0xF57EE0; // unknown     //48 83 EC 28 48 8D 0D ? ? ? ? E8 ? ? ? ? 0F 28 C8 F3 0F 10 05 ? ? ? ? 

        public static int CharacterName = 0x2FD5778;    // data offset
        public static int CliServicesCurrConnection = 0x2FD5280;
        public static int RealmNameOffset = 0x420;
        public static int GuildGuid = 0xD6D0;
        public static int PlayerCliDeclineGuildInvites = 0x734A80; // int64 (PlayerCliDeclineGuildInvites*) //48 8D 05 ? ? ? ? C7 41 ? ? ? ? ? 48 89 01 48 8B C1 
        public static int ClearAFK = 0x139CB50; // void (CGPlayer* localPlayer, int32 afk/*=0*/)    //48 81 EC ? ? ? ? 83 3D ? ? ? ? ? 74 67 85 D2 75 0C 48 8B 05 ? ? ? ? 
        public static int ChatAFK = 0x30872B0;
        public static int ChangeStandState = 0x139C6C0; // int64 (CGPlayer* unit, uint32 sit)   //48 89 5C 24 ? 57 48 83 EC 50 0F B6 41 10 48 8B D9 48 8D 0D ? ? ? ?
        public static int LocalPlayerFlags = 0xF250;
        public static int RepopRequest = 0x13A7680; // void (CGPlayer* localPlayer, bool checkInstance/*=0*/)   //40 53 48 83 EC 50 48 8B 01 0F B6 DA 
        public static int ResurrectRequest = 0x1399D20; // void (CGPlayer* localPlayer, int32 unk/*=0*/)    //40 53 48 83 EC 70 48 8B 05 ? ? ? ? 8B DA 48 C1 E8 3A 84 C0 
        public static int PlayerCliReclaimCorpse = 0x736570; // int64 (PlayerCliReclaimCorpse *this)
        public static int CorpseGuid = 0x2C1C578;
        public static int SendClientServices = 0x11C5AE0; // int64 (ClientServices *this)
        public static int ConfirmBinder = 0x13B1FB0; // void (CGPlayer* localPlayer, WowGuid* target/*=[0,0]*/)
        public static int BindAreaID = 0x302B0C8;
        public static int PlayerCliGuildBankDepositMoney = 0x7352C0; // int64 (PlayerCliGuildBankDepositMoney*)
        public static int GuildBankInfoBanker = 0x30E5A48;
        public static int ShowHelm = 0x13B03A0; // void (CGPlayer* localPlayer)
        public static int HideHelm = 0x13A7A80; // void (CGPlayer* localPlayer)
        public static int ShowCloak = 0x13B02F0; // void (CGPlayer* localPlayer)
        public static int HideCloak = 0x13A7A10; // void (CGPlayer* localPlayer)
        public static int PickupEquipItem = 0x173EF20; // int64 (uint32)
        public static int LootInfoLoot = 0x30BACD0;
        public static int LootInfoLootPending = 0x30BACC9;
        public static int LootInfoCountOffset1 = 0x1688;
        public static int LootInfoCountOffset2 = 0xA08;

        public static int LocalPlayerProfession1 = 0x10860;
        public static int LocalPlayerProfession2 = 0x10864;
        public static int SkillIndex = 0xE3C4;
        public static int SkillNumIndexMax = 0x100;
        public static int SkillCurr = 0xE7C4;
        public static int SkillMax = 0xEBC4;
        public static int SkillEnchant = 0xEDC4;
        public static int SkillTalent = 0xEFC4;
        public static int SpellBookNumSkillLines = 0x30BA0C0;
        public static int SpellBookSkillLines = 0x30BA238;

        public static int EquippedBagGUID = 0x30D5560;
        public static int ContainerNumSlots = 0x670;
        public static int ContainerSlots = 0x418;
        public static int LocalPlayerNumBackpackSlots = 0xF2AE;
        public static int LocalPlayerInvSlots = 0x12460;
        public static int LocalPlayerCoinage = 0xE3B0;
        public static int LocalPlayerExperience = 0xE3B8;
        public static int LocalPlayerNextLevelXP = 0xE3BC;

        public static int FunAccessInputControlPointer = 0x109E270;    //unknown    //40 53 48 83 EC 20 48 8B D9 48 8D 0D ? ? ? ? E8 ? ? ? ? 84 C0 74 0D 
        public static int InputControlPointer = 0x2C023D0;
        public static int InputControlOffset = 0x4;
        public static int IsMovingMask1 = 0x1E010F0;
        public static int IsMovingMask2 = 0x302;
        public static int IsMovingMask3 = 0x1;

        public static int FunAccessCurMgrPointer = 0x15AEC30; // unknown //48 83 EC 28 48 8B 05 ? ? ? ? 48 85 C0 0F 84 ? ? ? ? 48 89 5C 24 ? 48 8B 58 08
        public static int CurMgrPointer = 0x3085910;
        public static int FirstEntry = 0x120;
        public static int ObjectOffset = 0x68;
        public static int ObjectType = 0x10;
        public static int ObjectGuid = 0x18;
        public static int MapId = 0x160;
        public static int GetBaseFromToken = 0x19856B0; // int64 (char* token, uint32 notUnit/*=0*/)
        public static int GlobalMovement = 0x170;

        public static int CameraMgr = 0x2FB1C28;
        public static int CameraOffset = 0x38E8;
        public static int CameraPosition = 0x10;
        public static int CameraMatrix3x3 = 0x1C;
        public static int CameraFOV = 0x40;
        public static int CameraZoom = 0x23C;

        public static int InWorld = 0x3086BE4;
        public static int IsInside = 0x301E2A5;
        public static int LoadingScreen = 0x2CFBAE0;
        public static int ZoneText = 0x3085968;
        public static int SubZoneText = 0x3085970;
        public static int ZoneId = 0x30865C8;
        public static int SubZoneId = 0x30865CC;
        public static int WorldFrameOnLayerUpdate = 0x10A1DF0; // int64 (CGWorldFrame*, int64 unk0, int64 unk1, int64 unk2, int64 unk3)

        public static int LastErrorString = 0x3085A10;
        public static int GetScriptEventName = 0x846EAC; // char* (uint64 eventId)

        public static int CorpseMapId = 0x2C1C560;
        public static int CorpseActureMapId = 0x2C1C564;
        public static int CorpsePositionX = 0x2C1C5A0;
        public static int CorpsePositionY = 0x2C1C5A4;
        public static int CorpsePositionZ = 0x2C1C5A8;

        public static int ObjectBoundingRadius = 0xD604;
        public static int ObjectCombatReach = 0xD608;
        public static int ObjectDynamicFlags = 0xDC;
        public static int ObjectEntryID = 0xD8;

        public static int DBCacheName = 0x2D0C690;

        public static int GameObjectLocation = 0x108;
        public static int GameObjectFishingBobberFlag = 0xA0;
        public static int GameObjectNamePointer = 0x148;
        public static int GameObjectName = 0xE0;
        public static int GameObjectState = 0x1A8;
        public static int GameObjectFlags = 0x230;
        public static int GameObjectCreator = 0x210;
        public static int GameObjectIsLocked = 0x1499A40; // bool (CGGameObject* go, int64/*=0*/, int64/*=0*/, int64/*=0*/, int64/*=0*/, int64/*=0*/)   //48 89 5C 24 ? 4C 89 44 24 ? 48 89 54 24 ? 48 89 4C 24 ? 55 56 57 41 54 41 55 41 56 41 57 48 81 EC ? ? ? ? 4D 8B E9 48 8B D9 E8 ? ? ? ? 48 89 44 24 ? 48 8B F8 48 85 C0 0F 84 ? ? ? ?
        public static int GameObjectCanUse = 0x1490D40; // int64 (CGGameObject* go) //48 8B 89 ? ? ? ? 48 8B 01 48 FF 60 40 
        public static int GameObjectCanUseNow = 0x1491160; // int64 (CGGameObject* go)  //48 8B 89 ? ? ? ? 45 33 C9 45 33 C0 33 D2 48 8B 01 48 FF 60 48 

        public static int UnitDataPointer = 0x380;
        public static int UnitName = 0xF8;
        public static int UnitSkinningTypeOffset = 0xE8;
        public static int UnitSkinableHerb = 0x100;
        public static int UnitSkinableRock = 0x200;
        public static int UnitSkinableBolts = 0x8000;
        public static int UnitHealth = 0xD4C8;
        public static int UnitMaxHealth = 0xD4D0;
        public static int UnitFlags1 = 0xD5F0;
        public static int UnitFlags2 = 0xD5F4;
        public static int UnitFlags3 = 0xD5F8;
        public static int UnitPowerType = 0xD5C0;
        public static int UnitPower1 = 0xD3B0;
        public static int UnitPower2 = 0xD3B4;
        public static int UnitPower3 = 0xD3B8;
        public static int UnitMaxPower = 0xD7FC;
        public static int UnitLocation = 0x20;
        public static int UnitYaw = 0x30;
        public static int UnitPitch = 0x34;
        public static int UnitLevel = 0xD5C8;
        public static int UnitMountDisplayID = 0x418;
        public static int UnitTarget = 0xD588;
        public static int UnitChannelID = 0x5B8;
        public static int UnitCastStartTime = 0x5B0;
        public static int UnitCastEndTime = 0x5B4;
        public static int UnitChannelCastStartTime = 0x5C0;
        public static int UnitChannelCastEndTime = 0x5C4;
        public static int UnitNpcFlags = 0xD500;
        public static int UnitRace = 0xD5BC;
        public static int UnitFactionTemplate = 0xD5EC;
        public static int UnitSummonedBy = 0xD538;
        public static int UnitCreatedBy = 0xD558;
        public static int UnitSex = 0xD5BF;
        public static int UnitClass = 0xD5BD;
        public static int UnitCreatureClassification = 0x38;
        public static int UnitCreatureFamily = 0x34;
        public static int UnitCreatureType = 0x30;
        public static int UnitMovementPointer = 0xF0;
        public static int UnitCurrentSpeed = 0xA4;
        public static int UnitWalkSpeed = 0xA8;
        public static int UnitRunSpeed = 0xAC;
        public static int UnitFlightSpeed = 0xBC;
        public static int UnitSwimSpeed = 0xB4;
        public static int UnitMovementFlags = 0x58;
        public static int UnitCanAttack = 0x1401E00; // bool (CGObject* player, CGUnit* unit, bool isPet)   //48 89 5C 24 ? 48 89 6C 24 ? 56 57 41 56 48 83 EC 20 0F B6 41 10 4C 8D 35 ? ? ? ? 45 0F B6 D0 48 8B DA
        public static int UnitCastingInfo = 0x568;
        public static int UnitFlight = 0x142AFB0; // int64 (CGUnit* unit, int32 eventTime, int32 takeoff/*=1*/) //48 89 5C 24 ? 57 48 83 EC 20 45 85 C0 41 8B D8 8B FA 41 0F 95 C0
        public static int UnitSwimStart = 0x142EC50; // int64 (CGUnit* unit, int32 eventTime)   //40 53 48 83 EC 20 48 81 C1 ? ? ? ? 8B DA E8 ? ? ? ? B9 ? ? ? ? E8 ? ? ? ? 89 1D ? ? ? ? 48 83 C4 20 5B C3
        public static int UnitJump = 0x142B010; // int64 (CGUnit* unit, int32 eventTime)    //48 89 5C 24 ? 48 89 6C 24 ? 48 89 74 24 ? 57 48 83 EC 70 48 8B D9 8B EA 48 8B 89 ? ? ? ? 48 8B 01 48 39 05 ? ? ? ?
        public static int UnitDismount = 0x1411F50; // int64 (CGUnit* unit) //40 53 48 83 EC 50 0F B6 41 10 48 8B D9 48 8D 0D ? ? ? ? F6 04 81 80 74 2B
        public static int UnitIsOutdoors = 0x1421940; // bool (CGUnit* unit)    //48 83 EC 28 48 8B 49 08 48 85 C9 74 18 48 8B 01 FF 50 40 48 8B C8 E8 ? ? ? ? 85 C0 0F 95 C0 48 83 C4 28

        public static int PlayerFlags = 0xDBA0;
        public static int PlayerFlagsEx = 0xDBA4;

        public static int ItemGetName = 0x146D3C0; // int64 (CGItem* item, char* nameBuff, uint32 size/*=256*/) //48 89 5C 24 ? 48 89 74 24 ? 57 48 81 EC ? ? ? ? 48 8B D9 41 8B F0 48 81 C1 ? ? ? ?
        public static int ItemStackCount = 0x1D0;
        public static int ItemFlags = 0x1D8;
        public static int ItemDurability = 0x1E4;
        public static int ItemMaxDurability = 0x1E8;
        public static int ItemEnchantment = 0x2DC;
        public static int ItemEnchantmentOffset = 0xC;
        public static int ItemContainedIn = 0x1A0;
        public static int ItemOwner = 0x190;
        public static int ItemUseParam = 0x3020AE0;
        public static int ItemUse = 0x1473EB0; // bool (CGItem* item, WowGuid* target/*=[0,0]*/, bool skipConfirm/*=false*/, int64* unk_0/*=0*/)    //4C 89 4C 24 ? 44 89 44 24 ? 48 89 54 24 ? 55 53 56 57 41 54 41 55 41 56 41 57
        public static int ItemGetUseSpell = 0x15EA2E0; // int64 (uint32_t itemId)   //40 53 48 83 EC 20 8B D9 E8 ? ? ? ? 48 85 C0 74 0F 33 D2 48 8B C8 48 83 C4 20 5B
        public static int ItemGetCooldown = 0x13586B0; // bool (CGItem* item, int32& startime, int32& duration, int64 unk_0/*=0*/, int64 unk_1/*=0*/)   //48 89 5C 24 ? 48 89 6C 24 ? 48 89 74 24 ? 57 48 83 EC 50 49 8B F9 49 8B F0 48 8B EA 48 8B D9 48 85 C9 75 04 32 C0 EB 49 
        public static int ItemSell = 0x13A76C0; // bool (Merchant* merchant, WowGuid& itemGuid, int64 unk_0/*=0*/)  //48 89 5C 24 ? 48 89 6C 24 ? 48 89 7C 24 ? 41 56 48 81 EC ? ? ? ? 48 8B FA
        public static int ItemIsBinded = 0x1471220; // bool (CGItem* item)  //40 53 48 81 EC ? ? ? ? F6 81 ? ? ? ? ? 48 8B D9 0F 85 ? ? ? ? E8 ? ? ? ?
        public static int ItemIsBindedForTrade = 0x1471360; // bool (CGItem* item)  //40 53 48 83 EC 20 48 8B D9 E8 ? ? ? ? 84 C0 74 55 48 8D 8B ? ? ? ?
        public static int ItemAutoEquipToSlot = 0x13BC490; // void (CGPlayer* localPlayer, CGItem* item, int32 slot, uint32 unk_0/*=0*/)    //48 85 D2 0F 84 ? ? ? ? 48 89 5C 24 ? 48 89 74 24 ? 48 89 7C 24 ? 41 56 48 81 EC ? ? ? ?
        public static int GameUICursorItem = 0x3085998;
        public static int GameUICursorItemContainer = 0x30859A8;
        public static int GameUICursorItemSlot = 0x30859B8;
        public static int PlayerSwapItem = 0x13CA2A0; // int64 (CGPlayer* localPlayer, int64 cursorItem, int64 cursorItemContainer, uint32 cursorItemSlot, GUID& targetItemGuid, uint32_t bagSlot, int32_t unk0/*=0*/)  //4C 8B DC 4D 89 43 18 55 57 41 54 41 55 49 8D 6B B9

        public static int AuraCount = 0x6A0;
        public static int AuraTable = 0x6A8;
        public static int AuraSize = 0xB0;
        public static int AuraCaster = 0x68;
        public static int AuraSpellId = 0x88;
        public static int AuraFlags = 0x90;
        public static int AuraStackCount = 0x91;
        public static int AuraDuration = 0x98;
        public static int AuraExpiration = 0x9C;
        public static int AuraEntrySize = 0xA0;

        public static int ClickToMoveFacing = 0x14369E0; // void (CGObject* localPlayer, float facing)  //48 83 EC 58 48 83 B9 ? ? ? ? ? 7E 72 48 8B 91 ? ? ? ? 48 8B 02 48 39 05 ? ? ? ? 75 5F 48 8B 42 08
        public static int ClickToMoveObjectInteract = 0x1436AD0; // bool (CGObject* localPlayer, WowGuid* unitGuid) //48 89 5C 24 ? 56 48 83 EC 50 48 83 B9 ? ? ? ? ? 48 8B F2 0F 29 74 24 ? 48 8B D9 0F 28 F2
        public static int ClickToMoveMoveTo = 0x1436C30; // void (CGObject* localPlayer, Vector3& clickPos) //48 83 EC ? 48 83 B9 ? ? ? ? ? 7E ? 4C 8B 81 ? ? ? ? 49 8B ?
        public static int ClickToMoveTargetTo = 0x1436CE0; // bool (CGObject* localPlayer, WowGuid* unitGuid, int32 clickType)  // 40 53 55 56 57 41 54 41 56 41 57 48 83 EC 70 48 8B 02 41 8B E8 4C 8B F2 48 8B F9 48 39 41 18

        public static int MoveOpActionCode = 0x0;
        public static int ResetControl = 0x11711B0; // int64 (CGInputControl* inputCtrl)    //40 53 57 41 57 48 83 EC 30 48 8B F9 E8 ? ? ? ? 45 33 C9
        public static int SetControlBit = 0x1171B70; // int64 (CGInputControl* inputCtrl, int32 bits)   //40 53 56 48 83 EC 68 80 B9 ? ? ? ? ? 8B F2 8B 41 04 48 8B D9 89 44 24 28 74 46
        public static int UnSetControlBit = 0x1172F00; // int64 (CGInputControl* inputCtrl, int32 bits, int32 eventTime, uint8 unk0/*=0*/, int8 unk1/*=0*/) //44 89 44 24 ? 53 56 41 54 41 55 48 83 EC 58 80 B9 ? ? ? ? ?
        public static int MovementCancelSpells = 0x1351C50; // int64 (CGObject* player) //48 89 5C 24 ? 48 89 6C 24 ? 57 48 83 EC 20 48 8B F9 E8 ? ? ? ? 8B E8
        public static int UpdatePlayer = 0x1173640; // int64 (CGInputControl* inputCtrl, int32 eventTime, uint32 unk0/*=1*/, uint32 unk1/*=1*/) //48 89 6C 24 ? 48 89 74 24 ? 57 41 56 41 57 48 83 EC 30 45 8B F9 45 8B F0 8B EA 48 8B F9 E8 ? ? ? ?
        public static int ActiveMover = 0x2F67A68;
        public static int ActiveMoverOffset = 0x160;

        public static int GameUIClearCursor = 0x15E2F20; // int64 (int32 unk0/*=1*/, int32 unk1/*=1*/)  //48 8B C4 55 48 8D A8 ? ? ? ? 48 81 EC ? ? ? ? 48 89 58 08 8B D9 48 89 78 18 8B FA
        public static int GameUIOnSpriteLeftClick = 0x15F3EB0;  // int64 (WowGuid* unitGuid) //48 89 5C 24 ? 56 57 41 57 48 83 EC 40 48 8B F1 
        public static int GameUIOnSpriteRightClick = 0x15F42E0; // int64 (WowGuid* unitGuid) //40 57 48 83 EC 20 48 8B F9 E8 ? ? ? ? 48 85 C0 75 0B B8 ? ? ? ? 48 83 C4 20 
        public static int GameUIClearInteractTarget = 0x15E43B0; // int64 (WowGuid* unitGuid, bool unk/*=0*/)   //48 89 5C 24 ? 48 89 7C 24 ? 55 48 8B EC 48 83 EC 70 48 8B 41 08 0F B6 FA 48 C1 E8 3A
        public static int GameUILockedTarget = 0x2D646D8;
        public static int GameUITarget = 0x5CB400; // int64 (int64 guitarget, WowGuid* unitGuid, int unk/*=0*/) //40 55 53 57 41 54 41 55 48 8D AC 24 ? ? ? ? 48 81 EC ? ? ? ? 45 8B E0
        public static int GameUISetCursorItem = 0x15FCA30; // int64 (WowGuid* itemGuid, WowGuid* containerGuid, int64 itemSlot, int64 unk0/*=1*/, int32 stackSplit/*=0*/, int32 playSnd/*=1*/)  //48 89 5C 24 ? 48 89 6C 24 ? 48 89 7C 24 ? 41 56 48 83 EC 50 41 8B D9 41 8B E8 4C 8B F2 48 8B F9
        public static int GameUILockItem = 0x15F1760; // int64 (WowGuid* itemGuid, uint16 lock/*=1*/)
        public static int GameUICanPerformAction = 0x16A5250; // bool (uint32 act)
        public static int DestroyItemActionCode = 0x3D;
        public static int PlayerCliDestroyItem = 0x6429D0; // int64 (PlayerCliDestroyItem*)
        public static int GameUISetCursorPetAction = 0x16A1270; // char* (uint32 act)
        public static int GameUIReload = 0x1684E20; // int64 ()
        public static int GameUIReloadRequested = 0x3085A02;

        public static int IsSpellKnown = 0x1662340; // bool (int32 spellId, bool isPet) //48 89 5C 24 ? 57 48 83 EC 20 0F B6 FA 8B D9 E8 ? ? ? ? 
        public static int SpellBookCastSpell = 0x16595A0; // int64 (int32 spellId, bool isPet, int64 target, uint8 unk_0/*=0*/, uint8 unk_1/*=0*/)  //48 89 5C 24 ? 48 89 6C 24 ? 48 89 74 24 ? 41 56 48 83 EC 50 41 0F B6 F1 49 8B E8 44 0F B6 F2 
        public static int FindSlotBySpellId = 0x165B630; // int64 (int32 spellId, bool isPet) //44 8B ? 85 C9 74 ? 84 D2 74 ?
        public static int CastSpellBySlot = 0x16595A0;   //int CastSpellBySlot(Int32 SpellSlot, IntPtr PlayerGuidPointer)   //48 89 5C 24 ? 48 89 6C 24 ? 48 89 74 24 ? 41 56 48 83 EC ? 41 0F B6 F1
        public static int IsCurrentSpell = 0x1365FF0; // bool (int32 spellId)   //48 89 5C 24 ? 48 89 6C 24 ? 56 57 41 54 41 56 41 57 48 81 EC ? ? ? ? 8B D9 E8 ? ? ? ? 48 8B 10 48 3B D0 74 0D 
        public static int SpellBookFindSpellByName = 0x165B840; // int64 (char* spellName, char* unk) //4C 8B DC 41 56 48 81 EC ? ? ? ? 4C 8B F2 48 85 C9 0F 84 ? ? ? ? 0F B6 01 
        public static int SpellIsUsableAction = 0x1366620; // bool (CGObject* localPlayer, uint32_t spellId, WowGuid* unitGuid, uint32_t entryId, uint8 unk_0/*=0*/, uint8 unk_1/*=0*/, uint8 unk_2/*=0*/, uint8 unk_3/*=0*/) //44 89 4C 24 ? 55 53 56 57 41 55 48 8B EC 48 81 EC ? ? ? ? 41 8B D9 4D 8B E8 8B F2 48 8B F9 48 85 C9
        public static int SpellHavePower = 0x1360A60; // bool (CGObject* localPlayer, uint32_t spellId, uint32_t& unk_0, uint32_t& unk_1) //4C 89 4C 24 ? 4C 89 44 24 ? 89 54 24 10 53 55 56 57 41 54 41 55 41 56 41 57 48 81 EC ? ? ? ?
        public static int SpellCastSpell = 0x1353870; // bool (uint32_t spellId, uint32 itemptr/*=0x3020ae0*/, int64 parameters/*=0*/, WowGuid* unitGuid) //48 89 5C 24 ? 48 89 6C 24 ? 48 89 74 24 ? 57 48 83 EC 30 49 8B F1 49 8B E8 48 8B DA 8B F9
        public static int SpellCastSpellItem = 0x3020AE0;
        public static int SpellGetMinMaxRange = 0x135A140; // void (CGUnit* unit, int32 spellId, float* minRange, float* maxRange, CGObject* assist/*=0*/   //48 89 5C 24 ? 48 89 6C 24 ? 48 89 74 24 ? 41 54 41 56 41 57 48 83 EC 40 4D 8B F1 4D 8B F8
        public static int SpellRangeCheck = 0x1368BC0; // bool (CGObject* player, int32 spellId, WowGuid* unitGuid, bool* inRange, uint8 unk_0/*=0*/, uint8 unk_1/*=0*/)    //4C 89 4C 24 ? 53 55 41 57 48 81 EC ? ? ? ? 49 8B 40 08 4D 8B D0 48 C1 E8 3A 44 8B FA 48 8B E9
        public static int TerrainClick = 0x13600C0; // bool (TerrainClickStruct* tc)
        public static int IsSpellCoolDown = 0x135C7F0; // bool (uint32 spellId, uint32 unk_0, int32 isPet, uint32* startTime, int32* duration, int32* enabled, int64 unk_1, char modrate, int64 unk_2)  //48 83 EC 58 44 8B D1 C6 44 24 ? ? 41 F7 D8 48 8D 05 ? ? ? ? 44 8B C2 41 8B D2 48 1B C9 81 E1 ? ? ? ?
        public static int CancelActiveSpell = 0x1351420; // void (int32 spellId, CGObject* localPlayer, int32 unk/*=-1*/)   //48 85 D2 0F 84 ? ? ? ? 4C 8B DC 45 89 43 18 55 57 41 57 49 8D 6B A1

        public static int CooldownHistory = 0x3020B00;
        public static int CooldownEntrySize = 0x40;
        public static int CooldownOffset = 0x10;
        public static int CooldownNext = 0x8;
        public static int CooldownSpellId = 0x10;
        public static int CooldownItemId = 0x14;
        public static int CooldownStartTime = 0x1C;
        public static int CooldownSpellCooldownDuration = 0x20;
        public static int CooldownSpellCategoryId = 0x24;
        public static int CooldownCategoryCooldownStartTime = 0x28;
        public static int CooldownCategoryCooldownDuration = 0x2C;
        public static int CooldownGCDStartTime = 0x34;
        public static int CooldownGCDCategoryId = 0x38;
        public static int CooldownGCDDuration = 0x3C;

        public static int WorldFrameHitTestPoint = 0x109FD40;
        public static int WorldFrameHitTest = 0x109F770;
        public static int WorldIntersect = 0xF588A0; // bool (CGWorld* activeScene, Vector3& from, Vector3& to, Vector3& result, float& distance, int64 flags)
        public static int WorldActiveSceneOffset = 0x350;
        public static int CollisionCheckFlags = 0x100111;

        public static int SpellCancelChannel = 0x1351D80; // bool (int32 spellId, int32 reason/*=40*/) //48 89 5C 24 ? 48 89 74 24 ? 57 48 83 EC 50 8B F2 8B D9 E8 ? ? ? ? BA ? ? ? ? 
        public static int CancelChannelArg = 0x28;
        public static int GetCastingSpellCast = 0x138A880; // void** (WowGuid **targetGuid, int32 spellId, WowGuid* unitGuid, bool unk/*=true*/)    //4C 8B C1 85 D2 74 64 48 8B 05 ? ? ? ? 4C 8D 1D ? ? ? ? 49 3B C3 74 1E 4C 8B 11 0F 1F 00 4C 3B 50 50
        public static int SpellCastArg1 = 0x540;
        public static int SpellCastArg2 = 0x530;
        public static int SpellDBGetRecord = 0x2240A00; // const SpellRec* (int32 spellId)
        public static int DBRecordArg1 = 0x48;
        public static int DBRecordArg2 = 0xA;
        public static int DBRecordArg3 = 0x40000000;
        public static int CheckSpellAttribute = 0x22409A0; // bool (const SpellRec* spellRec, SPELL_ATTRIBUTE_WORDS word/*=10*/, int32 arrFlag)
        public static int CancelSpell = 0x13521A0; // int64 (CGTradeSkillInfo* skillInfo, bool unk1/*=true*/, bool unk2/*=true*/, uint32 failedReason/*=30*/)
        public static int CancelArg = 0x20;
        public static int AutoRepeatSpell = 0x3020CBC;

        public static int GetDefaultLanguage = 0x13C2850; // uint64 (wowobject* local_player)
        public static int SendChatMessage = 0x1625C90; // bool (int chatType, int languageType, uint64 channelId, void* unk/*=0*/, const char* message/*max 255*/)
        public static int ChatHistoryInitialized = 0x30B6E00;
        public static int ChatHistory = 0x30872C0;
        public static int ChatHistoryCount = 0x3C;  //unknown
        public static int ChatHistoryCurrentIndex = 0x30872BC;  //total message count
        public static int ChatHistoryNextOffset = 0xCB8;
        public static int ChatHistorySenderGuid = 0x0;  //checked
        public static int ChatHistorySenderName = 0x34; //checked
        public static int ChatHistoryFullMessage = 0xE6; //checked
        public static int ChatHistoryChatType = 0xCA0;  //checked
        public static int ChatHistoryChannelNum = 0xCA4;

        public static int UIScreenWidth = 0x2BC6B9C;
        public static int UIScreenHeight = 0x2BC6BA0;
        public static int UIScreenAspect = 0x2BC6B98;
        public static int UIFrameBase = 0x2D61DF0;
        public static int UIFirstFrame = 0xF18;
        public static int UINextFrame = 0xF08;
        public static int UINextFrameOffset = 0x8;
        public static int UIFirstRegion = 0x2B0;
        public static int UINextRegion = 0x2A0;
        public static int UINextRegionOffset = 0x8;
        public static int UIVisible = 0x1CC;
        public static int UIVisibleShift = 0x1;
        public static int UIVisibleBit = 0x1;
        public static int UILableText = 0x288;
        public static int UIEffectiveScale = 0x1C0;
        public static int UIParentFrame = 0x1D0;
        public static int UIName = 0x20;
        public static int UIFrameBottom = 0x190;
        public static int UIFrameLeft = 0x194;
        public static int UIFrameTop = 0x198;
        public static int UIFrameRight = 0x19C;
        public static int UIButtonEnabledPointer = 0x320;
        public static int UIButtonEnabledMask = 0xF;
        public static int UICheckboxIsChecked = 0x370;
        public static int UIEditboxText = 0x348;
        public static int UIButtonClickVMT = 0x230; // void (CSimpleButton* button, const char* buttonType/*=LeftButton/RightButton*/, bool isClicked/*=true*/, uint64 unk_0/*=0*/)
        public static int UIButtonSetText = 0x22EE240; // void (CSimpleButton* button, const char* text)
        public static int UIEditBoxSetText = 0x22F8990; // void (CSimpleEditBox* button, const char* text, int64_t unk_0/*=lua_tainted*/, char unk_1/*=0*/)
        public static int LuaTainted = 0x2D007C0;

        public static int AuctionOwnerCount = 0x3108538;
        public static int AuctionOwnerList = 0x3108540;
        public static int AuctionBidderTotalCount = 0x31070CC;
        public static int AuctionBidderCount = 0x3108558;
        public static int AuctionBidderList = 0x3108560;
        public static int AuctionItemsTotalCount = 0x3106F88;
        public static int AuctionItemsCount = 0x3108518;
        public static int AuctionItemsList = 0x3108520;
        public static int AuctionEntrySize = 0x288;
        public static int AuctionEntryId = 0x8;
        public static int AuctionEntryCount = 0x228;
        public static int AuctionEntryMinBid = 0x248;
        public static int AuctionEntryBuyoutPrice = 0x258;
        public static int JamItemGetName = 0x146D500; // int64 (JamItemInstance* item, char* nameBuff, uint32 size/*=256*/)

        public static int TrainerFilterServiceType = 0x30D62B8;
        public static int TrainerFilterAvailable = 0x1;
        public static int TrainerFilterUnavailable = 0x2;
        public static int TrainerFilterUsed = 0x0;
        public static int TrainerFilterNumServices = 0x30D62B0;
        public static int TrainerBuyAllFilteredServices = 0x1835850; // int64 ()
        public static int TrainerBuyServices = 0x18358D0; // int64 (uint32 index)

        public static int MailNextTime = 0x2C27D80;
        public static int MailCommandPending = 0x30C9D72;
        public static int MailSendPending = 0x30C9D71;

        public static int MerchantItemCount = 0x30CA120;
        public static int MerchantPointer = 0x30CA108;
        public static int MerchantShift = 0x3A;
        public static int MerchantItems = 0x30CA118;
        public static int MerchantItemSize = 0xA8;
        public static int MerchantBuyItem = 0x13B4A20; // void (int64 merchant, int64 itemInfo, int32 stackCount, WowGuid* itemGuid, char unk/*=255*/)
        public static int DB2GetRecord = 0x48CC20; // int64 (DB2* dataBase, uint32 itemId, WowGuid* itemGuid, callback* funcCallback/*=0*/, int64 unk0/*=0*/, int unk1/*=0*/ )

        public static int CVarEnableOffset = 0x4C;
        public static int CVarLookupRegistered = 0x342920; // CVar* (const char* name)
        public static int CVarSetString = 0x343D20; // bool (CVar* cvar, const char* value, unsigned char setValue/*=1*/, unsigned char setReset/*=0*/, unsigned char setDefault/*=0*/, int8_t/*=1*/)
        public static int CVarSetInteger = 0x343CB0; // bool (CVar* cvar, uint32 value, unsigned char setValue/*=1*/, unsigned char setReset/*=0*/, unsigned char setDefault/*=0*/, int8_t/*=1*/)
        public static int CVarFloatOffset = 0x48;
        public static int CVarIntegerOffset = 0x4C;
        public static int GfxUpdateWindow = 0x34A1C0; // char ()

        public static int BattlefieldMaxQueued = 0x30BC3B8;
        public static int BattlefieldActiveID = 0x30BC3BC;
        public static int BattlefieldAccept = 0x7342F0; // int64 (int64)
        public static int BattlefieldCheck = 0x48;
        public static int BattlefieldGuid1 = 0x18;
        public static int BattlefieldGuid2 = 0x28;
        public static int BattlefieldLeave = 0x6414A0; // int64 (int64)
        public static int BattlefieldList = 0x2C26008;
        public static int BattlefieldListNext = 0x8;
        public static int BattlefieldListIndex = 0x10;
        public static int BattlefieldListLocked = 0x8C;
        public static int BattlefieldListError = 0x5C;
        public static int BattlefieldMapDone = 0x30BC6C4;
        public static int BattlefieldWinner = 0x30BC6C8;

        public static int PetSpellBookNumSpells = 0x30BA210;
        public static int PetSpellBookSpells = 0x30BA218;
        public static int PetSendAction = 0x16A3260; // int64 (int32* orderFlags, WowGuid* targetGuid, int32 unk_0/*=0*/, int64 unk_1/*=0x30BAB1C*/)
        public static int PetActionAddress = 0x3020AE0;
        public static int PetInfoPetMode = 0x30BAB1C;
        public static int PetInfoExpirationTime = 0x30BAB20;
        public static int PetInfoPetGUID = 0x30BAB38;
        public static int PetInfoPets = 0x30BAB48;
        public static int PetInfoCurrentPet = 0x30BAB50;
        public static int PetInfoActions = 0x30BAB68;
        public static int PetActionWait = 0x3800000;
        public static int PetActionFollow = 0x3800001;
        public static int PetActionAttack = 0x3800002;
        public static int PetActionMoveTo = 0x3800004;
        public static int PetActionPassiveMode = 0x3000000;
        public static int PetActionDefensiveMode = 0x3000001;
        public static int PetActionAggressiveMode = 0x3000002;

        public static int LoginState = 0x3020988;
        public static int LoginSuccessed = 0x3020951;
        public static int LoginSelectGameAccount = 0x10B9AC0; // int64 (int64)
        public static int LoginGetGlueMgr = 0x1318E30; // CGlueMgr* (int)
        public static int LoginGlueOffset = 0x2C15790;
        public static int LoginEnterWorld = 0x20F0C0; // char (CGlueMgr*)
        public static int LoginCharacterListIsReceived = 0x3020820;
        public static int LoginCharacterList = 0x3020840;
        public static int LoginCharacterSelectionIndex = 0x2C15790;
        public static int LoginCharacterShow = 0x131AC30; // int64 (uint32 unk_0/*=0*/, int64 unk_1/*=0*/, int32 unk_2/*=0*/)
        public static int FrameScriptSignalEvent = 0xD32B90; // int64 (int64 eventId)
        public static int WorldState = 0x2CFA160;

        public static int RealmListCategories = 0x2FD5208;
        public static int RealmListIsLoadComplete = 0x78;
        public static int RealmListNameOffset = 0x8;
        public static int RealmListPopulationState = 0x130;
        public static int RealmListConnectOffset = 0x138;
        public static int RealmListGetRealmInfoByAddress = 0x11C1200; // int64 (int64 categories, uint32 realmAddress)
        public static int RealmListGetRealmInfoNumChars = 0x11C11C0; // int64 (int64 categories, uint32 realmAddress)
        public static int RealmListConnectToRealm = 0x10B2060; // bool (int64 categoriesConnect, uint32 realmAddress, int64 categories)

        public static int GossipText = 0x30C3BA0;
        public static int GossipOptionsCount = 0x30C3B9C;
        public static int GossipOptions = 0x30C43A0;
        public static int GossipSelectOption = 0x171AB20; // void (int32, char*, int32)
        public static int GossipTypeStrings = 0x2C27670;
        public static int GossipOptionIndex = 0x10;
        public static int GossipOptionTypeIndex = 0x1C;
        public static int GossipOptionNext = 0x20;

        public static int Quests = 0x30C47A0;
        public static int QuestStatus = 0x18;
        public static int QuestStatusSub = 0x4;
        public static int QuestStatusCmp = 0x1;
        public static int QuestSize = 0x21C;
        public static int QuestMaxNumber = 0x28;
        public static int QuestDesc = 0x1C;
        public static int QuestGossip = 0x30C9C00;
        public static int QuestComplete = 0x13C0540; // int64 (CGObject* localPlayer, WowGuid* gossipGuid/*=QuestGossip*/, int32 id/*=QuestID*/, int32 unk_0/*=0*/)
        public static int QuestQuery = 0x13C74B0; // int64 (CGObject* localPlayer, WowGuid* gossipGuid/*=QuestGossip*/, int32 id/*=QuestID*/)
        public static int QuestTitle = 0x30FEF60;

        public static int PartyGroups = 0x30BAA60;
        public static int PartyNumGroupMembers = 0x178;
        public static int PartyNumSubgroupMembers = 0x17C;
        public static int PartyGroupFlags = 0x190;
        public static int PartyRaidFlag = 0x2;
        public static int PartyUserClientPartyInvite = 0x6193D0; // int64 (UserClientPartyInvite*)
        public static int PartyInvite = 0x1957F60; // char (char* name, char* pack_0, int64 size_pack_0/*=306*/, char* pack_1, int64 size_pack_1/*=257*/, char pack_2/*=0*/)
        public static int PartyInvitePack1Size = 0x132;
        public static int PartyInvitePack2Size = 0x101;
        public static int PartyUserClientLeaveGroup = 0x618C90; // int64 (UserClientLeaveGroup*)
        public static int PartyUserClientSetPartyLeader = 0x61B010; // int64 (UserClientSetPartyLeader*)
        public static int LootThresholdArg1 = 0x1C0;
        public static int LootThresholdArg2 = 0x1C8;
        public static int UserClientSetLootMethod = 0x61ADA0; // int64 (UserClientSetLootMethod*)
        public static int UserClientConvertRaid = 0x616690; // int64 (UserClientConvertRaid*)
        public static int UserClientResetInstances = 0x61A4A0; // int64 (UserClientResetInstances*)
        public static int GuildInviteActionCode = 0x19;
        public static int GuildInviteThrottle = 0x2C1C6C8;
        public static int IncrementAndCheck = 0x12D9630; // bool (int64 s_guildInviteThrottle)
        public static int UserClientGuildInviteByName = 0x618340; // int64 (UserClientGuildInviteByName*)
        public static int GuildLeaveActionCode = 0x2E;
        public static int GuildGuidOffset = 0xD6D0;
        public static int GlobalGuildLeave = 0x2390140; // int64 (GlobalGuildLeave*)

        public static int FriendList = 0x2FD5F80;
        public static int FriendListAddFriend = 0x11CA680; // int64 (FriendList* C_FriendList, char* name, char* note)
        public static int FriendListOffset = 0x228;
        public static int FriendListShift = 0x3A;
        public static int FriendListNext = 0x240;
        public static int FriendListMaxNum = 0x64;

        public static int TaxiNumNodes = 0x30D42E8;
        public static int TaxiNodes = 0x30D4328;
        public static int TaxiTakeNode = 0x1778660; // void (uint32 nodeIdex)

        public static int WardenBaseAddr = 0x2CF8B20;

        public static int ActionBarActionCode = 0xE;
        public static int ActionBarChangePage = 0x16EA2F0; // bool (int32 page)
        public static int ScriptsActionCode = 0x27;
        public static int MacroActionCode = 0xC;
        public static int MacroSetBody = 0x1785400; // macro* (char* name, char* iconfid, char* body, int32_t perCharacter, int32_t unk/*=0*/)
        public static int MacroFindByName = 0x1785AF0; // macro* (char* name)
        public static int MacroRun = 0x1787E70; // void (macro*, char* button)

        public static int NetLatency = 0xE39520; // int64 (SelectedRealmName* RealmNamePointer, int32 index, int32* latency)
        public static int LogoutActionCode = 0x2F;
        public static int CharacterLogout = 0x11BFC90; // int64 (char quit, char forceLogout, char forceQuit)
        public static int QuitGame = 0x1F5C60; // int64 ()

        /// <summary>
        /// offsets from maikel233
        /// </summary>
        public static int CGUnit_C_IsInMelee = 0x1421550;
        public static int CGUnit_C_OnAttackIconPressed = 0x1427990;
        public static int ClntObjMgrEnumVisibleObjectsPtr = 0x15AF490;
        public static int ClntObjMgrGetMapId = 0x15B5010;
        public static int ClntObjMgrIsValid = 0x15B5730;
        public static int CorpseMapID = 0x2D01BB0;
        public static int CorpsePos = CorpseMapID + 0x40;
        public static int HardwareEventPtr = 0x2D61DF8;
        public static int InvalidPtrCheckMax = 0x2F6A668;
        public static int InvalidPtrCheckMin = 0x2F6A660;
        public static int IsLootWindowOpen = 0x30BACA0;
        public static int NameCacheBase = 0x2C02408;
        public static int RedMessage = 0x3085A10;
        public static int FrameScript_ExecuteBuffer = 0x0;
        public static int FrameScript_GetLocalizedText = 0x0;
        public static int FrameScript_GetText = 0x5975D0;
        public static int GuidToString = 0x11ACF10;
        public static int Movement_InputControl = 0x1172010;
        public static int Movement_ToggleControlBit = 0x2C023D0;    //InputControlPointer
        public static int PartyInfo_GetActiveParty = 0x168E7D0;
        public static int Party_FindMember = 0x168E520;
        public static int PetInfo_FindSpellById = 0x16A1200;
        public static int PetInfo_SendPetAction = 0x16A3260;    //PetSendAction
        public static int Player_LeaveCombatMode = 0x136AD70;
        public static int Specialization_IsTalentSelectedById = 0x17B59C0;
        public static int SpellBook_FindSlotBySpellId = 0x165B630;  //FindSlotBySpellId
        public static int SpellBook_FindSpellOverrideById = 0x165CC30;
        public static int SpellBook_GetOverridenSpell = 0x165BF00;
        public static int SpellDB_GetRow = 0x2240A00;
        public static int SpellDB_HasAttribute = 0x22409A0;
        public static int Spell_C_CancelChannel = 0x1351D80;
        public static int Spell_C_CancelSpell = 0x13521A0;
        public static int Spell_C_CastSpell = 0x16595A0;
        public static int Spell_C_GetMinMaxRange = 0x135A140;
        public static int Spell_C_GetSpellCoolDown = 0x135C7F0;
        public static int Spell_C_HaveSpellPower = 0x1360A60;
        public static int Spell_C_IsCurrentSpell = 0x1365FF0;
        public static int Spell_CancelAutoRepeat = 0x1351B20;
        public static int Spell_ClickSpell = 0x1355E30;
        public static int Spell_GetSomeSpellInfo = 0x223F200;
        public static int Spell_GetSpellCharges = 0x1653F50;
        public static int Spell_GetSpellType = 0x4792B0;
        public static int Spell_HandleTerrainClick = 0x13600C0;
        public static int Spell_IsInRange = 0x1368BC0;
        public static int Spell_IsPlayerSpell = 0x16622B0;
        public static int Spell_IsStealable = 0x16592F0;
        public static int Spell_SomeInfo = 0x138A880;
        public static int Spell_isSpellKnown = 0x1662340;
        public static int SpriteLeftClick = 0x15F3EB0;
        public static int SpriteRightClick = 0x15F42E0;
        public static int Unit_CanAttack = 0x1401E00;
        public static int Unit_GetAuraByIndex = 0x25C9B454;
        public static int Unit_GetFacing = 0x11C4020;
        public static int Unit_GetPosition = 0x13A5130;
        public static int Unit_GetPower = 0x19C5960;
        public static int Unit_GetPowerMax = 0x19C5AC0;
        public static int Unit_IsFriendly = 0x143CFB0;
        public static int WorldFrame_GetWorld = 0x1F5B50;
        public static int WorldFrame_Intersect = 0xF57150;
        public static int World_GetFrameRateResult = 0x255ADE0;
        public static int World_GetFramerateOffset = 0x2FA3E00;

    }
}
