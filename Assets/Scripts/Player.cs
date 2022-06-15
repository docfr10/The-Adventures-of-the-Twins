using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 3f; //скорость движения
    [SerializeField] private int lives = 3; //количество жизней
    [SerializeField] private float jump_fource = 0.2f; //сила прыжка
    [SerializeField] private bool is_grounded = false;

    private Rigidbody2D rb;
    private bool is_look_right = true;

    private void Awake()
    {
        //Присваиваем значения Rigidbody игрока к переменным в скрипте
        rb = GetComponent<Rigidbody2D>();
    }

    private void Move()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        //Перемещение игрока
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        //Поворот игрока налево или направо
        Flip();
    }

    private void Jump()
    {
        //Прыжок игрока
        rb.AddForce(transform.up * jump_fource, ForceMode2D.Impulse);
    }

    private void Flip()
    {
        if (Input.GetAxisRaw("Horizontal") == 1)
            transform.localScale = new Vector3(1, 1, 1);
        else if (Input.GetAxisRaw("Horizontal") == -1)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void CheckGround()
    {
        //Проверка на то, на земле ли персонаж
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 1f);
        is_grounded = collider.Length > 1;
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButton("Horizontal"))
            Move();
        if (is_grounded && Input.GetButton("Jump"))
            Jump();
    }
}
