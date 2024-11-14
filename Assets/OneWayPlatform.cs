using System.Collections;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private GameObject player;
    private CapsuleCollider2D playerCollider;
    private BoxCollider2D platformCollider;
    private bool isOnPlatform = false;

    private void Start()
    {
        player = PlayerManager.instance.player; // Найти игрока по тегу
        playerCollider = player.GetComponent<CapsuleCollider2D>();
        platformCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // Если игрок на платформе и нажата клавиша, отключить коллайдер временно
        if (isOnPlatform && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            StartCoroutine(DisableCollision());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            isOnPlatform = true; // Игрок находится на платформе
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            isOnPlatform = false; // Игрок покидает платформу
        }
    }

    private IEnumerator DisableCollision()
    {
        if (platformCollider != null && playerCollider != null)
        {
            Debug.Log("Disabling collision"); // Отладочное сообщение
            Physics2D.IgnoreCollision(playerCollider, platformCollider, true);
            yield return new WaitForSeconds(0.5f);
            Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
            Debug.Log("Re-enabling collision"); // Отладочное сообщение
        }
        else
        {
            Debug.LogWarning("Platform or player collider missing"); // Сообщение при отсутствии коллайдера
        }
    }
}
