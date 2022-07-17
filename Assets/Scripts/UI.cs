using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour //Класс отвечает за пользовательский интерфейс на уровне
{
    public GameObject DeadScreenUI, WinScreenUI, PauseMenuUI;
    private bool GameIsPause = false;

    public void Win() //Метод, который вызывает экран выигрыша
    {
        if (!(GameObject.FindGameObjectWithTag("Standing_Enemy")) && !(GameObject.FindGameObjectWithTag("Moving_Enemy"))) //Если все враги уничтожены, то игрок победил
        {
            Time.timeScale = 0;
            WinScreenUI.SetActive(true);
        }
    }

    public void Death() //Метод, который вызвает экран проигрыша
    {
        if (FindObjectOfType<Player>().Die()) //Если у персонажа 0 или меньше жизней, то вызывается экран проигрыша и останавливается время
        {
            Time.timeScale = 0;
            DeadScreenUI.SetActive(true);
        }
    }

    public void Pause() //Метод постановки игры на паузу
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    }

    public void Button_Restart() //Метод вызывается когда нажимается кнопка "Рестарт"
    {
        SceneManager.LoadScene(1); //Перезагружаем сцену с уровнем
    }

    public void Button_Next_Level() //Метод вызывается когда нажимается кнопка перехода на следующий уровень
    {
        Debug.Log("Pressed");
        //При добавлении нового уровня добавить SceneManager.LoadScene(Номер сцены со следующим уровнем)
    }

    public void Resume() //Метод снятия игры с паузы
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
    }

    public void Button_Exit() //Метод вызывается когда нажимается кнопка "Выход"
    {
        SceneManager.LoadScene(0); //Выходим в меню
    }

    public bool IsPause() //Метод, который возвращает значение того на паузе игра или нет
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
        if (Input.GetKeyDown(KeyCode.Escape)) //Если была нажата клавиша ESC
        {
            if (GameIsPause) //Если игра на паузе вызывается метод снятия с паузы
            {
                Resume();
            }
            else //Иначе если игра не на паузе вызывается метод постановки игры на паузу
            {
                Pause();
            }
        }
        Win();
        Death();
    }
}
