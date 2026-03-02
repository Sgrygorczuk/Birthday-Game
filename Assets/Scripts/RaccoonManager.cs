using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaccoonManager : MonoBehaviour
{
    public static RaccoonManager Instance;
    public List<GameObject> raccoons;
    public float fadeInDuration = 0.2f;
    public float fadeOutDuration = 1.8f;

    private Coroutine activeRoutine;
    private GameObject lastActiveRaccoon;
    private Dictionary<Graphic, Color> lastOriginalColors = new Dictionary<Graphic, Color>();

    private void Awake() {
        Instance = this;
        GameObject parentObj = GameObject.Find("RaccoonMemes");

        if (parentObj != null)
        {
            foreach (Transform child in parentObj.transform)
            {
                // Add only if the child is currently inactive
                if (!child.gameObject.activeSelf)
                {
                    raccoons.Add(child.gameObject);
                }
            }
        }
        else
        {
            Debug.LogError("RaccoonMemes object not found in scene.");
        }
    }
    
    public void ShowRaccoon()
    {
        if (raccoons.Count == 0) return;

        // 1. Interrupt previous routine if it's running
        if (activeRoutine != null)
        {
            StopCoroutine(activeRoutine);
            ResetPreviousRaccoon();
        }

        // 2. Pick new raccoon
        int randomIndex = Random.Range(0, raccoons.Count);
        lastActiveRaccoon = raccoons[randomIndex];

        activeRoutine = StartCoroutine(RaccoonRoutine(lastActiveRaccoon));
    }

    private void ResetPreviousRaccoon()
    {
        if (lastActiveRaccoon == null) return;

        // Restore original colors immediately before hiding
        foreach (var entry in lastOriginalColors)
        {
            if (entry.Key != null) entry.Key.color = entry.Value;
        }
        
        lastActiveRaccoon.SetActive(false);
        lastOriginalColors.Clear();
    }

    private IEnumerator RaccoonRoutine(GameObject raccoon)
    {
        Graphic[] graphics = raccoon.GetComponentsInChildren<Graphic>(true);
        lastOriginalColors.Clear();

        foreach (Graphic g in graphics)
        {
            lastOriginalColors.Add(g, g.color);
            g.color = new Color(0, 0, 0, 0);
        }

        AudioManager.Instance.PlaySound("KillEnemy");
        raccoon.SetActive(true);

        // Fade In
        yield return StartCoroutine(FadeGraphics(graphics, lastOriginalColors, fadeInDuration, true));

        // Fade Out
        yield return StartCoroutine(FadeGraphics(graphics, lastOriginalColors, fadeOutDuration, false));

        // Final Cleanup
        ResetPreviousRaccoon();
        activeRoutine = null;
    }

    private IEnumerator FadeGraphics(Graphic[] graphics, Dictionary<Graphic, Color> originals, float duration, bool fadeIn)
    {
        float elapsed = 0f;
        Color transparent = new Color(0, 0, 0, 0);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;

            foreach (Graphic g in graphics)
            {
                if (g == null) continue;
                g.color = fadeIn ? Color.Lerp(transparent, originals[g], progress) : Color.Lerp(originals[g], transparent, progress);
            }
            yield return null;
        }
    }
}

