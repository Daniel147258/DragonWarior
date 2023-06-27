using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float fullHp;
    public float akutalneZivoty { get; private set; }

    private Animator animator;

    private float nezranitelny = 1.7f;
    private float pocetFlashov = 5;

    private SpriteRenderer sprite;
    public bool jeMrtvy { get; private set; }

    void Start()
    {
        jeMrtvy  = false;
        if(this.GetComponent<MeleeEnemy>() != null )
        {
            nezranitelny = 0;
            pocetFlashov = 1;
        }
        akutalneZivoty = fullHp;  
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();    
    }

    
    void Update()
    {
    }
    
    public void TakeDamage(float damage)
    {
        if (!jeMrtvy)
        {
            akutalneZivoty = Mathf.Clamp(akutalneZivoty - damage, 0, fullHp);
            if (akutalneZivoty > 0)
            {
                animator.SetTrigger("damage");
                StartCoroutine(Nezranitelny());
            }
            else
            {
                jeMrtvy = true;
                animator.SetTrigger("dead");
            }
        }
    }

    public void PridajZivot(float kolko)
    {
        akutalneZivoty = Mathf.Clamp(akutalneZivoty + kolko, 0, fullHp);
        StartCoroutine (Zivot());
    }
    public bool maPlneZivoty()
    {
        if(akutalneZivoty > fullHp)
        {
            return true;
        }
        return akutalneZivoty == fullHp;
    }
    public float getFullHP()
    {
        return fullHp;
    }

    public void Zomrel()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator Nezranitelny()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        Physics2D.IgnoreLayerCollision(8, 10, true);
        for (int i = 0; i < pocetFlashov; i++)
        {
            sprite.color = new Color(1, 0, 0, 05f );
            yield return new WaitForSeconds(nezranitelny / (pocetFlashov * 2));
            sprite.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(nezranitelny / (pocetFlashov * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
        Physics2D.IgnoreLayerCollision(8, 10, false);
    }

    private IEnumerator Zivot() 
    {
        for (int i = 0; i < pocetFlashov; i++)
        {
            sprite.color = new Color(0, 1, 0, 05f);
            yield return new WaitForSeconds(nezranitelny / (pocetFlashov * 2));
            sprite.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(nezranitelny / (pocetFlashov * 2));
        }
    }

    public void ReSpawn()
    {
        PridajZivot(fullHp);
        animator.ResetTrigger("dead");
        animator.Play("Player_idle");
        StartCoroutine(Zivot());
        jeMrtvy = false; 
        gameObject.SetActive(true);
    }

    
}
