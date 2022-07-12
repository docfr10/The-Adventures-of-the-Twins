using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Enemy : MonoBehaviour //Класс отвечает за поведение движущегося врага
{
    public float speed;
    [SerializeField] private int health = 4;

    public Rigidbody2D rb;
    public LayerMask groundLayer;
    public Transform groundCheck;
    private SpriteRenderer sprite;

    bool isFacingRight = true;
    RaycastHit2D hit;

    private void Move()
    {
        hit = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayer);
    }

    public void TakeDamage(int damage) //Метод, отнимающмй у врага жизнь при попадании снаряда, вызывается в Bullet
    {
        health -= damage;
    }

    private void Death() //Метод, уничтожающий врага при 0 здоровья
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) //Проверка столкновения с игроком, если игрок столкнулся со врагом у игрока отнимаются жизни
    {
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
            FindObjectOfType<Player>().GetDamage(); //Если игрок столкнулся с врагом у игрока вызывается метод GetDamage() и у игрока отнимается здоровье
    }

    // Start is called before the first frame update
    private void Start()
    {

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
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
        }
        if (health <= 0)
            Death();
    }
}
