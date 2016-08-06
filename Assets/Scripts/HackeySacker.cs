using UnityEngine;
using System.Collections;

public class HackeySacker : MonoBehaviour
{


    //general body
    [SerializeField]
    Rigidbody2D _ThisRigid;

    [SerializeField]
    Animator _ThisAnimCtrl;

    [SerializeField]
    Transform _ThisTrans;

    public float playerStrafeSpeed = 0.4f;

    private static HackeySacker _Instance;
    public static HackeySacker Instance
    {
        get { return _Instance; }
    }

    void Awake()
    {
        if (_Instance != null) throw new System.Exception("Fuck");
        _Instance = this;
    }

    enum HackeyMoves
    {
        None,
        Left,
        Right,
        LeftKick1,
        LeftKick1Out,
        LeftKickLow,
        LeftKickOver,
        RightKick1,
        RightKick1Out,
        RightKickLow,
        RightKickOver,

    }

    void Move(HackeyMoves hackMoves)
    {
        switch (hackMoves)
        {
            case HackeyMoves.RightKick1:
                _ThisAnimCtrl.Play("RightLeg");
                _ThisRigid.velocity = new Vector2(0, 0);
                _ThisAnimCtrl.SetBool("walking", false);
                break;
            case HackeyMoves.RightKick1Out:
                _ThisAnimCtrl.Play("RightLegOut");
                _ThisRigid.velocity = new Vector2(0, 0);
                _ThisAnimCtrl.SetBool("walking", false);
                break;
            case HackeyMoves.RightKickLow:
                _ThisAnimCtrl.Play("RightLegLow");
                _ThisRigid.velocity = new Vector2(0, 0);
                _ThisAnimCtrl.SetBool("walking", false);
                break;
            case HackeyMoves.RightKickOver:

                _ThisRigid.velocity = new Vector2(0, 0);
                _ThisAnimCtrl.SetBool("walking", false);
                break;
            case HackeyMoves.LeftKick1:
                _ThisAnimCtrl.Play("LeftLeg");
                _ThisRigid.velocity = new Vector2(0, 0);
                _ThisAnimCtrl.SetBool("walking", false);
                break;
            case HackeyMoves.LeftKickOver:
                _ThisAnimCtrl.Play("Kick1Over");
                _ThisRigid.velocity = new Vector2(0, 0);
                _ThisAnimCtrl.SetBool("walking", false);
                break;
            case HackeyMoves.LeftKickLow:
                _ThisAnimCtrl.Play("Kick1Low");
                _ThisRigid.velocity = new Vector2(0, 0);
                _ThisAnimCtrl.SetBool("walking", false);
                break;
            case HackeyMoves.LeftKick1Out:
                _ThisAnimCtrl.Play("LeftLegOut");
                _ThisRigid.velocity = new Vector2(0, 0);
                _ThisAnimCtrl.SetBool("walking", false);
                break;
            case HackeyMoves.Left:
                _ThisRigid.velocity = new Vector2(-playerStrafeSpeed, 0);
                _ThisAnimCtrl.SetBool("walking", true);
                break;
            case HackeyMoves.Right:
                _ThisRigid.velocity = new Vector2(playerStrafeSpeed, 0);
                _ThisAnimCtrl.SetBool("walking", true);
                break;
            case HackeyMoves.None:
                _ThisRigid.velocity = new Vector2(0, 0);
                _ThisAnimCtrl.SetBool("walking", false);
                break;
        }
    }


    void Update()
    {

        //Restart
        if (Input.GetKeyDown(KeyCode.M))
        {
            Application.LoadLevel(0);
        }


        //None Idle
        if (!Input.anyKey) { Move(HackeyMoves.None); }



        if (Input.GetKeyDown(KeyCode.D)) { Move(HackeyMoves.LeftKick1); }


        if (Input.GetKeyDown(KeyCode.F)) { Move(HackeyMoves.LeftKick1Out); }


        if (Input.GetKeyDown(KeyCode.A)) { Move(HackeyMoves.RightKick1Out); }


        if (Input.GetKeyDown(KeyCode.UpArrow)) { Move(HackeyMoves.RightKickLow); }


        if (Input.GetKeyDown(KeyCode.S)) { Move(HackeyMoves.RightKick1); }
        

        if (Input.GetKey(KeyCode.LeftArrow)) { Move(HackeyMoves.Left); }
        if (Input.GetKey(KeyCode.RightArrow)) { Move(HackeyMoves.Right); }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            Move(HackeyMoves.None);
            _ThisRigid.velocity = new Vector2(0, 0);
        }

    }
}
