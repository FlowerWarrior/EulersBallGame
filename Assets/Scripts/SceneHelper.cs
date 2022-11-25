using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper : MonoBehaviour
{
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {	
    	Debug.Log("scene loaded got");
        GameObject[] objs = GameObject.FindGameObjectsWithTag("AdManager");
        objs[0].GetComponent<InterestialAd>().SceneLoaded();
    }
}
