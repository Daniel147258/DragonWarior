using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using static ShootingTrap;

public class Hliadkovanie : MonoBehaviour
{

    [SerializeField] private Transform lavyKraj;
    [SerializeField] private Transform pravyKraj;
    [SerializeField] private Transform enemy;
    [SerializeField] private float speed;
    [SerializeField] private Transform nepovolenyObjekt;
    [SerializeField] private float coolDown;
    private Vector3 pociatocny;
    private bool vlavo;
    private Animator animator;
    private bool pokracuj = true;
    private RangedEnemy r;

    private void Start()
    {
        vlavo = true;
        pociatocny = enemy.localScale;
        animator = enemy.GetComponent<Animator>();
        r = enemy.GetComponent<RangedEnemy>();
    }
    private void Update()
    {
        if (r != null)
        {
            if (r.maCiel)
            {
                r.GetComponent<Animator>().SetBool("pohyb", false);
                pokracuj = false;
            }
            else
                pokracuj = true;
        }

        if (vlavo && pokracuj)
        {
            if (enemy.position.x >= lavyKraj.position.x)
            {
                PohniSaDosmeru(-1);
                if (JeHracVIntervale())
                {
                    OtockaNaNepovolenyObjekt();
                }
            }
            else
            {
                ZmenSmer();
            }
        }
        else if (pokracuj && !vlavo)
        {
            if (enemy.position.x <= pravyKraj.position.x)
            {
                PohniSaDosmeru(1);
                if (JeHracVIntervale())
                {
                    OtockaNaNepovolenyObjekt();
                }
            }
            else
            {
                ZmenSmer();
            }
        }
    }
    private void PohniSaDosmeru(int smer)
    {

        enemy.localScale = new Vector3(Mathf.Abs(pociatocny.x) * smer, pociatocny.y, pociatocny.z);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * smer * speed, enemy.position.y, enemy.position.z);
        animator.SetBool("pohyb", true);

    }

    private void ZmenSmer()
    {
        animator.SetBool("pohyb", false);
        vlavo = !vlavo;
        StartCoroutine(Pockaj());
    }

    private IEnumerator Pockaj()
    {
        pokracuj = false;
        yield return new WaitForSeconds(coolDown);
        pokracuj = true;
    }

    private bool JeHracVIntervale()
    {
        return nepovolenyObjekt.position.x >= lavyKraj.position.x && nepovolenyObjekt.position.x <= pravyKraj.position.x;
    }

    private void OtockaNaNepovolenyObjekt()
    {
        if (nepovolenyObjekt.position.x < enemy.position.x - 0.1f)
        {
            vlavo = true;
        }
        else if (nepovolenyObjekt.position.x > enemy.position.x - 0.1f)
        {
            vlavo = false;
        }
    }
}
