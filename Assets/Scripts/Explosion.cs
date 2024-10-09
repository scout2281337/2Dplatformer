using UnityEngine;

public class Explosion : SoundManager
{
    private Animator animator;

    void Start()
    {


        PlaySound(sounds[0]);
        // Уничтожаем объект через 1 секунду после начала анимации
        Destroy(gameObject, 0.5f); // Настройте время в зависимости от длины вашей анимации
    }
}
