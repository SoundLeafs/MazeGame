using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowKey : MonoBehaviour
{
    public GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.yellowKey = true;
            Collect();
        }
    }

    private void Collect()
    {
        Destroy(gameObject);
    }

}
