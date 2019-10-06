using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    //  public static AudioManager instance;


    public List<AudioClip> audioClips;


    private Dictionary<string, AudioClip> audioMap;

    public int maxAudios = 16;

    public float musicVolume = 1;
    public float soundVolume = 1;
    private List<AudioSource> sourcePool;

    public Dictionary<string, AudioClip> AudioMap
    {
        get
        {
            if (audioMap == null) CreateAudioMap();

            return audioMap;
        }
        set { audioMap = value; }
    }


    private void CreateAudioMap()
    {
        audioMap = new Dictionary<string, AudioClip>();

        AudioMap = new Dictionary<string, AudioClip>();

        foreach (var clip in audioClips) AudioMap.Add(clip.name, clip);

        sourcePool = new List<AudioSource>();
        var parent = gameObject;

        for (var i = 0; i < maxAudios; i++)
        {
            var gob = new GameObject("Audio");
            gob.transform.parent = parent.transform;
            var audioSource = gob.AddComponent<AudioSource>();
            sourcePool.Add(audioSource);
        }

    //    SetMusicVolume(PlayerPrefs.GetInt("MusicVolume") / 10f);
   //     SetSoundVolume(PlayerPrefs.GetInt("SoundVolume") / 10f);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        if (sourcePool != null)
            foreach (var source in sourcePool)
                if (source.isPlaying && source.loop)
                    source.volume = volume;
    }

    public void SetSoundVolume(float volume)
    {
        soundVolume = volume;
    }

    public void StopAllMusic()
    {
        if (sourcePool == null) return;
        
        foreach (var source in sourcePool)
            if (source.isPlaying && source.loop)
                source.Stop();
    }

    public void PlayRandom(string[] audioName, float vol = 1f, bool isLooping = false, Vector3? position = null)
    {
        var i = Random.Range(0, audioName.Length);
        Play(audioName[i], vol, isLooping, position);
    }

    public void Stop(string audioName)
    {
        if (string.IsNullOrEmpty(audioName)) return;
        var succ = AudioMap.TryGetValue(audioName, out var clip);
        if (succ)
        {
            foreach (var source in sourcePool)
                if (source.clip == clip)
                {
                    source.Stop();
                    return;
                }
        }
        else
        {
//            Debug.LogWarning("Could not find audio: " + audioName);
        }

    }


    public IEnumerator FadeOut(float time = 1.0f)
    {
        var currMusic = sourcePool.FirstOrDefault(a => a.loop);

        if (currMusic == null)
            yield break;
        
        
        var startAudioVol = currMusic.volume;

        var timer = 0f;

        while ((timer += Time.unscaledDeltaTime) < time)
        {
            currMusic.volume = Mathf.Lerp(startAudioVol, 0, timer / time);

            yield return null;
        }
        
        currMusic.Stop();
    }


    public void Play(string audioName, float vol = 1f, bool isLooping = false, Vector3? position = null)
    {
        if (string.IsNullOrEmpty(audioName)) return;


        var succ = AudioMap.TryGetValue(audioName, out var clip);
        

        if (succ)
        {
            
            if (isLooping && IsAlreadyBeingPlayed(audioName)) return;
            
            foreach (var source in sourcePool)
                if (!source.isPlaying)
                {
                    source.clip = clip;
                    source.volume = vol * (isLooping ? musicVolume : soundVolume);
                    source.loop = isLooping;
                    source.Play();

                    if (position != null)
                        source.transform.position = (Vector3) position;
                    else
                        source.transform.localPosition = Vector3.zero;

                    break;
                }
        }

        else
        {
//            Debug.LogWarning("Could not find audio: " + audioName);
        }
                    
    }

    public bool IsAlreadyBeingPlayed(string audioName)
    {
        
        return sourcePool.Any(source =>
        {
            return source.clip && source.clip.name.Equals(audioName);
        });
    }

    public void Play(AudioClip clip, float vol = 1f, bool isLooping = false, Vector3? position = null)
    {
        foreach (var source in sourcePool)
            if (!source.isPlaying)
            {
                source.clip = clip;
                source.volume = vol * (isLooping ? musicVolume : soundVolume);
                source.loop = isLooping;
                source.Play();

                if (position != null)
                    source.transform.position = (Vector3) position;
                else
                    source.transform.localPosition = Vector3.zero;

                break;
            }
    }
}