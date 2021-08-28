using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIItem : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemText;
    
    public void SetInfo(InventoryItem item)
    {
        itemImage.sprite = item.Icon;
        itemText.SetText($"{item.Name}");
    }
}
