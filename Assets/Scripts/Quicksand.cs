using UnityEngine;

public class Quicksand : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private float delayDuration = 1f;
    private float delay = 0;
    private bool doDelay = false;
    private void Start()
    {
        delay = delayDuration;
    }
    private void FixedUpdate()
    {
        if (delay <= 0 && doDelay)
        {
            player.physicsMode = PlayerMovement.PhysicsMode.normal;
            doDelay = false;
            delay = delayDuration;
        }
        else if (doDelay)
        {
            delay -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            delay = delayDuration;
            player.physicsMode = PlayerMovement.PhysicsMode.quickSand;
            doDelay = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            delay = delayDuration;
            doDelay = true;
        }
    }
}
