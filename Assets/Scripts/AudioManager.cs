using System.Threading;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource sfxSource;

    public AudioClip runningSFX;
    public AudioClip cheerSFX;
    public AudioClip countDownSFX;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlayRunning(bool play)
    {
        if (play)
        {
            if (!sfxSource.isPlaying)
            {
                sfxSource.clip = runningSFX;
                sfxSource.loop = true;
                sfxSource.Play();
            }
        }
        else
        {
            sfxSource.Stop();
        }
    }

    public void PlayCheering() => sfxSource.PlayOneShot(cheerSFX);
    public void PlayCountDown() => sfxSource.PlayOneShot(countDownSFX);
}
