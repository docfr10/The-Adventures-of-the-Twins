using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Enemy : MonoBehaviour //����� �������� �� ��������� ����������� �����
{
    public float speed;
    [SerializeField] private int health = 4;

    public Rigidbody2D rb;
    public Rigidbody2D player_rb;
    public LayerMask groundLayer;
    public Transform groundCheck;
    private SpriteRenderer sprite;

    private Animator animator;

    bool isFacingRight = true;
    RaycastHit2D hit;

    public enum States //������ ���� ��������� ��������
    {
        Run,
        Punch
    }

    public States State
    {
        get { return (States)animator.GetInteger("state"); } //�������� �������� ��������� ��������
        set { animator.SetInteger("state", (int)value); } //������ �������� ��������� ��������
    }

    private void Move() //�����, ������������ �� ���� ����� ��������� �����
    {
        hit = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayer); //����� ��� � ������� ���� �� ������� �����
        State = States.Run;
    }

    public void TakeDamage(int damage) //�����, ���������� � ����� ����� ��� ��������� �������, ���������� � Bullet
    {
        health -= damage;
    }

    private void Death() //�����, ������������ ����� ��� 0 ��������
    {
        Destroy(gameObject);
    }

    private void Change_Direction() //�����, ������� ������ ����������� �������� ����� �� ���������������
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision) //�������� ������������
    {
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player")) //���� ����� ���������� � ������
        {
            State = States.Punch;
            FindObjectOfType<Player>().GetDamage(); //� ������ ���������� ����� GetDamage() � � ������ ���������� ��������
            if (isFacingRight)
                player_rb.AddForce(transform.right * 10f, ForceMode2D.Impulse); //���� ���� �������� ������, �� ������ ����������� ������
            else
                player_rb.AddForce(-transform.right * 10f, ForceMode2D.Impulse); //���� ���� �������� �����, �� ������ ����������� �����
            Change_Direction(); //����� ����� ���� ������ ����������� ��������
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>(); //����������� �������� Animator ������ � ���������� � �������
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
            Change_Direction();
        }
        if (health <= 0)
            Death();
    }
}
