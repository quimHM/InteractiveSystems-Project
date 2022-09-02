using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodObject : MonoBehaviour
{
    public int state = 0;
    public int type;
    public int tech;

    public Sprite raw;
    public Sprite cooked;
    public Sprite burned;

    public int bonus = 0;

    public Behaviour halo;

    Color32[] ColorTech = new Color32[] {new Color32(255, 255, 255, 255),new Color32(255, 255, 155, 255),new Color32(255, 155, 155, 255),new Color32(155, 155, 255, 255)};
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
	this.GetComponent<SpriteRenderer>().color = ColorTech[tech];
        if (state == 0) { this.GetComponent<SpriteRenderer>().sprite = raw; }
        else if (state == 1) { this.GetComponent<SpriteRenderer>().sprite = cooked; }
        else if (state == 2) { 
		this.GetComponent<SpriteRenderer>().sprite = burned; 
		this.GetComponent<SpriteRenderer>().color = new Color32(25, 25, 25, 255);
	}
	if(bonus>0){halo.enabled = true;}
    }
}