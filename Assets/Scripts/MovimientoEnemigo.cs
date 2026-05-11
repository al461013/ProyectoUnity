using Unity.VisualScripting;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{

    [SerializeField]
    private Transform punto1;
    [SerializeField]
    private Transform punto2;
    [SerializeField]
    private float velocidadMovimiento;

    private bool rotacion = true;
    private Transform objetivoActual;
    private SpriteRenderer visualizacion;
    private void Awake()
    {
        objetivoActual = punto1;
        visualizacion = gameObject.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        float desplazamiento = Time.deltaTime * velocidadMovimiento;

        transform.position = new Vector3(
            Mathf.MoveTowards(transform.position.x, objetivoActual.position.x, desplazamiento),
            Mathf.MoveTowards(transform.position.y, objetivoActual.position.y, desplazamiento),
            transform.position.z);
        if (Vector3.Distance(transform.position, objetivoActual.position) < 0.1f)
        {
            objetivoActual = objetivoActual == punto1 ? punto2 : punto1;
            visualizacion.flipX = ! visualizacion.flipX;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rope"))
        {
            RopeHealth.Instance.RecibirDaño();
        }
    }

}
