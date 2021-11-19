using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eMUSIC
{
    Button,
    Attack,
    GameOver,
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource audioSource;
    public List<AudioClip> clips;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
    }

    public void Play(eMUSIC music) => StartCoroutine(EPlay(music));

    IEnumerator EPlay(eMUSIC music)
    {
        switch (music)
        {
            case eMUSIC.Button:
                audioSource.clip = clips[0];
                break;
            case eMUSIC.Attack:
                audioSource.clip = clips[1];
                break;
            case eMUSIC.GameOver:

                audioSource.clip = clips[2];
                audioSource.Play();
                yield return new WaitForSeconds(0.15f);
                audioSource.clip = clips[3];

                break;
        }
        audioSource.Play();

        yield return null;
    }
}
