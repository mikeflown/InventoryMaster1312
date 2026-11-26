using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    [Header("Settings")]
    public int gridWidth = 6;
    public int gridHeight = 4;
    public int TotalSlots => gridWidth * gridHeight;
    [Header("References")]
    public Transform slotsParent;
    public GameObject slotPrefab;
    private List<InventorySlot> slots = new List<InventorySlot>();
    private string savePath;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        savePath = Path.Combine(Application.persistentDataPath, "inventory_save.json");
        CreateSlots();
        LoadInventory();
    }
    private void CreateSlots()
    {
        for (int i = 0; i < TotalSlots; i++)
        {
            var go = Instantiate(slotPrefab, slotsParent);
            var slot = go.GetComponent<InventorySlot>();
            slot.Initialize(i);
            slots.Add(slot);
        }
    }
    public bool AddItem(ItemData item)
    {
        Debug.Log("Попытка добавить предмет: " + item.Name);
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].IsEmpty)
            {
                slots[i].SetItem(item);
                SaveInventory();
                return true;
            }
        }
        Debug.Log("Инвентарь полон");
        return false;
    }
    public void RemoveItem(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < slots.Count)
        {
            slots[slotIndex].Clear();
            SaveInventory();
        }
    }
    public void ClearAll()
    {
        foreach (var slot in slots)
            slot.Clear();
        SaveInventory();
        Debug.Log("Инвентарь полностью очищен");
    }
    public void RemoveRandomPercent(float percent)
    {
        List<int> filled = new List<int>();
        for (int i = 0; i < slots.Count; i++)
            if (!slots[i].IsEmpty) filled.Add(i);
        int toRemove = Mathf.CeilToInt(filled.Count * percent);
        toRemove = Mathf.Clamp(toRemove, 0, filled.Count);
        for (int i = 0; i < toRemove; i++)
        {
            if (filled.Count == 0) break;
            int rand = Random.Range(0, filled.Count);
            int index = filled[rand];
            slots[index].Clear();
            filled.RemoveAt(rand);
        }
        SaveInventory();
        Debug.Log($"Удалено {toRemove} предметов ({percent * 100}%)");
    }
    public void SaveInventory()
    {
        InventorySaveData save = new InventorySaveData();
        for (int i = 0; i < slots.Count; i++)
        {
            if (!slots[i].IsEmpty)
                save.Slots.Add(new InventorySlotData(i, slots[i].CurrentItem));
        }
        string json = JsonConvert.SerializeObject(save, Formatting.Indented);
        File.WriteAllText(savePath, json);
        Debug.Log("Инвентарь сохранён");
    }
    public void LoadInventory()
    {
        if (!File.Exists(savePath)) return;
        string json = File.ReadAllText(savePath);
        InventorySaveData save = JsonConvert.DeserializeObject<InventorySaveData>(json);
        foreach (var slotData in save.Slots)
        {
            if (slotData.SlotIndex >= 0 && slotData.SlotIndex < slots.Count)
                slots[slotData.SlotIndex].SetItem(slotData.Item);
        }
        Debug.Log("Инвентарь загружен");
    }
    public List<InventorySlot> GetSlots() => slots;
}
