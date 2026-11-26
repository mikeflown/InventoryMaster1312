using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public int Index { get; private set; }
    public bool IsEmpty => CurrentItem == null;
    public ItemData CurrentItem { get; private set; }
    [Header("UI")]
    public Image icon;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI statsText;
    public Button deleteButton;
    public void Initialize(int index)
    {
        Index = index;
        Clear();
        if (deleteButton != null)
        {
            deleteButton.onClick.RemoveAllListeners();
            deleteButton.onClick.AddListener(() => InventoryManager.Instance.RemoveItem(Index));
            var nav = deleteButton.navigation;
            nav.mode = Navigation.Mode.None;
            deleteButton.navigation = nav;
        }
    }
    public void SetItem(ItemData item)
    {
        CurrentItem = item;
        if (icon != null) icon.enabled = true;
        if (nameText != null) nameText.text = item.Name;
        if (statsText != null)
            statsText.text = $"ATK:{item.Attack} DEF:{item.Defense}\n{item.Rarity}";
        if (deleteButton != null) deleteButton.gameObject.SetActive(true);
    }
    public void Clear()
    {
        CurrentItem = null;
        if (icon != null) icon.enabled = false;
        if (nameText != null) nameText.text = "";
        if (statsText != null) statsText.text = "";
        if (deleteButton != null) deleteButton.gameObject.SetActive(false);
    }
}
