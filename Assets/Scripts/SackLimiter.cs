using UnityEngine;
using System.Collections;

public class SackLimiter : MonoBehaviour {
    [SerializeField]
    Rigidbody2D _thisRigid;
    [SerializeField]
    float MaxVelNumber;
    void FixedUpdate()
    {
        if (_thisRigid.velocity.y > MaxVelNumber)
        {
            _thisRigid.velocity = new Vector2(_thisRigid.velocity.x, MaxVelNumber);
        }

        //Debug.Log(_thisRigid.velocity);
    }
}
