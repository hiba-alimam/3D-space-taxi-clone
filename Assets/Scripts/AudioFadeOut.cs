using UnityEngine;
using System.Collections;

public static class AudioFadeOut
{

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

}
/* 
Use example: 

StartCoroutine(AudioFadeOut.FadeOut(sound_open, 0.1f));

or:

public AudioSource Sound1;

IEnumerator fadeSound1 = AudioFadeOut.FadeOut(Sound1, 0.5f);
StartCoroutine(fadeSound1);
StopCoroutine(fadeSound1);
*/