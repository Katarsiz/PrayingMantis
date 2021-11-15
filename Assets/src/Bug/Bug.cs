﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Bugs are a set of modifiers that are applied when a function is called
/// </summary>
public class Bug : MonoBehaviour {

    public List<ModifierWrap> modifiers;

    public int maxModificators;

    public ScrollManager scrollManager;

    /// <summary>
    /// Animator that controls the function containers
    /// </summary>
    public Animator containerAnimator;

    public void TryAddModifierWrap(ModifierWrap modifierWrap) {
        if (modifiers.Count < maxModificators) {
            AddModifierWrap(modifierWrap);
        }
    }

    public void AddModifierWrap(ModifierWrap modifierWrap) {
        scrollManager.AddLast(modifierWrap.rectTransform);
        modifiers.Add(modifierWrap);
        modifierWrap.SetBug(this);
        containerAnimator.SetInteger("ModifierCount",modifiers.Count);
    }
    
    public void TryRemoveModifierWrap(ModifierWrap modifierWrap) {
        if (modifiers.Contains(modifierWrap)) {
            RemoveModifierWrap(modifierWrap);
        }
    }

    public void ShowModifiers() {
        containerAnimator.SetBool("ShowModifiers",true);
    }
    
    public void HideModifiers() {
        containerAnimator.SetBool("ShowModifiers",false);
    }

    public void RemoveModifierWrap(ModifierWrap modifierWrap) {
        scrollManager.Remove(modifierWrap.rectTransform);
        modifiers.Remove(modifierWrap);
        modifierWrap.SetBug(null);
        containerAnimator.SetInteger("ModifierCount",modifiers.Count);
    }

    public void ApplyAllModifiers() {
        foreach (ModifierWrap wrap in modifiers) {
            StartCoroutine(ApplyModifier(wrap.modifier));
        }
    }

    public IEnumerator ApplyModifier(Modifier m) {
        yield return m.Apply();
    }

    public void pprint(string s) {
        Debug.Log(s);
    }
}