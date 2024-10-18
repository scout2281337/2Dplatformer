using UnityEngine;
using TMPro;

public class FPS : MonoBehaviour
{
    private TextMeshProUGUI fpsCounter; // ���������� ��� ���������� ��� ������������
    private float deltaTime = 0.0f; // �������� �� ��������� �����

    // Start is called before the first frame update
    private void Start()
    {
        fpsCounter = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // ��������� deltaTime, �������� ��� ��� ����������� ����� ����� �������
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        // �������� �� ����, ����� �������� ������� �� ����
        if (deltaTime > 0)
        {
            float fps = 1.0f / deltaTime;

            // ��������� �����, ���� fpsCounter �� null
            if (fpsCounter != null)
            {
                fpsCounter.text = Mathf.Ceil(fps).ToString();
            }
        }
    }
}
