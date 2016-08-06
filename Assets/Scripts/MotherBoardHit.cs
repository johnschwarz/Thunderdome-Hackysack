using UnityEngine;
using System.Collections;

public class MotherBoardHit : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "loot")
        {
            PlayerMovement.Instance.motherBoardCount++;
            Destroy(col.gameObject);
        }
    }
}
