using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUrl : MonoBehaviour
{
    public string url;


    public void HandleAction()
    {
        Application.OpenURL(url);
    }
}
