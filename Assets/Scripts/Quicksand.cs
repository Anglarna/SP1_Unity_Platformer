using UnityEngine;

public class Quicksand : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    //TODO: lägg till kort delay innan physicsMode blir till normal
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.physicsMode = PlayerMovement.PhysicsMode.quickSand;
            print(player.physicsMode);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.physicsMode = PlayerMovement.PhysicsMode.normal;
            print(player.physicsMode);
        }
    }
}
