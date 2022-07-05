using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;
    public GameObject bullet;
    public bool is_active = true;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    private void OnBecameInvisible() //≈сли снар€д ушел за поле зрени€ камеры игрока, то он уничтожаетс€
    {
        Destroy(gameObject);    
    }

    public void Throwing() //ћетод, задающий скорость снар€да
    {
        //transform.Translate(Vector2.right * speed * Time.deltaTime);
        if (Player.is_look_right == true)
        {
            gameObject.transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if (Player.is_look_right == false)
        {
            gameObject.transform.Translate(-Vector2.right * speed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        Throwing();
    }
}