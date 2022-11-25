using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Firebase.Database;
using Firebase.Auth;
using System;

public class FirebaseDbManager : MonoBehaviour
{
	DatabaseReference reference;
	
    // Start is called before the first frame update
    void Start()
    {
        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    
    public void SetGlobalBest(int newValue)
    {
    	Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
	
	auth.SignInAnonymouslyAsync().ContinueWith(task => {
	  if (task.IsCanceled) {
	    Debug.LogError("SignInAnonymouslyAsync was canceled.");
	    return;
	  }
	  if (task.IsFaulted) {
	    Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
	    return;
	  }

	  Firebase.Auth.FirebaseUser newUser = task.Result;
	  Debug.LogFormat("User signed in successfully: {0} ({1})",
	      newUser.DisplayName, newUser.UserId);
	      
	      reference.Child("globalbest").SetValueAsync(237189 - newValue);
	});    	
    }

    public IEnumerator GetGlobalBest(Action<int> onCallback)
    {
    	bool isOnline = false;
	
	yield return StartCoroutine(CheckInternetConnection(isConnected =>
	    {
		if (isConnected)
		{
		    isOnline = true;
		}
		else
		{
		    Debug.Log("Internet Not Available");
		    isOnline = false;
		}
	    }));
	    
	if (isOnline)
	{
		var globalbest = reference.Child("globalbest").GetValueAsync();
			yield return new WaitUntil(predicate: () => globalbest.IsCompleted);
					
			if (globalbest != null)
			{
				DataSnapshot snapshot = globalbest.Result;
				
				onCallback.Invoke(237189 - int.Parse(snapshot.Value.ToString()));
			}
	}
	else 
	{
		yield return new WaitForSeconds(0.1F);
    		onCallback.Invoke(-1);   
	}
    }
    
    IEnumerator CheckInternetConnection(Action<bool> action)
	{
	    UnityWebRequest request = new UnityWebRequest("https://www.google.com");
	    request.timeout = 1;
	    yield return request.SendWebRequest();
	    if (request.error != null) {
		Debug.Log ("Error");
		action (false);
	    } else{
		Debug.Log ("Success");
		action (true);
	    }
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
