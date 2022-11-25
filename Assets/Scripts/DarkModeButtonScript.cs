using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkModeButtonScript : MonoBehaviour
{
	public GameManager gameMgr;
	public Image pauseButtonImg;
	
	public Sprite sunImg;
	public Sprite moonImg;
	
	public Text[] texts;

	[SerializeField] ParticleSystem coinParticles;
	
    // Start is called before the first frame update
    void Start()
    {
        UpdateUIColors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Clicked()
    {
	bool darkMode = PlayerPrefs.GetInt("darkMode", 0) == 1;   
	
	if (darkMode)
	{
		PlayerPrefs.SetInt("darkMode", 0);
	}
	else
	{	
		PlayerPrefs.SetInt("darkMode", 1);
	}
	
	UpdateUIColors();	
    }
    
    void UpdateUIColors()
    {
    	bool darkMode = (PlayerPrefs.GetInt("darkMode", 0) == 1);

    	Color colCoinP = new Color32(0, 0, 0, 0);
    	if (darkMode)
		{
			GetComponent<Button>().image.sprite = moonImg;
			ColorUtility.TryParseHtmlString("#F3C85B", out colCoinP);
			
		}
		else
		{	
			GetComponent<Button>().image.sprite = sunImg;
			ColorUtility.TryParseHtmlString("#FFD53B", out colCoinP);
		}

		var main = coinParticles.main;
		main.startColor = colCoinP;
	
	gameMgr.darkMode = darkMode;
	gameMgr.UpdateGameColors();
		
        foreach (Text txt in texts)
        {
        	if (txt != null)
        	{
        		Color col = new Color32(0, 0, 0, 0);
        		if (darkMode)
        		{
        			ColorUtility.TryParseHtmlString("#ECECEC", out col);
        		}
        		else 
        		{
        			ColorUtility.TryParseHtmlString("#3D3D3D", out col);
        		}
        		
        		col.a = txt.color.a;
        		txt.color = col;
        	}
        }
    	
    	// Button color
    	Color col2 = new Color32(0, 0, 0, 0);
        if (darkMode)
        {
        	ColorUtility.TryParseHtmlString("#ECECEC", out col2);
        }
        else 
        {
        	ColorUtility.TryParseHtmlString("#3D3D3D", out col2);
        }
        
        // Dark mode Button color
        col2.a = GetComponent<Image>().color.a;
        GetComponent<Image>().color = col2;
        
        // Pause Button color        		
        col2.a = pauseButtonImg.color.a;
        pauseButtonImg.color = col2;
    }
}
