using UnityEngine;
using System.Collections;

public class HackyHits : MonoBehaviour
{

    public float minPowerUp;
    public float maxPowerUp;

    public float minPowerOut;
    public float maxPowerOut;

    public float minPowerIn;
    public float maxPowerIn;

    public float randomStraightHitAmount;

    public int PassChanceOutOfTen;

    public float PassPower;

    HackySackerAI AI;
    HackeySacker PLAYER;
       

    private static HackyHits _Instance;
    public static HackyHits Instance
    {
        get { return _Instance; }
    }

    void Awake()
    {
        if (_Instance != null) throw new System.Exception("Fuck");
        _Instance = this;

        if (AI != null)
        {
            AI = GameObject.FindGameObjectWithTag("HackerAI").GetComponent<HackySackerAI>();
        }
        PLAYER = gameObject.GetComponent<HackeySacker>();
    }

    public enum HackeyHts
    {
        Up,
        LeftOut,
        LeftIn,
        RightOut,
        RightIn,
        PassRightHit
    }

    public float upPower;
    public float randomNess;
    public float InPower;
    public float OutPower;

    //Each Sack uses this bool on their individual Hits scripts.
    public bool CanBeHit = true;
    public IEnumerator ITurnOffCollider()
    {
        yield return new WaitForSeconds(0.1f);
        CanBeHit = true;
        Score.Instance.score++;
    }

    void HitStyle(HackeyHts hackHits)
    {
        switch (hackHits)
        {
            case HackeyHts.Up:
                upPower = Random.Range(minPowerUp, maxPowerUp);
                randomNess = Random.Range(-1 * randomStraightHitAmount, randomStraightHitAmount);
                break;

            case HackeyHts.RightIn:
                upPower = Random.Range(minPowerUp, maxPowerUp);
                InPower = Random.Range(minPowerIn, maxPowerIn);
                break;
            case HackeyHts.RightOut:
                upPower = Random.Range(minPowerUp, maxPowerUp);
                OutPower = Random.Range(minPowerOut, maxPowerOut);
                break;

                //reverse for left
            case HackeyHts.LeftIn:
                upPower = Random.Range(minPowerUp, maxPowerUp);
                InPower = -1 * Random.Range(minPowerIn, maxPowerIn);
                break;
            case HackeyHts.LeftOut:
                upPower = Random.Range(minPowerUp, maxPowerUp);
                OutPower = -1 * Random.Range(minPowerOut, maxPowerOut);
                break;

            //For AI PASS
            case HackeyHts.PassRightHit:
                upPower = Random.Range(minPowerUp+ (8*PassPower), maxPowerUp+ (8*PassPower));
                if (AI.transform.position.x > PLAYER.transform.position.x)
                { OutPower = -1 * Random.Range(0.3f, 0.55f); }
                else
                { OutPower = Random.Range(0.3f, 0.55f); }
                AI.AIShouldStopMoving = true;
                AI.InHackySackMode = false;
                AI.AIShouldMoveLeft = false;
                AI.AIShouldMoveRight = false;
                AI.StopAllCoroutines();
                
                break;

        }
    }

    public void SetForce(HackeyHts hit)
    {
        HitStyle(hit);
        AudioManager.Instance.PlayRandomHit();
    }
}
