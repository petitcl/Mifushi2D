using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

//todo: remove the need to call Move(0) at each frame
[AddComponentMenu("Scripts/Mifushi 2D/PlayerController")]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ColoredGameObject))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
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
    private ColorsManager _colorsManager;
    private LevelManager _levelManager;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private ColoredGameObject _coloredGameObject;

    // is the player currently grounded
    private bool _isGrounded = false;
    // zas the player grounded last frame
    private bool _wasGrounded = false;
    // time since player left the ground
    private float _timeSinceLastGrounded = 0.0f;
    // is the player currently jumping
    private bool _isJumping = false;
    // time period after leaving the ground, during which the player can still jump
    private float _jumpGracePeriod = 0.5f;
    private Vector3 _velocity = Vector3.zero;
    private float _movementSmoothing = 0.05f;
    private float _timeSinceLastColorChange = 0.0f;
    private bool _facingRight = true;
    // can the player do anything
    private bool _enabled = true;

    [Inject]
    public void Init(ColorsManager colorsManager, SignalBus signalBus)
    {
        //Debug.Log("PlayerController.Init");
        _colorsManager = colorsManager;
        _signalBus = signalBus;
    }

    public bool SetColor(GameColor newColor)
    {
        //Debug.Log("SetColor " + _timeSinceLastColorChange  + " " + _changeColorCooldown);
        if (_timeSinceLastColorChange < changeColorCooldown)
        {
            return false;
        }

        bool changedColor = _coloredGameObject.SetColor(newColor);
        if (!changedColor)
        {
            return false;
        }
        _timeSinceLastColorChange = 0.0f;
        _signalBus.Fire(new PlayerChangedColorSignal() { newColor = newColor });
        return true;
    }

    public bool CycleColor()
    {
        switch (_coloredGameObject.Color)
        {
            case GameColor.RED:
                return SetColor(GameColor.GREEN);
            case GameColor.GREEN:
                return SetColor(GameColor.BLUE);
            case GameColor.BLUE:
                return SetColor(GameColor.RED);
            default:
                return false;
        }
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
            _animator.SetFloat("hSpeed", Mathf.Abs(_rigidbody.velocity.x));
            _animator.SetFloat("vSpeed", _rigidbody.velocity.y);

            // If the input is moving the player right and the player is facing left...
            if (direction > 0 && !_facingRight)
            {
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (direction < 0 && _facingRight)
            {
                Flip();
            }
        }
    }

    public void Jump()
    {
        if (!_isJumping && _timeSinceLastGrounded < _jumpGracePeriod)
        {
            //Debug.LogWarning("AddForce " + _isGrounded + " " + _isJumping);
            _rigidbody.AddForce(new Vector2(0f, jumpForce));
            _isJumping = true;
            //Debug.LogWarning("After AddForce " + _isGrounded + " " + _isJumping);
        }
    }

    public void Wave()
    {
        _animator.SetTrigger("Wave");
    }

    public void Hooray()
    {
        // todo: implement Hooray animation
        _animator.SetTrigger("Hooray");
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        _facingRight = !_facingRight;

        // Multiply the player's x local scale by -1.
        transform.Rotate(0, 180, 0);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _coloredGameObject = GetComponent<ColoredGameObject>();
    }

    private void Start()
    {
        Debug.Log("PlayerController.Start");
        _timeSinceLastColorChange = changeColorCooldown;
        SetColor(this._coloredGameObject.Color);
    }

    private void Update()
    {
        _timeSinceLastColorChange += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        //Debug.Log("PlayerController.FixedUpdate " + _isGrounded + " " + _isJumping);
        // update ground check
        _wasGrounded = _isGrounded;
        _isGrounded = Physics2D.Linecast(transform.position, groundCheck.transform.position, _coloredGameObject.GameColorLayerMask);
        if (_isGrounded)
        {
            _timeSinceLastGrounded = 0.0f;
        }
        if (!_isGrounded && !_wasGrounded)
        {
            _timeSinceLastGrounded += Time.fixedDeltaTime;
        }
        //Debug.Log("After PlayerController.FixedUpdate " + _isGrounded + " " + _isJumping);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isJumping = false;
    }
}
