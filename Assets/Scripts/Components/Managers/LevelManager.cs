using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
    }

    public void LoadScene(int sceneIndex, bool usePhoton = false)
    {
        StartCoroutine(LoadAsync(sceneIndex, usePhoton));
    }

    IEnumerator LoadAsync(int sceneIndex, bool usePhoton)
    {
        AsyncOperation operation = usePhoton
            ? PhotonNetwork.LoadLevelAsync(sceneIndex)
            : SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
		{
            // TODO: Implement loading screen
            yield return null;
        }
    }
}
