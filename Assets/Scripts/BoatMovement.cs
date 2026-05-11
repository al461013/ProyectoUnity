using UnityEngine;
using UnityEngine.InputSystem;

public class BoatMovement : MonoBehaviour
{
    public static BoatMovement Instance;
    private Rigidbody2D rb;

    private PlayerInputActions controls;

    private float moveInput;
    private bool allowMovement = true;

    [SerializeField]
    private float speed = 5f;

    [Header("Sprites")]
    [SerializeField]
    private Sprite barcoIzquierda;

    [SerializeField]
    private Sprite barcoDerecha;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;    
        }
        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        controls = new PlayerInputActions();

        controls.Player.Move.performed += ctx =>
        {
            if (allowMovement) { moveInput = ctx.ReadValue<float>(); }
        };

        controls.Player.Move.canceled += ctx =>
        {
            moveInput = 0;
        };
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * speed, 0);

        CambiarDireccionVisual();
    }

    private void CambiarDireccionVisual()
    {
        if (moveInput > 0)
        {
            spriteRenderer.sprite = barcoDerecha;

            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
        else if (moveInput < 0)
        {
            spriteRenderer.sprite = barcoIzquierda;

            transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
        }
    }
    public void ActivateMovement()
    {
        CameraFollow.seguimientoBarco = true;
        allowMovement = true;
    }
    public void DesactivateMovement()
    {
        CameraFollow.seguimientoBarco = false; 
        allowMovement = false;
    }
}