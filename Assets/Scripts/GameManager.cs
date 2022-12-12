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
    // External References ~ Scripts
    [SerializeField] GameSaver SM;
    [SerializeField] CleanupScript CleanGame;
    [SerializeField] BunnyPatScript PatGame;
    [SerializeField] FeedBunnyScript FeedGame;
    // External References - UI Related
    [SerializeField] GameObject ParentUI;
    [SerializeField] GameObject HungerBar;
    [SerializeField] GameObject CleanBar;
    [SerializeField] GameObject HappyBar;
    // Easier way to refer to the RectTransform of the above items, especially since those objects are actually empty parents of the things we want to edit.
    public RectTransform happyRect;
    public RectTransform hungerRect;
    public RectTransform cleanRect;
    // External Variables ~ Audio and Animation
    public AudioClip sound;
    public AudioSource speak;
    public Animator animator;
    // Internal Variables
    int maxBarVal = 100;
    [SerializeField] int MaxBarLength;
    float elapsed = 0;
    // Start is called before the first frame update
    void Start()
    {
        SM.Load();
        UpdateBar();
        StartCoroutine("Stretch");
    }
    // Honestly, this should just be a Coroutine as well, this will be reflected in later builds.
    void FixedUpdate()
    {
        elapsed += Time.deltaTime;
        if (elapsed > 60)
        {
            SM.Save();
            elapsed = 0;
        }
    }
    // Causes our bunny to use the Stand animation every 15 seconds, assuming we aren't in a minigame. That last part is handled via the hacky way I set up the Animator
    IEnumerator Stretch()
    {
        while (true)
        {
            yield return new WaitForSeconds(15);
            animator.SetTrigger("Stand");
            yield return new WaitForSeconds(1);
            animator.ResetTrigger("Stand");
        }
    }
    //If there's a more efficient way to update a progress bar, then I would love to know it.
    public void UpdateBar(RectTransform TheBar, int val)
    {
        // Prevents us from having values above our set maximum.
        // Ironically, every thing that increases these stats already does this, as the val here doesn't affect the actual variable.
        val = Mathf.Min(val, maxBarVal);
        TheBar.sizeDelta = new Vector2((float)(MaxBarLength / maxBarVal) * val, TheBar.rect.height);
    }
    // Generic version of UpdateBar that automatically updates all three rather than just one
    public void UpdateBar()
    {
        UpdateBar(happyRect, SM.friendship);
        UpdateBar(cleanRect, SM.clean);
        UpdateBar(hungerRect, SM.hunger);
    }
    // Utilized by our buttons to provide feedback to prove they were clicked.
    public void makeNoise()
    {
        speak.PlayOneShot(sound);
    }
}
