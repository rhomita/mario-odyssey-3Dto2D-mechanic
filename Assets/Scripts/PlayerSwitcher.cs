using UnityEngine;

public class PlayerSwitcher : MonoBehaviour
{
    [SerializeField] private Camera2DFollow _cam2D;
    [SerializeField] private Camera3DFollow _cam3D;
    [SerializeField] private GameObject _sprite;
    [SerializeField] private GameObject _model;

    private PlayerController3D _controller3D;
    private PlayerController2D _controller2D;
    private CharacterController _characterController;

    public bool Is3D { get; private set; }

    void Start()
    {
        _controller3D = transform.GetComponent<PlayerController3D>();
        _controller2D = transform.GetComponent<PlayerController2D>();
        _characterController = transform.GetComponent<CharacterController>();
        Enable3D();
    }

    public void Enable3D()
    {
        Is3D = true;
        Change();
    }

    public void Enable2D()
    {
        Is3D = false;
        Change();
    }

    public void Deactivate()
    {
        _controller2D.enabled = false;
        _controller3D.enabled = false;
        _characterController.enabled = false;
    }

    private void Change()
    {
        _sprite.SetActive(!Is3D);
        _cam2D.enabled = !Is3D;
        _controller2D.enabled = !Is3D;

        _model.SetActive(Is3D);
        _cam3D.enabled = Is3D;
        _controller3D.enabled = Is3D;

        _characterController.radius = Is3D ? .5f : 0.0001f;
        _characterController.enabled = true;
    }
}
