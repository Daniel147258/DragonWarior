using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewive : MonoBehaviour
{
    [SerializeField] private  float hodnota;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.GetComponent<Health>().maPlneZivoty())
        {
            collision.GetComponent<Health>().PridajZivot(hodnota);
            Destroy(gameObject);
        }
    }
}
