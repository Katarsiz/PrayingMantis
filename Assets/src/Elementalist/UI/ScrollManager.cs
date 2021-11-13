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

    private Vector3 _upperContainerBorder;

    private List<RectTransform> _objects;

    public int count;

    private void Start() {
        _objects = new List<RectTransform>();
        _upperContainerBorder = container.position;
        _upperContainerBorder += Vector3.up * container.rect.yMax;
        // Objects already in the container are added as children
        for (int i = 0; i < container.childCount; i++) {
            AddLast(container.GetChild(i).GetComponent<RectTransform>());
        }
    }

    public void AddLast(RectTransform obj) {
        // Adds the modifier contained in the wrap, and emparents it to the content panel
        if (_objects.Count > 0) {
            PlaceBelow(obj, _objects[_objects.Count-1]);
        }
        else {
            obj.transform.position = _upperContainerBorder;
            obj.anchoredPosition += Vector2.Scale(Vector2.down + Vector2.right, obj.rect.size)/2f;
        }
        obj.SetParent(container);
        _objects.Add(obj);
        count = _objects.Count;
    }
    
    public void Remove(RectTransform obj) {
        int removedObjectIndex = _objects.IndexOf(obj);
        _objects.Remove(obj);
        count = _objects.Count;
        // All objects below the removed object are re arranged
        if (_objects.Count == 0) {
            return;
        }
        for (int i = removedObjectIndex; i < _objects.Count; i++) {
            if (i == 0) {
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
