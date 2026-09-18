using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.8f;
    [SerializeField] private float bounciness = 100f;
    private SpriteRenderer rend;
    [SerializeField] private int DamageGiven = 1;

    //Knockback
    [SerializeField] private float knockbackForce = 100f;
    [SerializeField] private float upwardsForce = 5f;
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (moveSpeed > 0)
        {
            rend.flipX = true;
        }
        if (moveSpeed < 0)
        {
            rend.flipX = false;
        }
    }

    void FixedUpdate()
    {
        transform.Translate(new Vector2(moveSpeed, 0)  * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBlock") || other.gameObject.CompareTag("Enemy"))
        {
            moveSpeed = -moveSpeed;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(DamageGiven);

            if (other.transform.position.x > transform.position.x)
            {
                other.gameObject.GetComponent<PlayerMovement>().Takeknockback(knockbackForce, upwardsForce);
            }
            else
            {
                other.gameObject.GetComponent<PlayerMovement>().Takeknockback(-knockbackForce, upwardsForce);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rigidbody2D = other.attachedRigidbody;

            if (rigidbody2D != null)
            {
                rigidbody2D.linearVelocity = new Vector2(rigidbody2D.linearVelocity.x, 0);
                rigidbody2D.AddForce(new Vector2(0, bounciness));
            }

            Destroy(gameObject);
        }

    }
}
