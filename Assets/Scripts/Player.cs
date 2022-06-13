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
    private SpriteRenderer sprite;

    private void Awake()
    {
        //Присваиваем значения Rigidbody и SpriteRenderer игрока к переменным в скрипте
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Move()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jump_fource, ForceMode2D.Impulse);
    }

    private void CheckGround()
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
        if (Input.GetButton("Horizontal"))
            Move();
        if (is_grounded && Input.GetButton("Jump"))
            Jump();
    }
}
