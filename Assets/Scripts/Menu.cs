using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button start_button, exit_button;
    [SerializeField] private GameObject panel;

    // Start is called before the first frame update
    private void Start()
    {
        //Присваимвем кнопкам значения кнопок в меню
        start_button = panel.transform.Find("Start").GetComponentInChildren<Button>();
        exit_button = panel.transform.Find("Exit").GetComponentInChildren<Button>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void Start_Button()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit_Button()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}
