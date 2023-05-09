using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama.TinyJson;

public class MatchDataJson : MonoBehaviour
{
    public static string Position3D(Vector3 position)
    {
        var values = new Dictionary<string, string>
        {
            {"position.x", position.x.ToString() },
            {"position.y", position.y.ToString() },
            {"position.z", position.z.ToString() }
        };

        return values.ToJson();
    }

    public static string Rotation3D(Vector3 rotation)
    {
        var values = new Dictionary<string, string>
        {
            {"rotation.x", rotation.x.ToString() },
            {"rotation.y", rotation.y.ToString() },
            {"rotation.z", rotation.z.ToString() }
          
        };

        return values.ToJson();
    }

    public static string TeleportPlus(float count)
    {
         var values = new Dictionary<string, string>
        {
            {"count", count.ToString()}
           
          
        };
        return values.ToJson();
    }
    public static string Sound(string sound)
    {
         var values = new Dictionary<string, string>
        {
            {"sound", sound}
        };
        return values.ToJson();
    }

    // public static string LeavedGame(int leavedGame)
    // {
    //     var values = new Dictionary<string, string>

    //     {
    //         {"leavedGame", leavedGame.ToString()}
    //     };
        
    //     return values.ToJson();
        
    // }
}
