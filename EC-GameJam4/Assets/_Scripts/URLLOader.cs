using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLLOader : MonoBehaviour
{
    //public string URL;

    public void LadURL(string URL)
    {
        Application.OpenURL(URL);
    }
}
