using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    int score;
    Text scoreText;

    [SerializeField] int scorePerHit = 12;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
    }


    /** 
     * Update score. Use the default score per hit.
     */ 
    public void ScoreHit()
    {
        UpdateScore(scorePerHit);
    }

    /**
     * Enemy passes in their value to add to score.
     */
    public void ScoreHit(int points)
    {
        UpdateScore(points);
    }

    private void UpdateScore(int scoreToAdd)
    {
        score = score + scoreToAdd;
        scoreText.text = score.ToString();
    }
}
