using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    [SerializeField]
    GameObject[] tutorialMessages;
    private int currentTutMessage = 0;

    [SerializeField]
    Image panelImage;

    [SerializeField]
    Button BackButton;

   public bool canStartTut = false;

    private static Tutorial instance = null;
    public static Tutorial Instance { get { return instance; } }


    void Awake()
    {
        BackButton.gameObject.SetActive(false);
        panelImage.CrossFadeAlpha(0, 0, true);
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        if (!canStartTut)
        foreach (var obj in tutorialMessages)
        {
            obj.SetActive(false);
            
        }

       
    }

    void Start()
    {

        
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
                if (Input.GetKeyUp(KeyCode.UpArrow))
                {
                    foreach (var obj in tutorialMessages)
                    {
                        obj.SetActive(false);
                    }
                    currentTutMessage++;
                    tutorialMessages[currentTutMessage].SetActive(true);
                }
            }

            if (currentTutMessage == 2)
            {
                if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
                {
                    foreach (var obj in tutorialMessages)
                    {
                        obj.SetActive(false);
                    }
                    currentTutMessage++;
                    tutorialMessages[currentTutMessage].SetActive(true);
                }
            }

            if (currentTutMessage == 3)
            {
                if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.F))
                {
                    foreach (var obj in tutorialMessages)
                    {
                        obj.SetActive(false);
                    }
                    currentTutMessage++;
                    tutorialMessages[currentTutMessage].SetActive(true);
                }
            }

            if (currentTutMessage == 4)
            {
                StartCoroutine(IFINISHTUT());
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

    public void PressBackButton()
    {
        Application.LoadLevel(0);
    }

    void Update()
    {
        NextMessage();

    }


}
