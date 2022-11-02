using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloogBot.Game
{
    internal class Field_oiramario
    {
        const int UnitDataPointer = 0x380;
        const int UnitName = 0xF8;
        const int UnitSkinningTypeOffset = 0xE8;
        const int UnitSkinableHerb = 0x100;
        const int UnitSkinableRock = 0x200;
        const int UnitSkinableBolts = 0x8000;
        const int UnitHealth = 0xD4C8;
        const int UnitMaxHealth = 0xD4D0;
        const int UnitFlags1 = 0xD5F0;
        const int UnitFlags2 = 0xD5F4;
        const int UnitFlags3 = 0xD5F8;
        const int UnitPowerType = 0xD5C0;
        const int UnitPower1 = 0xD3B0;
        const int UnitPower2 = 0xD3B4;
        const int UnitPower3 = 0xD3B8;
        const int UnitMaxPower = 0xD7FC;
        const int UnitLocation = 0x20;
        const int UnitYaw = 0x30;
        const int UnitPitch = 0x34;
        const int UnitLevel = 0xD5C8;
        const int UnitMountDisplayID = 0x418;
        const int UnitTarget = 0xD588;
        const int UnitChannelID = 0x5B8;
        const int UnitCastStartTime = 0x5B0;
        const int UnitCastEndTime = 0x5B4;
        const int UnitChannelCastStartTime = 0x5C0;
        const int UnitChannelCastEndTime = 0x5C4;
        const int UnitNpcFlags = 0xD500;
        const int UnitRace = 0xD5BC;
        const int UnitFactionTemplate = 0xD5EC;
        const int UnitSummonedBy = 0xD538;
        const int UnitCreatedBy = 0xD558;
        const int UnitSex = 0xD5BF;
        const int UnitClass = 0xD5BD;
        const int UnitCreatureClassification = 0x38;
        const int UnitCreatureFamily = 0x34;
        const int UnitCreatureType = 0x30;
        const int UnitMovementPointer = 0xF0;
        const int UnitCurrentSpeed = 0xA4;
        const int UnitWalkSpeed = 0xA8;
        const int UnitRunSpeed = 0xAC;
        const int UnitFlightSpeed = 0xBC;
        const int UnitSwimSpeed = 0xB4;
        const int UnitMovementFlags = 0x58;
        const int UnitCanAttack = 0x133C410; // bool (CGObject* player, CGUnit* unit, bool isPet)
        const int UnitCastingInfo = 0x568;
        const int UnitFlight = 0x1365060; // int64 (CGUnit* unit, int32 eventTime, int32 takeoff/*=1*/)
        const int UnitSwimStart = 0x1368D00; // int64 (CGUnit* unit, int32 eventTime)
        const int UnitJump = 0x13650C0; // int64 (CGUnit* unit, int32 eventTime)
        const int UnitDismount = 0x134C560; // int64 (CGUnit* unit)
        const int UnitIsOutdoors = 0x135B970; // bool (CGUnit* unit)


    }
}
