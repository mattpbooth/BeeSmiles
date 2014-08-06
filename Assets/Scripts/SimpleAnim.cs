using UnityEngine;
using System.Collections;

public class SimpleAnim : MonoBehaviour {

	public Sprite[] _sprites;
	public float _delayBetweenPlays = 0.0f;
	public float _speed = 0.33f;
	 
	private enum PlayState
	{
		Playing,
		Waiting
	}
	private int _currentFrame = 0;
	private float _lastPlayed = 0.0f;
	private float _currentPlay = 0.0f;
	private PlayState _playState = PlayState.Waiting;
	private SpriteRenderer _spriteRenderer;

	// Use this for initialization
	void Start () {
		_spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		switch (_playState) {
			case PlayState.Waiting:
				_lastPlayed += Time.deltaTime;			
				if (_lastPlayed >= _delayBetweenPlays) {
					_playState = PlayState.Playing;
					_lastPlayed = 0.0f;
				}
				break;

			case PlayState.Playing:			
				_currentPlay += Time.deltaTime;
				if (_currentPlay >= _speed) {
					++_currentFrame;
					if(_currentFrame >= _sprites.Length)
					{
						_playState = PlayState.Waiting; 
						_currentFrame = 0;
					}
					
					_spriteRenderer.sprite = _sprites[_currentFrame];
					_currentPlay = 0.0f;
				}
				break;
		}
	}
}
