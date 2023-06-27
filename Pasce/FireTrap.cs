using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float idleTime;
    [SerializeField] private float activeTime;
    [SerializeField] private bool aktivnaCelyCas = false;
    public bool aktivna { get; private set; }
    private Animator animator;
    private bool spracovanaKolizia = false;
    [SerializeField] private FireTrap striedacka;
    [SerializeField] private FireTrap azPo;

    [Header("Aktivuje pascu ihned potom ako sa aktivuje azPo a vyprsi zdrzanie")]
    [SerializeField] private float zdrzanie;

    private bool dokoncene = true;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(10, 10, true);
        animator = GetComponent<Animator>();

        if (!aktivnaCelyCas)
            aktivna = false;
        else { aktivna = true; animator.SetBool("aktivna", true); }

    }

    void Update()
    {
        if (azPo != null)
        {
            if (azPo.aktivna)
            {
                if (dokoncene)
                {
                    StartCoroutine(Zdrzanie());
                    StartCoroutine(BudAktivna(activeTime)); 
                }
            }
        }
        else
        {

            if (striedacka != null)
            {
                activeTime = striedacka.getActiveTime();
                idleTime = striedacka.getIdleTime();
                if (striedacka.aktivna)
                {
                    animator.SetBool("aktivna", false);
                    aktivna = false;
                }
                else
                {
                    animator.SetBool("aktivna", true);
                    aktivna = true;
                }


            }
            else
                StartCoroutine(Aktivuj());
        }
    }

    private IEnumerator Aktivuj()
    {

        if (!aktivnaCelyCas)
        {


            if (aktivna)
            {
                yield return new WaitForSeconds(activeTime);
                animator.SetBool("aktivna", false);
                aktivna = false;
            }

            else
            {
                yield return new WaitForSeconds(idleTime);
                animator.SetBool("aktivna", true);
                aktivna = true;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && aktivna && !spracovanaKolizia)
        {
            spracovanaKolizia = true;
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            StartCoroutine(Neublizuj());

        }
    }

    private IEnumerator Neublizuj()
    {
        yield return new WaitForSeconds(2.1f);
        spracovanaKolizia = false;
    }

    private IEnumerator Zdrzanie()
    {
        dokoncene = false;
        yield return new WaitForSeconds(zdrzanie);
        aktivna = true;
        animator.SetBool("aktivna", true);
    }

    private IEnumerator BudAktivna(float cas)
    {
        yield return new WaitForSeconds(cas);
        aktivna = false;
        animator.SetBool("aktivna", false);
        dokoncene = true;
    }

    public float getIdleTime()
    {
        return idleTime;
    }

    public float getActiveTime()
    {
        return activeTime;
    }
}
