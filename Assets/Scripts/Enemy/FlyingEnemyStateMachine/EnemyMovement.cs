using UnityEngine;
using Zenject;

public class EnemyMovement : MonoBehaviour
{


    [SerializeField] private EnemyScriptableObject basicEnemy;
    [SerializeField] private Animator anim;

    
    private IEnemyState currentState; // Текущее состояние
    private Rigidbody2D rb;
    
    public Transform groundCheck;
    public GameObject sprite;
    private GameObject player;
    public Transform playerTransform;

    [Inject]
    public void Constructor(GameObject player) 
    {
        this.player = player;      
    
    }

    private void Start()
    {
        // Пример поиска дочернего объекта по имени
        groundCheck = transform.Find("GroundCheck");
        sprite = transform.Find("spriteOfenemy").gameObject;
        playerTransform = player.transform; 

        rb = GetComponent<Rigidbody2D>();
        SwitchState(new PatrolState(this)); // Начинаем с состояния патрулирования
    }

    private void Update()
    {
        currentState.Update(); // Обновляем текущее состояние
        anim.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x)); // Запуск анимации
    }

    public void SwitchState(IEnemyState newState)
    {
        currentState?.Exit(); // Выход из старого состояния
        currentState = newState; // Переход в новое состояние
        currentState.Enter(); // Вход в новое состояние
    }

    public void Move(Vector3 direction)
    {
        rb.velocity = new Vector2(direction.x * basicEnemy.speed, rb.velocity.y);
    }

    public void Flip()
    {
        Vector3 theScale = sprite.transform.localScale;
        theScale.x *= -1;
        sprite.transform.localScale = theScale;
    }
}
