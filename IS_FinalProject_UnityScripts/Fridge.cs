using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour
{
    public GameObject foodPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject spawnIngredient()
    {
        SoundManager.Instance.PlayFridge();
        return Instantiate(foodPrefab,transform.position,foodPrefab.transform.rotation);
    }
}
