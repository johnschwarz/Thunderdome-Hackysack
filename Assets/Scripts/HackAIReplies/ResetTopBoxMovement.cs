using UnityEngine;
using System.Collections;

public class ResetTopBoxMovement : MonoBehaviour {

    HackySackerAI AIScript;

    void Awake()
    {
        AIScript = gameObject.GetComponentInParent<HackySackerAI>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Sack" )
        {
            AIScript.AIShouldStopMoving = true;
            StartCoroutine(AIScript.IMoveToHackThenStop());
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Sack")
        {
            AIScript.AIShouldStopMoving = true;
            StartCoroutine(AIScript.IMoveToHackThenStop());
        }
    }


}
