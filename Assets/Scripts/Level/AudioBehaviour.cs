using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Util;

public class AudioBehaviour : MonoBehaviour {

    private Dictionary<string, Tuple<AudioSource, status>> audio;
    public enum status
    {
        PLAYING,
        STOPPED,
        CRASHING
    }

	// Use this for initialization
	void Start () {
        audio = new Dictionary<string, Tuple<AudioSource, status>>();
	   var sources = GetComponents<AudioSource>();
       foreach (var s in sources) {
           audio.Add(s.clip.name, new Tuple<AudioSource, status>(s, status.STOPPED));
       }

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public status Status(string clip)
    {
        return audio[clip].Second;
    }

    public void Play(string clip)
    {
        audio[clip].First.Play();
        audio[clip].Second = status.PLAYING;
    }

    public void StopPlaying(string clip)
    {
        audio[clip].First.Stop();
        audio[clip].Second = status.STOPPED;
    }

    public bool IsPlaying(string clip)
    {
        return audio[clip].First.isPlaying;
    }


    public void CrashEnding(string clip, int fadeoutTime = 1500)
    {
        if (audio[clip].Second == status.PLAYING)
        {
            StartCoroutine(this._crashEnding(audio[clip].First, fadeoutTime));
            audio[clip].Second = status.CRASHING;
        }
    }

    private IEnumerator _crashEnding(AudioSource audio, int fadeoutTime)
    {
        float startVolume = audio.volume;
        float timeout = 1000 / 30.0f;
        float volumeDiff = audio.volume * timeout / fadeoutTime;
        float pitchDiff = audio.pitch * timeout / fadeoutTime;
        Logger.Log("start");
        while (audio.volume > 0.01)
        {
            audio.volume -= volumeDiff;
            audio.pitch -= pitchDiff;
            yield return new WaitForSeconds(timeout / 1000);
        }
        Logger.Log("End");
        audio.Stop();
        audio.volume = startVolume;
        audio.pitch = 1;
        yield return 0;

    }
}
