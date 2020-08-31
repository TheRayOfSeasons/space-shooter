using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text score;

    private static UIManager instance;
    public static UIManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        score.text = $"Score: {GameManager.Instance.Score}";
    }
}
