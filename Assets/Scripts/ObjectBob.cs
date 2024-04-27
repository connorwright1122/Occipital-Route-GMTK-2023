using UnityEngine;

public class ObjectBob : MonoBehaviour
{
    public float bobHeight = 1f; // Maximum height of the bobbing motion
    public float bobSpeed = 1f; // Speed of the bobbing motion

    public float rotationSpeed = 10f;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        float newY = initialPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
