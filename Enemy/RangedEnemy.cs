using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ShootingTrap;

public class RangedEnemy : MonoBehaviour
{
    [Header("Prefab musi byt Projectil aby to fungovalo")]
    [SerializeField] private GameObject prefabStrela;
    private GameObject strela;
    [SerializeField] private float vzdialenostSnimaca;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float coolDown;
    [SerializeField] private LayerMask ciel;
    [SerializeField] private float rychlostStrely;
    [SerializeField] private float lifetime;
    private Vector3 smerPohybu;
    private Vector3 smerStrely;
    private bool mozeStrelit = true;
    private Animator animator;
    public bool maCiel { get; private set; }

    void Start()
    {
        maCiel = false;
        smerPohybu = transform.right;
        animator = GetComponent<Animator>();
        if (transform.localScale.x > 0)
        {
            strela.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            smerStrely = transform.right;
        }
        else
        {
            strela.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            smerStrely = -transform.right;
        }
    }

    void Update()
    {
        if (!GetComponent<Health>().jeMrtvy)
        {
            if (transform.localScale.x > 0)
            {
                smerPohybu = transform.right;
                smerStrely = transform.right;
            }

            else
            {
                smerPohybu = -transform.right;
                smerStrely = -transform.right;
            }
            NajdiCiel();
            HybStrelou();
        }
    }


    private void NajdiCiel()
    {
        Debug.DrawRay(transform.position, smerPohybu, Color.yellow);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, smerPohybu, vzdialenostSnimaca, ciel);

        if (hit.collider.CompareTag("Player") && mozeStrelit)
        {
            strela = Instantiate(prefabStrela, firePoint.position, Quaternion.identity);
            strela.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            Projectile projectile = strela.GetComponent<Projectile>();
            projectile.OnCollison += HandleProjectileCollision;
            animator.SetTrigger("utok");
            Destroy(strela, lifetime);
            StartCoroutine(CoolDown());
        }

    }
    private IEnumerator CoolDown()
    {
        mozeStrelit = false;
        maCiel = true;
        yield return new WaitForSeconds(coolDown);
        mozeStrelit = true;
        maCiel = false;
    }

    private void HybStrelou()
    {
        strela.GetComponent<Rigidbody2D>().velocity = smerStrely * rychlostStrely;
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
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + vzdialenostSnimaca,
            transform.position.y, transform.position.z));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x - vzdialenostSnimaca,
            transform.position.y, transform.position.z));
    }
}
