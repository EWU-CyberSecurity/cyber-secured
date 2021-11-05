using UnityEngine;

public class MuteToggle : MonoBehaviour
{
    public bool muted = false;
    public void Toggle()
    {
        muted = !muted;
        if (muted)
            AudioListener.volume = 0;
        else
            AudioListener.volume = 1;
    }

    public bool getMuted()
    {
        return this.muted;
    } 
}
