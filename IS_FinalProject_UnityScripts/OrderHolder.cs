using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderHolder : MonoBehaviour
{
    public Order order;
    public GameObject orderPrefab;
    //public int id;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	ServingSpace parentSpace = transform.parent.gameObject.GetComponent<ServingSpace>();
	if(!order && parentSpace.accept_new && parentSpace.active_orders<3){
        	//if (transform.parent.gameObject.GetComponent<ServingSpace>().active_orders==id){
			newOrder();
			parentSpace.active_orders+=1;
			parentSpace.accept_new=false;
			//parentSpace.wait_for_new=false;
			parentSpace.last_order_t = Time.time;
		//}
	}
    }

    public void newOrder()
    {
        SoundManager.Instance.PlayOrderIncoming();
        order = Instantiate(orderPrefab, transform.position, orderPrefab.transform.rotation, GameObject.FindGameObjectWithTag("Canvas").transform).GetComponent<Order>();
        order.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
    }
}
