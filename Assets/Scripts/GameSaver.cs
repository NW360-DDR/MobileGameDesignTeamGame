using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaver : MonoBehaviour
{
    public int wheatCount;
    public int friendship;
    string lastMeaningfulLogin;
    DateTime tempForLogin;
    string currentLoginTime;
    public bool hasFedFriend;
    // Start is called before the first frame update
    void Start()
    {
        lastMeaningfulLogin = PlayerPrefs.GetString("Meaningful");
        wheatCount = PlayerPrefs.GetInt("Wheat");
        friendship = PlayerPrefs.GetInt("Friendship");
        tempForLogin = DateTime.Now;
        currentLoginTime = tempForLogin.ToString();
        Debug.Log(tempForLogin.ToString("yyyy-MM-dd HH:mm:ss"));

        SceneManager.sceneUnloaded += Save;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("Meaningful", lastMeaningfulLogin);
        PlayerPrefs.SetInt("Wheat", wheatCount);
        PlayerPrefs.SetInt("Friendship", friendship);
        PlayerPrefs.Save();
    }


    void Save<Scene>(Scene scene)
    {
        PlayerPrefs.SetString("Meaningful", lastMeaningfulLogin);
        PlayerPrefs.SetInt("Wheat", wheatCount);
        PlayerPrefs.SetInt("Friendship", friendship);
        PlayerPrefs.Save();
    }
}
