using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour //����� �������� �� ������������ ���������
{
    public float jump_fource; //���� ������
    [SerializeField] private float speed = 3f; //�������� ��������
    [SerializeField] private int lives = 3; //���������� ������
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
        rb = GetComponent<Rigidbody2D>(); //����������� �������� Rigidbody ������ � ���������� � �������
        animator = GetComponent<Animator>(); //����������� �������� Animator ������ � ���������� � �������
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

    public States State
    {
        get { return (States)animator.GetInteger("state"); } //�������� �������� ��������� ��������
        set { animator.SetInteger("state", (int)value); } //������ �������� ��������� ��������
    }

    private void Move() //�����, ���������� �� ������������ ���������
    {
        if (is_grounded) State = States.Move; //��� ������ ���������� ����� Move ������������� �������� �����������
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime); //����������� ������
        if ((is_look_right == false) && (Input.GetAxisRaw("Horizontal") > 0))
        {
            Flip(); //������� ������ �������
        }
        else if ((is_look_right == true) && (Input.GetAxisRaw("Horizontal") < 0))
        {
            Flip(); //������� ������ ������
        }
    }

    private void Jump() //�����, ���������� �� ������ ���������
    {
        if (is_grounded && Input.GetButton("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); //�������� ������ ��������� ���������, ����� ��� ������������ � ���������� ������� ������ �� ������������
            rb.AddForce(Vector2.up * jump_fource * 2, ForceMode2D.Impulse); //������ ������
            State = States.Jump; //��� ������ ���������� ����� Jump ������������� �������� ������
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Physics2D.IgnoreLayerCollision(8, 9, true); //���������� ���� ��� ������ � ���������
            Invoke("IgnoreLayerOff", 0.7f); //��������� ����� ����������� ������������� ����� ����� n ������
        }
    }

    private void Flip()//�������� ������
    {
        is_look_right = !is_look_right;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void CheckGround() //�������� �� ��, �� ����� �� ��������
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        is_grounded = collider.Length > 1;
    }

    private void Fall�heck() //�������� �� ��, ������ �� ��������
    {
        if (rb.velocity.y < 0 && (State != States.AppleThrow))
            State = States.JumpDOWN;
    }

    private void Throw() //�����, ���������� �� ��������
    {
        Instantiate(bullet, shortPoint.position, transform.rotation);
        timeBTWShots = startTimeBTWShots;
        State = States.AppleThrow;
    }

    public void GetDamage() //�����, ���������� �� ���������� ������ � ������, ���������� � Enemy
    {
        lives -= 1;
        Debug.Log(lives);
    }

    private void IgnoreLayerOff() //����� ��������� ����� ���������� ����������� ����� ���������� � �������
    {
        Physics2D.IgnoreLayerCollision(8, 9, false); 
    }

    public int Lives() //����� ���������� ���������� ������ ������, ��� ���������� ����� �������� ����� ���������
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
        if (lives > 0) //���� ���������� ������ ������ ����, �� ���������� ����� ���������, � ���� ������, �������� "��������������"
        {
            Time.timeScale = 1; //������������ �����
            if (is_grounded) //���� �������� �� ����� � �� ��������, �� ������������� �������� ������� �� �����
                State = States.idle;

            if (Input.GetButton("Horizontal"))
                Move();

            Jump();
            CheckGround();
            Fall�heck();

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
