using UnityEngine;

public class GeneradorPeces : MonoBehaviour
{
    [Header("Prefabs de peces")]
    public GameObject[] pecesPrefabs; // Aquí metes tus 3 prefabs

    [Header("Límites del mapa")]
    public float limiteIzquierdo = -12f;
    public float limiteDerecho = 12f;
    public float alturaMin = -3f;
    public float alturaMax = 3f;

    [Header("Tiempo de aparición")]
    public float tiempoMin = 1f;
    public float tiempoMax = 3f;

    void Start()
    {
        Invoke(nameof(GenerarPez), 1f);
    }

    void GenerarPez()
    {
        // Elegir pez aleatorio
        GameObject prefabElegido = pecesPrefabs[Random.Range(0, pecesPrefabs.Length)];

        // Elegir lado aleatorio
        bool saleDesdeIzquierda = Random.value > 0.5f;

        float x;
        int direccion;

        if (saleDesdeIzquierda)
        {
            x = limiteIzquierdo;
            direccion = 1; // Va hacia la derecha
        }
        else
        {
            x = limiteDerecho;
            direccion = -1; // Va hacia la izquierda
        }

        // Altura aleatoria
        float y = Random.Range(alturaMin, alturaMax);

        // Crear pez
        GameObject pez = Instantiate(prefabElegido, new Vector2(x, y), Quaternion.identity);

        // Girarlo según dirección
        Vector3 escala = pez.transform.localScale;
        escala.x = Mathf.Abs(escala.x) * direccion;
        pez.transform.localScale = escala;

        // Siguiente pez en tiempo aleatorio
        float siguienteTiempo = Random.Range(tiempoMin, tiempoMax);
        Invoke(nameof(GenerarPez), siguienteTiempo);
    }
}
