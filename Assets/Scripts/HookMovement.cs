using UnityEngine;
using UnityEngine.InputSystem;

public class HookMovement : MonoBehaviour
{
    public static HookMovement Instance;
    private PlayerInputActions controls;

    [Header("Movimiento")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform cuerda;
    [SerializeField] private float maxDepth = -20f;
    [SerializeField] private float estiramiento;

    [Header("Sprites")]
    [SerializeField] private SpriteRenderer hookRenderer;
    [SerializeField] private Sprite spriteNormal;
    [SerializeField] private Sprite spriteConPez;

    [Header("Enganche")]
    [SerializeField] private Transform puntaAnzuelo;

    private float startY;
    private float verticalInput;

    private GameObject pezEnganchado;

    private void Awake()
    {
        Instance = this;
        startY = transform.localPosition.y;
        controls = new PlayerInputActions();

        controls.Player.VerticalMove.performed += ctx =>
        {
            verticalInput = ctx.ReadValue<float>();
            BoatMovement.Instance.DesactivateMovement();
        };

        controls.Player.VerticalMove.canceled += ctx =>
        {
            verticalInput = 0;
        };
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void Update()
    {
        Vector3 pos = transform.localPosition;
        float desplazamiento = verticalInput * speed * Time.deltaTime;

        if (desplazamiento != 0)
        {
            if (pos.y + desplazamiento > startY)
            {
                pos.y = startY;
                BoatMovement.Instance.ActivateMovement();

                if (pezEnganchado != null)
                {
                    Destroy(pezEnganchado);

                    pezEnganchado = null;

                    hookRenderer.sprite = spriteNormal;

                    ScoreManager.Instance.AddPoints(100);
                }
            }
            else if (pos.y + desplazamiento < maxDepth)
            {
                pos.y = maxDepth;
            }
            else
            {
                pos.y += desplazamiento;

                cuerda.position += new Vector3(0, desplazamiento / 2f, 0);
                cuerda.localScale += new Vector3(0, desplazamiento * estiramiento, 0);
            }
        }

        transform.localPosition = pos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (pezEnganchado != null) return;

        if (other.CompareTag("Fish"))
        {
            pezEnganchado = other.gameObject;

            // Desactivar movimiento del pez
            Rigidbody2D rb = pezEnganchado.GetComponent<Rigidbody2D>();
            if (rb != null) rb.linearVelocity = Vector2.zero;

            MonoBehaviour scriptMovimiento = pezEnganchado.GetComponent<MonoBehaviour>();
            if (scriptMovimiento != null)
                scriptMovimiento.enabled = false;

            // Colocar pez en la punta
            pezEnganchado.transform.SetParent(puntaAnzuelo);
            pezEnganchado.transform.localPosition = Vector3.zero;
            pezEnganchado.transform.localRotation = Quaternion.Euler(0, 0, 90);

            // Cambiar sprite del anzuelo
            hookRenderer.sprite = spriteConPez;
        }
    }

    public void ResetHook()
    {
        transform.localPosition = new Vector3(
            transform.localPosition.x,
            startY,
            transform.localPosition.z
        );

        BoatMovement.Instance.ActivateMovement();

        // Reset cuerda visual si hace falta

        if (pezEnganchado != null)
        {
            Destroy(pezEnganchado);
            pezEnganchado = null;
        }

        hookRenderer.sprite = spriteNormal;
    }
}