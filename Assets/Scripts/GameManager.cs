using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
	private int coins = 0;
	private bool isCoinActive = true;
	private bool isLoaded = false;
	public bool isGame = false;
	
	public FirebaseDbManager dbMgr;
	public GameObject menuCanvas;
	public GameObject gameplayCanvas;
	public GameObject tutorialCanvas;
	
	public bool darkMode = false;
	
	public GameObject platformObj;
	public GameObject ballObj;
	
	public GameObject gameOverText;
	public Text scoreText;
	public Rigidbody ballRb;
	public Text highscoreText;
	
	public Camera m_Camera;
	
	public Material[] ballMats;
	public Material[] platformMats;
	
	private int lastColChange = 5;
	ParticleSystem coinParticles;

	[SerializeField] ParticleSystem smashParticles;
	
	void Awake()
	{    	
		darkMode = PlayerPrefs.GetInt("darkMode", 0) != 0;
		UpdateGameColors();
		ballRb.isKinematic = true;
		
		EncryptScript.Initialize();
		coins = EncryptScript.Obfuscate(0);
	}
	
	IEnumerator Start()
	    {   	
	    	// Smooth framerate
	    	Application.targetFrameRate = Screen.currentResolution.refreshRate;
	    
	    	// Restore time flow
	    	Time.timeScale = 1;

			coinParticles = transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
	    
			Debug.Log("Showing splash screen");
			//SplashScreen.Begin();
			while (!SplashScreen.isFinished)
			{
				SplashScreen.Draw();
				yield return null;
			}
			Debug.Log("Finished showing splash screen");
			
			isLoaded = true;
	    }

	

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartGame()
    {
    	if (!isGame && isLoaded)
        {
        	// Gameplay
        	if (PlayerPrefs.GetInt("completedTutorial", 0) == 1)
        	{
        		isGame = true;
				menuCanvas.SetActive(false);
				tutorialCanvas.SetActive(false);
				gameplayCanvas.SetActive(true);
				ballRb.isKinematic = false;
				platformObj.GetComponent<PlatformController>().isGame = true;
				
			RespawnCoin();
        	}
        	
        	// Tutorial Canvas
        	else
        	{
        		menuCanvas.SetActive(false);
        		tutorialCanvas.SetActive(true);
        	}
        }
    }
    
    public void TutorialSkip()
    {
    	PlayerPrefs.SetInt("completedTutorial", 1);
        StartGame();
    }
    
    public void CoinCollected()
    {
    	coins = EncryptScript.Obfuscate(EncryptScript.Deobfuscate(coins) + 1);
    	
    	scoreText.text = EncryptScript.Deobfuscate(coins).ToString();
    	
    	if (EncryptScript.Deobfuscate(coins) > PlayerPrefs.GetInt("HighscoreEnc", -893729) + 893729)
    	{
    		highscoreText.text = "HIGHSCORE: " + EncryptScript.Deobfuscate(coins).ToString();
    	}

		// Particles
		transform.GetChild(1).position = transform.GetChild(0).position + new Vector3(0, 0, 1.1F);
    	coinParticles.Play();
    	
    	isCoinActive = false;
    	Debug.Log("Collected coin");
    	
    	if (platformObj.transform.localScale.x > 3F)
    	{
    		platformObj.transform.localScale -= new Vector3(0.1F, 0, 0);
    		platformObj.GetComponent<PlatformController>().torque -= 3.75F;
    	}
    	
    	UpdateGameColors();
    }
    
    public void UpdateGameColors()
    {
    	// Adjust colors
    	Color m_Color = new Color32(0, 0, 0, 0);
    	if (EncryptScript.Deobfuscate(coins) < 5)
    	{
    		ColorUtility.TryParseHtmlString("#FFEFCC", out m_Color);
    		ballObj.GetComponent<MeshRenderer>().material = ballMats[0];
    		platformObj.GetComponent<MeshRenderer>().material = platformMats[0];
    	}
    	else if (EncryptScript.Deobfuscate(coins) >= 5 && EncryptScript.Deobfuscate(coins) < 10)
    	{
    		ColorUtility.TryParseHtmlString("#BFFFBD", out m_Color);
    		ballObj.GetComponent<MeshRenderer>().material = ballMats[1];
    		platformObj.GetComponent<MeshRenderer>().material = platformMats[1];
    	}
    	else if (EncryptScript.Deobfuscate(coins) >= 10 && EncryptScript.Deobfuscate(coins) < 15)
    	{
    		ColorUtility.TryParseHtmlString("#BDF3FF", out m_Color);
    		ballObj.GetComponent<MeshRenderer>().material = ballMats[2];
    		platformObj.GetComponent<MeshRenderer>().material = platformMats[2];
    	}
    	else if (EncryptScript.Deobfuscate(coins) >= 15 && EncryptScript.Deobfuscate(coins) < 20)
    	{
    		ColorUtility.TryParseHtmlString("#F1BDFF", out m_Color);
    		ballObj.GetComponent<MeshRenderer>().material = ballMats[3];
    		platformObj.GetComponent<MeshRenderer>().material = platformMats[3];
    	}
    	else if (EncryptScript.Deobfuscate(coins) >= 20 && EncryptScript.Deobfuscate(coins) < 25)
    	{
    		ColorUtility.TryParseHtmlString("#FFBDC2", out m_Color);
    		ballObj.GetComponent<MeshRenderer>().material = ballMats[4];
    		platformObj.GetComponent<MeshRenderer>().material = platformMats[4];
    	}
    	else if (EncryptScript.Deobfuscate(coins) >= 25 && EncryptScript.Deobfuscate(coins) < 30)
    	{
    		ColorUtility.TryParseHtmlString("#DAFFFF", out m_Color);
    		ballObj.GetComponent<MeshRenderer>().material = ballMats[5];
    		platformObj.GetComponent<MeshRenderer>().material = platformMats[5];
    	}
    	else if (EncryptScript.Deobfuscate(coins) >= 30 && EncryptScript.Deobfuscate(coins) < 35)
    	{
    		ColorUtility.TryParseHtmlString("#D1FFCB", out m_Color);
    		ballObj.GetComponent<MeshRenderer>().material = ballMats[6];
    		platformObj.GetComponent<MeshRenderer>().material = platformMats[6];
    	}
    	else if (EncryptScript.Deobfuscate(coins) >= 35 && EncryptScript.Deobfuscate(coins) < 40)
    	{
    		ColorUtility.TryParseHtmlString("#FFFFFF", out m_Color);
    		ballObj.GetComponent<MeshRenderer>().material = ballMats[7];
    		platformObj.GetComponent<MeshRenderer>().material = platformMats[7];
    	}
    	else if (EncryptScript.Deobfuscate(coins) >= 40 && EncryptScript.Deobfuscate(coins) < 45)
    	{
    		ColorUtility.TryParseHtmlString("#FFFFFF", out m_Color);
    		ballObj.GetComponent<MeshRenderer>().material = ballMats[8];
    		platformObj.GetComponent<MeshRenderer>().material = platformMats[8];
    	}
    	else if (EncryptScript.Deobfuscate(coins) >= 45  && EncryptScript.Deobfuscate(coins) < 50)
    	{
    		ColorUtility.TryParseHtmlString("#FFFFFF", out m_Color);
    		ballObj.GetComponent<MeshRenderer>().material = ballMats[9];
    		platformObj.GetComponent<MeshRenderer>().material = platformMats[9];
    	}
    	else if (EncryptScript.Deobfuscate(coins) >= 50)
    	{
    		ColorUtility.TryParseHtmlString("#FFFFFF", out m_Color);
    	
    		if (lastColChange >= 5)
    		{
    			System.Random rnd = new System.Random(EncryptScript.Deobfuscate(coins) * 3);
    			int index1 = rnd.Next(0, 10);
    			int index2 = rnd.Next(0, 10);
    			Debug.Log(index1);
	    		ballObj.GetComponent<MeshRenderer>().material = ballMats[index1];
	    		platformObj.GetComponent<MeshRenderer>().material = ballMats[index2];
	    		lastColChange = 1;
    		}
    		else
    		{
    			lastColChange++;
    		}
    		
    		
    	}
    	
    	
    	if (darkMode)
    	{
    		//ColorUtility.TryParseHtmlString("#202020", out m_Color);
    		
    		m_Color *= 0.1F;
    	}
    	
		MeshRenderer meshRenderer = platformObj.GetComponent<MeshRenderer>();
	
		var main = smashParticles.main;
        main.startColor = meshRenderer.material.GetColor("_Color");

    	m_Camera.backgroundColor = m_Color;
    }
    
    public void RespawnCoin()
    {
    	isCoinActive = true;
    	float y = Random.Range(6F, 8F - (EncryptScript.Deobfuscate(coins) * 2F/40F));
    	float x = Random.Range(-2.5F + (EncryptScript.Deobfuscate(coins) * 0.05F), 2.5F - (EncryptScript.Deobfuscate(coins) * 0.05F)) ;
    	transform.GetChild(0).position = new Vector3(x, y, 0);
    	transform.GetChild(0).gameObject.SetActive(true);
    }
    
    public void BallOnPlatform()
    {
    	if (!isCoinActive)
    	{
    		RespawnCoin();
    	}
    }
    
    public void BallOutOfScreen()
    {
    	if (isGame)
    	{
    			isGame = false;
			float moreTime = 0F;
	
			if (EncryptScript.Deobfuscate(coins) > PlayerPrefs.GetInt("HighscoreEnc", -893729) + 893729)
			{	
				PlayerPrefs.SetInt("HighscoreEnc", EncryptScript.Deobfuscate(coins) - 893729);
				gameOverText.GetComponent<Text>().text = "PERSONAL BEST!";
				gameOverText.SetActive(true);
				moreTime = 2F;
			}
			
			StartCoroutine(dbMgr.GetGlobalBest((int score) => 
			{
				if (score != -1 && EncryptScript.Deobfuscate(coins) > score)
			    	{
			    		dbMgr.SetGlobalBest(EncryptScript.Deobfuscate(coins));
			    		Debug.Log("Worlds best score");
			    		gameOverText.GetComponent<Text>().text = "NEW WORLD RECORD!";
			    	}
			    	gameOverText.SetActive(true);
			    	moreTime = 2F;
			}));   	
				
			gameOverText.SetActive(true);
					
			Scene scene = SceneManager.GetActiveScene(); 
			StartCoroutine(LoadSceneAfter(3F + moreTime, scene.name));
    	}
    }
    
    IEnumerator LoadSceneAfter(float s, string n)
    {
    	yield return new WaitForSeconds(s);
        SceneManager.LoadScene(n);
    }
    
    public void RestartScene()
    {
    	Scene scene = SceneManager.GetActiveScene(); 
    	SceneManager.LoadScene(scene.name);
    }
}
