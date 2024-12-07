using UnityEngine;

public class PlayerLocator : MonoBehaviour
{
   public static Transform PlayerTransform { get; private set; }

    public static void RegisterPlayer(Transform playerTransform)
    {
        if (PlayerTransform == null)
        {
            PlayerTransform = playerTransform;
            Debug.Log("PlayerLocator: Player registered.");
        }
        else
        {
            Debug.LogWarning("PlayerLocator: Player already registered.");
        }
    }

    public static void UnregisterPlayer()
    {
        PlayerTransform = null;
        Debug.Log("PlayerLocator: Player unregistered.");
    }
}
