using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _groundCheck;

    // Jump
    private float _jumpHeight = 4.6f;
    private float _groundCheckRadius = 0.3f;
    private float _gravity = -20f;
    private bool _runningWhenJumped = false;
    private Vector3 _gravityDirection;
    
    // Running
    private static float MAX_ENERGY = 200f;
    private static float ENERGY_CHARGE_SPEED = 3f;
    private float _energy = MAX_ENERGY;

    private CharacterController _controller;

    public Vector3 Direction { get; private set; }
    public bool IsRunning { get; private set; }
    public bool IsGrounded { get; private set; }
    public float Speed => 5f;
    public float RunningSpeed { get; private set; }


    void Awake()
    {
        _controller = transform.GetComponent<CharacterController>();
    }

    protected void Update()
    {
        Direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        IsRunning = Direction.magnitude >= 0.1f; 

        // Run
        RunningSpeed = 1;
        bool runningPressed = Input.GetKey(KeyCode.LeftShift);
        if (runningPressed && IsGrounded)
        {
            _energy -= ENERGY_CHARGE_SPEED * 2f * Time.deltaTime;
            RunningSpeed = _energy > 0 ? 1.5f : 1;
        }
        else
        {
            _energy += ENERGY_CHARGE_SPEED * Time.deltaTime;
            _energy = Mathf.Clamp(_energy, 0, MAX_ENERGY);
        }

        if (_runningWhenJumped && !IsGrounded)
        {
            RunningSpeed = 1.5f;
        }


        // Jump and gravity.
        IsGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _groundMask);
        if (IsGrounded && _gravityDirection.y < 0)
        {
            _runningWhenJumped = false;
            _gravityDirection.y = -10f; // Push the player to the ground.
        }
        
        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            _runningWhenJumped = runningPressed;
            _gravityDirection.y = Mathf.Sqrt(_jumpHeight * -_gravity);
        }

        _gravityDirection.y += _gravity * Time.deltaTime;
        _controller.Move(_gravityDirection * Time.deltaTime);
    }

}
