using UnityEngine;

public class RopeHealth : MonoBehaviour
{
    public static RopeHealth Instance;

    [SerializeField] private int vidaMax = 3;

    public int vidaActual;

    private void Awake()
    {
        Instance = this;
        vidaActual = vidaMax;
    }

    public void RecibirDaño()
    {
        vidaActual--;

        Debug.Log("Vida restante: " + vidaActual);

        HookMovement.Instance.ResetHook();

        if (vidaActual <= 0)
        {
            Debug.Log("GAME OVER");
        }
    }
}
