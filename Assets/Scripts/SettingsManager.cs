﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SettingsManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void reset(){

		Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene (currentScene.name);
	}
}
