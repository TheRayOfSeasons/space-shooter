using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags
{
    public static readonly string PLAYER = "Player";
    public static readonly string PLAYERBULLET = "PlayerBullet";
    public static readonly string PLAYERFRIENDLY = "PlayerFriendly";
    public static readonly string ENEMY = "Enemy";
    public static readonly string ENEMYBULLET = "EnemyBullet";

    public static readonly List<string> collection = new List<string>()
    {
        PLAYER,
        PLAYERBULLET,
        PLAYERFRIENDLY,
        ENEMY,
        ENEMYBULLET
    };

    public static bool Validate(string tag, bool throwInvalid = true)
    {
        bool valid = collection.Contains(tag);
        string message = (
            $"{tag} evaluated as invalid. Did you forget to add it in the Tags class?");
        if(!valid && throwInvalid)
            throw new System.ArgumentException(message, "tag");
        if(!valid && !throwInvalid)
            Debug.Log(message);

        return valid;
    }
}
