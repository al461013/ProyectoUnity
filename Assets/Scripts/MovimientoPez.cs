using UnityEngine;

public class MovimientoPez : MonoBehaviour
{
    public float velocidad = 2f;

    // Límites horizontales del mapa
    public float limiteIzquierdo = -12f;
    public float limiteDerecho = 12f;

    private int direccion = 1;

    void Start()
    {
        // Si está rotado mirando a la izquierda
        if (transform.localScale.x < 0)
        {
            direccion = -1;
        }
        else
        {
            direccion = 1;
        }
    }

    void Update()
    {
        // Movimiento continuo
        transform.Translate(Vector2.right * direccion * velocidad * Time.deltaTime);

        // Si sale del mapa, destruir
        if (transform.position.x < limiteIzquierdo || transform.position.x > limiteDerecho)
        {
            Destroy(gameObject);
        }
    }
}