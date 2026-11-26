using UnityEngine;

public class ChestController : MonoBehaviour
{
    public enum ChestType { Normal, BlackHole, Percent10, Percent30, Percent50 }
    public ChestType type = ChestType.Normal;
    public int itemsToGive = 3;
    private static readonly ItemData[] possibleItems = new ItemData[]
    {
        new ItemData("sword1", "Iron Sword", "Обычный меч", 10, 0, 50, "Common"),
        new ItemData("shield1", "Wood Shield", "Деревянный щит", 0, 8, 40, "Common"),
        new ItemData("potion1", "Health Potion", "Зелье лечения", 0, 0, 20, "Common"),
        new ItemData("axe1", "Steel Axe", "Стальной топор", 18, 0, 120, "Rare"),
        new ItemData("armor1", "Chain Armor", "Кольчуга", 0, 15, 150, "Rare"),
        new ItemData("ring1", "Power Ring", "Кольцо силы", 5, 5, 200, "Epic"),
        new ItemData("sword2", "Dragon Blade", "Клинок дракона", 35, 5, 500, "Legendary")
    };
    public void OpenChest()
    {
        switch (type)
        {
            case ChestType.Normal:
                GiveRandomItems(itemsToGive);
                break;
            case ChestType.BlackHole:
                InventoryManager.Instance.ClearAll();
                Debug.Log("Чёрная дыра поглотила всё");
                break;
            case ChestType.Percent10:
                InventoryManager.Instance.RemoveRandomPercent(0.1f);
                break;
            case ChestType.Percent30:
                InventoryManager.Instance.RemoveRandomPercent(0.3f);
                break;
            case ChestType.Percent50:
                InventoryManager.Instance.RemoveRandomPercent(0.5f);
                break;
        }
        Debug.Log("Сундук открыт: " + type);
    }
    private void GiveRandomItems(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var item = possibleItems[Random.Range(0, possibleItems.Length)];
            var newItem = new ItemData(
                item.Id + "_" + System.Guid.NewGuid().ToString().Substring(0, 4),
                item.Name, item.Description, item.Attack, item.Defense, item.Value, item.Rarity);
            InventoryManager.Instance.AddItem(newItem);
        }
    }
}
