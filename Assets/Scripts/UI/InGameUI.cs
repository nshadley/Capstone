using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    GameManager gm;
    Text text;

    void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        text = GetComponent<Text>();
        text.text = "key not obtained";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateKeyUI()
    {
        if(gm.playerScriptRef.haveKey)
        {
            text.text = "Key obtained";
        }
        else
        {
            text.text = "Key not obtained";
        }
    }
}
