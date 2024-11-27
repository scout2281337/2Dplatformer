using UnityEngine;

public class PatrolState : IEnemyState
{
    public EnemyScriptableObject basicEnemy;
    private EnemyMovement enemy;
    private bool facingRight;
    public PatrolState(EnemyMovement enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Entering Patrol State");
    }

    public void Exit()
    {
        Debug.Log("Exiting Patrol State");
    }

    public void Update()
    {
        // Проверка на наличие платформы перед врагом
        bool isGroundAhead = Physics2D.Raycast(enemy.groundCheck.position, Vector2.down,  basicEnemy.checkDistance, basicEnemy.groundLayer);

        if (!isGroundAhead)
        {
            // Если нет платформы, меняем направление
            facingRight = !facingRight;
            enemy.Flip();
        }

        // Двигаемся в текущем направлении
        Vector3 direction = facingRight ? Vector3.right : Vector3.left;
        enemy.Move(direction);

        // Если игрок находится в радиусе действия, переходим в режим преследования
        if (Vector3.Distance(enemy.transform.position, enemy.player.position) < 5f) // Примерный радиус
        {
            enemy.SwitchState(new ChaseState(enemy));
        }
    }
}
