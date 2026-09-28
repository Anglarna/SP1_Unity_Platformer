using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private int startingHealth = 5;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Color normalHealthColor, criticalHealthColor;
    private int currentHealth;

    void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth-= damage;

        if (currentHealth <= 0)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        currentHealth = startingHealth;

        transform.position = spawnPosition.position;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }

    public bool RestoreHealth(int healthToRestore)
    {
        if (currentHealth >= startingHealth)
        {
            return false;
        }
        currentHealth += healthToRestore;

        if(currentHealth > startingHealth)
        {
            currentHealth = startingHealth;
        }
        return true;
    }
}
