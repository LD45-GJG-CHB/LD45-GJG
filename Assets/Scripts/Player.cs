using System.Collections;
using Extensions;
using RaycastEngine2D;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Player : Singleton<Player>
{
    
    [SerializeField] private float _accelerationTimeAirborne = .2f;
    [SerializeField] private float _accelerationTimeGrounded = .1f;

    private BoxController2D _controller;
    
    private Vector2 _lastFacingDirection;
    private Vector3 _lastInput;
    
    [SerializeField] private float _maxJumpHeight = 4f;
    [SerializeField] private float _minJumpHeight = 1f;
    [SerializeField] private float _timeToJumpApex = .4f;
    private float _maxJumpVelocity;
    private float _minJumpVelocity;

    [SerializeField] private float _moveSpeed = 10;
    private float _velocityXSmoothing;

    public Vector3 Velocity;


    private SpriteRenderer _renderer;
    private bool _justTurnedAround;

    private const string Letters = "abcdefghijklmnopqrstuvwxyz";

    // Use this for initialization
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        
        _lastFacingDirection = Vector2.right;

        _controller = GetComponent<BoxController2D>();

        var gravity = -(2 * _maxJumpHeight) / Mathf.Pow(_timeToJumpApex, 2);

        Constants.GRAVITY = gravity;

        _maxJumpVelocity = Mathf.Abs(gravity * _timeToJumpApex);
        _minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * _minJumpHeight);

        Velocity.x = _moveSpeed;
    }


    // Update is called once per frame
    private void Update()
    {
        if (!ApplicationSettings.IsPaused())
        {
            HandleActions();   
        }
    }

    private void HandleActions()
    {
        HandleOnTriggerEnter();
        HandleMovement();
        UpdateDirectionDumb();
        HandleTileSwitching();
    }

    private static void HandleTileSwitching()
    {
        foreach (var letter in Letters)
        {
            if (!Input.GetKeyDown(letter.ToString())) continue;
            
            if (MapLoader.Instance.tileMap.TryGetValue(letter.ToString(), out var tiles))
            {
                tiles.ForEach(tile => tile.ToggleState());
            }
        }
    }

    private void HandleOnTriggerEnter()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Tile>().tileype == TileType.DOOR)
        {
            GameRunner.LoadNextLevel();
            Debug.Log("exit: @");
        }
    }

    private void UpdateDirectionDumb()
    {
        if (_controller.Collisions.Left ||
            _controller.Collisions.Right)
        {
            StartCoroutine(TurnAround());
        }
    }

    private bool IsMoving()
    {
        return Mathf.Abs(Velocity.x) > float.Epsilon;
    }
    

    private void HandleMovement()
    {
        _renderer.flipX = Mathf.Sign(Velocity.x) < float.Epsilon; // moving right
        
        if (_controller.Collisions.Above || _controller.Collisions.Below)
        {
            Velocity.y = 0;
        }
        
        if (Input.GetButtonDown("Jump") && _controller.Collisions.Below)
        {
            Velocity.y = _maxJumpVelocity;
        }

        if (Input.GetButtonUp("Jump") && !_controller.Collisions.Below)
            if (Velocity.y > _minJumpVelocity)
                Velocity.y = _minJumpVelocity;

//        var targetVelocityX = Mathf.Round(Velocity.x) * _moveSpeed;

//        Velocity.x = Mathf.SmoothDamp(Velocity.x, targetVelocityX, ref _velocityXSmoothing,
//            _controller.Collisions.Below ? _accelerationTimeGrounded : _accelerationTimeAirborne);

        Velocity.y += Constants.GRAVITY * Time.deltaTime;

        _controller.Move(Velocity * Time.deltaTime);

        if (IsMoving())
            _lastFacingDirection = Velocity.normalized.ToVector2();
    }
    
    
    private void CheckCollision()
    {
        if (_controller.Collisions.Left ||
            _controller.Collisions.Right ||
            _controller.Collisions.Above ||
            _controller.Collisions.Below)
        {
            UpdateDirection();
        }
    }
    
    private void CheckDirection()
    {
        if (!_justTurnedAround)
        {
            UpdateDirection();
        }
    }

    private void UpdateDirection()
    {
        StartCoroutine(TurnAround());
    }
    
    private IEnumerator TurnAround()
    {
        Velocity.x = -Velocity.x;
        var timer = 1.0f;
        while (timer > .0f)
        {
            _justTurnedAround = true;
            timer -= Time.deltaTime;
            yield return null;
        }

        _justTurnedAround = false;
    }
}