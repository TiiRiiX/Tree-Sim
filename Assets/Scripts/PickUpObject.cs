using UnityEngine;

public class PickUpObject : MonoBehaviour, IInteractable
{
    private bool isPicked = false;
    
    public void Action()
    {
        if (!isPicked)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            isPicked = true;
        }
    }

    public bool IsNeedDelay => false;
}
