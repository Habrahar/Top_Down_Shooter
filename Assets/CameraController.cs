using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Ссылка на игрока
    public Vector3 offset = new Vector3(0f, 10f, -10f); // Смещение камеры относительно игрока
    public float smoothSpeed = 5f; // Скорость сглаживания движения камеры

    private void LateUpdate()
    {
        if (player == null) return;

        // Целевая позиция камеры
        Vector3 targetPosition = player.position + offset;

        // Плавное перемещение камеры
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Камера не поворачивается за игроком (сохраняет текущую ориентацию)
    }
}
