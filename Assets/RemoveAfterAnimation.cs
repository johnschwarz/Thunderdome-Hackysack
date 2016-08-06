using UnityEngine;
using System.Collections;

public class RemoveAfterAnimation : MonoBehaviour {
    

    [SerializeField]
    Animator _DustAnmi;

    private IEnumerator IRemove()
    {
        yield return new WaitForSeconds(0.333f);
        Destroy(this.gameObject);
    }
    
    void Start () {

        StartCoroutine(IRemove());
	}
}
