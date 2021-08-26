using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float interactableRadius;
    [SerializeField] private GameObject interactableTipObject;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private Animator animator; 

    private RaycastHit2D[] interactableResult = new RaycastHit2D[1];
    private int interactableMask;
    private bool isCanInteract = false;
    private Interactable interactObject;
    private Vector2 movement = Vector2.zero;
    
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
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + movement * speed * Time.fixedDeltaTime);
    }
}
