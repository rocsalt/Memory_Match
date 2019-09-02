using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings
{

    private static GameSettings gameSettings;
    private GameSettings() { }

    public static GameSettings Instance()
    {
        if (gameSettings == null)
        {
            gameSettings = new GameSettings();
        }

        return gameSettings;
    }

    public enum GameDifficulty { Easy, Medium, Hard }
    public GameDifficulty difficulty = GameDifficulty.Easy;
    private string[] easyDifficulty = { "Blue", "Gold", "Green" };
    private string[] mediumDifficulty = { "Blue", "Gold", "Green", "Pink", "Purple" };
    private string[] hardDifficulty = { "Blue", "Gold", "Green", "Pink", "Purple", "Red", "Teal" };

    public List<string> CardTypes
    {
        get
        {
            List<string> tempList = new List<string>();
            switch (difficulty)
            {
                case GameDifficulty.Easy:
                    tempList.AddRange(easyDifficulty);
                    break;
                case GameDifficulty.Medium:
                    tempList.AddRange(mediumDifficulty);
                    break;
                case GameDifficulty.Hard:
                    tempList.AddRange(hardDifficulty);
                    break;
            }
            return tempList;
        }
    }

    public void SetDifficulty(GameDifficulty diff)
    {
        difficulty = diff;
    }

    public string GetRandomType()
    {
        return CardTypes[Random.Range(0, CardTypes.Count)];
    }
}
