using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneButtonScript : MonoBehaviour {
	public string sceneName;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void ButtonSceneSelect(){
		SceneManager.LoadScene (sceneName);
	}


}
