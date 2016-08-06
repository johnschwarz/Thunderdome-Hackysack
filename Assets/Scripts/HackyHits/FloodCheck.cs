using UnityEngine;
using System.Collections;

public class FloodCheck : MonoBehaviour {

    
    HackySackerAI hackerAI;

    void Awake()
    {
        if (hackerAI != null)
        {
            hackerAI = GameObject.Find("HackerAI").GetComponent<HackySackerAI>();
        }
    }
  

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Floor")
        {
           
            Score.Instance.score = 0;
            AudioManager.Instance.PlayRandomHit();

            if (hackerAI != null)
            {
                //Used with AI
                StartCoroutine(hackerAI.IMoveToHackThenStop(0.4f));
            }

        }

      


    }
}
