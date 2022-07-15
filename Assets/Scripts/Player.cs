using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour //Класс отвечает за передвижение персонажа
{
    public float jump_fource; //сила прыжка
    [SerializeField] private float speed = 3f; //скорость движения
    [SerializeField] private int lives = 3; //количество жизней
    [SerializeField] private bool is_grounded = false;

    public static Rigidbody2D rb;
    private Animator animator;
    public static bool is_look_right = true;

    public GameObject bullet;
    public Transform shortPoint;

    private float timeBTWShots;
    public float startTimeBTWShots;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //Присваиваем значение Rigidbody игрока к переменным в скрипте
        animator = GetComponent<Animator>(); //Присваиваем значение Animator игрока к переменным в скрипте
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

    public States State
    {
        get { return (States)animator.GetInteger("state"); } //Получаем активное состояние анимации
        set { animator.SetInteger("state", (int)value); } //Меняем активное состтяние анимации
    }

    private void Move() //Метод, отвечающий за передвижение персонажа
    {
        if (is_grounded) State = States.Move; //Как только вызывается метод Move проигрывается анимация перемещения
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime); //Перемещение игрока
        if ((is_look_right == false) && (Input.GetAxisRaw("Horizontal") > 0))
        {
            Flip(); //Поворот игрока направо
        }
        else if ((is_look_right == true) && (Input.GetAxisRaw("Horizontal") < 0))
        {
            Flip(); //Поворот игрока налево
        }
    }

    private void Jump() //Метод, отвечающий за прыжок персонажа
    {
        if (is_grounded && Input.GetButton("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); //Обнуляем вектор ускорения персонажа, чтобы при столкновении с платформой импульс прыжка не увеличивался
            rb.AddForce(Vector2.up * jump_fource * 2, ForceMode2D.Impulse); //Прыжок игрока
            State = States.Jump; //Как только вызывается метод Jump проигрывается анимация прыжка
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Physics2D.IgnoreLayerCollision(8, 9, true); //Игнорируем слои для Игрока и Платформы
            Invoke("IgnoreLayerOff", 0.7f); //Запускаем метод отключающий игнорирование слоев через n секунд
        }
    }

    private void Flip()//Повороты игрока
    {
        is_look_right = !is_look_right;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void CheckGround() //Проверка на то, на земле ли персонаж
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        is_grounded = collider.Length > 1;
    }

    private void FallСheck() //Проверка на то, падает ли персонаж
    {
        if (rb.velocity.y < 0 && (State != States.AppleThrow))
            State = States.JumpDOWN;
    }

    private void Throw() //Метод, отвечающий за стрельбу
    {
        Instantiate(bullet, shortPoint.position, transform.rotation);
        timeBTWShots = startTimeBTWShots;
        State = States.AppleThrow;
    }

    public void GetDamage() //Метод, отвечающий за отнимаение жизней у игрока, вызывается в Enemy
    {
        lives -= 1;
        Debug.Log(lives);
    }

    private void IgnoreLayerOff() //Метод позволяет вновь определять столновения между Платформой и Игроком
    {
        Physics2D.IgnoreLayerCollision(8, 9, false); 
    }

    public int Lives() //Метод возвращает количество жизней игрока, это необходимо чтобы вызвался экран проигрыша
    {
        return lives;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Moving_Platform")
            this.transform.parent = collision.transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Moving_Platform")
            this.transform.parent = null;
    }

    // Start is called before the first frame update
    private void Start()
    {

    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (lives > 0) //Если количество жизней больше нуля, то персонажем можно управлять, в ином случае, персонаж "замораживается"
        {
            Time.timeScale = 1; //Возобновляем время
            if (is_grounded) //Если персонаж на земле и не стреляет, то проигрывается анимация стояния на месте
                State = States.idle;

            if (Input.GetButton("Horizontal"))
                Move();

            Jump();
            CheckGround();
            FallСheck();

            if (timeBTWShots <= 0)
            {
                if (Input.GetKey(KeyCode.R))
                {
                    Throw();
                }
            }
            else
            {
                timeBTWShots -= Time.deltaTime;
            }
        }
    }
}
