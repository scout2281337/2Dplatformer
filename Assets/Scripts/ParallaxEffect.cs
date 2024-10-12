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
        // �������� ��������� SpriteRenderer �� �������� ��������
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            length = spriteRenderer.bounds.size.x;  // �������� ����� �������
            startPosX = transform.position.x;  // ��������� ������� �������
            initialCamPosX = cam.transform.position.x;  // ��������� ������� ������
        }
        else
        {
            Debug.LogError("SpriteRenderer �� ������ �� �������� ��������!");
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
