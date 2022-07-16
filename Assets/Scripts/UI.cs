using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour //����� �������� �� ���������������� ��������� �� ������
{
    public GameObject DeadScreenUI;

    public void Begin()
    {

    }

    public void Death() //�����, ������� ������� ����� ���������
    {
        if (FindObjectOfType<Player>().Lives() <= 0) //���� � ��������� 0 ��� ������ ������, �� ���������� ����� ��������� � ��������������� �����
        {
            Time.timeScale = 0;
            DeadScreenUI.SetActive(true);
        }
    }

    public void Button_Restart() //����� ���������� ����� ���������� ������ "�������"
    {
        SceneManager.LoadScene(1); //������������� ����� � �������
    }

    public void Button_Exit() //����� ���������� ����� ���������� ������ "�����"
    {
        SceneManager.LoadScene(0); //������� � ����
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Death();
    }
}
