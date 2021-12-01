using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : MonoBehaviour {

    /// <summary>
    /// Amount of time the logo is shown to the player
    /// </summary>
    public float logoShowTime;

    public UIManager uiManager;

    public String mainMenuSceneName;
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(LoadSceneAfter(logoShowTime, mainMenuSceneName));
    }

    public IEnumerator LoadSceneAfter(float s, String sceneName) {
        yield return new WaitForSeconds(s);
        uiManager.LoadScene(sceneName);
    }
}
