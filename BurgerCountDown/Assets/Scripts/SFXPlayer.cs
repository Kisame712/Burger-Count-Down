using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public static SFXPlayer Instance { private set; get; }

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one instance of SFXPlayer - " + transform);
            Destroy(gameObject);
            return;
        }
        Instance = this;

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
