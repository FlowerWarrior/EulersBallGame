using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class InterestialAd : MonoBehaviour, IUnityAdsListener
{   
    [SerializeField] private string _gameID;
    public string pID = "Interstitial_Android";
    
    public GameManager gameMgrScript;
    private int lastAdAgo = 0;
    
    // called first
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {	
    	SceneLoaded();
    }
    
    void Awake()
    {
    	GameObject[] objs = GameObject.FindGameObjectsWithTag("AdManager");

		    if (objs.Length > 1)
		    {
		        Destroy(this.gameObject);
		    }

		    DontDestroyOnLoad(this.gameObject);
    }
    
    void Update()
    {
    	if (lastAdAgo >= 4)
    	{
    		if (Advertisement.IsReady())
    		{
    			Advertisement.Show(pID);
    			lastAdAgo = 0;
    			PlayerPrefs.SetInt("LastAdAgo", 0);
    		}
    	}
    }
    
    void Start()
    {
    	Advertisement.AddListener(this);
    	Advertisement.Initialize(_gameID, false);   	
    }
    
    void ShowAd()
	{
		
		if (PlayerPrefs.GetInt("LastAdAgo") >= 4)
    		{
    			Advertisement.Show(pID);
    			PlayerPrefs.SetInt("LastAdAgo", 0);
    		}
    		else 
    		{
    			PlayerPrefs.SetInt("LastAdAgo", PlayerPrefs.GetInt("LastAdAgo") + 1);
    		}
    	Debug.Log("ShowAd() run" + PlayerPrefs.GetInt("LastAdAgo").ToString());
		
	}
	
	public void SceneLoaded()
    {
    	lastAdAgo = PlayerPrefs.GetInt("LastAdAgo");
    	
    	if (PlayerPrefs.GetInt("LastAdAgo") < 4)
    	{
    		PlayerPrefs.SetInt("LastAdAgo", PlayerPrefs.GetInt("LastAdAgo") + 1);
    	}
    }
    
    public void OnDestroy ()
	{
		Advertisement.RemoveListener(this);
	}
    
    void IUnityAdsListener.OnUnityAdsReady(string placementId)
    {
    	Debug.Log("Ads Intialized");
    	if (placementId == pID) //&& gameMgrScript.isGame == false)
    	{	
    		Debug.Log("INTERISITAL AD READY");
    		//ShowAd();
    	}
    }
    
    void IUnityAdsListener.OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
    	switch(showResult)
    	{
    		case ShowResult.Failed:
    			Debug.LogError("Ad Errored");
    			break;
    		case ShowResult.Skipped:
    			Debug.LogError("Ad Skipped, No Reward");
    			break;
    		case ShowResult.Finished:
    			Debug.Log("Reward from Ad");
    			break;
    		default:
    			break;
    	}
    }
    
    void IUnityAdsListener.OnUnityAdsDidError(string message)
    {
    	Debug.LogError("Ad Errored");
    }
    
    void IUnityAdsListener.OnUnityAdsDidStart(string placementId)
    {
    	// Not implemented
    }
}
