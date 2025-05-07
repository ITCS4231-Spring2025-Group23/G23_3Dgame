using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioClip[] musicTracks;  // Drag multiple tracks into this array in Inspector

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.playOnAwake = false;
            audioSource.volume = 0.5f;

            // Pick a random track from the list
            if (musicTracks.Length > 0)
            {
                AudioClip selected = musicTracks[Random.Range(0, musicTracks.Length)];
                audioSource.clip = selected;
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("MusicManager: No tracks assigned!");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
