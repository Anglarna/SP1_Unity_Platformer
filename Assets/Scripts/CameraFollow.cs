using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -15);
    [SerializeField] private float smoothing;
    [SerializeField] private Vector3 offsetLeft = new Vector3 (-5, 0, -15);
    [SerializeField] private Vector3 offsetRight = new Vector3(5, 0, -15);

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            offset = offsetLeft;
        }
        if (Input.GetKey(KeyCode.D))
        {
            offset = offsetRight;
        }
    }

    private void LateUpdate()
    {

        Vector3 newPosition = Vector3.Lerp(transform.position, target.position + offset, smoothing * Time.deltaTime);
        transform.position = newPosition;

    }
}
