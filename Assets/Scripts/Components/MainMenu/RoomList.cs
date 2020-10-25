using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomList : MonoBehaviour
{
    public List<RoomListButton> roomButtons;

    public float intervals = 50f;
    [SerializeField] private GameObject roomButtonPrefab;

    void OnReceivedRoomListUpdate()
    {
        this.RenderRoomButtons(PhotonRoomService.GetRooms());
    }

    public void RenderRoomButtons(RoomInfo[] rooms)
    {
        RectTransform listRect = this.GetComponent<RectTransform>();
        float offsetY = 0f;
        // TODO: Scale using pagination. Do not load all rooms each call.
        foreach(RoomInfo room in rooms)
        {
            GameObject button = Instantiate(roomButtonPrefab);
            RectTransform buttonRect = button.GetComponent<RectTransform>();
            string roomName = room.Name;

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
}
