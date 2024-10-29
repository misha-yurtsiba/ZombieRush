using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private float detectTime;
    [SerializeField] private float detectDistance;

    public Action<Vector2> playerTouched;
    public Action<Vector2> startDragAndDrop;
    public Action<Vector2> endDragAndDrop;
    public Action<Vector2> dragAndDrop;

    private PlayerInput playerInput;
    private IGameOver gameOver;
    private IPause pauseGame;
    private Vector2 startPosition;

    private float timer;
    private bool isTouching;
    private bool isDragAndDrop;
    private Vector2 touchPosition => playerInput.Gameplay.TouchPosition.ReadValue<Vector2>();

    [Inject]
    private void Construct(IGameOver gameOver, IPause pauseGame)
    {
        this.gameOver = gameOver;
        this.pauseGame = pauseGame;
    }
    private void OnEnable()
    {
        gameOver.gameOver += PlayerLose;
        pauseGame.pause += Pause;
    }

    private void OnDisable()
    {
        gameOver.gameOver -= PlayerLose;
        pauseGame.pause -= Pause;
    }

    public void Init()
    {
        playerInput = new PlayerInput();
        playerInput.Gameplay.Enable();
        playerInput.Gameplay.PlayerTouch.performed += PlayerTouch;
        playerInput.Gameplay.DragAndDrop.performed += StartTouching;
        playerInput.Gameplay.DragAndDrop.canceled += EndTouching;
    }

    private void PlayerLose()
    {
        playerInput.Gameplay.Disable();
        playerInput.Gameplay.PlayerTouch.performed -= PlayerTouch;
        playerInput.Gameplay.DragAndDrop.performed -= StartTouching;
        playerInput.Gameplay.DragAndDrop.canceled -= EndTouching;
    }
    private void Update()
    {
        if (!isTouching) return;

        if (isDragAndDrop)
        {
            dragAndDrop?.Invoke(touchPosition);
        }

        if(timer > detectTime || Vector2.Distance(startPosition, touchPosition) > detectDistance)
        {
            timer += Time.deltaTime;
            isDragAndDrop = true;
        }
    }
    private void PlayerTouch(InputAction.CallbackContext context)
    {
        playerTouched?.Invoke(touchPosition);     
    }

    private void StartTouching(InputAction.CallbackContext context)
    {
        timer = 0;
        isTouching = true;
        startPosition = touchPosition;
        startDragAndDrop?.Invoke(startPosition);
    }
    private void EndTouching(InputAction.CallbackContext context)
    {
        isTouching = false;
        isDragAndDrop = false;
        endDragAndDrop?.Invoke(touchPosition);
    }

    private void Pause(bool isPaused)
    {
        if(isPaused)
            playerInput.Gameplay.Disable();
        else
            playerInput.Gameplay.Enable();
    }
}
