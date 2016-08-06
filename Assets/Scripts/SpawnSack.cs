using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SpawnSack : MonoBehaviour {
    [SerializeField]
    GameObject _Sack;

    [SerializeField]
    Transform playerPosition;

    Vector3 spawnSackSpotAbovePlayer;

    public List<GameObject> allSacksTransformsList; 

    private static SpawnSack _Instance;
    public static SpawnSack instance { get { return _Instance; } }
    void Awake()
    {
        if (_Instance != null)  Debug.Log("SpawnSack is broken");
        _Instance = this;
        allSacksTransformsList = new List<GameObject>();
    }


    private void GetAllSacksOnaListToTurnToaTansform()
    {
        allSacksTransformsList.AddRange(GameObject.FindGameObjectsWithTag("Sack"));
    }



    private void RemoveAllOnLPublicSackist()
    {
        if (allSacksTransformsList != null)
        {
            for (var i = 0; i < allSacksTransformsList.Count; i++)
            {
                Debug.Log("Removed: " + allSacksTransformsList[i].transform);
                allSacksTransformsList.RemoveAt(i);
            }
        }
        else { Debug.Log("NoSackList"); }
    }



    //For AI
    public Transform GetClosestSack(List<GameObject> SacksList, Transform AIPosition)
    {
        allSacksTransformsList = new List<GameObject>();
        //RemoveAllOnLPublicSackist();
        GetAllSacksOnaListToTurnToaTansform();
        Transform sMin = null;
        float closestDistanceSQ = Mathf.Infinity;
        Vector3 currentAIPos = AIPosition.position;

        if (allSacksTransformsList != null && allSacksTransformsList.Count != 0)
        {
            for (int i = 0; i < SacksList.Count; i++)
            {
                Transform s = SacksList[i].gameObject.transform;
                Vector3 directionToTarget = s.position - AIPosition.position;
                float sSqrToTarget = directionToTarget.sqrMagnitude;
                if (sSqrToTarget < closestDistanceSQ)
                {
                    closestDistanceSQ = sSqrToTarget;
                    sMin = s;
                }
            }
        }
        return sMin;
    }

    public Transform GetClosestSack(List<GameObject> SacksList, Transform AIPosition, HackySackerAI inScript, bool ShouldMoveLeft, bool ShouldMoveRight)
    {
        RemoveAllOnLPublicSackist();
        GetAllSacksOnaListToTurnToaTansform();
        Transform sMin = null;
        Vector3 currentAIPos = AIPosition.position;
        for (int i = 0; i < SacksList.Count; i++)
        {
            Transform s = SacksList[i].gameObject.transform;
            if (s.transform.position.x >= AIPosition.transform.position.x)
            {
                inScript.AIShouldMoveLeft = false;
                inScript.AIShouldMoveRight= true;
                sMin = s;
            }
            else
            {
                sMin = s;
                inScript.AIShouldMoveLeft = true;
                inScript.AIShouldMoveRight = false;
            }
        }
        return sMin;
    }



    void GetPlayerPostAndDestroyAllSacks(Transform player)
    {
        GameObject[] objs;
        objs = GameObject.FindGameObjectsWithTag("Sack");
        foreach (GameObject sack in objs)
        {            Destroy(sack);        }
        spawnSackSpotAbovePlayer = new Vector3 (  player.position.x, player.position.y + 0.3f, player.position.z);
    }

    void GetPlayerPost(Transform player)
    {
        if (playerPosition != null)
        spawnSackSpotAbovePlayer = new Vector3(player.position.x, player.position.y + 0.3f, player.position.z);
        else
            spawnSackSpotAbovePlayer = new Vector3(0, 0 + 0.3f, 0);
    }


    void SpawnASackAndAddItToTheList()
    {
        GameObject tempSack = (GameObject)Instantiate(_Sack, spawnSackSpotAbovePlayer, Quaternion.identity);

        if (allSacksTransformsList != null)
        {
            allSacksTransformsList.Add(tempSack);
        }
        else {
            allSacksTransformsList = new List<GameObject>();
            GetAllSacksOnaListToTurnToaTansform();
        }
    }

    void Update () {

        if (Input.GetKeyDown(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftShift))
        {
            GetPlayerPostAndDestroyAllSacks(playerPosition);
            SpawnASackAndAddItToTheList();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GetPlayerPost(playerPosition);
            SpawnASackAndAddItToTheList();
        }  
    }
}
