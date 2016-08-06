using UnityEngine;
using System.Collections;

public class LittleRobotAI : MonoBehaviour {

    [SerializeField]
    Rigidbody2D _ThisRigid;

    [SerializeField]
    public
    Animator _ThisAnimCtrl;

    [SerializeField]
    Transform _ThisTrans;

    [SerializeField]
    private float robotSpeed;

    enum MoveDir
    {
        None,
        Left,
        Right,
    }

    void Move(MoveDir moveDir)
    {
        switch (moveDir)
        {
            case MoveDir.Left:
                _ThisAnimCtrl.Play("Walk");
                _ThisTrans.localScale = new Vector3(1, 1, 0);
                _ThisRigid.velocity = new Vector2(-robotSpeed, _ThisRigid.velocity.y);
                _ThisAnimCtrl.SetBool("IsWalking", true);
                break;
            case MoveDir.Right:
                _ThisAnimCtrl.Play("Walk");
                _ThisTrans.localScale = new Vector3(-1, 1, 0);
                _ThisRigid.velocity = new Vector2(robotSpeed, _ThisRigid.velocity.y);
                _ThisAnimCtrl.SetBool("IsWalking", true);
                break;
            case MoveDir.None:
                _ThisAnimCtrl.SetBool("IsWalking", false);
                break;
        }
    }


    //used for random number, could be done better I think...
    int i;

    void MakeRandomNumber()
    {
        i = Random.Range(0, 3);
    }

    IEnumerator IRobotChoice()
    {
        MakeRandomNumber();

        yield return new WaitForSeconds(1);
        StartCoroutine(IRobotChoice());
    }


    void Start()
    {
        i = Random.Range(0, 3);
        StartCoroutine(IRobotChoice());
    }

    void Update ()
    {
        if (i == 0)
        {
            Move(MoveDir.None);
        }
        if (i == 1)
        {
            Move(MoveDir.Left);
        }
        if (i == 2)
        {
            Move(MoveDir.Right);
        }
    }

    
}
