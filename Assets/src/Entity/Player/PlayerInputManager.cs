using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PlayerInputManager : MonoBehaviour {

    public ModifierWrap activeModifierWrap;

    public Bug activeBug;

    /// <summary>
    /// Canvas where the object manipulation is done
    /// </summary>
    public Canvas mainCanvas;

    public ScrollManager modifiersContainer;
    
    /// <summary>
    /// Layer containing modifiable objects
    /// </summary>
    public LayerMask modifiablesLayer;

    public Camera activeCamera;
    
    public CinemachineVirtualCamera freeLookCMVC;
    
    /// <summary>
    /// Speed at which the camera pans when getting near the camera borders
    /// </summary>
    public float panSpeed;

    private Modifiable _hoveredModifiableObject;

    private LevelManager _levelManager;
    
    private bool _wrapFollowsMouse;

    private Vector2 _panDirection;

    private Transform _cameraTransform;

    public void Initialize(LevelManager manager) {
        _levelManager = manager;
        _cameraTransform = freeLookCMVC.transform;
    }

    /// <summary>
    /// Image moves to wherever the mouse is being moved to
    /// </summary>
    private void Update() {
        if (activeModifierWrap) {
            if (_wrapFollowsMouse) {
                activeModifierWrap.rectTransform.position = Input.mousePosition;
            }
            
            if (Input.GetMouseButtonUp(0)) {
                OnLMBUp();
            }
            
            if (Input.GetMouseButtonDown(0)) {
                OnLMBDown();
            }
            
            if (Input.GetMouseButtonDown(1)) {
                OnRMBDown();
            }
        }
        // Get the active modifiable
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(activeCamera.ScreenToWorldPoint(Input.mousePosition).x,
            activeCamera.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f, modifiablesLayer);
        if (!_levelManager.simulationRunning && hit.collider) {
            if (hit.collider.TryGetComponent(out Modifiable modifiable)) {
                // Modifiable hovered object is updated
                if (_hoveredModifiableObject != modifiable) {
                    _hoveredModifiableObject = modifiable;
                    _hoveredModifiableObject.OnMouseHovered();
                    _hoveredModifiableObject.ShowModifiers();
                }
            }
            else {
                if (_hoveredModifiableObject) {
                    _hoveredModifiableObject.OnMouseExited();
                    _hoveredModifiableObject.HideModifiers();
                    _hoveredModifiableObject = null;
                }
            }
        } else {
            if (_hoveredModifiableObject) {
                _hoveredModifiableObject.OnMouseExited();
                _hoveredModifiableObject.HideModifiers();
                _hoveredModifiableObject = null;
            }
        }
        
        // Move the camera if too close to border, only use the free look if the
        if (!_levelManager.simulationRunning) {
            _panDirection = PanDirection(Input.mousePosition.x, Input.mousePosition.y);
            freeLookCMVC.transform.position = Vector3.Lerp(_cameraTransform.position,
                _cameraTransform.position + (Vector3)_panDirection * panSpeed,
                0.1f);
            
        }
    }

    public Vector2 PanDirection(float x, float y) {
        Vector2 result = Vector2.zero;
        if (y >= Screen.height * 0.95) {
            result.y = 1;
        }
        else if (y < Screen.height * 0.05f) {
            result.y = -1;
        }
        if (x >= Screen.width * 0.95) {
            result.x = 1;
        }
        else if (x < Screen.width * 0.05f) {
            result.x = -1;
        }
        return result;
    }
    
    /// <summary>
    /// Tries to add the active modifier to the target bug
    /// </summary>
    /// <param name="target"></param>
    public void TryAddModifier(Bug target) {
        if (target) {
            target.AddModifierWrap(activeModifierWrap);
        }
        else {
            Debug.Log("ERROR : Modifier doesn't exist");
        }
    }

    /// <summary>
    /// Function called when the mouse is hovered over a bug container
    /// </summary>
    /// <param name="b"></param>
    public void OnMouseHoverBug(Bug b) {
        SetActiveBug(b);
        activeBug.ShowModifiers();
    }
    
    public void OnMouseExitBug() {
        if (activeBug) {
            activeBug.HideModifiers();
            ResetActiveBug();
        }
    }

    public void SetActiveScrollManager(ScrollManager newContainer) {
        if (!modifiersContainer) {
            modifiersContainer = newContainer;
        }
    }

    public void ResetActiveScrollManager() {
        modifiersContainer = null;
    }
    
    public void SetActiveModifier(ModifierWrap m) {
        // Wrap is only set as active if there isn't already an active wrap
        if (!activeModifierWrap) {
            activeModifierWrap = m;
        }
    }
    
    public void ResetActiveModifierWrap() {
        // Wrap is reseted only if it isn't following the mouse
        if (!_wrapFollowsMouse) {
            activeModifierWrap = null;
        }
    }

    public void SetActiveBug(Bug b) {
        activeBug = b;
    }
    
    public void ResetActiveBug() {
        activeBug = null;
    }

    public void OnLMBUp() {
        // Sets the active modifier and active bug if it exists
        if (activeModifierWrap) {
            _wrapFollowsMouse = false;
            if (activeBug) {
                activeBug.AddModifierWrap(activeModifierWrap);
            }
            else {
                modifiersContainer.AddLast(activeModifierWrap.rectTransform);
            }
            ResetActiveModifierWrap();
        }
    }
    
    public void OnLMBDown() {
        // Makes the wrap follow the mouse and un parents it from the container 
        if (activeModifierWrap) {
            activeModifierWrap.transform.SetParent(mainCanvas.transform);
            activeModifierWrap.transform.SetSiblingIndex(0);
            modifiersContainer.Remove(activeModifierWrap.rectTransform);
            _wrapFollowsMouse = true;
        }
        // Show modifiers of the object that is being examined only if simulation is not active
        if (_hoveredModifiableObject) {
            //_hoveredModifiableObject.ShowModifiers();
        }
    }
    
    public void OnRMBDown() {
        // Tries to remove the wrap from the bug
        if (activeModifierWrap) {
            Bug b = activeModifierWrap.GetBug();
            if (b) {
                b.TryRemoveModifierWrap(activeModifierWrap);
                modifiersContainer.AddLast(activeModifierWrap.rectTransform);
            }
        }
    }

}
