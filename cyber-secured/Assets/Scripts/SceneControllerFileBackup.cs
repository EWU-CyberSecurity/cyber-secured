using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneControllerFileBackup : MonoBehaviour
{
    public Toggle hdd;
    public Toggle usb;
    public Toggle cld;
    public Button choose;

    
    public void PressChoose()
    {
        // glitch screen
        GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();

        // Set backups
        if(hdd.isOn) {
            GameControllerV2.Instance.backup_hdd = true;
            GameControllerV2.Instance.DecreaseNP(30);
        }
        if (usb.isOn) {
            GameControllerV2.Instance.backup_usb = true;
            GameControllerV2.Instance.DecreaseNP(Mathf.RoundToInt((float) GameControllerV2.Instance.GetNetworkPower() * 0.1f));
        }
        if (cld.isOn) {
            GameControllerV2.Instance.backup_cld = true;
            GameControllerV2.Instance.DecreaseMonthlyNP(5);
            GameControllerV2.Instance.AddCloudEventToList();
        }

        // update decision text
        if (hdd.isOn || usb.isOn || cld.isOn) {
            GameControllerV2.Instance.current_decision_text = "After backing up your files," +
                "\nyou feel a bit more secure.";
        } else {
            GameControllerV2.Instance.current_decision_text = "You choose not to backup your files." +
                "\n Hopefully that doesn't backfire!";
        }
        // deactivate quiz, and display results
        GameControllerV2.Instance.scn_filebackup.SetActive(false);
        GameControllerV2.Instance.DisplayDecision();
    }

}
