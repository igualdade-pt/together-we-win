using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AmbientManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixerSnapshot ambientOn;
    [SerializeField]
    private AudioMixerSnapshot ambientOff;

    [SerializeField]
    private AudioMixerSnapshot ambientLow;
    [SerializeField]
    private AudioMixerSnapshot ambientUp;

    [SerializeField]
    float fadeTime = 8f;
    [SerializeField]
    float overlap = 6f;

    [SerializeField]
    float fadeTimeStopAmbient = 3f;

    [SerializeField]
    float fadeTimeResumeAmbient = 3f;

    [SerializeField]
    private float fadeTimeLowAmbient = 1f;

    [SerializeField]
    private float fadeTimeUpAmbient = 0.5f;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        ambientLow.TransitionTo(0);
    }


    public void StopAmbient()
    {
        StartCoroutine(StopMusicByFade());
    }

    private IEnumerator StopMusicByFade()
    {
        ambientOff.TransitionTo(fadeTimeStopAmbient);

        yield return new WaitForSeconds(fadeTimeStopAmbient);

        audioSource.Pause();
       
    }

    public void ResumeMusic()
    {
        ambientOn.TransitionTo(fadeTimeResumeAmbient);

        audioSource.UnPause();


        /*StartCoroutine(ResumeMusicByFade());*/
    }


    public void LowMusic()
    {
        ambientLow.TransitionTo(fadeTimeLowAmbient);
    }

    public void UpMusic()
    {
        ambientUp.TransitionTo(fadeTimeUpAmbient);

        /*StartCoroutine(ResumeMusicByFade());*/
    }
}
