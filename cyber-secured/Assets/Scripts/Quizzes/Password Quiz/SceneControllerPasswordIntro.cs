using UnityEngine;

public class SceneControllerPasswordIntro : MonoBehaviour {

    public GameObject questions;
    public GameObject PasswordIntro;

    // Use this for initialization
    void Start () {

    }
    
    public void ContinueOnClick()
    {
        // glitch screen
        GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();

        PasswordIntro.SetActive(false);
        questions.SetActive(true);

    }
}
