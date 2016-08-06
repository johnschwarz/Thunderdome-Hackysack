using UnityEngine;
using System.Collections;
using System;

public class HackySackerAI : MonoBehaviour {
    [SerializeField]
    Rigidbody2D _ThisRigid;

    [SerializeField]
    Animator _ThisAnimCtrl;

    [SerializeField]
    Transform _ThisTrans;

    //formally playerStrafeSpeed
    public float hackySackerCloneAIStrafeSpeed = 0.4f;
    [SerializeField]
    public bool InHackySackMode;

    //-3 in random, so he just kicks?
    enum HackeyMovesAI
    {
        
        LeftKick1,
        LeftKick1Out,
        RightKick1,
        RightKick1Out,
        RightKickLow,
        PassSackRightKick,
        //keep these 3 at the end 
        None,
        Left,
        Right,
    }



    void Move(HackeyMovesAI hackMoves)
    {
        switch (hackMoves)
        {
            case HackeyMovesAI.RightKick1:
                _ThisAnimCtrl.Play("RightLeg");
                _ThisRigid.velocity = new Vector2(0, 0);
                _ThisAnimCtrl.SetBool("walking", false);
                break;
            case HackeyMovesAI.PassSackRightKick:
                _ThisAnimCtrl.Play("RightLeg");
                _ThisRigid.velocity = new Vector2(0, 0);
                _ThisAnimCtrl.SetBool("walking", false);
                break;
            case HackeyMovesAI.LeftKick1:
                _ThisAnimCtrl.Play("LeftLeg");
                _ThisRigid.velocity = new Vector2(0, 0);
                _ThisAnimCtrl.SetBool("walking", false);
                break;
            case HackeyMovesAI.RightKick1Out:
                _ThisAnimCtrl.Play("RightLegOut");
                _ThisRigid.velocity = new Vector2(0, 0);
                _ThisAnimCtrl.SetBool("walking", false);
                break;
            case HackeyMovesAI.RightKickLow:
                _ThisAnimCtrl.Play("RightLegLow");
                _ThisRigid.velocity = new Vector2(0, 0);
                _ThisAnimCtrl.SetBool("walking", false);
                break;
            case HackeyMovesAI.LeftKick1Out:
                _ThisAnimCtrl.Play("LeftLegOut");
                _ThisRigid.velocity = new Vector2(0, 0);
                _ThisAnimCtrl.SetBool("walking", false);
                break;
            case HackeyMovesAI.Left:
                _ThisRigid.velocity = new Vector2(-hackySackerCloneAIStrafeSpeed, 0);
                _ThisAnimCtrl.SetBool("walking", true);
                break;
            case HackeyMovesAI.Right:
                _ThisRigid.velocity = new Vector2(hackySackerCloneAIStrafeSpeed, 0);
                _ThisAnimCtrl.SetBool("walking", true);
                break;
            case HackeyMovesAI.None:
                _ThisRigid.velocity = new Vector2(0, 0);
                _ThisAnimCtrl.SetBool("walking", false);
                break;
        }
    }

    //used to stop the player
    public bool AIShouldStopMoving = false;
    void StopMoving()
    {
        if (AIShouldStopMoving)
        {
            StopAllCoroutines();
            Move(HackeyMovesAI.None);
            _ThisRigid.velocity = new Vector2(0, 0);
        }
    }

    //ReliesHeabily OnSpawnSack
    public bool AIShouldMoveLeft;
    public bool AIShouldMoveRight;
    private Transform closestCurrentSack;
  

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            FindNearestSackAndMoveTowardsIt();
        }

        if (AIShouldMoveLeft)
        {
            Move(HackeyMovesAI.Left);
        }

        if (AIShouldMoveRight)
        {
            Move(HackeyMovesAI.Right);
        }

        StopMoving();
    }

    private void FindNearestSackAndMoveTowardsIt()
    {
        AIShouldMoveLeft = false;
        AIShouldMoveRight = false;
        closestCurrentSack = SpawnSack.instance.GetClosestSack(SpawnSack.instance.allSacksTransformsList, _ThisTrans);
        AIShouldStopMoving = false;
        StartCoroutine(ILoopHackFind());
        if (SpawnSack.instance.allSacksTransformsList != null && SpawnSack.instance.allSacksTransformsList.Count != 0)
        {
            if (closestCurrentSack.position.x < _ThisTrans.position.x)
            {
                AIShouldMoveLeft = true;
                AIShouldMoveRight = false;

            }
            if (closestCurrentSack.position.x > _ThisTrans.position.x)
            {
                AIShouldMoveLeft = false;
                AIShouldMoveRight = true;

            }
        }
    }

    private IEnumerator ILoopHackFind()
    {
        //Debug.Log("loop");
        yield return new WaitForSeconds(1);
        AIShouldMoveLeft = false;
        AIShouldMoveRight = false;
        if (InHackySackMode)
        {
            FindNearestSackAndMoveTowardsIt();
        }
            yield return new WaitForSeconds(1);
        
        if (InHackySackMode)
        {
            AIShouldMoveLeft = false;
            AIShouldMoveRight = false;
            StartCoroutine(ILoopHackFind());
            FindNearestSackAndMoveTowardsIt();
            
        }
    }

    public float TimeForMovementToaSack;
    public  IEnumerator IMoveToHackThenStop(float time)
    {
        yield return new WaitForSeconds(time);
        FindNearestSackAndMoveTowardsIt();
    }

    public IEnumerator IMoveToHackThenStop()
    {
        yield return new WaitForFixedUpdate();
        FindNearestSackAndMoveTowardsIt();
    }

    public IEnumerator IStartHacking()
    {
        AIShouldMoveLeft = false;
        AIShouldMoveRight = false;
        yield return new WaitForFixedUpdate();
        Move(HackeyMovesAI.RightKickLow);
        AIShouldStopMoving = true;
        yield return new WaitForSeconds(0.5f);
        FindNearestSackAndMoveTowardsIt();
       //
    }

    public IEnumerator ILeftHit()
    {
        AIShouldMoveLeft = false;
        AIShouldMoveRight = false;
        AIShouldStopMoving = false;
        StopAllCoroutines();
        yield return new WaitForFixedUpdate();
        Move(HackeyMovesAI.RightKick1);
        StartCoroutine(IMoveToHackThenStop(TimeForMovementToaSack));
        
    }

    public IEnumerator ILeftHitOut()
    {
        AIShouldMoveLeft = false;
        AIShouldMoveRight = false;
        AIShouldStopMoving = false;
        StopAllCoroutines();
        yield return new WaitForFixedUpdate();
        Move(HackeyMovesAI.RightKick1Out);
        StartCoroutine(IMoveToHackThenStop(TimeForMovementToaSack));
    }

    public IEnumerator IRightHit()
    {
        AIShouldMoveLeft = false;
        AIShouldMoveRight = false;
        AIShouldStopMoving = false;
        StopAllCoroutines();
       yield return new WaitForFixedUpdate();
        Move(HackeyMovesAI.LeftKick1);
        StartCoroutine(IMoveToHackThenStop(TimeForMovementToaSack));

    }

    public IEnumerator IRightHitOut()
    {
        AIShouldMoveLeft = false;
        AIShouldMoveRight = false;
        AIShouldStopMoving = false;
        StopAllCoroutines();
        yield return new WaitForFixedUpdate();
        Move(HackeyMovesAI.LeftKick1Out);
        StartCoroutine(IMoveToHackThenStop(TimeForMovementToaSack));

    }

    void Start()
    {
        StartCoroutine(ILoopHackFind());
    }

    /*Array enumActionValues;
    HackeyMovesAI randomAction;

    void SetRandomAction()
    {
        enumActionValues = Enum.GetValues(typeof(HackeyMovesAI));
        randomAction = (HackeyMovesAI)enumActionValues.GetValue(UnityEngine.Random.Range(0, enumActionValues.Length));
    }*/

}
