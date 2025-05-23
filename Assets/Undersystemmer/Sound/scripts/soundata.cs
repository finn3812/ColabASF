using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "NewSound", menuName = "Audio/SoundData")]
public class SoundData : ScriptableObject
{
    public string soundName;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
}
