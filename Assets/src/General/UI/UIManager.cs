using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    /// <summary>
    /// Image to fade when the scene is just loaded
    /// </summary>
    public Image fadingInitialImage;
    
    /// <summary>
    /// Image to opaque when quiting the scene
    /// </summary>
    public Image opaquingFinalImage;

    /// <summary>
    /// Image that opaques the game world elements
    /// </summary>
    public Image gameWorldImage;

    /// <summary>
    /// Game objects that contain the current amount of stars earned in the level
    /// </summary>
    public GameObject[] stars;

    /// <summary>
    /// Time that the initial image takes to dissapear
    /// </summary>
    public float initialFadingTime;
    
    /// <summary>
    /// Time the final image takes to opaque itself.
    /// </summary>
    public float finalOpaquingTime;
    
    /// <summary>
    /// Time the game world image takes to opaquate / fade
    /// </summary>
    public float gameWorldImageOpaquingTime;

    /// <summary>
    /// Panel containing slides with an inamge and text for the victory screen
    /// </summary>
    public UIDFadable victoryPanel;
    
    /// <summary>
    /// Panel containing slides with an inamge and text for the victory screen
    /// </summary>
    public UIDFadable defeatPanel;

    public Slider healthSlider;

    private Animator _animator;

    public String nextSceneName;

    void Start() {
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Updates the health shown in the player UI
    /// </summary>
    /// <param name="healthFactor"></param>
    public void UpdateHealth(float healthFactor) {
        healthSlider.value = healthFactor;
    }

    public void LoadScene(String sceneName) {
        nextSceneName = sceneName;
        ShowBlackScreen();
    }
    
    public void LoadNextScene() {
        SceneManager.LoadScene(nextSceneName);
    }

    /// <summary>
    /// Shows a black screen that is painted on top of the game
    /// </summary>
    public void ShowBlackScreen() {
        _animator.SetTrigger("FinalFade");
    }

    public IEnumerator ShowVictoryPanel(int starNumber) {
        for (int i = 0; i < starNumber; i++) {
            stars[i].SetActive(true);
        }
        yield return ShowPanel(victoryPanel);
    }
    
    public IEnumerator ShowDefeatPanel() {
        yield return ShowPanel(defeatPanel);
    }

    public IEnumerator ShowPanel(UIDFadable panel) {
        panel.gameObject.SetActive(true);
        yield return null;
    }
}
