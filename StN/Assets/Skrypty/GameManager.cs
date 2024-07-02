using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

   public enum GameState
    {
    Gameplay,
    Wstrzymano,
    Koniec_Gry
    }

    public GameState currentState;

    public GameState previousState;

    [Header("UI")]
    public GameObject pauseScreen;
    public GameObject resultsScreen;

    //wyœwietlacz aktualnych stat
    public Text currentHPDisplay;
    public Text currentRecoveryDisplay;
    public Text currentMoveSpeedDisplay;
    public Text currentMightDisplay;
    public Text currentProjectileSpeedDisplay;
    public Text currentMagnetDisplay;

    //oznacza czy gra jest zakonczona
    public bool isKoniec_Gry = false;

    public void Awake()
    {
        //ostrzerzenie jesli jest jeszczejeden singleton tego typu w grze
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("EXTRA " + this + " DELETED");
            Destroy(gameObject);
        }

        DisableScreen();
    }
    void Update()
    {


        switch (currentState)
        {
            case GameState.Gameplay:
                CheckForPauseAndResume();
                break;

            case GameState.Wstrzymano:
                CheckForPauseAndResume();
                break;

            case GameState.Koniec_Gry:
                if (!isKoniec_Gry)
                {
                    isKoniec_Gry = true;
                    Time.timeScale = 0f; //calkowicie zatrzymuje gre
                    Debug.Log("Koniec Gry");
                    DisplayResults();
                }
                break;
            default:
                Debug.LogWarning("STATE DOES NOT EXIST");
                break;
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }


    public void PauseGame()
    {
        if (currentState != GameState.Wstrzymano)
        {
            previousState = currentState;
            ChangeState(GameState.Wstrzymano);
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            Debug.Log("Gra jest wstrzymana");
        }
    }
    public void ResumeGame()
    {
        if (currentState == GameState.Wstrzymano)
        {
            ChangeState(previousState);
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
            Debug.Log("Wznowienie gry");
        }
    }

    void CheckForPauseAndResume()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(currentState ==GameState.Wstrzymano)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void DisableScreen()
    {
        pauseScreen.SetActive(false);
        resultsScreen.SetActive(false);
    }

    public void Koniec_Gry()
    {
        ChangeState(GameState.Koniec_Gry);
    }

    void DisplayResults()
    {
        resultsScreen.SetActive(true);
    }
}