using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5f;

    private Rigidbody2D rb;

    private Vector2 moveInput;

    private bool canMove = false;

    [SerializeField] private GameObject torchLight;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void StartMovement()
    {
        canMove = true;
    }

    public void StopMovement()
    {
        canMove = false;
        rb.linearVelocity = Vector2.zero;
    }

    void Update()
    {
        if (!canMove)
        {
            moveInput = Vector2.zero;
            return;
        }

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput = moveInput.normalized;
    }

    void FixedUpdate()
    {
        if (!canMove)
            return;

        rb.linearVelocity = moveInput * speed;
    }

    public void EnableTorch()
    {
        torchLight.SetActive(true);
    }

    public void DisableTorch()
    {
        torchLight.SetActive(false);
    }
}