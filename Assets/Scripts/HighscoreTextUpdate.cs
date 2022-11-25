using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTextUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    	// Migrate from old to new save
    	if (PlayerPrefs.GetInt("Highscore", 0) != 0)
    	{
    		PlayerPrefs.SetInt("HighscoreEnc", PlayerPrefs.GetInt("Highscore", 0) - 893729);
    		PlayerPrefs.SetInt("Highscore", 0);
    	}
    
        int m_highscore = PlayerPrefs.GetInt("HighscoreEnc", -893729);
        GetComponent<Text>().text = "HIGHSCORE: " + (m_highscore + 893729).ToString();
    }
	
    // Update is called once per frame
    void Update()
    {
        
    }
}
