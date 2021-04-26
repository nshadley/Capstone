using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioScript : MonoBehaviour
{

    [SerializeField]
    PlayerScript player;

    public AudioClip bgMusic, winMusic, winSound, keySound, doorSound;
    public AudioSource bgMusicSource, soundEffectSource;

    // Start is called before the first frame update
    void Awake()
    {
        player= FindObjectOfType<PlayerScript>();
        bgMusicSource = gameObject.AddComponent<AudioSource>();
        soundEffectSource = gameObject.AddComponent<AudioSource>();
        bgMusicSource.clip = bgMusic;
        bgMusicSource.volume = .5f;
    }

    void Start()
    {
        bgMusicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
