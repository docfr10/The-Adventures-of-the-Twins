using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Standing_Enemy : MonoBehaviour //Класс отвечает за поведение неподвижного врага
{
    public int health;
    public bool is_look_right__;

    public Rigidbody2D rb;

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
        {
            FindObjectOfType<Player>().GetDamage(); //Если игрок столкнулся с врагом у игрока вызывается метод GetDamage() и у игрока отнимается здоровье
            if (is_look_right__)
                rb.AddForce(-transform.right * 10f, ForceMode2D.Impulse); //Если игрок повернут вправо, то игрока отбрасывает влево
            else
                rb.AddForce(transform.right * 10f, ForceMode2D.Impulse); //Если игрок повернут влево, то игрока отбрасывает вправо
        }
    }

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        is_look_right__ = Player.is_look_right;

        if (health <= 0)
            Death();
    }
}
