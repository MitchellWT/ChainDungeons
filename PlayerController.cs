using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpVelocity = 1f;
    public float movementSpeed = 5f;
    public Rigidbody2D playerBody;
    public CapsuleCollider2D playerCollider;
    public Animator playerAnimator;
    private float horizontalMovement;
    private bool isGrounded = true;
    private Collider2D currentLeverCollider;
    private Collider2D currentDialogueCollider;
    private bool isJumping = false;
    private bool interactKeyDown = false;

    // Update is called once per frame
    private void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        checkGrounded();

        if (horizontalMovement > 0)
        {
            FaceRight();
        }
        else if (horizontalMovement < 0)
        {
            FaceLeft();
        }

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

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true;
            playerAnimator.SetBool("isJumping", true);
            playerAnimator.SetBool("isWalking", false);
            playerBody.velocity = Vector2.up * jumpVelocity;
            FindObjectOfType<AudioManager>().PlayClip("player-jump");
            isJumping = false;
        }

        if (currentLeverCollider != null && Input.GetKeyDown(KeyCode.L))
        {
            Component leverComponent = currentLeverCollider.gameObject.GetComponent(typeof(LeverTrigger));
            FindObjectOfType<DialogueManager>().StartDialogue(((LeverTrigger) leverComponent).dialogueText);
        }

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

        if (currentDialogueCollider != null && FindObjectOfType<DialogueManager>().GetIsDialogue() && 
        (Input.GetKeyDown(KeyCode.L)))
        {
            FindObjectOfType<DialogueManager>().EndDialogue();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // Frame independent update method
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
        if (collision.gameObject.tag == "boulder")
        {
            FindObjectOfType<AudioManager>().PlayClip("player-death-boulder");
            FindObjectOfType<GameManager>().playerDied();
        }
    }

    private void checkGrounded()
    {
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

        if (collider.gameObject.tag == "change-level")
        {
            currentLeverCollider = collider;
        }

        if (currentDialogueCollider == null && collider.gameObject.tag == "dialogue-trigger")
        {
            DialogueTrigger dialogueTrigger = collider.gameObject.GetComponent(typeof(DialogueTrigger)) as DialogueTrigger;
            FindObjectOfType<DialogueManager>().StartDialogue(dialogueTrigger.dialogueText);

            currentDialogueCollider = collider;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "change-level")
        {
            currentLeverCollider = null;
        }

        if (collider.gameObject.tag == "dialogue-trigger")
        {
            Destroy(collider.gameObject);
        }
    }

}
