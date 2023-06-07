using Debugging.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliderReset : MonoBehaviour
{

    public Movement movement;
        
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            movement.takeDammage();
        }

       
    }
}
