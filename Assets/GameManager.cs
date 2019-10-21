using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public delegate void RESETDELEGATE();
    public RESETDELEGATE reset;

    public bool isStarted;
    public bool IsGameOver;

    public GameObject GameOverCanvas;
    [SerializeField] AudioSource audioSource;

    [SerializeField] private BoolValue musicBool;
    [SerializeField] private BoolValue soundEffectBool;
    [SerializeField] private GameObject settingsCanvas;

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        if(musicBool.statValue) audioSource.Play();
        else audioSource.Stop();
    }

    public void GameOver()
    {
        IsGameOver = true;
        GameOverCanvas.SetActive(true);
    }
    public void OnResetButtonClick()
    {
        reset.Invoke();
        GameOverCanvas.SetActive(false);
        IsGameOver = false;
        isStarted = false;
    }

    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle soundEffectToggle;
    public void OnSettingsCanvasClicked()
    {
        settingsCanvas.SetActive(!settingsCanvas.activeInHierarchy);
        
        if(settingsCanvas.activeInHierarchy)
        {
            musicToggle.isOn = musicBool.statValue;
            soundEffectToggle.isOn = soundEffectBool.statValue; 
        }
    }

    public void OnMusicSettingsChange()
    {
        musicBool.statValue = !musicBool.statValue;
        if(musicBool.statValue) audioSource.Play();
        else audioSource.Stop();
    }
    public void OnSoundEffectSettingsChanged()
    {
         soundEffectBool.statValue = !soundEffectBool.statValue;
    }
}
