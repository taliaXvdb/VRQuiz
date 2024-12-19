using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioMixerGroup AudioMixerGroup;
    public AudioClip PlaySound;
    public AudioClip StopSound;

    void Start()
    {
        if (AudioSource == null)
        {
            AudioSource = GetComponent<AudioSource>();
        }
        AudioSource.outputAudioMixerGroup = AudioMixerGroup;
    }

    public void PlayPlaySound()
    {
        AudioSource.clip = PlaySound;
        AudioSource.Play();
    }

    public void PlayStopSound()
    {
        AudioSource.clip = StopSound;
        AudioSource.Play();
    }
}
