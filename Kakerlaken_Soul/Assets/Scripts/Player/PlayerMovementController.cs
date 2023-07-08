using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeneGames.BugController
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private PlayerManager PM;

        private bool isMove;
        private bool isSprint;
        private bool isJump;
        
        private Vector3 moveDirection;
        private Vector3 smoothMoveDirection;
        private Vector3 smoothMoveVelocity;
        private CharacterController controller;
        private float currentSpeed;
        private float currentMovementSmoothnes;

        [Header("Components")]
        [SerializeField] private Transform cameraBase;
        [SerializeField] private GameObject playerModel;

        [Header("Variables")]
        [Range(0.01f, 0.5f)] 
        [SerializeField] private float movementSmoothnes = 0.1f;
        [Range(3f, 10f)]
        [SerializeField] private float jumpforce = 4f;
        [SerializeField] private float walkSpeed = 3f;
        [SerializeField] private float runSpeed = 6f;
        [SerializeField] private float gravity = 0.8f;
        [SerializeField] private float turnSpeed = 5f;
        [Range(0.1f, 10f)]
        [SerializeField] private float SprintMana = 0.1f;
        

        [Header("Keys")]
        [SerializeField] private KeyCode runKey = KeyCode.LeftShift;
        [SerializeField] private KeyCode jumpKey = KeyCode.Space;
        

        [Header("etc")]
        [SerializeField] private Stamina _stamina;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            PM = GetComponent<PlayerManager>();
            _stamina = GetComponent<Stamina>();
        }

        void Update()
        {
            if (PM.isDead || PM.state == PlayerManager.State.Cine) return;
            SetSpd();
            Movement();
            Jump();
            Gravity();
            Rotation();
            setState();
            Vector3 moveThisDirection = new Vector3(smoothMoveDirection.x, moveDirection.y, smoothMoveDirection.z);
            if (!(PM.state == PlayerManager.State.atk || PM.state == PlayerManager.State.wait))
            {
                if (isSprint) _stamina.UseStamina(SprintMana);
                controller.Move(moveThisDirection * Time.deltaTime);
            }
            PM.moveSpd = currentSpeed;
           
        }

        private void SetSpd()
        {
            if (Input.GetKey(runKey) && _stamina.stamina.Value > SprintMana)
            {
                isSprint = true;
                currentSpeed = runSpeed;
                currentMovementSmoothnes = movementSmoothnes * 3f;
            }
            else
            {
                isSprint = false;
                currentSpeed = walkSpeed;
                currentMovementSmoothnes = movementSmoothnes;
            }
        }

        private void Movement()
        {
            //Movement
            smoothMoveDirection = Vector3.SmoothDamp(smoothMoveDirection, moveDirection, ref smoothMoveVelocity, currentMovementSmoothnes);
            float yStore = moveDirection.y;
            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDirection = moveDirection.normalized * currentSpeed;

            if(moveDirection.x ==0 && moveDirection.z == 0) { isMove = false; }
            else { isMove = true; }
            
            moveDirection.y = yStore;
        }

        private void Jump()
        {
            //Jump
            if (controller.isGrounded)
            {
                moveDirection.y = -1f;

                if (Input.GetKeyDown(jumpKey))
                {
                    moveDirection.y = jumpforce;
                    isJump = true;
                }
                else
                {
                    moveDirection.y = moveDirection.y + (Physics.gravity.y * gravity * Time.deltaTime);
                    isJump = false;
                }
            }
        }

        private void Gravity()
        {
            //Gravity
            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravity * Time.deltaTime);

        }

        private void Rotation()
        {
            //Move the player based on the camera direction
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                Quaternion _playerRotation = Quaternion.Euler(0f, cameraBase.rotation.eulerAngles.y, 0f);
                transform.rotation = Quaternion.Slerp(transform.rotation, _playerRotation, turnSpeed / 2f * Time.deltaTime);

                Quaternion _newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, _newRotation, turnSpeed * Time.deltaTime);
            }
        }

        public bool IsGrounded()
        {
            return controller.isGrounded;
        }

        public void setState()
        {
            if (!(PM.state == PlayerManager.State.atk || PM.state == PlayerManager.State.wait))
                PM.state = isJump ? PlayerManager.State.idle : 
                    (isMove ? (isSprint ? PlayerManager.State.sprint : PlayerManager.State.move) : PlayerManager.State.idle);
        }
    }
}