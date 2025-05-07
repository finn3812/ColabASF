
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiomanager : MonoBehaviour
{
    public static audiomanager Instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> sounds; // List to store AudioClips

    private Dictionary<string, AudioClip> clipDictionary = new Dictionary<string, AudioClip>();

    private bool canPlaySound = true; // Flag to control sound playback timing

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps AudioManager across scenes
        }
        else
        {
            Destroy(gameObject);
        }

        PopulateClipDictionary();
    }

    private void PopulateClipDictionary()
    {
        foreach (var clip in sounds)
        {
            clipDictionary[clip.name] = clip;
        }
    }

    public void PlaySound(string soundName, float volume = 1f)
    {
        if (!canPlaySound)
        {
            Debug.Log("PlaySound is on cooldown.");
            return;
        }

        if (clipDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            audioSource.PlayOneShot(clip, volume);
            StartCoroutine(SoundCooldown());
        }
        else
        {
            Debug.LogWarning($"Sound '{soundName}' not found in AudioManager!");
        }
    }

    private IEnumerator SoundCooldown()
    {
        canPlaySound = false; // Set the flag to false
        yield return new WaitForSeconds(1f); // Wait for 1 second
        canPlaySound = true; // Reset the flag
    }

    public void PlaySoundWithFadeIn(string soundName, float fadeDuration, float volume = 1f)
    {
        if (clipDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            StartCoroutine(FadeInSound(clip, fadeDuration, volume));
        }
        else
        {
            Debug.LogWarning($"Sound '{soundName}' not found in AudioManager!");
        }
    }

    private IEnumerator FadeInSound(AudioClip clip, float duration, float targetVolume)
    {
        audioSource.clip = clip;
        audioSource.volume = 0;
        audioSource.Play();

        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            audioSource.volume = Mathf.Lerp(0, targetVolume, (Time.time - startTime) / duration);
            yield return null;
        }
        audioSource.volume = targetVolume;
    }

    public void FadeOutAndStop(float fadeDuration)
    {
        StartCoroutine(FadeOutSound(fadeDuration));
    }

    private IEnumerator FadeOutSound(float duration)
    {
        float startVolume = audioSource.volume;
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, (Time.time - startTime) / duration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public void PlaySoundForLimitedTime(string soundName, float duration, float volume = 1f)
    {
        if (clipDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            StartCoroutine(PlayAndStopSound(clip, duration, volume));
        }
        else
        {
            Debug.LogWarning($"Sound '{soundName}' not found in AudioManager!");
        }
    }

    private IEnumerator PlayAndStopSound(AudioClip clip, float duration, float volume)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        yield return new WaitForSeconds(duration);

        audioSource.Stop();
    }
}