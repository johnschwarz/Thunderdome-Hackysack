using UnityEngine;
using System.Collections;

public class AiHackerMainBoxChec : MonoBehaviour {

    HackySackerAI AIScript;

    void Awake()
    {
        AIScript = gameObject.GetComponentInParent<HackySackerAI>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Sack")
        {
            if (col.gameObject.GetComponent<Rigidbody2D>().velocity.y == 0)
            {
                AIScript.AIShouldStopMoving = true;
                StartCoroutine(AIScript.IStartHacking());
            }
        }

        
    }
                

}
