using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InventoryStoreManager storeManager;

    private void Start()
    {
        storeManager.Init();
    }
}
