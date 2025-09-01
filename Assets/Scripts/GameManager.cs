using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void LoadGame(string gameName)
    {
        SceneLoader.Scene scene = GetGameScene(gameName);
        SceneLoader.Load(scene);
    } 

    private SceneLoader.Scene GetGameScene(string gameName)
    {
        if (gameName == "PopTheLockScene") return SceneLoader.Scene.PopTheLockScene;
        else if (gameName == "FlappyBirdScene") return SceneLoader.Scene.FlappyBirdScene;
        else return SceneLoader.Scene.MainPage;
    }
}
