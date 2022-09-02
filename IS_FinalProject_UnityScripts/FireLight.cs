using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLight : MonoBehaviour
{
    public bool isFlickering = false;
    public float timeDelay;
    public bool isActive = false;
    public int active_fires = 0;
    
    // Start is called before the first frame update
    void Start()
    {
	 this.gameObject.GetComponent<Light>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active_fires > 0){
		if (!isFlickering && isActive){
            		StartCoroutine(FlickeringLight());
        	}
	}
        else{this.gameObject.GetComponent<Light>().enabled = false;}
    }

    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
