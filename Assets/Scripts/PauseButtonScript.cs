using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonScript : MonoBehaviour
{
	private bool paused = false;
	
	private Color oldColor;
	
	public Sprite resumeImg;
	public Sprite pauseImg;
	
	public GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Clicked()
    {
    	if (!paused) 
    	{
    		paused = true;
    		GetComponent<Button>().image.sprite = resumeImg;
    		
    		oldColor = GetComponent<Button>().image.color;
    		GetComponent<Button>().image.color = new Color32(236,236,236,255);
    		pauseScreen.SetActive(true);
    		
    		Time.timeScale = 0;
    		Debug.Log("Paused");
    	}
    	else 
    	{
    		Resume();
    	}
    }
    
    public void Resume()
    {
    	pauseScreen.SetActive(false);
    	GetComponent<Button>().image.sprite = pauseImg;
    	GetComponent<Button>().image.color = oldColor;
    	paused = false;
    	Time.timeScale = 1;
    	Debug.Log("Resumed");
    }
}
