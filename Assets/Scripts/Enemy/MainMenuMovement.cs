using UnityEngine;

public class MainMenuMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform pointA; // Точка A
    public Transform pointB; // Точка B
    public float moveSpeed = 2f; // Скорость бега
    public float jumpForce = 5f; // Сила прыжка
    public float jumpInterval = 2f; // Интервал прыжков (в секундах)

    private Transform targetPoint; // Текущая цель (A или B)
    private Rigidbody2D rb;
    private float jumpTimer; // Таймер для прыжков
    [SerializeField] private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPoint = pointA; // Начинаем движение к точке A
        jumpTimer = jumpInterval; // Устанавливаем начальное время для прыжка
    }

    void Update()
    {
        // Лог для отладки скорости
        //Debug.Log(rb.velocity.x);

        // Передача скорости в анимацию
        anim.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));

        // Перемещение к точке
        MoveToTarget();

        // Прыжок через определенное время
        HandleJump();
    }

    private void MoveToTarget()
    {
        // Рассчитываем направление к целевой точке
        Vector2 direction = (targetPoint.position - transform.position).normalized;

        // Устанавливаем горизонтальную скорость
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        // Проверяем, достигли ли точки
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            // Меняем цель на противоположную
            targetPoint = targetPoint == pointA ? pointB : pointA;

            // Разворачиваем врага по горизонтали
            Flip();
        }
    }

    private void HandleJump()
    {
        jumpTimer -= Time.deltaTime;

        if (jumpTimer <= 0)
        {
            // Прыжок вверх
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimer = jumpInterval; // Сброс таймера
        }
    }

    private void Flip()
    {
        // Разворачиваем врага по оси X
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
