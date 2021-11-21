using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static int phaseToLoad;

    public Transform[] checkpoints;
    
    public AIFixedCommandChar mainCharacter;

    private UIManager uiManager;
    
    public PlayerInputManager playerInputManager;
    
    public CinemachineVirtualCamera freeLookCMVC;

    public CinemachineVirtualCamera mainCharacterCMVC;

    public String nextSceneName;

    private String _currentScene;

    /// <summary>
    /// Is the simulation currently running or not
    /// </summary>
    public bool simulationRunning;

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
    
    public void LoadNextScene() {
        uiManager.LoadScene(nextSceneName);
    }
    
    public void RestartLevel() {
        nextSceneName = SceneManager.GetActiveScene().name;
        uiManager.LoadScene(nextSceneName);
    }

    public void OnHealthChange() {
        float healthFraction = (float)mainCharacter.health / mainCharacter.maxHealth;
        uiManager.UpdateHealth(healthFraction);
    }

    public void ReloadCurrentScene() {
        uiManager.LoadScene(_currentScene);
    }

    public void PauseSimulation() {
        mainCharacterCMVC.gameObject.SetActive(false);
        freeLookCMVC.gameObject.SetActive(true);
        simulationRunning = false;
        mainCharacter.OnSimulationEnd();
    }

    public void ResumeSimulation() {
        mainCharacterCMVC.gameObject.SetActive(true);
        freeLookCMVC.gameObject.SetActive(false);
        simulationRunning = true;
        mainCharacter.OnSimulationStart();
    }

    public void OnLevelLost() {
        StartCoroutine(ShowLevelLostInterface());
    }
    
    public void OnLevelWon() {
        StartCoroutine(ShowLevelCompletionInterface());
    }
    
    public void OnLevelCompleted() {
        PauseSimulation();
        phaseToLoad = 0;
        if (mainCharacter.health < 3) {
            OnLevelWon();
        } else {
            OnLevelLost();
        }
    }

    public IEnumerator ShowLevelCompletionInterface() {
        int starNumber = mainCharacter.maxHealth - mainCharacter.health;
        yield return uiManager.ShowVictoryPanel(starNumber);
    }
    
    public IEnumerator ShowLevelLostInterface() {
        yield return uiManager.ShowDefeatPanel();
    }
}
