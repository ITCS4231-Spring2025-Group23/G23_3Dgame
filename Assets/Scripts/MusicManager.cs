using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] musicTracks;
    private AudioSource audioSource;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        PlayRandomTrack();
    }

    void PlayRandomTrack()
    {
        if (musicTracks.Length > 0)
        {
            audioSource.clip = musicTracks[Random.Range(0, musicTracks.Length)];
            audioSource.Play();
        }
    }
}
