using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource _sound;
    [SerializeField] private List<AudioClip> winClips;
    
    public void PlayWin()
    {
        int soundIndex = Random.Range(0, winClips.Count);
        AudioClip clip = winClips[soundIndex];

        Play(clip);
    }
    
    public void Play(AudioClip audio)
    {
        _sound.PlayOneShot(audio);
    }
}