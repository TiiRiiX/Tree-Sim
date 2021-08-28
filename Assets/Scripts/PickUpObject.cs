using UnityEngine;

public class PickUpObject : MonoBehaviour, IInteractable
{
    [SerializeField] private InventoryStoreManager storeManager;
    [SerializeField] private InventoryItem item;
    
    public void Action()
    {
        if (storeManager.CanAddItems)
        {
            storeManager.AddItem(item);
            Destroy(gameObject);   
        }
    }

    public bool IsNeedDelay => false;
}
