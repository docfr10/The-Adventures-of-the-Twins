using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour //����� �������� �� ���������������� ��������� �� ������
{
    public GameObject DeadScreenUI, WinScreenUI;

    public void Begin()
    {

    }

    public void Win() //�����, ������� �������� ����� ��������
    {
        if (!(GameObject.FindGameObjectWithTag("Standing_Enemy")) && !(GameObject.FindGameObjectWithTag("Moving_Enemy"))) //���� ��� ����� ����������, �� ����� �������
        {
            Time.timeScale = 0;
            WinScreenUI.SetActive(true);
        }
    }

    public void Death() //�����, ������� ������� ����� ���������
    {
        if (FindObjectOfType<Player>().Die()) //���� � ��������� 0 ��� ������ ������, �� ���������� ����� ��������� � ��������������� �����
        {
            Time.timeScale = 0;
            DeadScreenUI.SetActive(true);
        }
    }

    public void Button_Restart() //����� ���������� ����� ���������� ������ "�������"
    {
        SceneManager.LoadScene(1); //������������� ����� � �������
    }

    public void Button_Next_Level() //����� ���������� ����� ���������� ������ �������� �� ��������� �������
    {
        Debug.Log("Pressed");
        //��� ���������� ������ ������ �������� SceneManager.LoadScene(����� ����� �� ��������� �������)
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
        Win();
        Death();
    }
}
