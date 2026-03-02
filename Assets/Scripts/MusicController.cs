using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour
{
    public static MusicController Instance;
    public AudioSource musicSource;
    public float defaultFadeDuration = 1.5f;
    public float targetVolume = 1.0f;

    private Coroutine fadeCoroutine;

    private void Awake() {
        Instance = this;
        FadeIn();
    }

    public void FadeIn()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeMusic(0f, targetVolume, defaultFadeDuration));
    }

    public void FadeOut()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeMusic(musicSource.volume, 0f, defaultFadeDuration));
    }

    private IEnumerator FadeMusic(float startVolume, float endVolume, float duration)
    {
        if (!musicSource.isPlaying) musicSource.Play();

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, endVolume, elapsed / duration);
            yield return null;
        }

        musicSource.volume = endVolume;
        if (endVolume <= 0) musicSource.Stop();
        
        fadeCoroutine = null;
    }
}