using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip death;
    public AudioClip checkpoint;
    public AudioClip wallTouch;
    public AudioClip protalIn;
    public AudioClip protalOut;

    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
