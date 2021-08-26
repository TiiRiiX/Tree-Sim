using UnityEngine;

public class Log : MonoBehaviour, Interactable
{
    public void Action()
    {
        Destroy(gameObject);
    }
}
