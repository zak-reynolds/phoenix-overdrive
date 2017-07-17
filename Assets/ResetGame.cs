using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour {

    private float timer = 7;
	
	void Update () {
        timer -= Time.deltaTime;
        if (timer < 0) SceneManager.LoadScene(1);
	}
}
