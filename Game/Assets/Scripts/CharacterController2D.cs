using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement

    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    [Range(.25f, 100.0f)]
    public float speed_ = .75f;
    public static bool block_input = false;

    [HideInInspector]
    public UnityEvent eventToTrigger;



    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        if (!block_input)
        {
            float movement = Input.GetAxis("Horizontal") * speed_;

            if (Mathf.Abs(movement) > .05f)
            {
                transform.position += Vector3.right * (movement * Time.deltaTime);
                // If the input is moving the player right and the player is facing left...
                if (movement > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (movement < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }

            if (Input.GetButtonDown("Action"))
            {
                eventToTrigger.Invoke();
                block_input = true;
            }
        }
    }


    public void Move(float move)
    {


        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);



    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
