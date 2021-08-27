using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactableRadius;
    [SerializeField] private GameObject interactableTipObject;
    [SerializeField] private Animator axeAnimator;
    [SerializeField] private float interactDelay;
    [SerializeField] private Transform pickUpTransform;
    
    private RaycastHit2D[] interactableResult = new RaycastHit2D[1];
    private int interactableMask;
    private bool isCanInteract = false;
    private IInteractable interactObject;
    private float lastInteractTime = 0f;
    private PickUpObject pickUpObject = null;

    private void Start()
    {
        interactableMask = LayerMask.NameToLayer("Interactable");
        interactableTipObject.SetActive(false);
    }
    
    private void Update()
    {
        FindInteract();
        Interact();
    }
    
    private void Interact()
    {
        if (!isCanInteract || interactObject == null) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (interactObject is PickUpObject pickUp)
            {
                interactObject.Action();
                pickUp.transform.SetParent(pickUpTransform);
                pickUp.transform.localPosition = Vector3.zero;
                pickUpObject = pickUp;
                RemoveInteraction();
            } 
            else if (interactObject.IsNeedDelay)
            {
                if (lastInteractTime + interactDelay < Time.time)
                {
                    interactObject.Action();
                    lastInteractTime = Time.time;
                    RemoveInteraction();
                    axeAnimator.SetTrigger("Hit");
                }
            }
            else
            {
                interactObject.Action();
                RemoveInteraction();
            }
        }
    }

    private void RemoveInteraction()
    {
        interactObject = null;
        isCanInteract = false;
        interactableTipObject.SetActive(false);
    }

    private void FindInteract()
    {
        if (Physics2D.CircleCastNonAlloc(transform.position, interactableRadius, Vector2.up, interactableResult,
            interactableRadius, 1 << interactableMask) > 0)
        {
            if (pickUpObject == null)
            {
                var readyForPickUp = interactableResult[0].collider.GetComponent<PickUpObject>();
                if (readyForPickUp != null)
                {
                    interactObject = readyForPickUp;
                    isCanInteract = true;
                    interactableTipObject.SetActive(true);
                }
            }
            
            if (!isCanInteract)
            {
                var interactable = interactableResult[0].collider.GetComponent<IInteractable>();
                if (!(interactable is PickUpObject))
                {
                    interactObject = interactable;
                    isCanInteract = true;
                    interactableTipObject.SetActive(true);   
                }
            }
        }
        else if (isCanInteract)
        {
            RemoveInteraction();
        }
    }
}
