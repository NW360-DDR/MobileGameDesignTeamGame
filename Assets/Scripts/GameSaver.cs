// GameSaver.cs -> This script handles the data saving for the Bao Bunny project.
// Written by Nathaniel Owens, 10/20/2022.
// You may use parts of this code in your own projects provided you credit me at the top of your file and next to where the code is used.
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSaver : MonoBehaviour
{
    // PUBLIC VARIABLES: These can be accessed from anywhere, like in a minigame script.
    public int hunger;
    public int clean;
    public int friendship;
    public int maxVal = 100;
    public int decayVal = 4; //How much we lower a stat by per hour of neglect.
    // PRIVATE VARIABLES: These can only be read and used inside this script.
    DateTime MeaningfulLogin;
    DateTime CurrentLogin;
    // Start is called before the first frame update.
    // Start will call the Load function, and pull the current time for the user.
    // Later versions plan to include a check to see how long since the last play.
    void Start()
    {
        CurrentLogin = DateTime.Now;
        Load();
        // Debug.Log("Current Time is: " + CurrentLogin.ToString());
    }
    //Saves the values such as Clean, Friendship, and Meaningful.
    // To add more to the Save function, it either needs to be an int, a float, or
    // converted to a string by something like a ToString() command.
    // Example: PlayerPrefs.SetFloat("PoundsOfBeef", BeefAmount);
    public void Save()
    {
        MeaningfulLogin = DateTime.Now;
        PlayerPrefs.SetString("Meaningful", MeaningfulLogin.ToString());
        PlayerPrefs.SetInt("Hunger", hunger);
        PlayerPrefs.SetInt("Friendship", friendship);
        PlayerPrefs.SetInt("Cleanliness", clean);
        PlayerPrefs.Save();
        // Debug.Log("Game Saved at " + MeaningfulLogin);
    }
    // Similarly, loads everything into the proper values.
    // When converting from a string to another data type, make sure you know HOW to do it.
    // DateTime has a built in function to parse it, but other data types may require
    // a flat out typecast, like doubleVar = (double) PlayerPrefs.GetString("doubleVar");
    public void Load()
    {
        MeaningfulLogin = DateTime.Parse(PlayerPrefs.GetString("Meaningful"));
        // This debug log exists to make sure that parsing worked properly. We don't need it anymore, but it's here just in case.
        // Debug.Log(MeaningfulLogin.ToString());
        hunger = PlayerPrefs.GetInt("Hunger");
        friendship = PlayerPrefs.GetInt("Friendship");
        clean = PlayerPrefs.GetInt("Cleanliness");
        // And now we must commit the act of changing the values based on time passed.
        for (int i = 0; i < (int) CurrentLogin.Subtract(MeaningfulLogin).TotalHours; i++)
        {
            hunger -= decayVal;
            friendship -= decayVal;
            clean -= decayVal;
        }
        // Make sure nothing is below 0, because after our decay, it has a good chance of being so.
        hunger = Math.Max(hunger, 0);
        friendship = Math.Max(friendship, 0);
        clean = Math.Max(clean, 0);
    }
}