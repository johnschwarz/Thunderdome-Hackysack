using UnityEngine;
using System.Collections;

public class ShootDirection : MonoBehaviour {

    [SerializeField]
    Rigidbody2D _SelfRigid;
    // Use this for initialization

    [SerializeField]
    float timeTilGone;

    [SerializeField]
    float moveSpeed;

    private bool direction;
	void Start () {

        direction = PlayerMovement.Instance.shootLeft;
        StartCoroutine(IDestoryTime(timeTilGone));

        
	}
	
	// Update is called once per frame
	void Update () {

        if(direction)
        _SelfRigid.velocity = new Vector2(-moveSpeed, 0);
        else
            _SelfRigid.velocity = new Vector2(moveSpeed, 0);
    }

    IEnumerator IDestoryTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
