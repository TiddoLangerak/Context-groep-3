using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Util;

/// <summary>
/// Implements the audio functionality of the game.
/// </summary>
public class AudioBehaviour : MonoBehaviour
{
    /// <summary>
    /// Contains the mapping of a string to the corresponding audio
    /// </summary>
    private Dictionary<string, Tuple<AudioSource, status>> audio;

    /// <summary>
    /// The status of an audio source
    /// </summary>
    public enum status
    {
        PLAYING,
        STOPPED,
        CRASHING
    }

    /// <summary>
    /// Initializes this object. (This function is called automatically by Unity)
    /// </summary>
    void Start()
    {
        audio = new Dictionary<string, Tuple<AudioSource, status>>();
        var sources = GetComponents<AudioSource>();
        foreach (var s in sources)
        {
            audio.Add(s.clip.name, new Tuple<AudioSource, status>(s, status.STOPPED));
        }
    }

    /// <summary>
    /// Returns the status of the clip
    /// </summary>
    /// <param name="clip">The clip</param>
    /// <returns></returns>
    public status Status(string clip)
    {
        return audio[clip].Second;
    }

    /// <summary>
    /// Plays the audio
    /// </summary>
    /// <param name="clip">The audio to be played</param>
    public void Play(string clip)
    {
        audio[clip].First.Play();
        audio[clip].Second = status.PLAYING;
    }

    /// <summary>
    /// Stops the audio
    /// </summary>
    /// <param name="clip">The audio to be stopped</param>
    public void StopPlaying(string clip)
    {
        audio[clip].First.Stop();
        audio[clip].Second = status.STOPPED;
    }

    /// <summary>
    /// Returns true iff the audio is playing.
    /// </summary>
    /// <param name="clip">The audio</param>
    /// <returns>True iff the clip is playing</returns>
    public bool IsPlaying(string clip)
    {
        return audio[clip].First.isPlaying;
    }

    /// <summary>
    /// Adds an effect to the audio that is playing when the avatar crashes.
    /// </summary>
    /// <param name="clip">The audio</param>
    /// <param name="fadeoutTime">The time that the effect takes</param>
    public void CrashEnding(string clip, int fadeoutTime = 1500)
    {
        if (audio[clip].Second == status.PLAYING)
        {
            StartCoroutine(this._crashEnding(audio[clip].First, fadeoutTime));
            audio[clip].Second = status.CRASHING;
        }
    }

    /// <summary>
    /// Adds a fade out effect to the audio that is currently playing.
    /// </summary>
    /// <param name="audio">The audio</param>
    /// <param name="fadeoutTime">The time that the effect takes</param>
    /// <returns></returns>
    private IEnumerator _crashEnding(AudioSource audio, int fadeoutTime)
    {
        float startVolume = audio.volume;
        float timeout = 1000 / 30.0f;
        float volumeDiff = audio.volume * timeout / fadeoutTime;
        float pitchDiff = audio.pitch * timeout / fadeoutTime;
        while (audio.volume > 0.01)
        {
            audio.volume -= volumeDiff;
            audio.pitch -= pitchDiff;
            yield return new WaitForSeconds(timeout / 1000);
        }
        audio.Stop();
        audio.volume = startVolume;
        audio.pitch = 1;
        yield return 0;
    }
}
