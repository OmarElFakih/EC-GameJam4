﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaders : MonoBehaviour
{

    public void SceneLoad(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
