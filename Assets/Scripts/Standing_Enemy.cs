using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Standing_Enemy : MonoBehaviour //����� �������� �� ��������� ������������ �����
{
    public int health;
    public bool is_look_right__;

    public Rigidbody2D rb;

    public void TakeDamage(int damage) //�����, ���������� � ����� ����� ��� ��������� �������, ���������� � Bullet
    {
        health -= damage;
    }

    private void Death() //�����, ������������ ����� ��� 0 ��������
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) //�������� ������������ � �������, ���� ����� ���������� �� ������ � ������ ���������� �����
    {
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            FindObjectOfType<Player>().GetDamage(); //���� ����� ���������� � ������ � ������ ���������� ����� GetDamage() � � ������ ���������� ��������
            if (is_look_right__)
                rb.AddForce(-transform.right * 10f, ForceMode2D.Impulse); //���� ����� �������� ������, �� ������ ����������� �����
            else
                rb.AddForce(transform.right * 10f, ForceMode2D.Impulse); //���� ����� �������� �����, �� ������ ����������� ������
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
