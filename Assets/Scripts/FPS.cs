using UnityEngine;
using TMPro;

public class FPS : MonoBehaviour
{
    private TextMeshProUGUI fpsCounter; // Исправлено имя переменной для единообразия
    private float deltaTime = 0.0f; // Изменено на маленькую букву

    // Start is called before the first frame update
    private void Start()
    {
        fpsCounter = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Обновляем deltaTime, вычисляя его как усредненное время между кадрами
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        // Проверка на ноль, чтобы избежать деления на ноль
        if (deltaTime > 0)
        {
            float fps = 1.0f / deltaTime;

            // Обновляем текст, если fpsCounter не null
            if (fpsCounter != null)
            {
                fpsCounter.text = Mathf.Ceil(fps).ToString();
            }
        }
    }
}
