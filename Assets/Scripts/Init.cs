using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        Screen.SetResolution(600, 800, false);
    }
}
