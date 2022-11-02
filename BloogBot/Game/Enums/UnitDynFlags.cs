

namespace BloogBot.Game.Enums
{
    public enum UnitDynFlags
    {
        UNIT_DYNFLAG_NONE = 0x0000,
        UNIT_DYNFLAG_HIDE_MODEL = 0x0001, // Object model is not shown with this flag
        UNIT_DYNFLAG_LOOTABLE = 0x0004,
        UNIT_DYNFLAG_TRACK_UNIT = 0x0008,   //possibly more distinct animation of GO
        UNIT_DYNFLAG_TAPPED = 0x0010, // Lua_UnitIsTapped - Indicates the target as grey for the client.
        UNIT_DYNFLAG_TAPPEDBYME = 0x0020, // 
        UNIT_DYNFLAG_DEAD = 0x0040,
        UNIT_DYNFLAG_REFER_A_FRIEND = 0x0080,
        UNIT_DYNFLAG_ISTAPPEDBYALL_THREATLIST = 0x100
    }
}
