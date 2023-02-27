using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;
    int startingScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        score = startingScore;
    }

    // Update is called once per frame
    void Update()
    {
        print(score);
    }
}
