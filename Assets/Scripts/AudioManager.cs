using System.Threading;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource sfxSource;
    public AudioSource loopingSource;

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
            if (!loopingSource.isPlaying)
            {
                loopingSource.clip = runningSFX;
                loopingSource.loop = true;
                loopingSource.Play();
            }
        }
        else
        {
            if (loopingSource.clip == runningSFX)
            {
                loopingSource.Stop();
            }
        }
    }

    public void PlayCheering()
    {
        sfxSource.PlayOneShot(cheerSFX);
    }
    public void PlayCountDown() => sfxSource.PlayOneShot(countDownSFX);
}
