using UnityEngine;
using System.Collections;

public class BeeController : MonoBehaviour 
{
    public bool _useController = true;
    public float _speed = 10.0f;

    private Transform _transform;
    private bool _scaleInverted = false;
  
	// Use this for initialization
	void Start () 
    {
        _transform = transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
        var vert = Input.GetAxis("Vertical") * _speed * Time.deltaTime;
        var horiz = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;

        bool dirty = false;
        if(horiz < 0.0f && !_scaleInverted)
        {
            dirty = true;
            _scaleInverted = true;
        }
        else if(horiz > 0.0f && _scaleInverted)
        {
            dirty = true;
            _scaleInverted = false;
        }

        if (dirty)
        {
            var localScale = _transform.localScale;
            localScale.x *= -1.0f;
            _transform.localScale = localScale;
        }

        _transform.Translate(horiz, vert, 0.0f); 
	}
}
