using UnityEngine;

public class MoveBridge : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private AudioClip buttonSoundEffect;
    private Animator anim;
    private AudioSource audioSource;
    private bool hasBeenActivated = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && hasBeenActivated == false)
        {
            anim.SetTrigger("Move");
            button.SetActive(false);
            audioSource.PlayOneShot(buttonSoundEffect);
            hasBeenActivated = true;
            
        }
            
    }
}
