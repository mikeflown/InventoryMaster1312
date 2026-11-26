using System;

[Serializable]
public class ItemData
{
    public string Id;
    public string Name;
    public string Description;
    public int Attack;
    public int Defense;
    public int Value;
    public string Rarity;
    public ItemData() { }
    public ItemData(string id, string name, string description, int attack, int defense, int value, string rarity)
    {
        Id = id;
        Name = name;
        Description = description;
        Attack = attack;
        Defense = defense;
        Value = value;
        Rarity = rarity;
    }
}
