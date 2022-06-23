using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 3f; //�������� ��������
    [SerializeField] private int lives = 3; //���������� ������
    [SerializeField] private float jump_fource = 0.2f; //���� ������
    [SerializeField] private bool is_grounded = false;

    private Rigidbody2D rb;
    private Animator animator;
    private bool is_look_right = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();//����������� �������� Rigidbody ������ � ���������� � �������
        animator = GetComponent<Animator>();//����������� �������� Animator ������ � ���������� � �������
    }

    public enum States //������ ���� ��������� ��������
    {
        idle,
        Move
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
        rb.AddForce(transform.up * jump_fource, ForceMode2D.Impulse);//������ ������
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
        if (is_grounded)//���� �������� �� �����, �� ������������� �������� ������� �� �����
            State = States.idle;
        if (Input.GetButton("Horizontal"))
            Move();
        if (is_grounded && Input.GetButton("Jump"))
            Jump();
    }
}
