using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour //����� �������� �� ���������������� ��������� �� ������
{
    public GameObject DeadScreenUI, WinScreenUI, PauseMenuUI;
    private bool GameIsPause = false;

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

    public void Pause() //����� ���������� ���� �� �����
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
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

    public void Resume() //����� ������ ���� � �����
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
    }

    public void Button_Exit() //����� ���������� ����� ���������� ������ "�����"
    {
        SceneManager.LoadScene(0); //������� � ����
    }

    public bool IsPause() //�����, ������� ���������� �������� ���� �� ����� ���� ��� ���
    {
        return GameIsPause;
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //���� ���� ������ ������� ESC
        {
            if (GameIsPause) //���� ���� �� ����� ���������� ����� ������ � �����
            {
                Resume();
            }
            else //����� ���� ���� �� �� ����� ���������� ����� ���������� ���� �� �����
            {
                Pause();
            }
        }
        Win();
        Death();
    }
}
