using UnityEngine;
using System.Collections;

public class LeftLegOut : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        if (HackyHits.Instance.CanBeHit)
        {
            if (col.gameObject.tag == "Sack")
            {
                TimeScaleManager.Instance.StartCoroutine(TimeScaleManager.Instance.IHit());
                HackyHits.Instance.CanBeHit = false;
                HackyHits.Instance.SetForce(HackyHits.HackeyHts.LeftOut);
                Rigidbody2D temp = col.GetComponent<Rigidbody2D>();
                temp.velocity = new Vector2(HackyHits.Instance.OutPower, HackyHits.Instance.upPower);
                StartCoroutine(HackyHits.Instance.ITurnOffCollider());
            }
        }
    }
}
