using System;
using UnityEngine;

//--------------------------------------------------------;
//--------------------------------------------------------;
//----------------------SORRY FOR NESTY CODE--------------;
//--------------------------------------------------------;
//--------------------------------------------------------;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _JumpForce;
    [SerializeField] private float _Speed;
    [SerializeField] private Rigidbody2D _Rb;
    [SerializeField] private Grounded _Grounded;
    [SerializeField, Range(0, 1f)] private float _JumpCut;
    [SerializeField] private SpriteRenderer _sp;
    public bool _fuckThisTime;
    float _currentJumpCutValue;
    float _directionX, _directionY;
    public event Action OnJump;
    public bool CanJump;
    public Transform _child;
    [SerializeField] private Animator _Animator;

    void Awake()
    {
        _currentJumpCutValue = _JumpCut;
    }
    void Update()
    {

        //not a good code but İts a jam baby;
        //theese code blocks are just jumping with a jump cut;
        //we are cutting the jump after a quick jump;
        //if we cut the jump, we are changing the velocityY to 0;
        _Animator.SetFloat("VelocityY", _Rb.linearVelocityY);

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.GetKey(KeyCode.Space))
        {
            _Rb.AddForceY(_currentJumpCutValue);
            _currentJumpCutValue -= Time.deltaTime * (_JumpForce / 2);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (_Rb.linearVelocityY >= 15) return;
            _currentJumpCutValue = _JumpCut;
            if (_Rb.linearVelocityY >= 0)
            {
                _Rb.linearVelocityY = 0;
            }
        }
        _directionX = Input.GetAxisRaw("Horizontal");

        if (_directionX != 0)
            _sp.flipX = _directionX <= 0 ? true : false;

        _child.localScale = new Vector2(_directionX == 0 ? _child.transform.localScale.x : Mathf.Sign(_directionX), _child.localScale.y);
    }
    public int GetDirection() => (int)_directionX;
    void FixedUpdate()
    {
        Run();
    }
    void Run()
    {
        _Animator.SetFloat("VelocityX", _directionX != 0 ? Mathf.Abs(_directionX) : -1);
        //_Rb.linearVelocity = new Vector2(_directionX * _Speed, (_fuckThisTime) ? _directionY * _Speed : _Rb.linearVelocityY);
        transform.position += _directionX * _Speed * Time.deltaTime * transform.right;
        _Rb.linearVelocityX = 0;
    }
    void Jump()
    {
        if (!CanJump) return;

        if (_Grounded.IsGrounded())
        {
            _Rb.AddForce(_JumpForce * 100 * transform.up);
            _Rb.gravityScale = 1;
            CanJump = false;
        }
        OnJump?.Invoke();
    }



}