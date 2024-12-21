using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Ссылка на игрока
    public Vector3 offset = new Vector3(0f, 10f, -10f); // Смещение камеры относительно игрока
    public float smoothSpeed = 5f; // Скорость сглаживания движения камеры

    private void LateUpdate()
    {
        if (player == null) return;
        
        Vector3 targetPosition = player.position + offset;
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
