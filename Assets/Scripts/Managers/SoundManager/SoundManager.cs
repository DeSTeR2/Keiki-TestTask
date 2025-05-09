using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource _sound;
    [SerializeField] private List<AudioClip> winClips;
    
    public float PlayWin()
    {
        int soundIndex = Random.Range(0, winClips.Count);
        AudioClip clip = winClips[soundIndex];
        
        Play(clip);
        return clip.length;
    }
    
    public void Play(AudioClip audio)
    {
        _sound.PlayOneShot(audio);
    }
}