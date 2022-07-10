using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public bool is_moving = true;
    public float speed;

    private void Moving()
    {
        if (is_moving)
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        if (!is_moving)
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.position.y > 4f)
            is_moving = false;
        if (transform.position.y < -4f)
            is_moving = true;
        Moving();
    }
}
