using DG.Tweening;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    [Header("Animation settings")]
    public float scale = 1.2f;
    public float duration = 0.1f;
    public Ease ease;

    private int clicks = 0;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseDown() 
    {
        clicks++;
        UiManager.instance.UpdateClicks(clicks);
        transform
            .DOScale(1, duration)
            .ChangeStartValue(scale * Vector3.one)
            .SetEase(ease); // ease - how the animation will be played
            // .SetLoops(2, LoopType.Yoyo);

        audioSource.Play();
    }
}