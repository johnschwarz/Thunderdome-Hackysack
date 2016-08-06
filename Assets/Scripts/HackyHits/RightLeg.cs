using UnityEngine;
using System.Collections;

public class RightLeg : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        if (HackyHits.Instance.CanBeHit)
        {
            if (col.gameObject.tag == "Sack")
            {
                TimeScaleManager.Instance.StartCoroutine(TimeScaleManager.Instance.IHit());
                HackyHits.Instance.CanBeHit = false;

                //Add a check to see if the gameobject's parent's tag is AI or not?

                int rndPassChance = Random.Range(0, 11);
                if (rndPassChance < HackyHits.Instance.PassChanceOutOfTen)
                {
                    //pass
                    Debug.Log("Pass");
                    HackyHits.Instance.SetForce(HackyHits.HackeyHts.PassRightHit);
                    Rigidbody2D temp = col.GetComponent<Rigidbody2D>();
                    temp.velocity = new Vector2(HackyHits.Instance.OutPower, HackyHits.Instance.upPower);
                    StartCoroutine(HackyHits.Instance.ITurnOffCollider());
                    
                }
                else
                {
                    //normalHit
                    Debug.Log("normal Ht");
                    HackyHits.Instance.SetForce(HackyHits.HackeyHts.RightIn);
                    Rigidbody2D temp = col.GetComponent<Rigidbody2D>();
                    temp.velocity = new Vector2(HackyHits.Instance.InPower, HackyHits.Instance.upPower);
                    StartCoroutine(HackyHits.Instance.ITurnOffCollider());
                }
            }
        }
    }
}
