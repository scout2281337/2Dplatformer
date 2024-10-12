using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float length, startPosX;
    public GameObject cam;
    public float parallaxEffect;

    private float initialCamPosX;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Получаем компонент SpriteRenderer из дочерних объектов
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            length = spriteRenderer.bounds.size.x;  // Получаем длину спрайта
            startPosX = transform.position.x;  // Стартовая позиция объекта
            initialCamPosX = cam.transform.position.x;  // Начальная позиция камеры
        }
        else
        {
            Debug.LogError("SpriteRenderer не найден на дочерних объектах!");
        }
    }

    void Update()
    {
        if (spriteRenderer != null)
        {
            float camMoveDistance = cam.transform.position.x - initialCamPosX;
            float dist = camMoveDistance * parallaxEffect;

            transform.position = new Vector3(startPosX + dist, transform.position.y, transform.position.z);

            float temp = cam.transform.position.x * (1 - parallaxEffect);
            if (temp > startPosX + length) startPosX += length;
            else if (temp < startPosX - length) startPosX -= length;
        }
    }
}
