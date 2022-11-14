// FeedBunnyScript.cs -> This script handles the Feeding minigame for the Bao Bunny project.
// Written by Nathaniel Owens, 10/26/2022.
// You may use parts of this code in your own projects provided you credit me at the top of your file and next to where the code is used.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBunnyScript: MonoBehaviour
{

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
    public AudioSource jej;
    public AudioClip fof;
    public Animator animator;
    private Rigidbody2D CarrotRb;
    GameObject temp;
    int carrotCount = 0;
    public void Activate()
    {
        isActive = true;
        BackButton.SetActive(true);
        ParentUI.SetActive(false);
        temp = Instantiate(Carrot);
        temp.transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.5f, Screen.height* 0.8f) );
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
        animator.SetFloat("Chew", 1);
        bool gaming = true;
        while (gaming)
        {
            jej.PlayOneShot(fof);
            Saver.hunger += 15;
            if (Saver.hunger > Saver.maxVal)
            {
                Saver.hunger = Saver.maxVal;
            }
            GM.UpdateBar();
            carrotCount++;
            if (carrotCount >= 3)
            {
                Destroy(temp);
                Deactivate();
                gaming = false;
                animator.SetFloat("Chew", 0);
                carrotCount = 0;
            }
            yield return new WaitForSeconds(1.5f);
        }
    }


}
