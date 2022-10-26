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
    public GameObject[] MessyPrefabs;

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
        // Unless... actually, making the prefab a child of the Canvas could theoretically solve this.
        // NOTE: Not only does this work for placement, but somehow, OnMouseOver works again. Not sure if it works on actual mobile builds though.
    }

    void FixedUpdate()
    {
        if (isActive)
        {
            // This is how we check how many messes remain. If this becomes zero, we cleaned the whole place.
            if (GameObject.FindGameObjectsWithTag("Mess").Length == 0)
            {
                Deactivate();
            }
        }
    }
    public void Activate()
    {
        SpawnMess();
        isActive = true;
        // TODO: Hide all the UI elements except for the messes we spawned,, and the bars. 
        ParentUI.SetActive(false);
        Bunny.SetActive(false);
    }

    public void Deactivate()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Mess"))
        {
            Destroy(item);
        }
        isActive = false;
        //TODO: Unhide everything we hid earlier, and put them back if needed.
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
            GameObject mess = Instantiate(MessyPrefabs[0]);
            mess.transform.position = new Vector2(tempX, tempY).normalized * Random.Range(-2f, 2f);
            mess.transform.SetParent(Canvas.transform);
        }
    }
}
