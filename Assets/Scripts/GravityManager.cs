using UnityEngine;
using System.Collections;

public class GravityManager : MonoBehaviour {

    private static GravityManager _Instance;
    public static GravityManager Instance {
        get{return _Instance;}}

	// Use this for initialization
	void Awake() {
        if (_Instance != null && _Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    [SerializeField]
    Vector2 GravityLevel;


    private Vector2 currentGravity;
    private Vector2 newGravity;
     
    void AdjustGravityByOne(float i)
    {
        currentGravity = Physics2D.gravity;
        newGravity = new Vector2(currentGravity.x, currentGravity.y + i);

        Physics2D.gravity = newGravity;
    }

    public void SetGravity(Vector2 gravity)
    {
        Physics2D.gravity = gravity;
        Debug.Log(Physics2D.gravity);
    }

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.Q))
        {
            AdjustGravityByOne(1);
            Debug.Log(Physics2D.gravity);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            AdjustGravityByOne(-1);
            Debug.Log(Physics2D.gravity);
        }

    }
}
