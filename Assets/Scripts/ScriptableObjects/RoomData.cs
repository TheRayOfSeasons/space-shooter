using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This allows the persistence of room data throughout scenes.
/// </summary>
[CreateAssetMenu(fileName = "RoomData", menuName = "RoomData", order = 1)]
public class RoomData : ScriptableObject
{
    public string roomName;
}
