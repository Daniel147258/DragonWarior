using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Strela : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] private float coolDown = 2f;
    private bool mozeStrelit = true;
    private float timer;
    [SerializeField] private GameObject strelaPrefab;
    [SerializeField] private Transform vychbod;
    private float lifetime = 2f;
    private GameObject strela;
    private Rigidbody2D body;
    [SerializeField] GameObject pouzivatel;



    void Update()
    {
        if (!GetComponent<Health>().jeMrtvy)
        {

            if (mozeStrelit && Input.GetKeyDown(KeyCode.W) && !pouzivatel.GetComponent<PlayerMovement>().znemozni)
            {
                timer = coolDown;
                mozeStrelit = false;

                strela = Instantiate(strelaPrefab, vychbod.position, Quaternion.identity);
                body = strela.GetComponent<Rigidbody2D>();
                Destroy(strela, lifetime);

                if (transform.localScale.x > 0)
                {
                    strela.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    body.velocity = transform.right * speed;
                }
                else
                {
                    strela.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    body.velocity = -transform.right * speed;
                }
                Projectile projectile = strela.GetComponent<Projectile>();
                projectile.OnCollison += HandleProjectileCollision;
                Destroy(strela, lifetime);
                StartCoroutine(ResetShoot());
            }
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    private IEnumerator ResetShoot()
    {
        yield return new WaitForSeconds(coolDown);
        mozeStrelit = true;
    }

    private void HandleProjectileCollision(GameObject collision)
    {
        float damage = strela.GetComponent<Projectile>().getDamage();
        if (collision.gameObject.CompareTag("Stena") || collision.gameObject.CompareTag("Zem"))
        {
            strela.GetComponent<Animator>().SetTrigger("explozia");
            strela.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            strela.GetComponent<Animator>().SetTrigger("explozia");
            strela.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            collision.GetComponent<Health>().TakeDamage(damage);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            strela.GetComponent<Animator>().SetTrigger("explozia");
            strela.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
    public void Znic()
    {
        Destroy(gameObject);
    }
}
