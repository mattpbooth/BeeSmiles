using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Random.seed = (int)(Time.realtimeSinceStartup * 1000.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
