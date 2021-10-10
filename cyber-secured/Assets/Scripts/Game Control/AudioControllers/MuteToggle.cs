﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteToggle : MonoBehaviour
{
    public void muteToggle(bool muted)
    {
        if (muted)
            AudioListener.volume = 0;
        else
            AudioListener.volume = 1;
    }
}
