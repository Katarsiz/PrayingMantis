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

    public String nextSceneName;

    public Slider healthSlider;

    void Start() {
        Task fadeTask = VanishInitialImage();
    }

    /// <summary>
    /// Updates the health shown in the player UI
    /// </summary>
    /// <param name="healthFactor"></param>
    public void UpdateHealth(float healthFactor) {
        healthSlider.value = healthFactor;
    }

    public async Task VanishInitialImage() {
        fadingInitialImage.gameObject.SetActive(true);
        await UIImageManager.InterpolateUImageColor(fadingInitialImage, new Color(0, 0, 0, 0), initialFadingTime);
        fadingInitialImage.gameObject.SetActive(false);
    }
    
    public IEnumerator OpaqueFinalImage() {
        opaquingFinalImage.gameObject.SetActive(true);
        yield return UIImageManager.InterpolateUImageColor(opaquingFinalImage, Color.black, finalOpaquingTime);
    }
    
    public IEnumerator OpaqueGameWorldImage() {
        gameWorldImage.gameObject.SetActive(true);
        yield return UIImageManager.InterpolateUImageColor(gameWorldImage, Color.black, gameWorldImageOpaquingTime);
    }
    
    public IEnumerator VanishGameWorldImage() {
        yield return UIImageManager.InterpolateUImageColor(gameWorldImage, Color.black, gameWorldImageOpaquingTime);
        gameWorldImage.gameObject.SetActive(false);
    }

    public IEnumerator ShowVictoryPanel() {
        yield return ShowPanel(victoryPanel);
    }
    
    public IEnumerator ShowDefeatPanel() {
        yield return ShowPanel(defeatPanel);
    }

    public IEnumerator ShowPanel(UIDFadable panel) {
        panel.gameObject.SetActive(true);
        yield return null;
    }
    
    public void LoadScene(String name) {
        nextSceneName = name;
        StartCoroutine(LoadNexSceneCor());
    }

    public IEnumerator LoadNexSceneCor() {
        yield return OpaqueFinalImage();
        SceneManager.LoadScene(nextSceneName);
    }
}
