using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSaver : MonoBehaviour
{
    // PUBLIC VARIABLES: These can be accessed from anywhere, like in a minigame script.
    public int wheatCount;
    public int friendship;
    public bool hasFedFriend;
    // PRIVATE VARIABLES: These can only be read and used inside this script.
    DateTime MeaningfulLogin;
    DateTime CurrentLogin;
    // Start is called before the first frame update.
    // Start will call the Load function, and pull the current time for the user.
    // Later versions plan to include a check to see how long since the last play.
    void Start()
    {
        Load();
        CurrentLogin = DateTime.Now;
        Debug.Log("Current Time is: " + CurrentLogin.ToString());
    }
    //Saves the values such as Wheat, Friendship, and Meaningful.
    // To add more to the Save function, it either needs to be an int, a float, or
    // converted to a string by something like a ToString() command.
    // Example: PlayerPrefs.SetFloat("PoundsOfBeef", BeefAmount);
    public void Save()
    {
        if (hasFedFriend)
        {
            MeaningfulLogin = DateTime.Now;
        }
        PlayerPrefs.SetString("Meaningful", MeaningfulLogin.ToString());
        PlayerPrefs.SetInt("Wheat", wheatCount);
        PlayerPrefs.SetInt("Friendship", friendship);
        PlayerPrefs.Save();
        Debug.Log("Game Saved at " + MeaningfulLogin);
    }
    //Similarly, loads everything into the proper values.
    // When converting from a string to another data type, make sure you know HOW to do it.
    // DateTime has a built in function to parse it, but other data types may require
    // a flat out typecast, like doubleVar = (double) PlayerPrefs.GetString("doubleVar");
    public void Load()
    {
        MeaningfulLogin = DateTime.Parse(PlayerPrefs.GetString("Meaningful"));
        // This debug log exists to make sure that parsing worked properly.
        Debug.Log(MeaningfulLogin.ToString());
        wheatCount = PlayerPrefs.GetInt("Wheat");
        friendship = PlayerPrefs.GetInt("Friendship");
    }
}