using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenuController : MonoBehaviour
{

    [SerializeField]
    GameObject titlePanel;
    [SerializeField]
    GameObject creditsPanel;

    [SerializeField]
    Button playBut, creditBut, backBut, exitBut;

    // Start is called before the first frame update
    void Start()
    {
        creditsPanel.SetActive(false);
        titlePanel.SetActive(true);

        playBut.onClick.AddListener(PlayButtonPressed);
        creditBut.onClick.AddListener(CreditButtonPressed);
        backBut.onClick.AddListener(BackButtonPressed);
        exitBut.onClick.AddListener(ExitButtonPressed);
    }

    void PlayButtonPressed()
    {
        SceneManager.LoadScene("MainGame");
    }

    void CreditButtonPressed()
    {
        creditsPanel.SetActive(true);
        titlePanel.SetActive(false);
    }

    void BackButtonPressed()
    {
        creditsPanel.SetActive(false);
        titlePanel.SetActive(true);
    }

    void ExitButtonPressed()
    {
        Application.Quit();
    }
}
