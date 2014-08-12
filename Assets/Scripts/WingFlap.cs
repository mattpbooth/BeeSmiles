using UnityEngine;
using System.Collections;

public class WingFlap : MonoBehaviour {

	public float _maxRotation = Mathf.PI / 4.0f;
	public float _minRotation = -Mathf.PI / 4.0f;
    public float _speed = 1.0f;

    private float _angle = 0.0f;
	private float _direction = 1.0f;
	private Transform _transform;

	// Use this for initialization
	void Start () {
		_transform = GetComponent<Transform>() as Transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Should probably slerp this
        _angle += _speed * _direction * Time.deltaTime;
        if (_angle >= _maxRotation || _angle <= _minRotation)
        {
            _direction = -_direction;
        }
        _angle = Mathf.Clamp(_angle, _minRotation, _maxRotation);
        _transform.Rotate(Vector3.forward, _angle);
	}
}
