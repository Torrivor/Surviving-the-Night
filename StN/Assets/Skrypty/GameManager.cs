using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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


    public void Awake()
    {
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
    void ResumeGame()
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
    }
}


