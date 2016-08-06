using UnityEngine;
using System.Collections;
using System;

public class PatronAI : MonoBehaviour {


    [SerializeField]
    Rigidbody2D _ThisRigid;

    [SerializeField]
    public
    Animator _ThisAnimCtrl;

    [SerializeField]
    Transform _ThisTrans;

    [SerializeField]
    private Vector2 _MoveSpeed;

    Array enumActionValues;

    Action randomAction;

    bool movingLeft;
    bool movingRight;


    [SerializeField]
    private int moneymin;
    [SerializeField]
    private int moneyMax;
    [SerializeField]
    private int currentMoney;
    public int CurrentM { get { return currentMoney; } set { currentMoney = value; } }

    [SerializeField]
    private int hungermin;
    [SerializeField]
    private int hungerMax;
    [SerializeField]
    private int currentHunger;

    public int CurrentH { get { return currentHunger; }
        set { currentHunger = value; }
    }


    //must subtract anything after Turn from SetRandomAction
    public enum Action
    {
        Idle,
        MoveLeft,
        MoveRight,        
        Turn,
        Pickup,
        Interact,
        Consider
    }
    
    void ChangeAction(Action action )
    {
        switch (action)
        {
            case Action.Idle:
                _ThisRigid.velocity = new Vector2(0, 0);
                _ThisAnimCtrl.SetTrigger("Idle");
                StartCoroutine(IChooseAction(UnityEngine.Random.Range(5, 10)));
                break;
        }
        switch (action)
        {
            case Action.MoveLeft:
                _ThisAnimCtrl.SetTrigger("Walk");
                _ThisTrans.localScale = new Vector3(-1, 1, 0);
                movingLeft = true;
                StartCoroutine(IChooseAction(UnityEngine.Random.Range(1,6)));
                break;
        }
        switch (action)
        {
            case Action.MoveRight:
                _ThisAnimCtrl.SetTrigger("Walk");
                _ThisTrans.localScale = new Vector3(1, 1, 0);
                movingRight = true;
                StartCoroutine(IChooseAction(UnityEngine.Random.Range(1, 6)));
                break;
        }
        switch (action)
        {
            case Action.Interact:
                _ThisRigid.velocity = new Vector2(0, 0);
                _ThisAnimCtrl.SetTrigger("Interact");
                ///StartCoroutine(IChooseAction(2f));
                break;
        }
        switch (action)
        {
            case Action.Pickup:
                _ThisRigid.velocity = new Vector2(0, 0);
                _ThisAnimCtrl.SetTrigger("Pickup");
                //StartCoroutine(IChooseAction(1.2f));
                break;
        }
        switch (action)
        {
            case Action.Turn:
                _ThisRigid.velocity = new Vector2(0, 0);
                _ThisAnimCtrl.Play("Idle");
                _ThisTrans.localScale = new Vector3((-1 * _ThisTrans.localScale.x), 1, 0);
                StartCoroutine(IChooseAction(0.2f));
                break;
        }
        switch (action)
        {
            case Action.Consider:
                movingLeft = false;
                movingRight = false;
                _ThisRigid.velocity = new Vector2(0, 0);
                _ThisAnimCtrl.SetTrigger("Idle");
                break;
        }
    }

    //stand alone from IChooseAction uses CheckForSnack to animate the Vending Machine...
    public IEnumerator IBuyVendingMachine(GameObject obj)
    {
        movingRight = false;
        movingLeft = false;
        StopAllCoroutines();
        ChangeAction(Action.Consider);
        yield return new WaitForSeconds(3f);
        ChangeAction(Action.Interact);
        obj.gameObject.GetComponentInParent<Animator>().Play("Shake");
        yield return new WaitForSeconds(1.6f);
        ChangeAction(Action.Pickup);
        yield return new WaitForSeconds(0.3f);
        ChangeAction(Action.Idle);

    }

    //Used for Random Actions (first 4 in the enum)
    IEnumerator IChooseAction(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SetRandomAction();
        ChangeAction(randomAction);
    }

    //also reset and bools
    //random length should be subtracted from to aboid the buying actions
    void SetRandomAction()
    {
        currentHunger--;
        movingRight = false;
        movingLeft = false;
        enumActionValues = Enum.GetValues(typeof(Action));
        randomAction = (Action)enumActionValues.GetValue(UnityEngine.Random.Range(0, enumActionValues.Length -3));
    }

    //only for other classes
    public void SetState(Action inAction)
    {
        ChangeAction(inAction);
    }


    void Start()
    {
        ChangeAction(Action.MoveLeft);
        currentMoney = UnityEngine.Random.Range(moneymin, moneyMax);
        currentHunger = UnityEngine.Random.Range(hungermin, hungerMax);
    }


    void FixedUpdate()
    {
        if (movingLeft)
        { _ThisRigid.MovePosition(_ThisRigid.position - _MoveSpeed * Time.fixedDeltaTime); }
        if (movingRight)
        {_ThisRigid.MovePosition(_ThisRigid.position + _MoveSpeed * Time.fixedDeltaTime);}
    }

  

}
