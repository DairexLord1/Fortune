using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip rotateWheel;
    [SerializeField] AudioClip pressRotate;
    [SerializeField] AudioClip getResult;

    private AudioSource aud;

    AudioClip currentAud;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="num"> num of audio clip/param>
    /// <param name="loop"> loop or noot</param>
    public void PlayAuido(int num,bool loop)
    {
        switch (num)
        {
            case 1:
                currentAud = rotateWheel;
                break;
            case 2:
                currentAud = pressRotate;
                break;
            case 3:
                currentAud = getResult;
                break;
        }

        StopAudio();
        aud.clip = currentAud;
        aud.Play();
        aud.loop = loop;
    }

    public void StopAudio()
    {
        aud.Stop();
        aud.loop = false;
    }
}
