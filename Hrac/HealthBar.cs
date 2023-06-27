using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health zivotyHraca;
    [SerializeField] private Image celkove;
    [SerializeField] private Image aktualne;

    void Start()
    {
        celkove.fillAmount = zivotyHraca.getFullHP() / 10;
    }


    void Update()
    {
        aktualne.fillAmount = zivotyHraca.akutalneZivoty / 10;
    }
}
