using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ukazovatel : MonoBehaviour
{
    private RectTransform rect;
    [SerializeField] private RectTransform[] moznosti;
    private int aktulanaPozicia;
    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ZmenPoziciu(-1);
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ZmenPoziciu(1);
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
            Interakcia();
    }
    private void ZmenPoziciu(int posunutie)
    {
        aktulanaPozicia += posunutie;
        if (aktulanaPozicia < 0)
            aktulanaPozicia = moznosti.Length - 1;
        else if (aktulanaPozicia > moznosti.Length -1)
        {
            aktulanaPozicia = 0;
        }
        rect.position = new Vector3(rect.position.x, moznosti[aktulanaPozicia].position.y, 0);


    }

    private void Interakcia()
    {
        moznosti[aktulanaPozicia].GetComponent<Button>().onClick.Invoke(); ;
    }

}
