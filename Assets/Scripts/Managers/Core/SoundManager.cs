using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager 
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.Maxcount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
    //mp3 player-> audiosource
    //mp3 음원 -> audioClip
    //관객(귀) -> audioListener
    
    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if(root == null)
        {
            root = new GameObject { name ="@Sound" } ;
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject{name = soundNames[i]};
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;

            }
            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    public void Clear()
    {
        foreach(AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();

        }
        _audioClips.Clear();

    }
    public void Play(string path, Define.Sound type=Define.Sound.Effect, float pitch=1.0f)
    {
        AudioClip audioClip=GetOrAddAudioClip(path, type);
        
        Play(audioClip, type, pitch);
    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;
        
        if (type == Define.Sound.Bgm)
        {
            
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }
    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if (type == Define.Sound.Bgm)
        {
            audioClip = Manager.Resource.Load<AudioClip>(path);//bgm은 어쩌다가 한번 바뀌니까 그냥 load씀
           
        }
        else//bgm 아니고 이펙트효과이면 자주쓰이니까 dictionary로 관리함
        {
            
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Manager.Resource.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing : {path}");

        return audioClip;
    }
}
