using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenFire : MonoBehaviour
{
    public FoodObject inOven;
    public Timer timer;
    public GameObject timerPrefab;
    public Vector3 offset;
    public ParticleSystem smoke;
    public GameObject smokePrefab;
    public ParticleSystem fire;
    public GameObject firePrefab;


    public int tech;

    // Start is called before the first frame update
    void Start()
    {
        smoke = Instantiate(smokePrefab, transform.position + new Vector3(0, 20, 0), smokePrefab.transform.rotation).GetComponent<ParticleSystem>();
        smoke.Stop();

        fire = Instantiate(firePrefab, transform.position + new Vector3(0, 20, 0), firePrefab.transform.rotation).GetComponent<ParticleSystem>();
        fire.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (inOven) { inOven.state = timer.state;}
        if (inOven && inOven.state == 2 && !fire.isPlaying)
        {
            // Start the fire
            fire.Play();


            
            // Show fire extinguisher
            GameObject fireExtinguisherPrefab = (GameObject)Resources.Load("Fire_Extinguisher/Prefab/fire extinguisher", typeof(GameObject));
            GameObject fireExtinguisher = Instantiate(fireExtinguisherPrefab, fireExtinguisherPrefab.transform.position, fireExtinguisherPrefab.transform.rotation).GetComponent<GameObject>();
            
            // Start flickering fire light
            FireLight fireLight = GameObject.FindGameObjectWithTag("FireLight").GetComponent<FireLight>();
            fireLight.isActive = true;

 	        fireLight.active_fires+=1;

            //Play the alarm
            if (fireLight.active_fires==1){SoundManager.Instance.StartAlarm();}
        }
    }

    public void startCooking()
    {
	    inOven.tech = tech;
        timer = Instantiate(timerPrefab, transform.position + offset, transform.rotation * Quaternion.Euler(0,180f,0), GameObject.FindGameObjectWithTag("Canvas").transform).GetComponent<Timer>();
	timer.Begin(timer.Duration);
        
        //Play sound
        switch (tech)
      {
          case 1:
              SoundManager.Instance.PlayFryer();
              break;
          case 2:
              SoundManager.Instance.PlayOven();
              break;
          default:
              SoundManager.Instance.PlayBoiler();
              break;
      }
        
        // Showing smoke
        smoke.Play();
    }

    public void stopFire()
    {
        // Stop fire
        fire.Stop();

        //Stop the alarm
        //SoundManager.Instance.StopAlarm();


        // Stop flickering fire light
        FireLight fireLight = GameObject.FindGameObjectWithTag("FireLight").GetComponent<FireLight>();
        fireLight.active_fires -=1;
	if(fireLight.active_fires==0){
		fireLight.isActive = false;
		fireLight.gameObject.GetComponent<Light>().enabled = false;
		SoundManager.Instance.StopAlarm();
	}


    }
}
