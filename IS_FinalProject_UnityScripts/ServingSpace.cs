using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServingSpace : MonoBehaviour
{
    //public OrderHolder OrderHolder1;
    //public OrderHolder OrderHolder2;
    //public OrderHolder OrderHolder3;
    public int active_orders = 0;
    public bool wait_for_new = false;
    public bool accept_new = true;
    public float last_order_t = 0;
    public float order_delay;
    private int cash_glow = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
	if(Time.time-last_order_t >= order_delay && !accept_new){accept_new=true;}
	if(cash_glow>0){cash_glow -= 1;}
	else if(cash_glow==0){cash_glow -= 1; GameObject.FindWithTag("TotalCash").GetComponent<Outline>().enabled = false;}
    }

    public void makeGlow(int mode){
	cash_glow = 150; 
	GameObject.FindWithTag("TotalCash").GetComponent<Outline>().enabled = true;
	if(mode==0){GameObject.FindWithTag("TotalCash").GetComponent<Outline>().effectColor = new Color32(0, 255, 0, 255);}
	else if (mode==1){GameObject.FindWithTag("TotalCash").GetComponent<Outline>().effectColor = new Color32(255, 155, 0, 255);}
	else{GameObject.FindWithTag("TotalCash").GetComponent<Outline>().effectColor = new Color32(255, 0, 0, 255);}
    }

}
