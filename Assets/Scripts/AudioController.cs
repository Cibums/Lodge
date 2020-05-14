using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController audioController;

    private void Awake()
    {
        //Creates instance of class
        if (audioController == null)
        {
            audioController = this;
        }
    }

    /// <summary>
    /// Plays sound
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="volume"></param>
    public void PlaySound(AudioClip clip, float volume = 1)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.loop = false;
        source.volume = volume;
        source.Play();
        StartCoroutine(DestroyAfter(source, clip.length));
    }

    /// <summary>
    /// Destroys specific component after a specfici amount of seconds
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="seconds"></param>
    /// <returns></returns>
    private IEnumerator DestroyAfter(Component obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(obj);
    }
}