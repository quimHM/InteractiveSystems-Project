using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterSpace : MonoBehaviour
{
    //public FoodObject inPreparation;
    public FoodObject [] components;
    public GameObject dishPrefab;

    // Start is called before the first frame update
    void Start()
    {
        components = new FoodObject[3];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject generateDish(string dishName, int reward)
    {
        GameObject dish = Instantiate(dishPrefab, transform.position, dishPrefab.transform.rotation);
        Dish finishedDish = dish.GetComponent<Dish>();
        finishedDish.dishName = dishName;

        string spritePath = "sprites/" + dishName;
        finishedDish.dishSprite = (Sprite)Resources.Load(spritePath, typeof(Sprite));

        finishedDish.reward = reward;
        
        return dish;
    }
}
