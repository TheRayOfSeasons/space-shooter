using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomList : MonoBehaviour
{
    public List<RoomListButton> roomButtons;

    public float intervals = 50f;
    [SerializeField] private GameObject roomButtonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        string[] values = {"room1", "room2", "room3", "room4"};
        this.RenderRoomButtons(values);
    }

    public void RenderRoomButtons(string[] roomNames)
    {
        RectTransform listRect = this.GetComponent<RectTransform>();
        float offsetY = 0f;
        foreach(string roomName in roomNames)
        {
            GameObject button = Instantiate(roomButtonPrefab);
            RectTransform buttonRect = button.GetComponent<RectTransform>();

            button.transform.parent = this.transform;
            buttonRect.position = new Vector3(
                listRect.position.x,
                listRect.position.y + offsetY,
                listRect.position.z
            );

            button.GetComponent<RoomListButton>().roomName = roomName;
            button.GetComponentInChildren<Text>().text = roomName;

            offsetY -= this.intervals;
        }
    }

    public void AddRoomButton()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
