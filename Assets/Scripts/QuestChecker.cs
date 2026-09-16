using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestChecker : MonoBehaviour
{
    [SerializeField] private GameObject panel, finishedText, unfinishedText;
    [SerializeField] private int levelIndex;

    private Animator animation;

    private void Start()
    {
        animation = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<PlayerQuest>().GetDiamonds() >= other.GetComponent<PlayerQuest>().GetDiamondsToCollect())
            {
                panel.SetActive(true);
                finishedText.SetActive(true);
                animation.SetTrigger("Chest");
                unfinishedText.SetActive(false);
                Invoke(nameof(LoadNextLevel), 3f);
            }
            else
            {
                panel.SetActive(true);
                unfinishedText.SetActive(true);
                finishedText.SetActive(false);
            }
                    
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        panel.SetActive(false);
        finishedText.SetActive(false);
        unfinishedText.SetActive(false);
    }
    private void LoadNextLevel()
    {
        SceneManager.LoadScene(levelIndex);
    }
}
