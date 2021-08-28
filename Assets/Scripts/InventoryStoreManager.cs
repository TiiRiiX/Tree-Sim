using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventoryStoreManager : ScriptableObject
{
    [SerializeField] private int maxInventoryItems;
    
    public event Action OnInventoryChange;
    public bool CanAddItems => Items.Count < maxInventoryItems;
    public List<InventoryItem> Items { get; private set; }

    public void Init()
    {
        Items = new List<InventoryItem>();
    }

    public void AddItem(InventoryItem item)
    {
        Items.Add(item);
        OnInventoryChange?.Invoke();
    }
}
