using UnityEngine;
using System.Collections;

public class VendingMachine : MonoBehaviour
{
    [SerializeField]
    Animator _VendingAnmi;

    [SerializeField]
    GameObject _Loot;

    private bool _CanSpawnLoot = true;

    IEnumerator ISpawnLoot()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(_Loot, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        _CanSpawnLoot = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "LeftPunch")
        {
            
            _VendingAnmi.Play("Shake");
            if (_CanSpawnLoot == true && PlayerMovement.Instance.moneyCount > 0)
            {
                PlayerMovement.Instance.moneyCount--;
                _CanSpawnLoot = false;
                StartCoroutine(ISpawnLoot());
            }
        }

        if (other.gameObject.tag == "LegKick")
        {
            _VendingAnmi.Play("Shake");
        }
    }
}
