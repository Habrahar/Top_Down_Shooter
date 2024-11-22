using UnityEngine;
using TMPro;

public class DamagePopup_Controller : MonoBehaviour
{
    public TextMeshPro damageText;
    private float moveSpeed;
    public float lifetime;

    public void Setup(float damage, Vector3 position, float speed, float time)
    {
        moveSpeed = speed;
        lifetime = time;
        transform.position = position;
        damageText.text = damage.ToString();

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
}
