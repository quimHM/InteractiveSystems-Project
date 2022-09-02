using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    public int reward;
    public string dishName;
    public Sprite dishSprite;
    public bool done;

    // Start is called before the first frame update
    void Start()
    {
        done = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dishSprite && !done)
        {
            this.GetComponent<SpriteRenderer>().sprite = dishSprite;
            done = true;
        }
    }

}
