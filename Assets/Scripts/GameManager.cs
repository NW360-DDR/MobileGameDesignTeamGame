// GameManager.cs -> This script handles the main functions of the Bao Bunny project.
// Written by Nathaniel Owens, 10/26/2022.
// You may use parts of this code in your own projects provided you credit me at the top of your file and next to where the code is used.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // The SerializeField modifier allows us to edit these values in the Unity Editor.
    // This does not allow other scripts to see and edit them like a "public" modifier.

    //~~~~~~~~~OTHER SCRIPTS CALLED~~~~~~~~~
    [SerializeField] GameSaver SM;
    [SerializeField] CleanupScript CleanGame;
    [SerializeField] BunnyPatScript PatGame;
    [SerializeField] FeedBunnyScript FeedGame;

    //~~~~~~~~~~~~~GAME OBJECTS~~~~~~~~~~~~~
    [SerializeField] GameObject ParentUI;
    [SerializeField] GameObject HungerBar;
    [SerializeField] GameObject CleanBar;
    [SerializeField] GameObject HappyBar;

    //~~~~~~~~~~~~~PROGRESS BARS~~~~~~~~~~~~
    // Doing it like this takes more memory but makes it a tad easier to edit the width later.
    public RectTransform happyRect;
    public RectTransform hungerRect;
    public RectTransform cleanRect;

    //~~~~~~~~~~~~~~~VARIABLES~~~~~~~~~~~~~~
    int maxBarVal = 100;
    [SerializeField] int MaxBarLength;
    float elapsed = 0;

    // Start is called before the first frame update
    void Start()
    {
        SM.Load();
        UpdateBar();
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > 60)
        {
            SM.Save();
            elapsed = 0;
        }
    }

    //If there's a more efficient way to update a progress bar, then I would love to know it.
    public void UpdateBar(RectTransform TheBar, int val)
    {
        // Prevents us from having values above our set maximum 
        val = Mathf.Min(val, maxBarVal);
        TheBar.sizeDelta = new Vector2((float)(MaxBarLength / maxBarVal) * val, TheBar.rect.height);
    }
    //Generic version of UpdateBar that automatically updates all three rather than just one
    public void UpdateBar()
    {
        UpdateBar(happyRect, SM.friendship);
        UpdateBar(cleanRect, SM.clean);
        UpdateBar(hungerRect, SM.hunger);
    }
}
