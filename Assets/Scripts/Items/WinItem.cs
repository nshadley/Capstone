using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinItem : ItemBase
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void PickUp()
    {

        Instantiate(pickUpParticle, transform);

        gm.playerScriptRef.win = true;
        gm.ingameUIRef.ShowWinText();
        FindObjectOfType<AudioScript>().soundEffectSource.PlayOneShot(FindObjectOfType<AudioScript>().winSound);
        FindObjectOfType<AudioScript>().bgMusicSource.clip = FindObjectOfType<AudioScript>().winMusic;
        FindObjectOfType<AudioScript>().bgMusicSource.volume = .8f;
        FindObjectOfType<AudioScript>().bgMusicSource.loop = true;
        FindObjectOfType<AudioScript>().bgMusicSource.Play();
        base.PickUp();
    }
}
