using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalBestUpdateScript : MonoBehaviour
{
	 public FirebaseDbManager dbMgr;
	
    // Start is called before the first frame update
    void Start()
    {
    	StartCoroutine(dbMgr.GetGlobalBest((int score) => 
	{
		if (score != -1)
	    	{
	    		GetComponent<Text>().text = "GLOBAL BEST: " + score.ToString();	
	    	}
	    	else
	    	{
	    		GetComponent<Text>().text = "OFFLINE";	
	    	}
	}));   	
        
        
    }

}
