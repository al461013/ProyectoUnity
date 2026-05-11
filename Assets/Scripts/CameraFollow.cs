using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform barco;


    public static bool seguimientoBarco = true;

    private void Update()
    { 
        if (seguimientoBarco)
        {
            transform.position = new Vector3(
                barco.position.x,
                barco.position.y,
                transform.position.z
            );
        }
        else
        {
            transform.position = new Vector3(
                barco.position.x,
                target.position.y,
                transform.position.z
            );
        }
            
    }
}