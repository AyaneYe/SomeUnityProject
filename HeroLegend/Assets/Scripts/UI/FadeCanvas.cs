using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeCanvas : MonoBehaviour
{
    [Header("ÊÂ¼þ¼àÌý")]
    public FadeEventSO fadeEvent;
    public Image FadeImage;

    void OnEnable()
    {
        fadeEvent.OnEventRaised += onFadeEvent;
    }

    private void OnDisable()
    {
        fadeEvent.OnEventRaised -= onFadeEvent;
    }

    private void onFadeEvent(Color targetColor, float duration, bool fadeIn)
    {
        FadeImage.DOBlendableColor(targetColor, duration);
    }
}
