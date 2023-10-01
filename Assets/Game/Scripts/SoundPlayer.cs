using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourceFailed;

    public void PlayFailedSound()
    {
        _audioSourceFailed.Play();
    } 
}
