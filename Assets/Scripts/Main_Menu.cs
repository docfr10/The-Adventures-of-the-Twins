using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_Menu : MonoBehaviour //Класс отвечает за главное игровое меню
{
    public Button start_button, control_button, exit_button, back_button;
    public GameObject menu_panel, control_panel;

    // Start is called before the first frame update
    private void Start()
    {
        //Присваимвем кнопкам значения кнопок в меню
        control_panel.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void Start_Button()
    {
        SceneManager.LoadScene("Game");
    }

    public void Control_button()
    {
        control_panel.SetActive(true);
        menu_panel.SetActive(false);
    }

    public void Exit_Button()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

    public void Back_Button()
    {
        control_panel.SetActive(false);
        menu_panel.SetActive(true);
    }
}
