using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour // ласс, отвечающий за метаемые снар€ды
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;
    public GameObject bullet;
    public bool is_active = true;
    public bool is_look_right__;

    // Start is called before the first frame update
    private void Start()
    {
        is_look_right__ = Player.is_look_right; //ѕолучаем значение того куда повернут игрок дл€ того чтобы запустить снар€д вправо или влево
    }

    private void OnBecameInvisible() //≈сли снар€д ушел за поле зрени€ камеры игрока, то он уничтожаетс€
    {
        Destroy(gameObject);    
    }

    public void Throwing() //ћетод, задающий скорость снар€да
    {
        if (is_look_right__ == true)
        {
            gameObject.transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if (is_look_right__ == false)
        {
            gameObject.transform.Translate(-Vector2.right * speed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid); //«адаем траекторию луча снар€да
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Standing_Enemy")) //—толкновение со сто€щим врагом
            {
                hitInfo.collider.GetComponent<Standing_Enemy>().TakeDamage(damage); //≈сли снар€д столкнулс€ со врагом у врага отнимметс€ здоровье
            }
            if (hitInfo.collider.CompareTag("Moving_Enemy")) //—толкновение с движущимс€ врагом
            {
                hitInfo.collider.GetComponent<Moving_Enemy>().TakeDamage(damage); //≈сли снар€д столкнулс€ со врагом у врага отнимметс€ здоровье
            }
            Destroy(gameObject); //—ам снар€д уничтожаетс€
        }
        Throwing();
    }
}