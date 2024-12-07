using UnityEngine;

public class ChaseState : IEnemyState
{
    public EnemyScriptableObject basicEnemy;
    private EnemyMovement enemy;
    private bool facingRight;

    public ChaseState(EnemyMovement enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Entering Chase State");
    }

    public void Exit()
    {
        Debug.Log("Exiting Chase State");
    }

    public void Update()
    {
        // Проверка на наличие платформы перед врагом
        bool isGroundAhead = Physics2D.Raycast(enemy.groundCheck.position, Vector2.down, basicEnemy.checkDistance, basicEnemy.groundLayer);

        if (!isGroundAhead)
        {
            // Если нет платформы, останавливаемся
            enemy.Move(Vector3.zero);
            return;
        }

        // Преследуем игрока
        Vector3 direction = new Vector3(enemy.playerTransform.position.x - enemy.transform.position.x, 0, 0).normalized;
        enemy.Move(direction);

        // Поворот персонажа в зависимости от направления
        if ((direction.x > 0 && !facingRight) || (direction.x < 0 && facingRight))
        {
            facingRight = !facingRight;
            enemy.Flip();
        }
    }
}
