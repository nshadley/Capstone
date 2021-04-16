using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    GameManager gm;
    [SerializeField]
    Text keyText;
    [SerializeField]
    Text winText;
    [SerializeField]
    Text goalText;
    [SerializeField]
    Text doorText;

    float disappearTime = 4f;
    float timer = 0f;

    void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        keyText.text = "key not obtained";
        keyText.gameObject.SetActive(true);
        winText.gameObject.SetActive(false);
        doorText.gameObject.SetActive(false);
        goalText.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(GoalTextWait());
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > disappearTime)
        {
            goalText.gameObject.SetActive(false);
        }
        timer += Time.deltaTime;
    }

    public void UpdateKeyUI()
    {
        if(gm.playerScriptRef.haveKey)
        {
            keyText.text = "Key obtained";
        }
        else
        {
            keyText.text = "Key not obtained";
        }
    }

    public void ShowWinText()
    {
        if(gm.playerScriptRef.win)
        {
            winText.gameObject.SetActive(true);
        }
    }

    IEnumerator GoalTextWait()
    {
        goalText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        goalText.gameObject.SetActive(false);
    }

    public IEnumerator DoorTextWait()
    {
        doorText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        doorText.gameObject.SetActive(false);
    }

    public void ShowDoorText()
    {
        doorText.gameObject.SetActive(true);
    }

    public void HideDoorText()
    {
        doorText.gameObject.SetActive(false);
    }
}
