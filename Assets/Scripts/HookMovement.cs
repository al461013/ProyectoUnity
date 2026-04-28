using UnityEngine;
using UnityEngine.InputSystem;

public class HookMovement : MonoBehaviour
{
    private PlayerInputActions controls;

    private float verticalInput;

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float maxDepth = -20f;

    private float startY;

    private void Awake()
    {
        controls = new PlayerInputActions();

        controls.Player.VerticalMove.performed += ctx =>
        {
            verticalInput = ctx.ReadValue<float>();
        };

        controls.Player.VerticalMove.canceled += ctx =>
        {
            verticalInput = 0;
        };
    }

    private void Start()
    {
        startY = transform.localPosition.y;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        Vector3 pos = transform.localPosition;

        pos.y += verticalInput * speed * Time.deltaTime;

        // Nunca subir más del origen
        if (pos.y > startY)
        {
            pos.y = startY;
        }

        // Nunca bajar más del fondo
        if (pos.y < maxDepth)
        {
            pos.y = maxDepth;
        }

        transform.localPosition = pos;
    }
}
