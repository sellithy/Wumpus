using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays a music file by name
/// </summary>
public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    private void Awake()
    {
        Instance = this;
    }

    //The current audioSource
    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private List<AudioClip> musicFiles;

    [SerializeField]
    private List<AudioClip> effectFiles;

    /// <summary>
    /// Plays the music
    /// </summary>
    /// <param name="name">The name of the file</param>
    /// <param name="volume">The volume from 0 to 1</param>
    /// <param name="loop">Should it be looped</param>
    public void PlayMusic(string name, float volume = 1, bool loop = false, int playbackStart = 0)
    {
        //Go through every file, and find the one that matches the name string
        foreach (AudioClip a in musicFiles)
        {
            if (a.name == name)
            {
                //Set the values
                source.clip = a;
                source.volume = volume;
                source.loop = loop;
                source.time = playbackStart;

                //Play the sound and break out of the loop
                source.Play();
                break;
            }
        }
    }

    /// <summary>
    /// Play a sound effect
    /// </summary>
    /// <param name="effectName">The name of the file</param>
    public void PlaySoundEffect(string effectName)
    {
        //Go through every file, and find the one that matches the name string
        foreach (AudioClip a in effectFiles)
        {
            if (a.name == effectName)
            {
                AudioSource.PlayClipAtPoint(a, Camera.main.transform.position, 1f);
                break;
            }
        }
    }
}