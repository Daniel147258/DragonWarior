using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage;
    public event Action<GameObject> OnCollison;
    [SerializeField] private CieleKolizie cieleKolizie; 


    public enum CieleKolizie { Enemy, Player, All};

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (cieleKolizie)
        {
            case CieleKolizie.All:
                CollisionAll(collision);
                break;
            case CieleKolizie.Enemy:
                ColliSionEnemy(collision);
                break;
            case CieleKolizie.Player:
                CollisiPlayer(collision);
                break;
            default:
                break;
        }
    }

    private void CollisionAll(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stena") || collision.gameObject.CompareTag("Zem") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            OnCollison?.Invoke(collision.gameObject);
        }
    }

    private void ColliSionEnemy(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stena") || collision.gameObject.CompareTag("Zem") || collision.gameObject.CompareTag("Enemy"))
        {
            OnCollison?.Invoke(collision.gameObject);
        }
    }

    private void CollisiPlayer(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stena") || collision.gameObject.CompareTag("Zem") || collision.gameObject.CompareTag("Player"))
        {
            OnCollison?.Invoke(collision.gameObject);
        }
    }

    public float getDamage()
    {
        return damage;
    }
    public void Znic()
    {
        Destroy(gameObject);
    }
}
