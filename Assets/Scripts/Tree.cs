using UnityEngine;

public class Tree : MonoBehaviour, IInteractable
{
    [SerializeField] private int hitToFall;
    [SerializeField] private GameObject logsPrefab;

    private int currentHitToFall;

    private void Start()
    {
        currentHitToFall = hitToFall;
    }

    public void Action()
    {
        currentHitToFall--;
        if (currentHitToFall <= 0)
        {
            Instantiate(logsPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public bool IsNeedDelay => true;
}
