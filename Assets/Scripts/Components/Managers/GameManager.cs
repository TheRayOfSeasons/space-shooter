using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float score = 0f;
    public float Score
    {
        get { return score; }
    }

    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
    }

    public void AddScore(float augment)
    {
        score += augment;
    }

    public void ResetScore()
    {
        score = 0f;
    }
}
