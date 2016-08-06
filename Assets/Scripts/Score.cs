using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {


    private static Score _Instance;
    public static Score Instance
    {
    get {return _Instance;}
    }

    void Awake()
    {

        if (_Instance != null) throw new System.Exception("!!");
        _Instance = this;
        HighScoreText.text = "";

        _Money = 343;
        score = 0;
    }


    [SerializeField]
    Text ScoreText;
    [SerializeField]
    Text HighScoreText;
    [SerializeField]
    Text MoneyText;


    private int _Money;
    public int money
    {
        get { return _Money; }

        set
        {
            _Money = value;
            MoneyText.text = "Money: " + _Money.ToString();

        }
    }


    private int _HighScore;
    public int highScore
    {
        get { return _HighScore; }

    }

    private int _Score;
    public int score
    {
        get { return _Score; }

        set {
            _Score = value;
            ScoreText.text = "Current: " +_Score.ToString();
            if (_HighScore <= value)
            {
                _HighScore = value;
                HighScoreText.text = "Highest: " + _HighScore.ToString();
                money++;
            }

            if (score % 5 == 0)
            {
                money += score;
            }
 

        }

    }

}
