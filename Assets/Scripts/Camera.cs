using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform transform_player;
    private Vector3 position;//¬ данный вектор будут записыватьс€ координаты движени€

    private void Awake()//¬ данном методе будет "искатьс€" игрок
    {
        if (!transform_player)//ѕроверка на то найден игрок или нет
            transform_player = FindObjectOfType<Player>().transform;
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        position = transform_player.position;//ѕолучаем координаты игрока
        position.z = -10f;

        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime);//ѕеремещаем камеру в эти координаты
    }
}
