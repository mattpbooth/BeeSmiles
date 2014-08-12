using UnityEngine;
using System.Collections;

public class Mouth : MonoBehaviour 
{
    public Sprite[] _mouths = new Sprite[(int)Mood.Max];
	
    SpriteRenderer _spriteRenderer;

    // Use this for initialization
	void Start () 
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
    void ChangeMood(Mood mood)
    {
        _spriteRenderer.sprite = _mouths[(int)mood];
    }
}
