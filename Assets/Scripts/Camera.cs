using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform transform_player;
    private Vector3 position;//� ������ ������ ����� ������������ ���������� ��������

    private void Awake()//� ������ ������ ����� "��������" �����
    {
        if (!transform_player)//�������� �� �� ������ ����� ��� ���
            transform_player = FindObjectOfType<Player>().transform;
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        position = transform_player.position;//�������� ���������� ������
        position.z = -10f;

        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime);//���������� ������ � ��� ����������
    }
}
