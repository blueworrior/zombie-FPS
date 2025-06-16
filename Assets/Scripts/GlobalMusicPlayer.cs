using UnityEngine;

public class GlobalMusicPlayer : MonoBehaviour
{
    private static GlobalMusicPlayer instance;
    private AudioSource audioSource;

    void Awake()
    {
        // Keep only one instance
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        audioSource.Play();
    }

    void Update()
    {
        // Update volume in real time from PlayerPrefs
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
    }
}
