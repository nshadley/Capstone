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

        Debug.Log("You Win!");

        gm.playerScriptRef.win = true;
        gm.ingameUIRef.ShowWinText();
        base.PickUp();
    }
}
