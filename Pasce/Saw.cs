using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private float dlzkaPohybu;
    [SerializeField] float speed;
    [SerializeField] private float damage;
    private bool dolava;
    private float lavyOkraj;
    private float pravyOkraj;

    private void Start()
    {
        lavyOkraj = transform.position.x - dlzkaPohybu;
        pravyOkraj = transform.position.x + dlzkaPohybu;
        Physics2D.IgnoreLayerCollision(10,10, true);
        Physics2D.IgnoreLayerCollision(10, 9, true);
    }

    private void Update()
    {
        if (dolava)
        {
            if (transform.position.x > lavyOkraj)
            {
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            }
            else dolava = false;
        }
        else
        {
            if(transform.position.x < pravyOkraj)
            {
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            }
            else dolava = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + dlzkaPohybu,
           transform.position.y , transform.position.z));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x - dlzkaPohybu,
            transform.position.y , transform.position.z));
    }
}
