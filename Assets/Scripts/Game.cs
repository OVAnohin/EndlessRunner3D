using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject _start;
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private Transform _checkLostPoint;
    [SerializeField] private Player _player;
    [SerializeField] private MazeSpawner _mazeSpawner;
    [SerializeField] private Transform _camera;
    [SerializeField] private TMP_Text _score;

    private StartScreen _startScreen;
    private GameOverScreen _gameOverScreen;
    private Vector3 _startCameraPosition;
    private DateTime _scoreTime;
    private bool _isGamePlaying;

    private void Awake()
    {
        _startScreen = _start.GetComponent<StartScreen>();
        _gameOverScreen = _gameOver.GetComponent<GameOverScreen>();
    }

    private void OnEnable()
    {
        _startScreen.PlayButtonClick += OnPlayButtonClick;
        _startScreen.ExitButtonClick += OnExitButtonClick;
        _gameOverScreen.ReStartButtonButtonClick += OnReStartButtonButtonClick;
        _gameOverScreen.ExitMainMenuButtonButtonClick += OnExitMainMenuButtonButtonClick;
    }

    private void OnDisable()
    {
        _startScreen.PlayButtonClick -= OnPlayButtonClick;
        _startScreen.ExitButtonClick -= OnExitButtonClick;
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
        _isGamePlaying = true;
        _scoreTime = DateTime.Now;
        Time.timeScale = 1;
    }

    private void StopGame()
    {
        if (_isGamePlaying)
        {
            TimeSpan deltaTime = (DateTime.Now - _scoreTime).Duration();
            _score.text= deltaTime.Hours.ToString() + ":"+ deltaTime.Minutes.ToString() + ":" + deltaTime.Seconds.ToString();
        }

        _isGamePlaying = false;
        Time.timeScale = 0;
        _gameOver.SetActive(true);
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }
}
