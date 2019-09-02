using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public List<string> levels = new List<string>();
    public enum State { Menu, InGame, Options, LevelSelect }
    public State state = State.Menu;

    public GameObject playmat;

    IEnumerator WaitForPlaymatToLoad(string levelName)
    {
        yield return new WaitForSeconds(0.5f);
        Playmat.GetPlaymat().CreateLayout(levelName);
    }

    private void OnGUI()
    {
        switch (state)
        {
            case State.Menu:
                Menu();
                break;
            case State.LevelSelect:
                LevelSelect();
                break;
            case State.Options:
                Options();
                break;
            case State.InGame:
                InGame();
                break;
        }
    }

    void Menu()
    {
        GUI.Label(new Rect(Screen.width / 2 - 50, 0, 100, 20), "Matching Tutorial");

        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2, 100, 30), "Play Game"))
        {
            state = State.LevelSelect;
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 35, 100, 30), "Options"))
        {
            state = State.Options;
        }
    }

    private void LevelSelect()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 50, 0, 100, 30), "Random"))
        {
            state = State.InGame;
            Instantiate(playmat);
            StartCoroutine(WaitForPlaymatToLoad("Random"));
        }

        for (int i = 0; i < levels.Count; i++)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 50, 35 + 35 * i, 100, 30), levels[i]))
            {
                state = State.InGame;
                Instantiate(playmat);
                StartCoroutine(WaitForPlaymatToLoad(levels[i]));
            }
        }
    }

    void InGame()
    {
        GUI.Label(new Rect(Screen.width / 2 - 50, 0, 100, 20), "Score: " + Playmat.GetPlaymat().GetPointsString());
        
        if (GUI.Button(new Rect(0,0,100,30), "Quit"))
        {
            Destroy(Playmat.GetPlaymat().gameObject);
            state = State.Menu;
        }

        if (Playmat.GetPlaymat().gameWon)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2, 100, 30), "Play Again?"))
            {
                Destroy(Playmat.GetPlaymat().gameObject);
                Instantiate(playmat);
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 35, 100, 30), "Quit?"))
            {
                Destroy(Playmat.GetPlaymat().gameObject);
                state = State.Menu;
            }
        }
    }

    void Options()
    {
        GUI.Label(new Rect(Screen.width / 2 - 50, 0, 100, 20), "Options");

        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2, 100, 30), "Back"))
        {
            state = State.Menu;
        }

        Color buttonColor = GameSettings.Instance().difficulty == GameSettings.GameDifficulty.Easy ? Color.green : Color.white;
        GUI.color = buttonColor;
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 35, 100, 30), "Easy"))
        {
            GameSettings.Instance().SetDifficulty(GameSettings.GameDifficulty.Easy);
        }

        buttonColor = GameSettings.Instance().difficulty == GameSettings.GameDifficulty.Medium ? Color.green : Color.white;
        GUI.color = buttonColor;
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 70, 100, 30), "Medium"))
        {
            GameSettings.Instance().SetDifficulty(GameSettings.GameDifficulty.Medium);
        }

        buttonColor = GameSettings.Instance().difficulty == GameSettings.GameDifficulty.Hard ? Color.green : Color.white;
        GUI.color = buttonColor;
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 105, 100, 30), "Hard"))
        {
            GameSettings.Instance().SetDifficulty(GameSettings.GameDifficulty.Hard);
        }
    }
}
