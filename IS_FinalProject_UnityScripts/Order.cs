using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Order : MonoBehaviour
{
	int completed = 0;
	
    	public Timer timer;
    	public GameObject timerPrefab;	

	[SerializeField] public Text vegetableText;
	[SerializeField] public Text meatText;
	[SerializeField] public Text pastaText;

	[SerializeField] public Image vegetableCircle;
	[SerializeField] public Image meatCircle;
	[SerializeField] public Image pastaCircle;

	private int vegetableTech = 0;
	private int meatTech = 0;
	private int pastaTech = 0;

	List<string> techinques = new List<string>(new string[] {"Not wanted","Fried","Baked","Boiled"});

	Color32[] ColorTech = new Color32[] {new Color32(255, 255, 255, 255),new Color32(155, 155, 0, 255),new Color32(255, 155, 155, 255),new Color32(155, 155, 255, 255)};
    
	//public bool done;

    // Start is called before the first frame update
    void Start()
    { 	
		timer = Instantiate(timerPrefab, transform.position + new Vector3(5, 1, 5), transform.rotation, GameObject.FindGameObjectWithTag("Canvas").transform).GetComponent<Timer>();
		int timeallowed = 25;
		while(vegetableTech == 0 && meatTech == 0 && pastaTech == 0){
			transform.GetChild(2).GetComponent<Text>().enabled = true;
			transform.GetChild(3).GetComponent<Text>().enabled = true;
			transform.GetChild(4).GetComponent<Text>().enabled = true;
			
			meatTech = Random.Range(0, 3);
			if(meatTech!=0){
				meatText.text = techinques[meatTech];
				meatText.color = ColorTech[meatTech];
				meatCircle.enabled = true;
				meatCircle.color = ColorTech[meatTech];
				timeallowed+=20;
			}
			else{
				transform.GetChild(2).GetComponent<Text>().enabled = false;
				meatText.color = new Color32(0, 0, 0, 255);
				meatCircle.enabled = false;}

			pastaTech = Random.Range(0, 2);
			if(pastaTech == 1){
				pastaTech = 3;
				pastaText.text = techinques[pastaTech];
				pastaText.color = ColorTech[pastaTech];
				pastaCircle.enabled = true;
				pastaCircle.color = ColorTech[pastaTech];
				timeallowed+=20;
			}
			else{
				transform.GetChild(3).GetComponent<Text>().enabled = false;
				pastaText.color = new Color32(0, 0, 0, 255);
				pastaCircle.enabled = false;}

			vegetableTech = Random.Range(0, 4);
			if(vegetableTech!=0){
				vegetableText.text = techinques[vegetableTech];
				vegetableText.color = ColorTech[vegetableTech];
				vegetableCircle.enabled = true;
				vegetableCircle.color = ColorTech[vegetableTech];
				timeallowed+=20;
			}
			else{
				transform.GetChild(4).GetComponent<Text>().enabled = false;
				vegetableText.color = new Color32(0, 0, 0, 255);
				vegetableCircle.enabled = false;}
			timer.Duration = timeallowed;
			timer.Begin(timeallowed);
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (timer && timer.remainingDuration<0){
		GameObject totalcash = GameObject.FindWithTag("TotalCash");
                    totalcash.GetComponent<Points>().add(-5);
			SoundManager.Instance.PlayOrderBad(); 
			
		ServingSpace serving_space = GameObject.FindWithTag("ServeryCounter").GetComponent<ServingSpace>();
		    serving_space.active_orders -= 1;
		    serving_space.accept_new = false;
		    serving_space.last_order_t = Time.time;
		    serving_space.makeGlow(2);
		Destroy(timer.gameObject);
		Destroy(this.gameObject);
	}
    }

    public int check(CounterSpace dish){
 	if(dish.components[0]){
		if(dish.components[0].tech==meatTech && dish.components[0].state==1){completed+=5;}
 			else if(dish.components[0].tech!=meatTech && dish.components[0].state==1){completed+=2;}
 			else {completed-=2;}
		completed += dish.components[0].bonus;
	}
 	if(dish.components[1]){
		if(dish.components[1].tech==pastaTech && dish.components[1].state==1){completed+=5;}
 			else if(dish.components[1].tech!=pastaTech && dish.components[1].state==1){completed+=2;}
 			else {completed-=2;}
		completed += dish.components[1].bonus;
	}
 	if(dish.components[2]){
		if(dish.components[2].tech==vegetableTech && dish.components[2].state==1){completed+=5;}
 			else if(dish.components[2].tech!=vegetableTech && dish.components[2].state==1){completed+=2;}
 			else {completed-=2;}
		completed += dish.components[2].bonus;
	}

 	return completed;
    }

}
