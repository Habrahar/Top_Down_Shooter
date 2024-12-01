using UnityEngine;
using TMPro;

public class DamagePopup_Controller : MonoBehaviour
{
    public TextMeshPro damageText;
    private float moveSpeed;
    private float lifetime;
    private Vector3 movementDirection;
    private float verticalSpeed; // Начальная скорость подъема
    private float gravity;       // Гравитация для падения
    private float fadeDelay;     // Задержка перед началом фейда
    private float fadeStartTime; // Время начала фейда
    
    public void Setup(
        float damage, 
        Vector3 position, 
        float speed, 
        float time, 
        float arcAngle, 
        float initialVerticalSpeed, 
        float gravityForce, 
        float fadeDelay)
    {
        moveSpeed = speed;
        lifetime = time;
        verticalSpeed = initialVerticalSpeed;
        gravity = gravityForce;
        this.fadeDelay = fadeDelay;

        transform.position = position;
        damageText.text = damage.ToString();

        fadeStartTime = Time.time + fadeDelay; 

        movementDirection = GetRandomDirection(arcAngle);

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        verticalSpeed -= gravity * Time.deltaTime;
        Vector3 totalMovement = movementDirection * moveSpeed + Vector3.up * verticalSpeed;
        transform.Translate(totalMovement * Time.deltaTime);
        
        HandleFadeOut();
    }
    
    private void HandleFadeOut()
    {
        float timeUntilDestroy = lifetime - (Time.time - (fadeStartTime - fadeDelay));

        if (Time.time >= fadeStartTime)
        {
            float fadeProgress = 1 - (timeUntilDestroy / (lifetime - fadeDelay));

            Color currentColor = damageText.color;

            currentColor.a = Mathf.Lerp(1f, 0f, fadeProgress);
            damageText.color = currentColor;
        }
    }
    
    private Vector3 GetRandomDirection(float arcAngle)
    {
        float randomAngle = Random.Range(-arcAngle / 2, arcAngle / 2);
        Quaternion rotation = Quaternion.Euler(0, 0, randomAngle);
        return rotation * Vector3.up; // Поворот базового направления
    }
}
