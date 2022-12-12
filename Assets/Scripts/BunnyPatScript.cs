// BunnyPatScript.cs -> This script handles the Petting minigame for the Bao Bunny project.
// Written by Nathaniel Owens, 11/07/2022.
// You may use parts of this code in your own projects provided you credit me at the top of your file and next to where the code is used.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyPatScript : MonoBehaviour
{
    // External References ~ Just about everything
    public GameManager GM;
    public GameSaver Saver;
    public GameObject ParentUI;
    public GameObject Bunny;
    public GameObject BackButton;
    public Animator animator;
    // Internal Variables
    [SerializeField] bool isActive = false;
    float patTotal = 0.0f;
    Touch fingy;
    Vector2 oldPos;
    public void Activate()
    {
        // Hides our three main buttons and shows the Back Button.
        isActive = true;
        ParentUI.SetActive(false);
        BackButton.SetActive(true);
    }
    public void Deactivate()
    {
        // The exact opposite of the above function.
        isActive = false;
        ParentUI.SetActive(true);
        BackButton.SetActive(false);
        Saver.Save();
    }
    float CalcDist(Vector2 first, Vector2 last)
    {
        // Calculating distance formula using Mathf.Pow for both squares and square root.
        float temp = Mathf.Pow((last.x - first.x), 2) + Mathf.Pow((last.y - first.y), 2);
        temp = Mathf.Pow(temp, 0.5f);
        return temp;
    }
    private void FixedUpdate()
    {
        if (isActive && Input.touchCount > 0)
        {
            fingy = Input.GetTouch(0);
            animator.SetFloat("Pet", 1);
            if (fingy.phase == TouchPhase.Began)
            {
                oldPos = fingy.position;
            }
            else if (fingy.phase == TouchPhase.Moved)
            {
                patTotal += (CalcDist(oldPos, fingy.position));
                // This exists because the friendship value needs to stay as an int for other functions, and I don't feel like reworking everything else to accomodate a float.
                if (patTotal >= 1)
                {
                    patTotal -= 1;
                    Saver.friendship += 1;
                    if (Saver.friendship > Saver.maxVal)
                    {
                        Saver.friendship = Saver.maxVal;
                    }
                    GM.UpdateBar();
                }
            }
        }
        else
        {
            // This technically runs a lot more often than it should, but honestly, it's not hurting anyone.
            animator.SetFloat("Pet", 0);
        }
    }
}
