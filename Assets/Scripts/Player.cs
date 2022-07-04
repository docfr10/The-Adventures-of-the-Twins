using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour//����� �������� �� ������������ ���������
{
    [SerializeField] private float speed = 3f; //�������� ��������
    [SerializeField] private int lives = 3; //���������� ������
    [SerializeField] private float jump_fource = 0.5f; //���� ������
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
        rb = GetComponent<Rigidbody2D>();//����������� �������� Rigidbody ������ � ���������� � �������
        animator = GetComponent<Animator>();//����������� �������� Animator ������ � ���������� � �������
    }

    public enum States //������ ���� ��������� ��������
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
        get { return (States)animator.GetInteger("state"); }//�������� �������� ��������� ��������
        set { animator.SetInteger("state", (int)value); }//������ �������� ��������� ��������
    }

    private void Move()
    {
        if (is_grounded) State = States.Move;//��� ������ ���������� ����� Move ������������� �������� �����������
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);//����������� ������
        Flip();//������� ������ ������ ��� �������
    }

    private void Jump()
    {
        rb.AddForce(transform.up * 2 * jump_fource, ForceMode2D.Impulse);//������ ������
        State = States.Jump;//��� ������ ���������� ����� Jump ������������� �������� ������
    }

    private void Flip()//�������� ������
    {
        if (Input.GetAxisRaw("Horizontal") == 1)
            transform.localScale = new Vector3(1, 1, 1);
        else if (Input.GetAxisRaw("Horizontal") == -1)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void CheckGround()//�������� �� ��, �� ����� �� ��������
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        is_grounded = collider.Length > 1;
    }

    private void Fall�heck()
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
        Fall�heck();
    }

    // Update is called once per frame
    private void Update()
    {
        if (is_grounded)//���� �������� �� �����, �� ������������� �������� ������� �� �����
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
