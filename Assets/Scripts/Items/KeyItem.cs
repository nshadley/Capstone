using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : ItemBase
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

        gm.playerScriptRef.haveKey = true;
        gm.ingameUIRef.UpdateKeyUI();
        FindObjectOfType<AudioScript>().soundEffectSource.PlayOneShot(FindObjectOfType<AudioScript>().keySound);
        Instantiate(pickUpParticle, transform);
        base.PickUp();
    }
}