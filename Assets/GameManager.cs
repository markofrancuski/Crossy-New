using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public delegate void ResetDelegate();
    public ResetDelegate reset;

    public bool isStarted;
    public bool IsGameOver;

    public GameObject gameOverCanvas;
    [SerializeField] AudioSource audioSource;

    [SerializeField] private bool isMusicOn;
    public bool IsMusicOn
    {
        get{return isMusicOn; }
        set
        {
            isMusicOn = value; 
            if(isMusicOn) audioSource.Play();
            else audioSource.Stop();
        }      
    }
    
    [SerializeField] private bool isSoundEffectOn;
    public bool IsSoundEffectOn
    {
        get{return isSoundEffectOn;}
        set{isSoundEffectOn = value;}
    }

    [SerializeField] private GameObject settingsCanvas;

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        if(isMusicOn) audioSource.Play();
        else audioSource.Stop();
    }

    public void GameOver()
    {
        IsGameOver = true;
        gameOverCanvas.SetActive(true);
    }
    public void OnResetButtonClick()
    {
        reset.Invoke();
        gameOverCanvas.SetActive(false);
        IsGameOver = false;
        isStarted = false;
    }

    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle soundEffectToggle;

    public void OnSettingsCanvasClicked()
    {
        settingsCanvas.SetActive(!settingsCanvas.activeInHierarchy);
    }

    public void OnMusicChanged()
    {
        IsMusicOn = !isMusicOn;
    }
    public void OnSoundEffectChanged()
    {
        IsSoundEffectOn = !isSoundEffectOn;
    }
}
