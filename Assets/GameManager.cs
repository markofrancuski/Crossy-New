using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public delegate void RESETDELEGATE();
    public RESETDELEGATE reset;

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    public bool isStarted;
    public bool IsGameOver;

    public void GameOver()
    {
        IsGameOver = true;
        GameOverCanvas.SetActive(true);
    }

    public GameObject GameOverCanvas;
    [SerializeField] AudioSource audioSource;

    private void Start()
    {
        audioSource.Play();
    }

    public void OnResetButtonClick()
    {
        reset.Invoke();
        GameOverCanvas.SetActive(false);
        IsGameOver = false;
        isStarted = false;

    }
}
