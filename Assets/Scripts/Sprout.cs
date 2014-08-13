using UnityEngine;
using System.Collections;

public class Sprout : MonoBehaviour {

    public float _timeToSprout = 1.0f;
    
    Transform _transform;
    float _timeTaken = 0.0f;

	// Use this for initialization
	void Start () {
        _transform = GetComponent<Transform>();
        _transform.localScale = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () 
    {
        _timeTaken += Time.deltaTime;
        _timeTaken = Mathf.Min(_timeTaken, _timeToSprout);
        _transform.localScale = Vector3.one * (_timeTaken / _timeToSprout);
	}
}
