using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _soundtrack;
    private Persistant _persistant;
    private AudioSource _audioPlayer;

    private void Awake()
    {
        _persistant = FindObjectOfType<Persistant>();
        _audioPlayer = _persistant.GetComponent<AudioSource>();
        if (_audioPlayer.clip == _soundtrack) return;
        _audioPlayer.clip = _soundtrack;
        _audioPlayer.GetComponent<AudioSource>().Play();
    }
}
