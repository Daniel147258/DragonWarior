using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HardFolowing : MonoBehaviour
{
    private Vector3 smer;
    [SerializeField] private float speed;
    [SerializeField] private float vzdialenost;
    [SerializeField] private float zistiMeskanie;
    private float checkTimer;
    private Vector3[] smery = new Vector3[4];
    private bool jeVutoku;
    [SerializeField] private float damage;
    [SerializeField] private LayerMask ciel;

    void Start()
    {
        smery[0] = transform.right * vzdialenost;
        smery[1] = -transform.right * vzdialenost;
        smery[2] = transform.up * vzdialenost;
        smery[3] = -transform.up * vzdialenost;
    }

    private void OnEnable()
    {
        Naraz();
    }

    void Update()
    {
        if(jeVutoku)
        transform.Translate(smer * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.deltaTime;
            if(checkTimer > zistiMeskanie)
            {
                NajdiObjekt();
            }
        }
    }

    private void NajdiObjekt()
    {

        for (int i = 0; i < smery.Length; i++)
        {
            Debug.DrawRay(transform.position, smery[i], Color.yellow);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, smery[i], vzdialenost, ciel);

            if(hit.collider != null && !jeVutoku)
            {
                jeVutoku = true;
                smer = smery[i];
                checkTimer = 0;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        Naraz();
    }

    private void Naraz()
    {
        smer = transform.position;
        jeVutoku = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < smery.Length; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + vzdialenost,
                transform.position.y, transform.position.z));
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x - vzdialenost,
                transform.position.y, transform.position.z));
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x,
                transform.position.y + vzdialenost, transform.position.z));
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x,
                transform.position.y - vzdialenost, transform.position.z));


        }


    }
}
