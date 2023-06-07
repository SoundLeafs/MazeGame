using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public float waitTime = 3f;
    public float speed = 5f;
    public Vector3 PositionDelta = Vector3.zero;

    private Vector3 _closedPosition ;
    private Vector3 _openPosition;

    
    void Start()
    {
        _closedPosition = transform.position;
        _openPosition = _closedPosition + PositionDelta;
        //Debug.Log("Before Coroutine");
        StartCoroutine(OpenClose());
        //Debug.Log("After Coroutine");
    }


    IEnumerator OpenClose()
    {
        //Debug.Log("Enter Coroutine!");
        Vector3 goal = _openPosition;
        yield return new WaitForSeconds(waitTime);
        bool isOpen = false;
        //Debug.Log("Waited 3 Seconds");
        while(true)
        {
            if(Vector3.Distance(transform.position, goal) <0.1f)
            {   
                // ! is not
                //inverts booleans

                isOpen = !isOpen;

                if(isOpen)

                {//if door is open, then close it
                    goal = _closedPosition;
                    
                }
                else
                {// if door is closed then open it
                    goal = _openPosition;
                }

                yield return new WaitForSeconds(waitTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position,
                                                  goal,  
                                                  speed * Time.deltaTime);
                yield return null; //wait for 1 frame 
            }
        }

        
    }
}
