using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject _start;
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private Transform _checkLostPoint;
    [SerializeField] private Player _player;
    [SerializeField] private MazeSpawner _mazeSpawner;
    [SerializeField] private Transform _camera;

    private StartScreen _startScreen;
    private GameOverScreen _gameOverScreen;
    private Vector3 _startCameraPosition;

    private void Awake()
    {
        _startScreen = _start.GetComponent<StartScreen>();
        _gameOverScreen = _gameOver.GetComponent<GameOverScreen>();
    }

    private void OnEnable()
    {
        _startScreen.PlayButtonClick += OnPlayButtonClick;
        _gameOverScreen.ReStartButtonButtonClick += OnReStartButtonButtonClick;
        _gameOverScreen.ExitMainMenuButtonButtonClick += OnExitMainMenuButtonButtonClick;
    }

    private void OnDisable()
    {
        _startScreen.PlayButtonClick -= OnPlayButtonClick;
        _gameOverScreen.ReStartButtonButtonClick -= OnReStartButtonButtonClick;
        _gameOverScreen.ExitMainMenuButtonButtonClick -= OnExitMainMenuButtonButtonClick;
    }

    private void Start()
    {
        _startCameraPosition = _camera.position;
        Time.timeScale = 0;
        _start.SetActive(true);
    }

    private void Update()
    {
        Vector3 vector = new Vector3(_checkLostPoint.position.x, _checkLostPoint.position.y, _player.transform.position.z);
        if (Vector3.Distance(_checkLostPoint.position, vector) < 1)
            StopGame();
    }

    private void OnPlayButtonClick()
    {
        _start.SetActive(false);
        StartGame();
    }

    private void OnReStartButtonButtonClick()
    {
        _gameOver.SetActive(false);
        ReInitializeGame();
        StartGame();
    }

    private void OnExitMainMenuButtonButtonClick()
    {
        _gameOver.SetActive(false);
        ReInitializeGame();
        _start.SetActive(true);
    }

    private void ReInitializeGame()
    {
        _camera.position = _startCameraPosition;
        _mazeSpawner.ResetSpawner();
        _player.ResetPlayer();
    }

    private void StartGame()
    {      
        Time.timeScale = 1;
    }

    private void StopGame()
    {
        Time.timeScale = 0;
        _gameOver.SetActive(true);
    }
}
