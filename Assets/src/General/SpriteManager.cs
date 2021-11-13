using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour {
    
    /// <summary>
    /// Fades all the sprites contained in the gameobject and it's children in the given time
    /// </summary>
    /// <param name="s"></param>
    /// <param name="fadingTime"></param>
    /// <returns></returns>
    public static IEnumerator FadeAllSprites(GameObject g, float fadingTime) {
        foreach (SpriteRenderer sr in g.GetComponentsInChildren<SpriteRenderer>()) {
            yield return InterpolateSpriteColor(sr, new Color(0, 0, 0, 0), fadingTime);
        }

        yield return null;
    }
    

    /// <summary>
    /// Fades the sprite renderer sprite in the given time
    /// </summary>
    /// <param name="s"></param>
    /// <param name="fadingTime"></param>
    /// <returns></returns>
    public static IEnumerator FadeSprite(SpriteRenderer s, float fadingTime) {
        return InterpolateSpriteColor(s, new Color(0, 0, 0, 0), fadingTime);
    }
    
    /// <summary>
    /// Opaques the sprite renderer sprite in the given time
    /// </summary>
    /// <param name="s"></param>
    /// <param name="fadingTime"></param>
    /// <returns></returns>
    public static IEnumerator OpaqueSprite(SpriteRenderer s, float fadingTime) {
        return InterpolateSpriteColor(s, Color.white, fadingTime);
    }

    /// <summary>
    /// Interpolates the sprite renderer color to the targetColor in interpolateTime units of time
    /// </summary>
    /// <param name="s"></param>
    /// <param name="targetColor"></param>
    /// <param name="interpolationTime"></param>
    /// <returns></returns>
    public static IEnumerator InterpolateSpriteColor(SpriteRenderer s, Color targetColor, float interpolationTime) {
        Color initialColor = s.color;
        float currentTime = 0;
        while (currentTime<interpolationTime) {
            currentTime += Time.deltaTime;
            s.color = Color.LerpUnclamped(initialColor, targetColor, currentTime / interpolationTime);
            yield return new WaitForEndOfFrame();
        }
    }
}