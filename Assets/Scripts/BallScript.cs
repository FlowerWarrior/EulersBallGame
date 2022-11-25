using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
	public GameManager gameMgrScript;

    [SerializeField] Transform meshTransform;

    bool gameOver = false;

    // Start is called before the first frame update
    void Start()    
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -2 && !gameOver)
        {
            gameMgrScript.BallOutOfScreen();
            gameOver = true;
        }
    }
    
    void OnBecameInvisible() {
        gameMgrScript.BallOutOfScreen();
    }
}
