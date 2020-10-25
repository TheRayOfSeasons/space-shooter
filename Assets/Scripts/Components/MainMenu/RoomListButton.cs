using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class RoomListButton : MonoBehaviour
{
    public string roomName;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => {
            MenuManager.Instance.roomData.roomName = this.roomName;
            StartCoroutine(LoadAsync(1));
        });
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
		{
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
