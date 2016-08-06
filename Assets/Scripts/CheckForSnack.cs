using UnityEngine;
using System.Collections;

public class CheckForSnack : MonoBehaviour {

    PatronAI pAI;
    [SerializeField]
    private bool CanBuySnack = true;
    void Awake()
    {
        pAI = gameObject.GetComponentInParent<PatronAI>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "VendingMachinecheckForAI")
        {
            //Debug.Log("Do I want a Snack?");
            if (pAI.CurrentH < 50 && pAI.CurrentM > 10)
            {
              //  Debug.Log("I do!");
                StartCoroutine(pAI.IBuyVendingMachine(other.gameObject));

                pAI.CurrentH += 20;
                pAI.CurrentM -= 10;
                Score.Instance.money += 10;
                CanBuySnack = false;
                StartCoroutine(ICoolDownOnEating());
                //Debug.Log("My hunger is " + pAI.CurrentH + ", and my Money is " + pAI.CurrentM);
            }
            else {
            //    Debug.Log("I don't."); Debug.Log("My hunger is " + pAI.CurrentH + ", and my Money is " + pAI.CurrentM);
            }
            
        }

        if (other.gameObject.tag == "Door")
        {
            int i = Random.Range(0, 4);
            if (i > 1 && other.gameObject.GetComponent<DoorSpawnPatrons>().canSpawn)
            {

                other.gameObject.GetComponent<DoorSpawnPatrons>().canSpawn = false;
                StartCoroutine(ILeave(other.gameObject));
                
            }
        }
    }

    //Used on the DoorSpawnPatron
    IEnumerator ILeave(GameObject obj)
    {
        pAI.StopAllCoroutines();
        Animator DoorAnim = obj.gameObject.GetComponent<Animator>();
        pAI.SetState(PatronAI.Action.Consider);
        yield return new WaitForSeconds(1);
        pAI.SetState(PatronAI.Action.Interact);
        yield return new WaitForSeconds(0.2f);
        DoorAnim.SetTrigger("Open");
        pAI.SetState(PatronAI.Action.Consider);
        yield return new WaitForSeconds(2f);
        //obj.gameObject.GetComponent<DoorSpawnPatrons>().canSpawn = true;
        Destroy(this.gameObject.transform.parent.gameObject);
    }

    IEnumerator ICoolDownOnEating()
    {
        yield return new WaitForSeconds(15);
        CanBuySnack = true;
    }
    
}
