using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaKill : MonoBehaviour
{
    [Header("Objekt")]
    [SerializeField] private Health zivotyObjektu;
    private float damage = float.MaxValue;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        zivotyObjektu.TakeDamage(damage);
    }
}
