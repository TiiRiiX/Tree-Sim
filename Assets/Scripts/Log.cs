using UnityEngine;

public class Log : MonoBehaviour, Interactable
{
    public void Action()
    {
        Destroy(gameObject);
    }

    public bool IsNeedDelay => false;
}
