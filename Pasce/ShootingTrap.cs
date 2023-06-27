using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShootingTrap : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject preFab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float coolDown;
    [SerializeField] private LayerMask ciel;
    [SerializeField] private float vzdialenostSnimaca;
    [Header ("Smer projektilu sa natoci na smer ktori sa zada")]
    [SerializeField] private Smer smer;
    private Vector3 smerPohybu ;
    private Vector3 smerSipu;
    private float lifetime = 5;
    private bool mozeStrelit = true;
    private GameObject strela;

    void Start()
    {
        smerPohybu = transform.up * vzdialenostSnimaca;
        smerSipu = transform.up;
    }


    void Update()
    {
        NajdiCiel();
        HybStrelou();
    }

    public enum Smer { doprava, dolava, hore, dole };

    private void NajdiCiel()
    {
        Debug.DrawRay(transform.position, smerPohybu, Color.yellow);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, smerPohybu, vzdialenostSnimaca, ciel);

        if (hit.collider.CompareTag("Player") && mozeStrelit)
        {
            strela = Instantiate(preFab, firePoint.position, Quaternion.identity);
            switch (smer)
            {
                case Smer.doprava:
                    strela.transform.localScale = new Vector3(Mathf.Abs(strela.transform.localScale.x), strela.transform.localScale.y, strela.transform.localScale.z);
                    break;
                case Smer.dolava:
                    strela.transform.localScale = new Vector3(Mathf.Abs(strela.transform.localScale.x) * -1, strela.transform.localScale.y, strela.transform.localScale.z);
                    break;
                case Smer.hore:
                    strela.transform.Rotate(0f, 0f, 90f);
                    break;
                case Smer.dole:
                    strela.transform.Rotate(0f, 0f, -90f);
                    break;
                default:
                    break;
            }
            Projectile projectile = strela.GetComponent<Projectile>();
            projectile.OnCollison += HandleProjectileCollision;
            Destroy(strela, lifetime);
            StartCoroutine(CoolDown());
        }
    }


    private IEnumerator CoolDown()
    {
        mozeStrelit = false;
        yield return new WaitForSeconds(coolDown);
        mozeStrelit = true;
    }

    private void HybStrelou()
    {
        strela.GetComponent<Rigidbody2D>().velocity = smerSipu * speed;
    }

    private void HandleProjectileCollision(GameObject collisionObject)
    {
        if (collisionObject.CompareTag("Player"))
        {
            collisionObject.GetComponent<Health>().TakeDamage(strela.GetComponent<Projectile>().getDamage());
            Destroy(strela);
        }
        else if (collisionObject.CompareTag("Stena") || collisionObject.CompareTag("Zem"))
        {
            Destroy(strela); 
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        switch (smer)
        {
            case Smer.doprava:
                Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + vzdialenostSnimaca, 
                    transform.position.y, transform.position.z));
                break;
            case Smer.dolava:
                Gizmos.DrawLine(transform.position, new Vector3(transform.position.x - vzdialenostSnimaca,
                     transform.position.y, transform.position.z));
                break;
            case Smer.hore:
                Gizmos.DrawLine(transform.position, new Vector3(transform.position.x ,
                     transform.position.y + vzdialenostSnimaca, transform.position.z));
                break;
            case Smer.dole:
                Gizmos.DrawLine(transform.position, new Vector3(transform.position.x,
                    transform.position.y - vzdialenostSnimaca, transform.position.z));
                break;
            default:
                break;
        }
    }
}
