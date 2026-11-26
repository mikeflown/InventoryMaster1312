using UnityEngine;
using UnityEngine.UI;

public class ChestButtonBinder : MonoBehaviour
{
    private void Start()
    {
        var buttons = GetComponentsInChildren<Button>(true);
        foreach (var btn in buttons)
        {
            var chest = btn.GetComponent<ChestController>();
            if (chest != null)
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(chest.OpenChest);
                Debug.Log("Привязал кнопку: " + btn.name);
            }
        }
    }
}