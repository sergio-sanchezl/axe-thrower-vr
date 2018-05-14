using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitcher : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Input.backButtonLeavesApp = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeScene(string name) {
        SceneManager.LoadScene(name);
    }

    public void CloseGame() {
        Application.Quit();
    }
}
