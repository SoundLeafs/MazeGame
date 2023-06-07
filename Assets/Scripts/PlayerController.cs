using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
		public float speed = 20f;
		 //shorthand name for deltatime
		
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
  /*  void Update()
    {
        bool dKey = Input.GetKey(KeyCode.D);
		bool aKey = Input.GetKey(KeyCode.A);
		bool wKey = Input.GetKey(KeyCode.W);
		bool sKey = Input.GetKey(KeyCode.S);
		bool qKey = Input.GetKey(KeyCode.Q);
		bool eKey = Input.GetKey(KeyCode.E);
		
		float dt = Time.deltaTime;
		if(dKey == true)
		{
			//Code goes here
			Debug.Log("Moving Right");
			
			Vector3 playerPosition = transform.position;
			
			playerPosition.z = playerPosition.z + speed *Time.deltaTime;
			
			transform.position = playerPosition;
		}
		
		if(aKey == true)
		{
			//Code goes here
			Debug.Log("Moving Left");
			
			Vector3 playerPosition = transform.position;
			
			playerPosition.z = playerPosition.z - speed *Time.deltaTime;
			
			transform.position = playerPosition;
		}
		if(wKey == true)
		{
			//Code goes here
			Debug.Log("Moving Left");
			
			Vector3 playerPosition = transform.position;
			
			playerPosition.x = playerPosition.x - speed *Time.deltaTime;
			
			transform.position = playerPosition;
		}
		if(sKey == true)
		{
			//Code goes here
			Debug.Log("Moving Left");
			
			Vector3 playerPosition = transform.position;
			
			playerPosition.x = playerPosition.x + speed *Time.deltaTime;
			
			transform.position = playerPosition;
		}
		
		if(qKey == true)
		{
			//Code goes here
			Debug.Log("Growing up");
			
			if(transform.localScale.y > 0)
			transform.localScale -= new Vector3(10f*Time.deltaTime, 10f*Time.deltaTime, 10f*Time.deltaTime);

		}
		if(eKey == true)
		{
			//Code goes here
			Debug.Log("Shrinking down");
			
			//if(transform.localScale.y > 0)
			transform.localScale += new Vector3(10f*Time.deltaTime, 10f*Time.deltaTime, 10f*Time.deltaTime);

		}
		
    }
	*/
	
	
	void Update()
	{
		//read the input
		//between -1 to 1
		
		float horizontal = Input.GetAxisRaw("Horizontal"); //GetAxisRaw will get raw values without smoothing
		float vertical = Input.GetAxisRaw("Vertical");
		
		//Get the main camera (camera tagged in unity as Main Camera)
		
		Camera camera = Camera.main;
		
		//Cameras forward and right vectors
		Vector3 forward = camera.transform.forward;
		Vector3 right = camera.transform.right;
		
		//We dont care about up and down (Y axis)
		forward.y = 0f;
		right.y = 0f;
		forward.Normalize(); //Modifys the original vector 
		right.Normalize();  //Modifys
		
		Vector3 desiredMoveDirection = (forward * vertical) + (right * horizontal);
		desiredMoveDirection.Normalize();
		
		//normalizing the vector will make it move at the same speed in any direction
		//Vector3 moveDir = new Vector3(vertical,0,horizontal).normalized; 
		//returns a new vector
		//transform.position += moveDir * speed * Time.deltaTime;
		
		transform.position += desiredMoveDirection * speed * Time.deltaTime;
	}
	
	
}
