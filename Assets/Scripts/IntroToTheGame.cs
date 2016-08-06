using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroToTheGame : MonoBehaviour {

    [SerializeField]
    TypeOutScript typescript;

    [SerializeField]
    GameObject introPanel;

    string[] introTexts = new string[] {"You own a sole vending machine here.","It takes in about $40 a day.","But you want to buy a spaceship...","...to expand your..." , "...vending machine empire...","...across the galaxy!" } ;
    private int stringNumner = 0;

    private bool canSkip = true;

    public void Next()
    {
        if (canSkip && introTexts.Length > stringNumner)
        {
            canSkip = false;
            StartCoroutine(INext(introTexts[stringNumner]));
        }
        else
        {
            introPanel.SetActive(false);
            Tutorial.Instance.gameObject.SetActive(true);
            Tutorial.Instance.canStartTut = true;
        }
    
    }

    IEnumerator INext(string textstring)
    {
        stringNumner++;
        typescript.reset = true;
        yield return new WaitForSeconds(0.2f);
        typescript.FinalText = textstring;
        typescript.On = true;
        canSkip = true;
    }

    public void PressBackButton()
    {
        Application.LoadLevel(0);
    }

    void Awake()
    {
        //Tutorial.Instance.gameObject.SetActive(false);
    }
}
