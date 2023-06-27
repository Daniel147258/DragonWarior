using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    [SerializeField] private Transform ciel;
    [SerializeField] private PlayerMovement hrac;

    private Vector3 pohlad; 
    void Start()
    {
        transform.position = new Vector3(ciel.position.x, ciel.position.y, -10f);    
    }

    void LateUpdate()
    {
        transform.position = new Vector3(ciel.position.x, ciel.position.y, -10f);
    }
   
}
