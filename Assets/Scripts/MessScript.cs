using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessScript : MonoBehaviour
{
    // EXTERNAL REFERENCES
    GameSaver Saver;
    GameManager GM;

    // INTERNAL VARIABLES
    int health = 3;
    float cooldown = 0.75f;
    // Setting it to this so you can start cleaning immediately.
    float elapsed = 0.75f;

    // Start is called before the first frame update.
    // Prepare our references, as this is a Prefab that we can't assign them to, and also randomly rotate ourselves for some variance.
    void Start()
    {
        GameObject temp = GameObject.Find("GameManager");
        Saver = temp.GetComponent<GameSaver>();
        GM = temp.GetComponent<GameManager>();
        transform.Rotate(new Vector3(0, 0, Random.Range(0,360)));
    }

    // FixedUpdate is called 60 times per second, and only handles our elapsed time.
    void FixedUpdate()
    {
        elapsed += Time.deltaTime;
    }

    // I should convert this to the Touch equivalent, supposedly this can cause certain phones to slow down.
    private void OnMouseEnter()
    {
        if (elapsed >= cooldown)
        {
            health -= 1;
            if (health <= 0)
            {
                Saver.clean += 10;
                if (Saver.clean > Saver.maxVal)
                {
                    Saver.clean = Saver.maxVal;
                }
                GM.UpdateBar();
                Destroy(gameObject);
            }
            // We've touched it, and it's been cleaned somewhat, but if we got here it's not fully cleaned yet!
            elapsed = 0;
        }
    }

}
