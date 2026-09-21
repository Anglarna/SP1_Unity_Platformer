using UnityEngine;

public class Paralax : MonoBehaviour
{
    private float length;
    [SerializeField] private GameObject cam;
    [SerializeField] private float parallaxEffect;
    private Vector2 startPos;
    void Start()
    {
        startPos = transform.position;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    void Update()
    {
        float temp = cam.transform.position.x * (1 - parallaxEffect);
        float distance = cam.transform.position.x * parallaxEffect;

        transform.position = new(startPos.x + distance, cam.transform.position.y);

        if (temp > startPos.x + (length * 1.0f)) startPos.x += length;
        else if (temp < startPos.x - (length * 1.0f)) startPos.x -= length;
    }
}
