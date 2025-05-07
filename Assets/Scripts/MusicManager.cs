using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Music Settings")]
    public AudioClip[] musicTracks;
    private AudioSource audioSource;
    private float fadeDuration = 2.0f;
    private string currentScene = "";

    void Awake()
    {
        // Singleton logic
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        SetupAudioSource();

        SceneManager.sceneLoaded += OnSceneLoaded;

        currentScene = SceneManager.GetActiveScene().name;
        PlayRandomTrack(); // Initial play
    }

    void SetupAudioSource()
    {
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = 0.8f;
        audioSource.spatialBlend = 0f; // 2D sound
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != currentScene)
        {
            currentScene = scene.name;
            StartCoroutine(FadeOutAndPlayNewTrack());
        }
    }

    IEnumerator FadeOutAndPlayNewTrack()
    {
        float startVolume = audioSource.volume;

        // Fade out
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        PlayRandomTrack();

        // Fade in
        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.volume = startVolume; // Just in case it overshoots
    }

    public void PlayRandomTrack()
    {
        if (musicTracks.Length == 0)
        {
            Debug.LogWarning("No music tracks assigned.");
            return;
        }

        AudioClip chosenTrack = musicTracks[Random.Range(0, musicTracks.Length)];
        audioSource.clip = chosenTrack;
        audioSource.Play();
    }

    public void PlaySpecificTrack(int index)
    {
        if (index >= 0 && index < musicTracks.Length)
        {
            audioSource.clip = musicTracks[index];
            audioSource.Play();
        }
    }
}
