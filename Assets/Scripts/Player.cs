using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Plugins;
using Extensions;
using RaycastEngine2D;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public float _moveSpeed = 10;
    private float _velocityXSmoothing;

    public Vector3 Velocity;
    
    private SpriteRenderer _renderer;
    private bool _justTurnedAround;

    public string letters = "abcdefghijklmnopqrstuvwxyz";

    public Image _darkness;

    public bool isWaiting = false;

    

    // Use this for initialization
    private void Start()
    {

        _moveSpeed = (int) GameState.Difficulty;
        
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

    private void FixedUpdate()
    {
        GameState.Score = Score.Instance.GetScore();
        if (gameObject.transform.position.x < -1 
            || gameObject.transform.position.x > MapRenderer.Instance.sizeX * MapRenderer.Instance.tileSize 
            || gameObject.transform.position.y < -MapRenderer.Instance.sizeY * MapRenderer.Instance.tileSize - 10) 
        {
            GameRunner.Instance.PlayerOutOfBoundsReset();
        }
    }

    private void HandleActions()
    {
        if (!isWaiting)
        {
            HandleMovement();
            UpdateDirectionDumb();
        }
        HandleTileSwitching();
    }

    private void HandleTileSwitching()
    {
        foreach (var letter in letters.Where(letter => Input.GetKeyDown(letter.ToString())))
        {
            if (MapRenderer.Instance.tileMap.TryGetValue(letter.ToString().ToLower(), out var tiles))
            {
                Score.Instance.DecrementScore(5);

                if (tiles.Any(tile => tile.IsActivated))
                {
                    AudioManager.Instance.Play("Deactivate");
                }
                else if (tiles.Any(tile => !tile.IsActivated))
                {
                    AudioManager.Instance.Play("Activate");    
                }
                
                tiles.ForEach(tile => tile.ToggleState());
                
                
                if (tiles.Where(tile => tile.IsActivated)
                    .Any(tile => Vector3.Distance(transform.position, tile.transform.position) < 1.48f))
                {
                    Stuck();
                }
            }
        }
    }

    private void Stuck()
    {
        GameRunner.Instance.PlayerOutOfBoundsReset();
//        GameState.IsPlayerDead = true;
//
//        DOTween.Sequence()
//            .SetUpdate(true)
//            .AppendCallback(() => Time.timeScale = 0f)
//            .Append(_darkness.DOFade(1.0f, 0.3f))
//            .AppendCallback(() => Time.timeScale = 1.0f)
//            .AppendCallback(() => SceneManager.LoadScene("HighscoreListScene"))
//            .Play();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Tile>().tileype == TileType.DOOR)
        {
            GameRunner.Instance.LoadNextLevel();
        } 
        if (col.gameObject.GetComponent<Tile>().tileype == TileType.TUTORIAL_DOOR)
        {
            GameRunner.Instance.LoadNextLevel();
            PlayerPrefs.SetString("tutorial_finished", "1");
        }
        if (col.gameObject.GetComponent<Tile>().tileype == TileType.JUMPER)
        {
            Velocity.y = _maxJumpVelocity * 2;
            _controller.Move(Velocity * Time.deltaTime);
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
            AudioManager.Instance.Play("Jump", .4f);
            Velocity.y = _maxJumpVelocity;
        }

        if (Input.GetButtonUp("Jump") && !_controller.Collisions.Below)
            if (Velocity.y > _minJumpVelocity)
                Velocity.y = _minJumpVelocity;
        
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