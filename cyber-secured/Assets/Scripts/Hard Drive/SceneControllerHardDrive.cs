using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneControllerHardDrive : MonoBehaviour {
    public Button btn_hdd;
    public Button btn_ssd;
    public Button btn_nvme;
    public Button choose;

    public int choice;

    public void ChooseDrive(int x) {
        // disable all outlines
        btn_hdd.GetComponent<Outline>().enabled = false;
        btn_ssd.GetComponent<Outline>().enabled = false;
        btn_nvme.GetComponent<Outline>().enabled = false;

        // enable only one
        if (x == 1) {
            choice = 1;
            btn_hdd.GetComponent<Outline>().enabled = true;
        } else if (x == 2) {
            choice = 2;
            btn_ssd.GetComponent<Outline>().enabled = true;
        } else {
            choice = 3;
            btn_nvme.GetComponent<Outline>().enabled = true;
        }

        choose.gameObject.SetActive(true);

        // play a beep sound
        GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);
    }

    public void PressChoose() {
        // glitch screen
        GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();
        
        switch (choice) {
        case (1): {
                // hdd chosen
                // TODO
            }
            break;

        case (2): {
                // ssd chosen
                // TODO
            }
            break;

        case (3): {
                // nvme chosen
                // TODO
            }
            break;
        }

        // update decision text
        GameControllerV2.Instance.current_decision_text = "After this upgrade" +
            "\nyou feel a bit more confident in your hardware.";

        // deactivate quiz, and display results
        GameControllerV2.Instance.scn_hard_drive.SetActive(false);
        GameControllerV2.Instance.DisplayDecision();
    }
}
