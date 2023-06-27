using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animator;
    private float smerX;
    [SerializeField] private float rychlostPohybu;
    [SerializeField] private float skok;
    private BoxCollider2D coll;
    public bool znemozni { get; private set; }
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private float coolDownStena;

    [Header("Hr·Ë")]
    [SerializeField] private GameObject player;

    [Header("Typ Skoku")]
    [SerializeField] private Skoky vyber;

    private int pocetSkokov;
    private int aktualneSkoky;

    [Header("Cooldown mezdi skokmi")]
    [SerializeField] private float coolDownMedziSkokmi;

    private bool mozeSkocit;

    public enum Skoky { JedenSkok, Dvojskok, Trojskok }

    void Start()
    {
        mozeSkocit = true;
        aktualneSkoky = 0;
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        znemozni = false;
        switch (vyber)
        {
            case Skoky.JedenSkok:
                pocetSkokov = 1;
                break;
            case Skoky.Dvojskok:
                pocetSkokov = 2;
                break;
            case Skoky.Trojskok:
                pocetSkokov = 3;
                break;
            default:
                pocetSkokov = 0;
                break;
        }
    }

    void Update()
    {
        if (!GetComponent<Health>().jeMrtvy)
        {
            // Znemozni pokracovat dalej
            if (znemozni)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.S))
            {
                StartCoroutine(JeMoznyPrechod());
            }
            UpdateAnimation();
            smerX = Input.GetAxisRaw("Horizontal");

            // Zmena smeru pohladu  
            if (smerX > 0.01f)
                transform.localScale = Vector3.one;
            else if (smerX < -0.01f)
                transform.localScale = new Vector3(-1, 1, 1);

            animator.SetBool("pohyb", smerX != 0);
            animator.SetBool("zem", JeNaZemi());

            if (JeNaZemi() || jeNaStene())
            {
                aktualneSkoky = 0;
            }

            if (coolDownStena > 0.2f)
            {
                body.velocity = new Vector2(smerX * rychlostPohybu, body.velocity.y);

                if (jeNaStene() && !JeNaZemi())
                {
                    body.gravityScale = 0;
                    body.velocity = Vector2.zero;
                }
                else
                    body.gravityScale = 2;

                if (Input.GetKey(KeyCode.Space))
                    Jump();
            }
            else
                coolDownStena += Time.deltaTime;
        }
        
    }

    private void UpdateAnimation()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger("utok");
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetTrigger("strike");
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("kop");
        }
         
    }
    private void Jump()
    {
        if (pocetSkokov > aktualneSkoky && mozeSkocit)
        {
            body.velocity = new Vector2(body.velocity.x, skok);
            StartCoroutine(ZnemozniSkok());
        }
        else if (jeNaStene() && !JeNaZemi())
        {
            if (smerX == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            coolDownStena = 0;
        }
    }

    private bool JeNaZemi()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool jeNaStene()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    private IEnumerator JeMoznyPrechod()
    {
        znemozni = true;
        float cas = 0f;

        while (cas < animator.GetCurrentAnimatorClipInfo(0)[0].clip.length)
        {
            cas += Time.deltaTime;
            yield return null;
        }
        znemozni = false;
    }

    public float getRychlostPohybu()
    {
        return rychlostPohybu;
    }

    private IEnumerator ZnemozniSkok()
    {
        aktualneSkoky++;
        mozeSkocit = false;
        yield return new WaitForSeconds(coolDownMedziSkokmi);
        mozeSkocit = true;
    }
}
