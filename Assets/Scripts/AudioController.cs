using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController audioController;

    private void Awake()
    {
        if (audioController == null)
        {
            audioController = this;
        }
    }

    public void PlaySound(AudioClip clip, float volume = 1)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.loop = false;
        source.volume = volume;
        source.Play();
        StartCoroutine(DestroyAfter(source, clip.length));
    }

    IEnumerator DestroyAfter(Component obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(obj);
    }
}
