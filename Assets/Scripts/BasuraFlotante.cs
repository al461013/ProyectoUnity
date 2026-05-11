using UnityEngine;

public class BasuraFlotante : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float floatHeight = 0.2f;

    public float rotateSpeed = 10f;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        transform.position = new Vector3(
            transform.position.x,
            newY,
            transform.position.z
        );

        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}