using UnityEngine;
using TMPro;

public class PlayerQuest : MonoBehaviour
{
    [SerializeField] private int diamondsToCollect = 10;
    [SerializeField] private TMP_Text diamondText;
    [SerializeField] private AudioClip pickupSoundEffect;
    private int diamonds = 0;
    private AudioSource audioSource;

    private void Start()
    {
        diamondText.text = "" + diamonds;
        audioSource = GetComponent<AudioSource>();
    }

    public void AddDiamond()
    {
        diamonds++;
        diamondText.text = "" + diamonds;
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(pickupSoundEffect);
    }

    public int GetDiamonds() {  return diamonds; }
    public int GetDiamondsToCollect() { return diamondsToCollect; }
}
