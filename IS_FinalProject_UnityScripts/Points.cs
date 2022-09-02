using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    public int total;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void add(int amount){
	total+=amount;
	gameObject.GetComponent<Text>().text = "Total $$$: "+total.ToString();
    }
}
