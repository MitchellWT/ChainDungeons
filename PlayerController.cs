using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script manages the player controls
 * and interactions with the world. It may be
 * beneficial to separate the controls from the
 * interactions.
 */
public class PlayerController : MonoBehaviour
{
    public float jumpVelocity = 1f;
    public float movementSpeed = 5f;
    public Rigidbody2D playerBody;
    public CapsuleCollider2D playerCollider;
    public Animator playerAnimator;
    private float horizontalMovement;
    private bool isGrounded = true;
    /* The below two colliders are gathered
     * when the player triggers it.
     */
    private Collider2D currentLeverCollider;
    private Collider2D currentDialogueCollider;
    private bool isJumping = false;
    private bool interactKeyDown = false;

    private void Update()
    {
        /* Gets horizontal movement input from the keyboard/controller
         * and checks If the player is grounded.
         */
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        checkGrounded();

        /* Checks the direction of the horizontal movement
         * and changes the player sprite appropriately.
         */
        if (horizontalMovement > 0)
        {
            FaceRight();
        }
        else if (horizontalMovement < 0)
        {
            FaceLeft();
        }

        /* Checks If the player is moving and plays/pauses  
         * the animation and audio clip.
         */
        if (horizontalMovement != 0)
        {
            playerAnimator.SetBool("isWalking", true);

            FindObjectOfType<AudioManager>().UnPauseClip("player-footsteps");
        }
        else
        {
            playerAnimator.SetBool("isWalking", false);

            FindObjectOfType<AudioManager>().PauseClip("player-footsteps");
        }

        /* Check If the player inputs to jump and performs
         * the appropriate actions to jump. These are: setting animation,
         * increasing velocity, and playing the sound clip.
         * Its recommended that physics is done outside of frame dependent
         * methods (I.e Update). However, when doing this the jump was
         * inconsistent with button pressing and time constrains didn't allow
         * for this fix.
         */
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true;
            playerAnimator.SetBool("isJumping", true);
            playerAnimator.SetBool("isWalking", false);
            playerBody.velocity = Vector2.up * jumpVelocity;
            FindObjectOfType<AudioManager>().PlayClip("player-jump");
            isJumping = false;
        }

        /* Checks If the player is at the lever and has activated it with 'L'.
         * And starts dialogue with the lever collider.
         */
        if (currentLeverCollider != null && Input.GetKeyDown(KeyCode.L))
        {
            Component leverComponent = currentLeverCollider.gameObject.GetComponent(typeof(LeverTrigger));
            FindObjectOfType<DialogueManager>().StartDialogue(((LeverTrigger) leverComponent).dialogueText);
        }

        /* The below two 'If' statements check If the player is in the lever dialogue and have selected
         * the level with either '1' or '2'. This will trigger the level change.
         */
        if (currentLeverCollider != null && FindObjectOfType<DialogueManager>().GetIsDialogue() && 
        (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)))
        {
            Component leverComponent = currentLeverCollider.gameObject.GetComponent(typeof(LeverTrigger));
            ((LeverTrigger) leverComponent).selectOptionOne();
        }

        if (currentLeverCollider != null && FindObjectOfType<DialogueManager>().GetIsDialogue() && 
        (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)))
        {
            Component leverComponent = currentLeverCollider.gameObject.GetComponent(typeof(LeverTrigger));
            ((LeverTrigger) leverComponent).selectOptionTwo();
        }

        /* Checks If the player is in dialogue and closes the dialogue box If the
         * 'L' button is pressed.
         */
        if (currentDialogueCollider != null && FindObjectOfType<DialogueManager>().GetIsDialogue() && 
        (Input.GetKeyDown(KeyCode.L)))
        {
            FindObjectOfType<DialogueManager>().EndDialogue();
        }

        // Closes the game If the 'Escape' button is pressed.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // Moves the player based on the horizontal input.
    private void FixedUpdate()
    {
        if (!FindObjectOfType<DialogueManager>().GetIsDialogue())
        {
            playerBody.velocity = new Vector2(horizontalMovement * movementSpeed * Time.fixedDeltaTime, playerBody.velocity.y);
        }
    }

    private void FaceLeft()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
    }

    private void FaceRight()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /* Checks If the player was hit by the boulder.
         * If they were a sound clip plays and the player dies.
         */
        if (collision.gameObject.tag == "boulder")
        {
            FindObjectOfType<AudioManager>().PlayClip("player-death-boulder");
            FindObjectOfType<GameManager>().playerDied();
        }
    }

    private void checkGrounded()
    {
        /* Checks plays vertical velocity and sets the 'isGrounded'
         * appropriately.
         */
        if (playerBody.velocity.y <= 1 && playerBody.velocity.y >= -1)
        {
            playerAnimator.SetBool("isJumping", false);
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        /* Checks If the player has interacted with a
         * player death entity. These entities kill the player.
         * It also performs a check to ensure that the correct sound
         * is playing.
         */
        if (collider.gameObject.tag == "pitfall" || collider.gameObject.tag == "spike" 
        || collider.gameObject.tag == "flying-enemy" || collider.gameObject.tag == "ground-enemy"
        || collider.gameObject.tag == "ranged-enemy")
        {
            if (collider.gameObject.tag == "ground-enemy")
            {
                FindObjectOfType<AudioManager>().PlayClip("melee-attack");
                FindObjectOfType<AudioManager>().PlayClip("player-death-combat");
            }
            else if (collider.gameObject.tag == "spike")
            {
                FindObjectOfType<AudioManager>().PlayClip("player-death-spike");
            }
            else if (collider.gameObject.tag == "pitfall")
            {
                FindObjectOfType<AudioManager>().PlayClip("player-death-pitfall");
            }
            else if (collider.gameObject.tag == "ranged-enemy")
            {
                FindObjectOfType<AudioManager>().PlayClip("player-death-combat");
            }
            else if (collider.gameObject.tag == "flying-enemy")
            {
                FindObjectOfType<AudioManager>().PlayClip("player-death-combat");
            }

            FindObjectOfType<GameManager>().playerDied();
        }

        // Checks If the player is in the lever zone.
        if (collider.gameObject.tag == "change-level")
        {
            currentLeverCollider = collider;
        }

        /* Checks If the player is in a dialogue zone and
         * triggers the dialogue event.
         */
        if (currentDialogueCollider == null && collider.gameObject.tag == "dialogue-trigger")
        {
            DialogueTrigger dialogueTrigger = collider.gameObject.GetComponent(typeof(DialogueTrigger)) as DialogueTrigger;
            FindObjectOfType<DialogueManager>().StartDialogue(dialogueTrigger.dialogueText);

            currentDialogueCollider = collider;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        /* Checks If the player has left the lever
         * zone.
         */
        if (collider.gameObject.tag == "change-level")
        {
            currentLeverCollider = null;
        }

        /* Checks If the player has left the dialogue
         * zone and deletes the dialogue object.
         */
        if (collider.gameObject.tag == "dialogue-trigger")
        {
            Destroy(collider.gameObject);
        }
    }

}
