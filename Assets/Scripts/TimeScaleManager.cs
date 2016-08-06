using UnityEngine;
using System.Collections;

public class TimeScaleManager : MonoBehaviour
{



    private static TimeScaleManager _Instance;
    public static TimeScaleManager Instance
    {
        get { return _Instance; }
    }

    void Awake()
    {
        if (_Instance != null) throw new System.Exception("Fuck");
        _Instance = this;


    }

    enum TimeSet
    {
        Slow,
        Hit,
        Normal

    }
   

     void ChangeTime(TimeSet timeSet)
    {
        switch (timeSet)
        {
        
            case TimeSet.Hit:
                StartCoroutine(IHit());
                break;
            case TimeSet.Normal:
                Time.timeScale = 0.9f;
                break;
        }
    }

    public IEnumerator IHit()
    {
        Time.timeScale = 0.4f;
        yield return new WaitForSeconds(0.05f);
        ChangeTime(TimeSet.Normal);
    }
        
}
