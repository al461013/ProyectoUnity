using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private void LateUpdate()
    {
        transform.position = new Vector3(
            transform.position.x,
            target.position.y,
            transform.position.z
        );
    }
}