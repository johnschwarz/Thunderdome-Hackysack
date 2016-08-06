using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    //UI
    [SerializeField]
    Canvas _PlayerCanvas;
    [SerializeField]
    Slider _PHealthSlider;
    [SerializeField]
    Slider _PStamSlider;
    [SerializeField]
    Text _PMoneyDisplay;

    //general body
    [SerializeField]
    Rigidbody2D _ThisRigid;

    [SerializeField] public
    Animator _ThisAnimCtrl;

    [SerializeField]
    Transform _ThisTrans;

    [SerializeField]
    BoxCollider2D _LeftPunch;

    [SerializeField]
    private float playerSpeed;

    [SerializeField]
    private GameObject _WindspellPrefab;

    //stats and stuff

    public bool shootLeft = false;

    private bool _CanCast = true;
    [SerializeField]
    private float _CoolDownTime;

    bool _PlayerCanJump;
    public bool playerCanJump
    {
        get { return this._PlayerCanJump; }
        set { this._PlayerCanJump = value; }
    }
    public Vector2 playerJumpHeight;

    [SerializeField]
    private int _MoneyCount;
    public int moneyCount
    {
        get { return this._MoneyCount; }
        set { this._MoneyCount = value; }
    }

    [SerializeField]
    private int _PHealth;
    public int playerHealth
    {
        get { return this._PHealth; }
        set { this._PHealth = value; }
    }

    private int maxHealth;
    private int maxStam;

    [SerializeField]
    private int _PHStam;
    public int playerStam
    {
        get { return this._PHStam; }
        set { this._PHStam = value; }
    }

    [SerializeField]
    private int _MotherBoardCount;
    public int motherBoardCount
    {
        get { return this._MotherBoardCount; }
        set { this._MotherBoardCount = value; }
    }


    //singleton.
    private static PlayerMovement _Instance;
    public static PlayerMovement Instance
    {
        get { return _Instance; }
    }

    void Awake()
    {
        if (_Instance != null) throw new System.Exception("Fuck");
        _Instance = this;

        moneyCount = 10;
    }


    enum MoveDir
    {
        None,
        Left,
        Right,
        Jump,
        Punch,
        Kick,
        Pickup,
        CastWind
    }

    void Move(MoveDir moveDir)
    {
        switch (moveDir)
        {
            case MoveDir.Left:
                shootLeft = true;
                _ThisTrans.localScale = new Vector3(-1, 1, 0);
                _ThisRigid.velocity = new Vector2(-playerSpeed, _ThisRigid.velocity.y);
                _ThisAnimCtrl.SetBool("Walking", true);
                break;
            case MoveDir.Right:
                shootLeft = false;
                _ThisTrans.localScale = new Vector3(1, 1, 0);
                _ThisRigid.velocity = new Vector2(playerSpeed, _ThisRigid.velocity.y);
                _ThisAnimCtrl.SetBool("Walking", true);
                break;
            case MoveDir.None:
                _ThisAnimCtrl.SetBool("Walking", false);
                _ThisAnimCtrl.SetTrigger("Idle");
                break;
            case MoveDir.Punch:
                if (_ThisAnimCtrl.GetBool("NotJump") == true)
                {
                    _ThisRigid.velocity = new Vector2(0, _ThisRigid.velocity.y);
                }
                _ThisAnimCtrl.Play("TestManInteract");
                break;
            case MoveDir.Jump:
                playerCanJump = false;
                _ThisRigid.AddForce(playerJumpHeight);
                _ThisAnimCtrl.SetBool("NotJump", false);
                _ThisAnimCtrl.Play("Jump");
                StartCoroutine(IPlayerJump());
                break;
            case MoveDir.Kick:
                if (_ThisAnimCtrl.GetBool("NotJump") == true)
                {
                    _ThisRigid.velocity = new Vector2(0, _ThisRigid.velocity.y);
                }
                _ThisAnimCtrl.Play("Kick");
                break;
            case MoveDir.Pickup:
                if (_ThisAnimCtrl.GetBool("NotJump") == true)
                {
                    _ThisRigid.velocity = new Vector2(0, _ThisRigid.velocity.y);
                }
                _ThisAnimCtrl.Play("Pickup");
                break;   
            case MoveDir.CastWind:
                if (_CanCast == true)
                {
                    if (_ThisAnimCtrl.GetBool("NotJump") == true)
                    {
                        _ThisRigid.velocity = new Vector2(0, _ThisRigid.velocity.y);
                    }
                    _CanCast = false;
                    StartCoroutine(ICastMagic());
                    Instantiate(_WindspellPrefab, new Vector3 (transform.position.x, transform.position.y - 0.1f, transform.position.z), Quaternion.Euler(0, 0, 180));
                    //_ThisAnimCtrl.Play("CastWind");
                    _ThisAnimCtrl.Play("Pickup");
                }
                break;
        }
    }

    IEnumerator ICastMagic()
    {
        yield return new WaitForSeconds(_CoolDownTime);
        _CanCast = true;
    }

    IEnumerator IPlayerJump()
    {
        yield return new WaitForSeconds(0.5f);
        playerCanJump = true;
    }

    void GetInput()
    {
        
        if (!Input.anyKey)
        {
            Move(MoveDir.None);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Move(MoveDir.Left);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Move(MoveDir.Right);
        }
        if (Input.GetKey(KeyCode.E))
        {
            Move(MoveDir.CastWind);
        }
        if (Input.GetKey(KeyCode.W))
        {
            Move(MoveDir.Punch);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Move(MoveDir.Kick);
        }
        if (Input.GetKey(KeyCode.X))
        {
            Move(MoveDir.Pickup);
        }

        if (Input.GetKey(KeyCode.Space) && playerCanJump)
        {
            Move(MoveDir.Jump);
        }
     

    }

    void Start () {
        playerCanJump = true;
        _LeftPunch.enabled = false;
        maxStam = 50;
        maxHealth = 50;
        playerStam = 50;
        playerHealth = 50;
        
    }

    void StatsUI()
    {
        _PMoneyDisplay.text = "Money: " + moneyCount.ToString();
        _PHealthSlider.maxValue = maxHealth;
        _PHealthSlider.value = playerHealth;
        _PStamSlider.maxValue = maxStam;
        _PStamSlider.value = playerStam;

        if (playerHealth > maxHealth)
        {
            playerHealth = maxHealth;
        }

        if (playerStam> maxStam)
        {
            playerStam = maxStam;
        }
    }

	void Update ()
    {
        StatsUI();
        
        GetInput();
    }
}


/*
if (_ThisRigid.velocity.y > 0.1f || _ThisRigid.velocity.y < -0.1f)
        {
            _ThisAnimCtrl.SetBool("NotJump", false);
            _ThisAnimCtrl.Play("Jump");
        }
        else
        {
                _ThisAnimCtrl.SetBool("NotJump", true);
        }

        if (Input.GetKey(KeyCode.W))
        {
            //_LeftPunch.enabled = true;
            // StartCoroutine(IPlayerLeftPunch());
            _ThisRigid.velocity = new Vector2(0, _ThisRigid.velocity.y);
            _ThisAnimCtrl.Play("TestManInteract");

        }

        if (Input.GetKey(KeyCode.D))
        {
            _ThisTrans.localScale = new Vector3(1, 1, 0);
            _ThisRigid.velocity = new Vector2(playerSpeed, _ThisRigid.velocity.y);
            // _ThisRigid.MovePosition(_ThisRigid.position + (new Vector2(playerSpeed, _ThisRigid.position.y)) * Time.fixedDeltaTime);
            _ThisAnimCtrl.SetBool("Walking", true);

        }
        else if (Input.GetKey(KeyCode.A))
        {
            _ThisTrans.localScale = new Vector3(-1, 1, 0);
            _ThisRigid.velocity = new Vector2(-playerSpeed, _ThisRigid.velocity.y);

            // _ThisRigid.MovePosition(_ThisRigid.position - (new Vector2(playerSpeed, _ThisRigid.position.y)) * Time.fixedDeltaTime);
            //_ThisAnimCtrl.SetTrigger("Walk");
            _ThisAnimCtrl.SetBool("Walking", true);

        }
        else
        {
            _ThisAnimCtrl.SetBool("Walking", false);
            _ThisAnimCtrl.SetTrigger("Idle");
        }

      
        if (Input.GetKey(KeyCode.Space))
        {
           
            if (_PlayerCanJump)
            {
                _ThisRigid.AddForce(height);
                playerCanJump = false;
                StartCoroutine(IPlayerJump());
                
            }

        }
    */
