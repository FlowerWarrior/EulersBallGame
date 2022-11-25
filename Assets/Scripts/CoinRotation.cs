using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    public float rotSpeed;

    void Update()
    {
        transform.Rotate(0, Time.deltaTime * rotSpeed, 0, Space.World);
    }
    
    private void OnTriggerEnter(Collider other)
    {
    	if (other.gameObject.tag == "Ball")
	{
		gameObject.SetActive(false);
        	transform.parent.gameObject.GetComponent<GameManager>().CoinCollected();
	}
    	
    }
    
}
