

namespace BloogBot.Game.Enums
{
    /// <summary>
    /// https://wowwiki-archive.fandom.com/wiki/ChatTypeId
    /// </summary>
    public enum ChatTypeId : int
    {
        SAY = 0, //checked
        EMOTE = 1,
        YELL = 6,
        PARTY = 49, //checked
        GUILD = 4,
        OFFICER = 5,
        RAID = 39,  //checked
        RAID_WARNING = 7,
        INSTANCE_CHAT = 8,
        BATTLEGROUND = 9,
        WHISPER = 7, //checked
        CHANNEL = 17,//checked
        AFK = 12,
        DND = 13,
    }
}
