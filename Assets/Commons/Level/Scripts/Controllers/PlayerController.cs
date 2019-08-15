using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[AddComponentMenu("Scripts/Mifushi 2D/PlayerController")]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ColoredGameObject))]
public class PlayerController : ColoredGameObject
{
    [SerializeField]
    public GameObject groundCheck;

    [SerializeField]
    public float xVelocity = 1.0f;

    [SerializeField]
    public float jumpForce = 400.0f;

    [SerializeField]
    public bool airControl = true;

    [SerializeField]
    public float changeColorCooldown = 0.1f;

    private SignalBus _signalBus;

    private Rigidbody2D _rigidbody;

    private bool _isGrounded = false;
    private bool _wasGrounded = false;
    private bool _isJumping = false;
    private Vector3 _velocity = Vector3.zero;
    private float _movementSmoothing = 0.05f;
    private float _timeSinceLastColorChange = 0.0f;

    [Inject]
    public void Init(ColorsManager colorsManager, SignalBus signalBus)
    {
        Debug.Log("PlayerController.Init");
        _colorsManager = colorsManager;
        _signalBus = signalBus;
    }

    public override bool SetColor(GameColor newColor)
    {
        //Debug.Log("SetColor " + _timeSinceLastColorChange  + " " + _changeColorCooldown);
        if (_timeSinceLastColorChange < changeColorCooldown)
        {
            return false;
        }

        bool changedColor = base.SetColor(newColor);
        if (!changedColor)
        {
            return false;
        }
        _timeSinceLastColorChange = 0.0f;
        _signalBus.Fire(new PlayerChangedColorSignal() { newColor = newColor });
        return true;
    }

    public void Move(float direction)
    {

        // update velocity
        if (_isGrounded || airControl)
        {
            Vector3 targetVelocity = Vector3.zero;
            if (direction > 0.0f)
            {
                targetVelocity = new Vector2(xVelocity, _rigidbody.velocity.y);
            }
            else if (direction < 0.0f)
            {
                targetVelocity = new Vector2(-xVelocity, _rigidbody.velocity.y);
            }
            else
            {
                targetVelocity = new Vector2(_isGrounded ? 0 : _rigidbody.velocity.x, _rigidbody.velocity.y);
            }
            _rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, targetVelocity, ref _velocity, _movementSmoothing);
        }
    }

    public void Jump()
    {
        if (_isGrounded && !_isJumping)
        {
            //Debug.LogWarning("AddForce " + _isGrounded + " " + _isJumping);
            _rigidbody.AddForce(new Vector2(0f, jumpForce));
            _isJumping = true;
            //Debug.LogWarning("After AddForce " + _isGrounded + " " + _isJumping);
        }
    }

    protected override void Start()
    {
        _register = false;
        _rigidbody = GetComponent<Rigidbody2D>();
        _timeSinceLastColorChange = changeColorCooldown;
        base.Start();
        Debug.Log("PlayerController.Start");
    }

    protected override void Update()
    {
        _timeSinceLastColorChange += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        //Debug.Log("PlayerController.FixedUpdate " + _isGrounded + " " + _isJumping);
        // update ground check
        _wasGrounded = _isGrounded;
        _isGrounded = Physics2D.Linecast(transform.position, groundCheck.transform.position, GameColorLayerMask);
        //Debug.Log("After PlayerController.FixedUpdate " + _isGrounded + " " + _isJumping);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isJumping = false;
    }
}
