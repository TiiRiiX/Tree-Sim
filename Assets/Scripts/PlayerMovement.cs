using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float interactableRadius;
    [SerializeField] private GameObject interactableTipObject;

    private RaycastHit2D[] interactableResult = new RaycastHit2D[1];
    private int interactableMask;
    private bool isCanInteract = false;
    private Interactable interactObject;
    
    private void Start()
    {
        interactableMask = LayerMask.NameToLayer("Interactable");
        interactableTipObject.SetActive(false);
    }

    private void Update()
    {
        Movement();
        FindInteract();
        Interact();
    }

    private void Interact()
    {
        if (!isCanInteract || interactObject == null) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactObject.Action();
            RemoveInteraction();
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
            if (!isCanInteract)
            {
                interactObject = interactableResult[0].collider.GetComponent<Interactable>();
                isCanInteract = true;
                interactableTipObject.SetActive(true);
            }
        }
        else if (isCanInteract)
        {
            RemoveInteraction();
        }
    }

    private void Movement()
    {
        var hAxis = Input.GetAxis("Horizontal");
        var vAxis = Input.GetAxis("Vertical");
        if (hAxis != 0)
        {
            transform.position += Vector3.right * hAxis * speed * Time.deltaTime;
        }

        if (vAxis != 0)
        {
            transform.position += Vector3.up * vAxis * speed * Time.deltaTime;
        }
    }
}
