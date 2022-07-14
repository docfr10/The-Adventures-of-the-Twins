using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Enemy : MonoBehaviour //Класс отвечает за поведение движущегося врага
{
    public float speed;
    [SerializeField] private int health = 4;

    public Rigidbody2D rb;
    public Rigidbody2D player_rb;
    public LayerMask groundLayer;
    public Transform groundCheck;
    private SpriteRenderer sprite;

    private Animator animator;

    bool isFacingRight = true;
    RaycastHit2D hit;

    public enum States //Запись всех состояний анимаций
    {
        Run,
        Punch
    }

    public States State
    {
        get { return (States)animator.GetInteger("state"); } //Получаем активное состояние анимации
        set { animator.SetInteger("state", (int)value); } //Меняем активное состтяние анимации
    }

    private void Move() //Метод, определяющий то куда можно двишаться врагу
    {
        hit = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayer); //Стоим луч и смотрим есть ли впереди земля
        State = States.Run;
    }

    public void TakeDamage(int damage) //Метод, отнимающмй у врага жизнь при попадании снаряда, вызывается в Bullet
    {
        health -= damage;
    }

    private void Death() //Метод, уничтожающий врага при 0 здоровья
    {
        Destroy(gameObject);
    }

    private void Change_Direction() //Метод, который меняет направление движения врага на противоположное
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision) //Проверка столкновения
    {
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player")) //Если игрок столкнулся с врагом
        {
            State = States.Punch;
            FindObjectOfType<Player>().GetDamage(); //У игрока вызывается метод GetDamage() и у игрока отнимается здоровье
            if (isFacingRight)
                player_rb.AddForce(transform.right * 10f, ForceMode2D.Impulse); //Если враг повернут вправо, то игрока отбрасывает вправо
            else
                player_rb.AddForce(-transform.right * 10f, ForceMode2D.Impulse); //Если враг повернут влево, то игрока отбрасывает влево
            Change_Direction(); //После этого враг меняет направление движения
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>(); //Присваиваем значение Animator игрока к переменным в скрипте
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        if (hit.collider != false)
        {
            if (isFacingRight)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        }
        else
        {
            Change_Direction();
        }
        if (health <= 0)
            Death();
    }
}
