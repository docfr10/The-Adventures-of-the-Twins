using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour //�����, ���������� �� �������� �������
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;
    public GameObject bullet;
    public bool is_active = true;
    public bool is_look_right__;

    // Start is called before the first frame update
    private void Start()
    {
        is_look_right__ = Player.is_look_right; //�������� �������� ���� ���� �������� ����� ��� ���� ����� ��������� ������ ������ ��� �����
    }

    private void OnBecameInvisible() //���� ������ ���� �� ���� ������ ������ ������, �� �� ������������
    {
        Destroy(gameObject);    
    }

    public void Throwing() //�����, �������� �������� �������
    {
        if (is_look_right__ == true)
        {
            gameObject.transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if (is_look_right__ == false)
        {
            gameObject.transform.Translate(-Vector2.right * speed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid); //������ ���������� ���� �������
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Standing_Enemy")) //������������ �� ������� ������
            {
                hitInfo.collider.GetComponent<Standing_Enemy>().TakeDamage(damage); //���� ������ ���������� �� ������ � ����� ���������� ��������
            }
            if (hitInfo.collider.CompareTag("Moving_Enemy")) //������������ � ���������� ������
            {
                hitInfo.collider.GetComponent<Moving_Enemy>().TakeDamage(damage); //���� ������ ���������� �� ������ � ����� ���������� ��������
            }
            Destroy(gameObject); //��� ������ ������������
        }
        Throwing();
    }
}