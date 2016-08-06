using UnityEngine;
using System.Collections;

public class IntroToPlenus : MonoBehaviour {

    [SerializeField]
    TypeOutScript typescript;

    [SerializeField]
    GameObject introPanel;

    AITut AITUT;

    void Awake()
    {
        AITUT = gameObject.GetComponentInChildren<AITut>();
    }

    string[] introTexts = new string[] { "Farmers and sutff.", "ProHacker lives here", "Watch him hack!" };
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
            AITUT.gameObject.SetActive(true);
            AITUT.canStartTut = true;
        }

    }

    public void PressBackButton()
    {
        Application.LoadLevel(0);
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

  
}
