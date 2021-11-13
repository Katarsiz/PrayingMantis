using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static int phaseToLoad;

    public Transform[] checkpoints;
    
    public MainCharacter mainCharacter;

    private UIManager uiManager;
    
    public PlayerInputManager playerInputManager;

    public String nextScene;

    private String _currentScene;

    /// <summary>
    /// Is the simulation currently running or not
    /// </summary>
    public bool simulationRunning;
    
    private bool _sceneTransitionTriggered;

    private void Awake() {
        uiManager = GetComponentInChildren<UIManager>();
        uiManager.UpdateHealth((float)mainCharacter.health/mainCharacter.maxHealth);
        simulationRunning = false;
        playerInputManager.Initialize(this);
        _currentScene = SceneManager.GetActiveScene().name;
    }

    private void Start() {
        mainCharacter.Initialize(this);
        PauseSimulation();
    }

    public IEnumerator LoadScene(int newScene) {
        if (!_sceneTransitionTriggered) {
            _sceneTransitionTriggered = true;
            yield return uiManager.OpaqueFinalImage();
            SceneManager.LoadScene(newScene);
        }
    }

    public void OnHealthChange() {
        float healthFraction = (float)mainCharacter.health / mainCharacter.maxHealth;
        uiManager.UpdateHealth(healthFraction);
    }

    public void SetAndLoadScene(int sceneIndex) {
        StartCoroutine(LoadScene(sceneIndex));
    }

    public void ReloadCurrentScene() {
        uiManager.LoadScene(_currentScene);
    }

    public void PauseSimulation() {
        simulationRunning = false;
        mainCharacter.FreezeMovement();
    }

    public void ResumeSimulation() {
        simulationRunning = true;
        mainCharacter.UnFreezeMovement();
    }

    public void OnLevelLost() {
        StartCoroutine(ShowLevelLostInterface());
    }
    
    public void OnLevelCompleted() {
        phaseToLoad = 0;
        StartCoroutine(ShowLevelCompletionInterface());
    }

    public IEnumerator ShowLevelCompletionInterface() {
        yield return uiManager.OpaqueGameWorldImage();
        yield return uiManager.ShowVictoryPanel();
    }
    
    public IEnumerator ShowLevelLostInterface() {
        yield return uiManager.OpaqueGameWorldImage();
        yield return uiManager.ShowDefeatPanel();
    }
}
