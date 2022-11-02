namespace BloogBot.Game.Enums
{
    public enum ObjectType : byte
    {
        Object = 0,
        Item = 1,   //物品
        Container = 2,
        EmpAzeriteItem = 3,   
        AzeriteItem = 4, 
        Unit = 5,
        Player = 6,
        LocalPlayer = 7,
        GameObject = 8,
        DynamicObject = 9,
        Corpse = 10,
        AreaTrigger = 11,
        SceneObject = 12,
        Conversation = 13,
        Nil = 255
    }
}
