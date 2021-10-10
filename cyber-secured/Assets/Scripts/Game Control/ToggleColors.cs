using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleColors : MonoBehaviour {
    public Image image;
    public Toggle toggle;

	public void OnToggleTrigger() {
        if(toggle.isOn) {
            image.GetComponent<Image>().color = new Color32(147, 255, 150, 255);
        } else {
            image.GetComponent<Image>().color = new Color32(242, 245, 222, 255);
        }
    }
}
