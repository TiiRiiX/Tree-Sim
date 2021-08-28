using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private Transform inventoryContainer;
    [SerializeField] private InventoryUIItem inventoryUIItem;
    [SerializeField] private InventoryStoreManager storeManager;

    private void Start()
    {
        storeManager.OnInventoryChange += OnChangeInventoryItems;
    }

    private void OnChangeInventoryItems()
    {
        RefillItems(storeManager.Items);
    }

    private void RefillItems(List<InventoryItem> items)
    {
        foreach (Transform child in inventoryContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in items)
        {
            var uiItem = Instantiate(inventoryUIItem, inventoryContainer, false);
            uiItem.SetInfo(item);
        }
    }

    private void OnDestroy()
    {
        storeManager.OnInventoryChange -= OnChangeInventoryItems;
    }
}
