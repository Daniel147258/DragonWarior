using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlick : MonoBehaviour
{
    [Header("Color 1")]
    [SerializeField] private Color color;

    [Header("Color2")]
    [SerializeField] private Color color2;

    private Text text;

    private bool jePrvaFarba;

    private float intenzita = 0.42f;

    private float timer;
    void Start()
    {
        text = GetComponent<Text>();
        jePrvaFarba = true;
        timer = 0;
        text.color = color;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= intenzita * Time.timeScale)
        {
            menFarby();
            timer = 0;
        }
        
            
    }

    public void menFarby()
    {

            if (text != null)
            {
                if (jePrvaFarba)            
                    text.color = color2;             
                else               
                    text.color = color;

                jePrvaFarba = !jePrvaFarba;
            }
        
    }

}
