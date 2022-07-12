using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Standing_Enemy : MonoBehaviour //����� �������� �� ��������� ������������ �����
{
    public int health;

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
            FindObjectOfType<Player>().GetDamage(); //���� ����� ���������� � ������ � ������ ���������� ����� GetDamage() � � ������ ���������� ��������
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (health <= 0)
            Death();
    }
}
