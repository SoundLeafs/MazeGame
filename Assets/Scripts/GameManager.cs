using Debugging.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool redKey = false, blueKey = false, yellowKey = false;
    public float collectedSpeedcubes = 0;
    public Movement movement;
    public int money = 0;
    public Text moneyText;
    public Text speedText;

   
    public void Start()
    {
        moneyText.text = "";
        speedText.text = "";
    }

    public void AddSpeed()
    {
        movement.extraSpeed = collectedSpeedcubes;
        Debug.Log(collectedSpeedcubes);
        speedText.text = $"Bonus Speed +{collectedSpeedcubes}";
    }
    
    public void UpdateMoney()
    {
        moneyText.text = $"You have $ {money} ";
    }
    
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game");
    }

}
