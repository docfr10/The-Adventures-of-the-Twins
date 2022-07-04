using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour//Класс отвечает за передвижение персонажа
{
    [SerializeField] private float speed = 3f; //скорость движения
    [SerializeField] private int lives = 3; //количество жизней
    [SerializeField] private float jump_fource = 0.5f; //сила прыжка
    [SerializeField] private bool is_grounded = false;

    private Rigidbody2D rb;
    private Animator animator;
    private bool is_look_right = true;

    public GameObject bullet;
    public Transform shortPoint;

    private float timeBTWShots;
    public float startTimeBTWShots;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();//Присваиваем значение Rigidbody игрока к переменным в скрипте
        animator = GetComponent<Animator>();//Присваиваем значение Animator игрока к переменным в скрипте
    }

    public enum States //Запись всех состояний анимаций
    {
        idle,
        Move,
        Jump,
        JumpDOWN,
        JumpLand,
        AppleThrow
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
        rb.AddForce(transform.up * 2 * jump_fource, ForceMode2D.Impulse);//Прыжок игрока
        State = States.Jump;//Как только вызывается метод Jump проигрывается анимация прыжка
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
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        is_grounded = collider.Length > 1;
    }

    private void FallСheck()
    {
        if (rb.velocity.y < 0)
            State = States.JumpDOWN;
    }

    private void Throwing()
    {
        Instantiate(bullet, shortPoint.position, transform.rotation);
        timeBTWShots = startTimeBTWShots;
        State = States.AppleThrow;
    }


    // Start is called before the first frame update
    private void Start()
    {

    }

    private void FixedUpdate()
    {
        CheckGround();
        FallСheck();
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

        if (timeBTWShots <= 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Throwing();
            }
        }
        else
        {
            timeBTWShots -= Time.deltaTime;
        }
    }
}
