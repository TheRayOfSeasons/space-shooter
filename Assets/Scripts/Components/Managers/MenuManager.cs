using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A manager for the main menu UI.
/// </summary>
public class MenuManager : MonoBehaviour
{

    private static MenuManager instance;
    public static MenuManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
    }
}
