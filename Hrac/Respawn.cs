using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Transform checkpointPosition;
    private Health zivotHraca;
    private Manager manager;
    void Start()
    {
        zivotHraca = GetComponent<Health>();
        manager = FindObjectOfType<Manager>();
    }

    public void ResPawn()
    {
        if (checkpointPosition == null)
        {
            manager.Gameover();
            return;
        }
        transform.position = checkpointPosition.position;
        zivotHraca.ReSpawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "CheckPoint")
        {
            checkpointPosition = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("aktivuj");
        }
    }
}
