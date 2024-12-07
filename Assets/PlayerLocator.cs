using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocator : MonoBehaviour
{
   public static Transform PlayerTransform { get; private set; }

    public void Update(){
        Locate();
    }
    public void Locate()
    {
        if (PlayerTransform == null)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                PlayerTransform = player.transform;
            }
            else
            {
                Debug.LogError("PlayerLocator: Player object with tag 'Player' not found.");
            }
        }
        else
        {
            Debug.LogWarning("PlayerLocator: " + PlayerTransform);
        }
    }
}
