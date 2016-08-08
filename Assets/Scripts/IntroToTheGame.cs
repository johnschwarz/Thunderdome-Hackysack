using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroToTheGame : MonoBehaviour {

    [SerializeField]
    TypeOutScript typescript;

    [SerializeField]
    GameObject introPanel;

    string[] introTexts = new string[] { "Merx was established several hundred years ago as a place where neighboring planets pooled their resources for the development and maintenance of the galactic highway’s central stop.", "The highway has been stable for centuries, and is still being developed great distances away.",
        "This is a peaceful and culturally-aware part of the galaxy, so no laws are needed, but everyone respects and understands each other.","Bloody Mary the Drunken Ex-Space Pirate lives here" } ;
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
