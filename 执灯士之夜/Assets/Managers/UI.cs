using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [Header("End screen")]
    [SerializeField] private UI_FadeScreen fadeScreen;
    [SerializeField] private GameObject endText;
    [SerializeField] private GameObject restartButton;
    [Space]

    [Header("Victory screen")]
    [SerializeField] private GameObject victoryText;

    [SerializeField] private GameObject InGameUI;

    void Start()
    {
        if (endText != null) endText.SetActive(false);
        if (victoryText != null) victoryText.SetActive(false);
        if (restartButton != null) restartButton.SetActive(false);
        SwitchTo(InGameUI);
    }
    
    public void SwitchTo(GameObject _menu)
    { 
        for (int i = 0; i < transform.childCount; i++)
        {
            bool fadeScreen = transform.GetChild(i).GetComponent<UI_FadeScreen>() != null;

            if (fadeScreen == false)
                transform.GetChild(i).gameObject.SetActive(false);
        }

        if (_menu != null)
             _menu.SetActive(true);
    }

    public void SwitchWithKeyTo(GameObject _menu)
    {
        if (_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            CheckForInGameUI();
            return;
        }
        SwitchTo(_menu);
    }

    public void CheckForInGameUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
                return;
        }
        SwitchTo(InGameUI);
    }

    public void ShowEndScreen(bool isVictory)
    {
        InGameUI.SetActive(false);

        fadeScreen.FadeOut();

        StartCoroutine(EndScreenCoroutine(isVictory));
    }

    IEnumerator EndScreenCoroutine(bool isVictory)
    {
        yield return new WaitForSeconds(1f); 

        if (isVictory)
        {
            if (victoryText != null)
                victoryText.SetActive(true);
        }
        else
        {
            if (endText != null)
                endText.SetActive(true);
        }

        yield return new WaitForSeconds(1.5f);

        if (restartButton != null)
            restartButton.SetActive(true);
    }
    public void RestartGameButton() => GameManager.instance.RestartScene();
}
