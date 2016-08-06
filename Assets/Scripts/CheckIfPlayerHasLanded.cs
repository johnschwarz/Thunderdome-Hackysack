using UnityEngine;
using System.Collections;

public class CheckIfPlayerHasLanded : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.tag == "Floor")
        {
            PlayerMovement.Instance._ThisAnimCtrl.SetBool("NotJump", true);
        }
    }

}
