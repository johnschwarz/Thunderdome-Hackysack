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

    string[] introTexts = new string[] { "All fruits and vegetables known in the galaxy are grown available.", "Jeorge the Grounded Farmer lives here", "The air is rich with oxygen, and nitrates; hermetically partitioned zones with regulated and calculated mixtures of particles exist all throughout the planet." };
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
