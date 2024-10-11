using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : EnemyAttack
{
    
    


    

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;
            canShoot = true;
            StartShooting(); // Запуск стрельбы
            //AllDirectionShoot();
            //RandomShoot();
        }
    }

    // Метод для отключения режима атаки
    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canShoot = false;
            StopShooting(); // Остановить стрельбу
        }
    }


    


}
