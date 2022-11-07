using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessScript : MonoBehaviour
{
    GameSaver Saver;
    GameManager GM;
    [SerializeField]int health = 30;
    float cooldown = 0.75f;
    float elapsed;
    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = GameObject.Find("GameManager");
        Saver = temp.GetComponent<GameSaver>();
        GM = temp.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        elapsed += Time.deltaTime;
    }

    private void OnMouseEnter()
    {
        if (elapsed >= cooldown)
        {
            health -= 10;
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
            elapsed = 0;
        }
    }

}
