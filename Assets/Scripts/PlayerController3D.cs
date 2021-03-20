using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    [SerializeField] private Transform _cam;
    [SerializeField] private Animator _animator;

    private float _turnSpeed = 0.1f;
    private float _turnSmoothVelocity;
    private PlayerController _playerController;
    private CharacterController _controller;
    
    void Start()
    {
        _playerController = transform.GetComponent<PlayerController>();
        _controller = transform.GetComponent<CharacterController>();
    }
    
    void Update()
    {
        _animator.SetBool("isRunning", _playerController.IsRunning);
        _animator.SetBool("isJumping", !_playerController.IsGrounded);
        
        Vector3 direction = _playerController.Direction;
        if (direction.magnitude <= 0.1f) return;

        float speed = _playerController.Speed;
        float runningSpeed = _playerController.RunningSpeed;
        
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
        
        // Rotation
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSpeed);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        
        // Movement
        Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        _controller.Move(moveDirection.normalized * (speed * runningSpeed * Time.deltaTime));
    }
}