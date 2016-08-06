using UnityEngine;
using System.Collections;

public class DoorSpawnPatrons : MonoBehaviour {

    private bool CanSpawn = true;
    public bool canSpawn {
        get { return CanSpawn; }
        set { value = CanSpawn; }
    }

    [SerializeField]
    GameObject[] ArrayOfSpawnableAIs;

    [SerializeField]
    Animator _Anim;

    private float startTime = 0;
    [SerializeField]
    private float spawnTime = 2;
    

    void Update()
    {
        SpawnOnTimer();
    }

    void SpawnOnTimer()
    {
        if (Time.time >= startTime + spawnTime)
        {
            if (CanSpawn) {
                
                CanSpawn = false;

                Debug.Log(canSpawn);
                StartCoroutine(ISpawnAIFromDoor());
            }
            
            startTime = Time.time;            
        }
    }

    IEnumerator ISpawnAIFromDoor()
    {
        
        _Anim.SetTrigger("Open");
        yield return new WaitForSeconds(0.5f);
        Instantiate(ArrayOfSpawnableAIs[Random.Range(0, ArrayOfSpawnableAIs.Length)], gameObject.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(10f);
        CanSpawn = true;
    }

}
