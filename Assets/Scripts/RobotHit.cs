using UnityEngine;
using System.Collections;

public class RobotHit : MonoBehaviour {

    [SerializeField]
    Rigidbody2D _ParentRigid;

    [SerializeField]
    GameObject _MoBo;


    [SerializeField]
    GameObject _Explosion;

    private int _LifeCount = 2;

    private Vector2 hitUp = new Vector2(0, 50);

    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "LegKick")
        {
            if (_LifeCount <= 0)
            {
                TimeScaleManager.Instance.StartCoroutine(TimeScaleManager.Instance.IHit());
                Instantiate(_MoBo, transform.parent.gameObject.transform.position, Quaternion.identity);
                Instantiate(_Explosion, transform.parent.gameObject.transform.position, Quaternion.identity);
                Destroy(transform.parent.gameObject);
            }
            else
            {

                TimeScaleManager.Instance.StartCoroutine(TimeScaleManager.Instance.IHit());
                _ParentRigid.AddForce(hitUp);
                _LifeCount--;
                
            }
        }
    }

    
}
