// CleanupScript.cs -> This script handles the Cleanup minigame for the Bao Bunny project.
// Written by Nathaniel Owens, 10/26/2022.
// You may use parts of this code in your own projects provided you credit me at the top of your file and next to where the code is used.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanupScript : MonoBehaviour
{
    // EXTERNAL REFERENCES
    public GameManager GM;
    public GameSaver Saver;
    public GameObject ParentUI;
    public GameObject Bunny;
    public GameObject Canvas;
    public GameObject Messy;

    // INTERNAL VARIABLES
    float width, height;
    [SerializeField] bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        // We're getting the center of the screen here, which will help me determine the best places to spawn prefabs.
        width = Screen.width * 0.5f;
        height = Screen.height * 0.5f;
        // Because I'm stupid, this does not take into account conversion between pixels and Units in Unity.
        // Note: This issue can be fixed later by using Camera.main.ScreenToWorldPoint in order to use more of the screen for randomness.
    }

    void FixedUpdate()
    { // As this only needs to run every once in a while, I have this script set to use Fixed Update. I apparently didn't feel like using another public variable to keep track of the count.
        if (isActive)
        {
            // This is how we check how many messes remain. If this becomes zero, we cleaned the whole place.
            if (GameObject.FindGameObjectsWithTag("Mess").Length == 0)
            {
                Deactivate();
            }
        }
    }
    // This minigame does not have a Back button because... I forgot to add one in for this build.
    // But it also makes sense. Your bunny needs to have good clean home.
    public void Activate()
    {
        SpawnMess();
        isActive = true;
        ParentUI.SetActive(false);
        Bunny.SetActive(false);
    }

    // Ironically, a built in failsafe, should I implement the Back Button in this minigame, and it's still inefficient.
    public void Deactivate()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Mess"))
        {
            Destroy(item);
        }
        isActive = false;
        ParentUI.SetActive(true);
        Bunny.SetActive(true);
        Saver.Save();
    }

    void SpawnMess()
    {
        for (int i = 0; i < 3; i += 1)
        {
            float tempX = Random.Range(width * 0f, width * 2f);
            float tempY = Random.Range(height * 0f, height * 2f);
            GameObject mess = Instantiate(Messy);
            // Change this stuff to utilize Camera.main.ScreenToWorldPoint in later builds, Nate.
            mess.transform.position = new Vector2(tempX, tempY).normalized * Random.Range(-2f, 2f);
            mess.transform.SetParent(Canvas.transform);
        }
    }
}
