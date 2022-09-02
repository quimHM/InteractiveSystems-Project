using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public FoodObject inHand; // Food object
    public FireExtinguisher inHandFE; // fire extinguisher
    public Order inHandO; // Order
    public Dish inHandDish; // dish

    public Collider other;

    public TimerInteraction interT;
    public GameObject timerPrefab;
    public ParticleSystem healingRing;
    public GameObject healingRingPrefab;
    public ParticleSystem sparkles;
    public GameObject sparklesPrefab;

    public int insideSomeTrigger = 0;

    // Start is called before the first frame update
    void Start()
    {
	    interT = Instantiate(timerPrefab, transform.position, timerPrefab.transform.rotation, GameObject.FindGameObjectWithTag("Canvas").transform).GetComponent<TimerInteraction>();

        healingRing = Instantiate(healingRingPrefab, transform.position, healingRingPrefab.transform.rotation).GetComponent<ParticleSystem>();
	sparkles = Instantiate(sparklesPrefab, transform.position+20*Vector3.up, sparklesPrefab.transform.rotation).GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inHandFE)
        {
            inHandFE.transform.position = transform.position + new Vector3(0,5,-3);
        }
        else if (inHand)
        {
            inHand.transform.position = transform.position + 14*Vector3.up;
        } 
	else if (inHandO)
        {
            inHandO.transform.position = transform.position + 14*Vector3.up;
        }
        else if (inHandDish)
        {
            inHandDish.transform.position = transform.position + 14 * Vector3.up;
        }
        interT.transform.position = transform.position + Vector3.up*15;
        healingRing.transform.position = transform.position;
	sparkles.transform.position = transform.position+20*Vector3.up;
        checkInteraction();
	//other = null;
	if(insideSomeTrigger==0)
        {
            interT.state=-1;
	    other=null;
        }
    }

    private void beginInteraction(Collider c_other)
    {
	    other = c_other;
	    if (other.CompareTag("Quit")){interT.Begin(interT.Duration);}

	    if (!inHandFE && !inHandO && !inHandDish){
            	if (other.CompareTag("PastaFridge") || other.CompareTag("MeatFridge") || other.CompareTag("VegetableFridge")){interT.Begin(interT.Duration);}
            	
		else if (other.CompareTag("Oven")){
                	OvenFire fire = other.gameObject.GetComponent<OvenFire>();
                	if (inHand){if (inHand.state == 0 && !fire.inOven){interT.Begin(interT.Duration);}}
                	else if (fire.inOven){if (fire.inOven.state > 0 && !fire.fire.isPlaying){interT.Begin(interT.Duration);}}
                }
                else if (other.CompareTag("Counter") && inHand){interT.Begin(interT.Duration);}

                else if (other.CompareTag("FireExtinguisher")){interT.Begin(interT.Duration);}
	    
	        else if (other.CompareTag("OrderSpace")){
		    OrderHolder holder = other.gameObject.GetComponent<OrderHolder>();
		    if(holder.order){interT.Begin(interT.Duration);}
                }

                else if (other.CompareTag("Player1")) {
                	PlayerBehaviour otherPlayer = other.gameObject.GetComponent<PlayerBehaviour>();
			if (inHand || otherPlayer.inHand){interT.Begin(interT.Duration);}
                	if (otherPlayer.inHand && otherPlayer.inHand.state == 2 || this.inHand && this.inHand.state == 2){
                        	otherPlayer.healingRing.Play();
				healingRing.Play();
                	}
	    	}

                else if (other.CompareTag("Trash")){if (inHand != null){interT.Begin(interT.Duration);}}
            } 

	    else if (inHandO){if (other.CompareTag("Counter")){interT.Begin(interT.Duration);}}
            
	    else if (inHandDish){
                if (other.CompareTag("ServeryCounter")){interT.Begin(interT.Duration);}
            }

            else if (inHandFE){
                if (other.CompareTag("Oven")){
                    OvenFire fire = other.gameObject.GetComponent<OvenFire>();
 		    if (fire.inOven && fire.inOven.state == 2){interT.Begin(interT.Duration);}
                }
            }
    }
    
    private void OnTriggerEnter(Collider c_other)
    {
	    insideSomeTrigger+=1;
	    beginInteraction(c_other);
	    Debug.Log(other.tag);
    }
    
    private void OnTriggerExit(Collider c_other)
    {
	insideSomeTrigger-=1;
    }

    private void checkInteraction(){
	    if(other && interT.state == 1){
	    
	    if (other.CompareTag("Quit")){Application.Quit();}

            if (!inHandFE && !inHandO && !inHandDish)
            {
            	if (other.CompareTag("PastaFridge") || other.CompareTag("MeatFridge") || other.CompareTag("VegetableFridge") )
            	{
                    if (inHand != null)
                    {
                    	Destroy(inHand.gameObject);
                    	inHand = null;
                    	Debug.Log("Left");
		            }
                    Fridge fridge = other.gameObject.GetComponent<Fridge>();
                    inHand = fridge.spawnIngredient().GetComponent<FoodObject>();
                    //Destroy(gameObject);
                    Debug.Log("Grabbed");
            	}
            	else if (other.CompareTag("Oven"))
            	{
                	OvenFire fire = other.gameObject.GetComponent<OvenFire>();
                	if (inHand)
                	{
                    		if (inHand.state == 0 && !fire.inOven)
                    		{
                        		fire.inOven = inHand;
                        		fire.inOven.transform.position = fire.transform.position + new Vector3(0, 15, 0);
                        		inHand = null;

                        		Debug.Log("Cooking", other);
                        		fire.startCooking();
                    		}
                	}
                	else if (fire.inOven)
                	{
                    	if (fire.inOven.state > 0 && !fire.fire.isPlaying)
                    	{
                       		inHand = fire.inOven;
                        	fire.inOven = null;
                        	Destroy(fire.timer.gameObject);

                        	// Removing smoke
                            fire.smoke.gameObject.GetComponent<ParticleSystem>().Stop();
                        }
                    }
                }
                else if (other.CompareTag("Counter") && inHand)
                {
                    CounterSpace counter = other.gameObject.GetComponent<CounterSpace>();
		    FoodObject aux = null;
                    if (counter.components[inHand.type]){aux = counter.components[inHand.type];}
		    counter.components[inHand.type] = inHand;
                    counter.components[inHand.type].transform.position = counter.transform.position + new Vector3(inHand.type*7-7, 15, 0);
                    if (aux){inHand = aux;}
		    else{inHand = null;}
                }

                else if (other.CompareTag("FireExtinguisher"))
                {
                    if (inHand != null)
                    {
                        Destroy(inHand.gameObject);
                        inHand = null;
                    }
                    inHandFE = other.gameObject.GetComponent<FireExtinguisher>();
                    Debug.Log("Grabbed FE");
                    Debug.Log(inHandFE);
                }
	    
	        else if (other.CompareTag("OrderSpace"))
                {
		    OrderHolder holder = other.gameObject.GetComponent<OrderHolder>();
		    if(holder.order){
                	    if (inHand != null)
                	    {
                    		Destroy(inHand.gameObject);
                    		inHand = null;
                	    }
                    	inHandO = holder.order;
			Destroy(inHandO.timer.gameObject);
		    	holder.order = null;
			inHandO.timer = null;	
		    }
                }

                else if (other.CompareTag("Player1"))
                {
                    Debug.Log("PLAYER INTERACTION");

                    PlayerBehaviour otherPlayer = other.gameObject.GetComponent<PlayerBehaviour>();
                    if (otherPlayer.inHand && otherPlayer.inHand.state == 2)
                    {
                        otherPlayer.inHand.state = 1;
                        
                    }
                    if (this.inHand && this.inHand.state == 2)
                    {
                        this.inHand.state = 1;
                    }
		    otherPlayer.healingRing.Stop();
                    healingRing.Stop();
	            SoundManager.Instance.PlayOrderGood(); //"Positive" sound
                    // Exchange of aliments
                    FoodObject temp = inHand;

                    this.inHand = otherPlayer.inHand;
                    otherPlayer.inHand = temp;
		    if(this.inHand){this.inHand.bonus+=1;}
                    if(otherPlayer.inHand){otherPlayer.inHand.bonus+=1;}

                }
                else if (other.CompareTag("Trash"))
                {
                    if (inHand != null)
                        {
                        	Destroy(inHand.gameObject);
                        	inHand = null;
				SoundManager.Instance.PlayOrderBad(); //"Negative" sound
                        }
			
                }
            } 
	    else if (inHandO)
            {
	        if (other.CompareTag("Counter"))
                {
                    CounterSpace counter = other.gameObject.GetComponent<CounterSpace>();
                    FoodObject[] components = counter.components;
                    string dishName = "dish";
                    if(components[0])
                    {
                        dishName += "1";
                    } 
                    else
                    {
                        dishName += "0";
                    }

                    if (components[1])
                    {
                        dishName += "1";
                    }
                    else
                    {
                        dishName += "0";
                    }

                    if (components[2])
                    {
                        dishName += "1";
                    }
                    else
                    {
                        dishName += "0";
                    }

                    int reward = inHandO.check(counter);

                    Destroy(inHandO.gameObject);
                    inHandO = null;

                    inHandDish = counter.generateDish(dishName, reward).GetComponent<Dish>();
                    
                    for (int i = 0; i < 3; i++)
                    {
                        if (counter.components[i])
                        {
                            Destroy(counter.components[i].gameObject);
                            counter.components[i] = null;
                        }
                    }
		    //ServingSpace serving_space = GameObject.FindWithTag("ServeryCounter").GetComponent<ServingSpace>();
		    //serving_space.active_orders -= 1;
                    //Debug.Log("Dish completed");
		    sparkles.Play();
                }
		/*else if (other.CompareTag("OrderSpace"))
                {
	        	OrderHolder holder = other.gameObject.GetComponent<OrderHolder>();
		        if(!holder.order){
                	    holder.order = inHandO;
			    holder.order.transform.position = holder.transform.position;
			    inHandO = null;
			}
                }*/
            }
            else if (inHandDish)
            {
                if (other.CompareTag("ServeryCounter"))
                {
		    ServingSpace serving_space = GameObject.FindWithTag("ServeryCounter").GetComponent<ServingSpace>();
		    serving_space.active_orders -= 1;
		    serving_space.accept_new = false;
		    serving_space.last_order_t = Time.time;

                    GameObject totalcash = GameObject.FindWithTag("TotalCash");
                    totalcash.GetComponent<Points>().add(inHandDish.reward);
			if (inHandDish.reward>10){SoundManager.Instance.PlayOrderVeryGood();serving_space.makeGlow(0);}
			else if (inHandDish.reward>=5){SoundManager.Instance.PlayOrderGood();serving_space.makeGlow(0);}
			else if (inHandDish.reward>0){SoundManager.Instance.PlayOrderGood();serving_space.makeGlow(1);}
			else {SoundManager.Instance.PlayOrderBad(); serving_space.makeGlow(2);}
                    
                    Destroy(inHandDish.gameObject);
                    inHandDish = null;
		    
		    sparkles.Play();
                }
		//else{Debug.Log("not found counter");}
            }
            else if (inHandFE)
            {
                if (other.CompareTag("Oven"))
                {
                    OvenFire fire = other.gameObject.GetComponent<OvenFire>();

                    if (fire.inOven && fire.inOven.state == 2)
                    {
                         inHand = fire.inOven;
                         fire.inOven = null;
                         Destroy(fire.timer.gameObject);

                         // Removing smoke
                         fire.smoke.gameObject.GetComponent<ParticleSystem>().Stop();
                         Destroy(inHandFE.gameObject);
                         inHandFE = null;
                         fire.stopFire();
                    }
                }
            }

            interT.state=-1;
	    }
    }
}