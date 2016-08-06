using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AITut : MonoBehaviour {

    [SerializeField]
    GameObject[] tutorialMessages;
    private int currentTutMessage = 0;

    [SerializeField]
    Image panelImage;

    [SerializeField]
    Button BackButton;

    public bool canStartTut = false;

    void Awake()
    {
        panelImage.CrossFadeAlpha(0, 0, true);

        BackButton.gameObject.SetActive(false);
        if (!canStartTut)
            foreach (var obj in tutorialMessages)
            {
                obj.SetActive(false);
            }
    }

    void NextMessage()
    {

        if (tutorialMessages.Length > currentTutMessage && canStartTut)
        {
            tutorialMessages[currentTutMessage].SetActive(true);

            if (currentTutMessage == 0)
            {
                panelImage.CrossFadeAlpha(1, 1, true);
                if (Input.GetKeyUp(KeyCode.DownArrow))
                {
                    foreach (var obj in tutorialMessages)
                    {
                        obj.SetActive(false);
                    }
                    currentTutMessage++;
                    tutorialMessages[currentTutMessage].SetActive(true);
                }
            }

            if (currentTutMessage == 1)
            {
                StartCoroutine(IFINISHTUT());
            }

            if (currentTutMessage == 2)
            {
               
            }

        }
    }

    IEnumerator IFINISHTUT()
    {
        yield return new WaitForSeconds(3.5f);
        BackButton.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);

    }

    void Update()
    {
        NextMessage();

    }

    public void LoadMainMenu()
    {
        Application.LoadLevel(0);
    }

}
