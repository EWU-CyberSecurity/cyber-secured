using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public GameObject [] questions;
    public GameObject next;

    public int i = 0;

    private AudioControllerV2 audioController;

    // Use this for initialization
    void Start()
    {
        audioController = GameObject.Find("SoundManager").GetComponent<AudioControllerV2>();
        audioController.PlayQuizMusic();

        next.SetActive (false);

        /*questions = GameObject.FindGameObjectsWithTag("question");
        for(int j = 1; j < 5; j++)
        {
            questions[j].SetActive(false);
        }*/
    }
}
