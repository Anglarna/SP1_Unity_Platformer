using UnityEngine;

public class DiamondPickup : MonoBehaviour
{
    [SerializeField] private GameObject diamondParticleSystem;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerQuest>().AddDiamond();
            Instantiate(diamondParticleSystem, transform.position, Quaternion.identity);
            
        }
        Destroy(gameObject);
    }
}
