using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollManager : MonoBehaviour {
    
    /// <summary>
    /// Content in which the modifiers are placed
    /// </summary>
    /// <returns></returns>
    public RectTransform container;


    public List<RectTransform> _objects;

    /// <summary>
    /// Flag that determines if the scroll manager must be hidden 
    /// </summary>
    public int showCount;

    protected virtual void Start() {
        _objects = new List<RectTransform>();
        
        // Objects already in the container are added as children
        for (int i = 0; i < container.childCount; i++) {
            AddLast(container.GetChild(i).GetComponent<RectTransform>());
        }
    }

    public void Show() {
        gameObject.SetActive(true);
        showCount++;
    }

    public void TryHide() {
        StartCoroutine(TryHide(0.1f));
    }

    public IEnumerator TryHide(float timeToHide) {
        showCount--;
        yield return new WaitForSeconds(timeToHide);
        if (showCount <= 0) {
            gameObject.SetActive(false);
        }
    }

    public void AddLast(RectTransform obj) {
        // Adds the modifier contained in the wrap, and emparents it to the content panel
        if (_objects.Count > 0) {
            PlaceBelow(obj, _objects[_objects.Count-1]);
        }
        else {
            Vector3 _upperContainerBorder = container.position + Vector3.up * container.rect.yMax;
            obj.transform.position = _upperContainerBorder;
            obj.anchoredPosition += Vector2.Scale(Vector2.down + Vector2.right, obj.rect.size)/2f;
        }
        obj.SetParent(container);
        _objects.Add(obj);
    }
    
    public void Remove(RectTransform obj) {
        int removedObjectIndex = _objects.IndexOf(obj);
        _objects.Remove(obj);
        for (int i = removedObjectIndex; i < _objects.Count; i++) {
            if (i == 0) {
                Vector3 _upperContainerBorder = container.position + Vector3.up * container.rect.yMax;
                _objects[i].transform.position = _upperContainerBorder;
                _objects[i].anchoredPosition += Vector2.Scale(Vector2.down + Vector2.right, obj.rect.size)/2f;
            }
            else {
                PlaceBelow(_objects[i],_objects[i-1]);
            }
        }
    }

    public void PlaceBelow(RectTransform obj1, RectTransform obj2) {
        // This only works for objects of the same y size
        obj1.transform.position = obj2.transform.position;
        obj1.anchoredPosition += Vector2.down * obj2.rect.size.y;
    }
}