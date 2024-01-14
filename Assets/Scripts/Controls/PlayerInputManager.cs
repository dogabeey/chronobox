using DG.Tweening;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace Doga.SilentCity
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager Instance;

        public Entity playerEntity;
        public float moveSpeed;  // Speed at which the character moves
        public float jumpForce;  // Amount of force added when the player jumps.
        public float limitVelocity;  // Limit the velocity of the player

        internal InputControls controls;

        private Vector3 movement;  // The direction of movement

        void Awake()
        {
            Instance = this;

            controls = new InputControls();
            controls.DefaultActionMap.Enable();
            controls.DefaultActionMap.Jump.performed += Jump_performed;
            controls.DefaultActionMap.Move.canceled += Move_canceled;
            controls.DefaultActionMap.Interact.performed += Interact_performed;
        }

        private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if(playerEntity.PickedObject == null)
            {
                playerEntity.PickupClosest();
            }
            else
            {
                playerEntity.Drop();
            }
        }

        private void Move_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            playerEntity.rb.velocity = new Vector2(0f, playerEntity.rb.velocity.y);
        }

        private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {

            if (playerEntity.rb.velocity.y == 0)
            {
                Jump();
            }
        }

        private void Start()
        {
        }
        void Update()
        {
            Vector2 axisVector = controls.DefaultActionMap.Move.ReadValue<Vector2>();
            float horizontal = axisVector.x;
            // Set the movement vector based on the axis input.

            movement.Set(horizontal, 0f, 0f);

            // Normalise the movement vector and make it proportional to the speed per second.
            movement = movement.normalized * moveSpeed * Time.deltaTime * 120 * playerEntity.MoveSpeed;

            // Move the player to it's current position plus the movement.
            playerEntity.rb.AddForce(movement);

            // Change the state of the player if running
            if (movement.magnitude > 0.01f)
            {
                playerEntity.state = Entity.EntityState.Run;
                playerEntity.transform.localEulerAngles = new Vector3(0, 90 * (movement.x > 0 ? 1 : -1), 0);
            }
            else
            {
                playerEntity.state = Entity.EntityState.Idle;
            }

            LimitVelocity();
        }

        private void LimitVelocity()
        {
            if (playerEntity.rb.velocity.x > playerEntity.maxVelocity)
            {
                playerEntity.rb.velocity = new Vector2(playerEntity.maxVelocity, playerEntity.rb.velocity.y);
            }
            if (playerEntity.rb.velocity.x < -limitVelocity)
            {
                playerEntity.rb.velocity = new Vector2(-playerEntity.maxVelocity, playerEntity.rb.velocity.y);
            }
        }

        // Jump method
        public void Jump()
        {
            // Add a vertical force to the player.
            playerEntity.rb.AddForce(new Vector2(0f, jumpForce * playerEntity.jumpForceMultiplier), ForceMode2D.Impulse);
            EventManager.TriggerEvent(Const.GameEvents.CREATURE_JUMP, new EventParam(paramObj: playerEntity.gameObject));
        }
    }
}
