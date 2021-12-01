using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIImageManager : MonoBehaviour {
    
    /// <summary>
    /// Fades the image sprite in the given time
    /// </summary>
    /// <param name="i"></param>
    /// <param name="fadingTime"></param>
    /// <returns></returns>
    public static async Task FadeImage(Image i, float fadingTime) {
        await InterpolateUImageColor(i, new Color(0, 0, 0, 0), fadingTime);
    }
    
    public static async Task InterpolateUImageColor(Image i, Color targetColor, float interpolationTime) {
        float deltaColor = 0.03f;
        Color initialColor = i.color;
        float currentTime = 0;
        while (currentTime<interpolationTime) {
            currentTime += deltaColor;
            i.color = Color.Lerp(initialColor, targetColor, currentTime / interpolationTime);
            await Task.Delay(Mathf.RoundToInt(deltaColor * 1000));
        }
    }
    
    /// <summary>
    /// Opaques the sprite renderer sprite in the given time
    /// </summary>
    /// <param name="s"></param>
    /// <param name="fadingTime"></param>
    /// <returns></returns>
    public static async Task OpaqueImage(Image i, float fadingTime) {
        await InterpolateUImageColor(i, Color.white, fadingTime);
    }
}
