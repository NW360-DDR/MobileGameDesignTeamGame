// FeedBunnyScript.cs -> This script handles the Feeding minigame for the Bao Bunny project.
// Written by Nathaniel Owens, 10/26/2022.
// You may use parts of this code in your own projects provided you credit me at the top of your file and next to where the code is used.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBunnyScript: MonoBehaviour
{
    // TODO: Holy crap make this more readable, add some comments, ANYTHING NATE I BEG YOU.
    // JUST BECAUSE YOU CAN READ IT DOESN'T MEAN IT'S READABLE TO SANE PEOPLE
    // ~ Nathaniel Owens, 2022
    public GameManager GM;
    public GameSaver Saver;
    public GameObject ParentUI;
    public GameObject Bunny;
    public GameObject Carrot;
    public GameObject BackButton;

    public bool isActive = false;
    // So we don't get stuck on frame 1 of the eating animation.
    bool animating;

    // Stuff for the Carrot
    Touch tipTap;
    float deltaX, deltaY;
    public AudioSource speaker;
    public AudioClip boop;
    public Animator animator;
    private Rigidbody2D CarrotRb;
    GameObject temp;
    int carrotBites = 0;

    public void Activate()
    {
        isActive = true;
        BackButton.SetActive(true);
        ParentUI.SetActive(false);
        temp = Instantiate(Carrot);
        temp.transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.5f, Screen.height* 0.8f));
        animator.SetFloat("Antici", 1);
    }

    public void Deactivate()
    {
        isActive = false;
        BackButton.SetActive(false);
        ParentUI.SetActive(true);
        if (temp != null)
        {
            Destroy(temp);
        }
        Saver.Save();
        animating = false;
        animator.SetFloat("Chew", 0);
        animator.SetFloat("Antici", 0);
        carrotBites = 0;
    }

    void Update()
    {
        if (Input.touchCount > 0 && isActive)
        {
            tipTap = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(tipTap.position);
            temp.transform.position = touchPos;
            bool withinX = (tipTap.position.x > Screen.width * 0.25f && tipTap.position.x < Screen.width * 0.4f);
            bool withinY = (tipTap.position.y > Screen.height * 0.3f && tipTap.position.y < Screen.height * 0.45f);
            if (withinX && withinY && tipTap.phase == TouchPhase.Stationary && !animating)
            {
                StartCoroutine("HungyBoi");
                animating = true;

            }


        }

    }

    IEnumerator HungyBoi()
    {
        animator.SetFloat("Antici", 0);
        animator.SetFloat("Chew", 1);
        bool chewing = true;
        while (chewing)
        {
            speaker.PlayOneShot(boop);
            Saver.hunger += 15;
            if (Saver.hunger > Saver.maxVal)
            {
                Saver.hunger = Saver.maxVal;
            }
            GM.UpdateBar();
            carrotBites++;
            if (carrotBites >= 3)
            {
                Deactivate();
                chewing = false;
            }
            yield return new WaitForSeconds(1.5f);
        }
    }


}
