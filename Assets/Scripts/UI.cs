using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour //Класс отвечает за пользовательский интерфейс на уровне
{
    public GameObject DeadScreenUI;

    public void Begin()
    {

    }

    public void Death() //Метод, который вызвает экран проигрыша
    {
        if (FindObjectOfType<Player>().Lives() <= 0) //Если у персонажа 0 или меньше жизней, то вызывается экран проигрыша и останавливается время
        {
            Time.timeScale = 0;
            DeadScreenUI.SetActive(true);
        }
    }

    public void Button_Restart() //Метод вызывается когда нажимается кнопка "Рестарт"
    {
        SceneManager.LoadScene(1); //Перезагружаем сцену с уровнем
    }

    public void Button_Exit() //Метод вызывается когда нажимается кнопка "Выход"
    {
        SceneManager.LoadScene(0); //Выходим в меню
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
