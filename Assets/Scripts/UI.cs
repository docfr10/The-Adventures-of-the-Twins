using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour //Класс отвечает за пользовательский интерфейс на уровне
{
    public GameObject DeadScreenUI;

    public void Begin()
    {

    }

    public void Death()
    {
        if (FindObjectOfType<Player>().Lives() <= 0)
        {
            Time.timeScale = 0;

            DeadScreenUI.SetActive(true);
        }
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
