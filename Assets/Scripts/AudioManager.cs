using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AudioManager : MonoBehaviour {

    [SerializeField]
    AudioSource _AudioS;

    [SerializeField]
    AudioClip[] ArrayOfHackyHits;

    private static AudioManager _Instance;
    public static AudioManager Instance {

        get { return _Instance; }        
        }

    void Awake () {
        if (_Instance != null) throw new System.Exception("Fuck");
        _Instance = this;
    }

    public void PlayRandomHit()
    {
        _AudioS.PlayOneShot(ArrayOfHackyHits[Random.Range(0,ArrayOfHackyHits.Length)]);
    }
	
}
