using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// A class to control the top-down character.
/// Implements the player controls for moving and shooting.
/// Updates the player animator so the character animates based on input.
/// </summary>


public class TopDownCharacterController : MonoBehaviour
{
    
    #region Framework Variables

    //The components that we need to edit to make the player move smoothly.
    private Animator m_animator;
    private Rigidbody2D m_rigidbody;
    
    //The direction that the player is moving in.
    private Vector2 m_playerDirection;
   

    [Header("Movement parameters")]
    //The speed at which the player moves
    [SerializeField] private float m_playerSpeed = 200f;
    //The maximum speed the player can move
    [SerializeField] private float m_playerMaxSpeed = 1000f;
    //The length of time between player rolls
    [SerializeField] private float m_rollRate;
    private float m_rollTimeOut;

    #endregion
    [Header("Projectile parameters")]
    //allows use of the projectile asset as a game object in the code
    [SerializeField] private GameObject m_projectilePrefab;
    //location the projectile will spawn form
    [SerializeField] private Transform m_firePoint;
    //speed of projectile once fired
    [SerializeField] private float m_projectileSpeed;
    [SerializeField] float m_fireRate;
    private float m_fireTimeout = 0;
    private Vector2 m_fireDirection;

    /// <summary>
    /// When the script first initialises this gets called.
    /// Use this for grabbing components and setting up input bindings.
    /// </summary>
    private void Awake()
    {
        
        //get components from Character game object so that we can use them later.
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Called after Awake(), and is used to initialize variables e.g. set values on the player
    /// </summary>
    void Start()
    {
        //not currently used - left here for demonstration purposes.
    }


    /// <summary>
    /// When a fixed update loop is called, it runs at a constant rate, regardless of pc performance.
    /// This ensures that physics are calculated properly.
    /// </summary>
    private void FixedUpdate()
    {
        //clamp the speed to the maximum speed for if the speed has been changed in code.
        //ternary form works by checking if the stuff before the question mark is true or false, then does either left side of colon if ture or right side if false.,
        float speed = m_playerSpeed > m_playerMaxSpeed ? m_playerMaxSpeed : m_playerSpeed;
        
        //apply the movement to the character using the clamped speed value.
        m_rigidbody.linearVelocity = m_playerDirection * (speed * Time.fixedDeltaTime);
    }
    
    /// <summary>
    /// When the update loop is called, it runs every frame.
    /// Therefore, this will run more or less frequently depending on performance.
    /// Used to catch changes in variables or input.
    /// </summary>
    void Update()
    {
        // store any movement inputs into m_playerDirection - this will be used in FixedUpdate to move the player.
        //m_playerDirection = m_moveAction.ReadValue<Vector2>();
        //sets the position of the mouse cursor on screen
        //mousePointOnScreen = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // ~~ handle animator ~~
        // Update the animator speed to ensure that we revert to idle if the player doesn't move.
        m_animator.SetFloat("Speed", m_playerDirection.magnitude);
        
        // If there is movement, set the directional values to ensure the character is facing the way they are moving.
        if (m_playerDirection.magnitude > 0)
        {
            m_animator.SetFloat("Horizontal", m_playerDirection.x);
            m_animator.SetFloat("Vertical", m_playerDirection.y);

        }

        
    }
    private void Fire()
    {
        Vector2 fireDirection = m_fireDirection;
        if (fireDirection == Vector2.zero)
        {
            fireDirection = Vector2.down;
        }
        GameObject spawnedProjectile = Instantiate(m_projectilePrefab, m_firePoint.position, Quaternion.identity);

        Rigidbody2D projectileRB = spawnedProjectile.GetComponent<Rigidbody2D>();
        if ( projectileRB!= null)
        {
            projectileRB.AddForce(fireDirection.normalized * m_projectileSpeed, ForceMode2D.Impulse);

        }
    }

    //makes the fire input as the arrow keys
    public void HandleAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && Time.time > m_fireTimeout)
        {
            m_fireTimeout = Time.time + m_fireRate;
            m_fireDirection = ctx.ReadValue<Vector2>();
            Fire();
        }
    }

    public void HandleMove(InputAction.CallbackContext ctx)
    {
        //reads the input given and determines the direction based on input
        if (ctx.performed)
        {
            m_playerDirection = ctx.ReadValue<Vector2>();
        }
        //when no input is pressed it will default the direction to zero/no direction
        else if (ctx.canceled)
        {
            m_playerDirection = Vector2.zero;
        }
    }

    public void HandleRoll(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && Time.time > m_rollTimeOut)
        {
            m_rollTimeOut = Time.time + m_rollRate;
            m_animator.SetTrigger("Rolling");
        }
    }

}
