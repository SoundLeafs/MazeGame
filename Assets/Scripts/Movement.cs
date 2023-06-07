using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace Debugging.Player

{
    [AddComponentMenu("RPG/Player/Movement")]
    //1
    [RequireComponent(typeof(CharacterController))]
    //2
    public class Movement : MonoBehaviour
    //3
    {
        [Header("Speed Vars")]
        private float moveSpeed = 20;
        private float walkSpeed = 20, runSpeed = 30, crouchSpeed = 10, jumpSpeed = 10;
        private float _gravity = 20.0f;
        public Transform respawnPoint;
        public float respawnHeight;
        public int playerHP = 5;
        public Text playerHPText;
        public float coolDown = 2f;

        public Transform playerTransform;

        public bool playerHit = false;

        //Extra Speed starts at zero, can be increased by collecting speedcubes
        public float extraSpeed = 0f;
        //5
        private Vector3 _moveDir;

        //MOST IMPORTANT
        private CharacterController _charC;
        //9
        private void Start()
        {
            playerTransform = transform;
            _charC = GetComponent<CharacterController>();
            playerHPText.text = $"HP = {playerHP}";
            //11 
            //MOST IMPORTANT
        }
        private void Update()
        //10 DONT FORGET
        {
           
                Move();
                HandleRespawn();
            
            if (coolDown < 1f)
            {
                coolDown += Time.deltaTime;
            }
              
        }
        private void Move()
        {
            if (_charC.isGrounded)
            {
                if (Input.GetButton("Sprint"))
                //6
                {
                    moveSpeed = runSpeed + extraSpeed * 2f;
                }
                else if (Input.GetButton("Crouch"))
                {
                    moveSpeed = crouchSpeed + extraSpeed * 2f;
                }
                else
                {
                    moveSpeed = walkSpeed + extraSpeed * 2f;
                }
                _moveDir = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed);
                //7
                if (Input.GetButton("Jump"))
                {
                    _moveDir.y = jumpSpeed;
                    //8
                }
            }
            _moveDir.y -= _gravity * Time.deltaTime;
            _charC.Move(_moveDir * Time.deltaTime);
        }
        private void HandleRespawn()
        {
            if (transform.position.y < respawnHeight || playerHit)
            {
                Respawn();
            }
        }

     

        public void RunRespawn()
        {
            playerHit = true;
            HandleRespawn();
        }

        public void Respawn()
        {
            //playerHit = false;

            if (respawnPoint != null)
            {
                playerTransform.position = respawnPoint.position;
                Debug.Log("Respawning");
                StartCoroutine(RespawnWait());
            }

            else
            {
                Debug.LogError("Respawn point is not assigned in the Movement script.");
            }
        }

         IEnumerator RespawnWait()
        {
            if (playerHit)
            {
                yield return new WaitForSeconds(0.01f);
            }
            playerHit = false;
            
        }

        public void takeDammage()
        {
            if(coolDown >= 1)
            {
                playerHP -= 1;
                coolDown = 0;
                playerHPText.text = $"HP = {playerHP}";
            }
        }

    }


}
