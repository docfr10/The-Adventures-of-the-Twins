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
    private Animator animator;
    private bool is_look_right = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();//Присваиваем значение Rigidbody игрока к переменным в скрипте
        animator = GetComponent<Animator>();//Присваиваем значение Animator игрока к переменным в скрипте
    }

    public enum States //Запись всех состояний анимаций
    {
        idle,
        Move
    }

    private States State
    {
        get { return (States)animator.GetInteger("state"); }//Получаем активное состояние анимации
        set { animator.SetInteger("state", (int)value); }//Меняем активное состтяние анимации
    }

    private void Move()
    {
        if (is_grounded) State = States.Move;//Как только вызывается метод Move проигрывается анимация перемещения
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);//Перемещение игрока
        Flip();//Поворот игрока налево или направо
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jump_fource, ForceMode2D.Impulse);//Прыжок игрока
    }

    private void Flip()//Повороты игрока
    {
        if (Input.GetAxisRaw("Horizontal") == 1)
            transform.localScale = new Vector3(1, 1, 1);
        else if (Input.GetAxisRaw("Horizontal") == -1)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void CheckGround()//Проверка на то, на земле ли персонаж
    {
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
        if (is_grounded)//Если персонаж на земле, то проигрывается анимация стояния на месте
            State = States.idle;
        if (Input.GetButton("Horizontal"))
            Move();
        if (is_grounded && Input.GetButton("Jump"))
            Jump();
    }
}
