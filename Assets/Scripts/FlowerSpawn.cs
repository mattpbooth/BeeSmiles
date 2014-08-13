using UnityEngine;
using System.Collections;

public class FlowerSpawn : MonoBehaviour 
{
    public GameObject _flower;
    public Transform[] _spawnPoints;

    public float _translationLeeway = 0.1f;
    public float _rotationLeeway = 20.0f;
    public float _spawnTime = 10.0f;

    private float _timeSinceSpawn = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        _timeSinceSpawn += Time.deltaTime;
        if(_timeSinceSpawn >= _spawnTime)
        {
            _timeSinceSpawn = 0;
            Spawn();
        }
	}

    void Spawn()
    {
        var random = Random.value;
        int index = (int)(random * _spawnPoints.Length);
        
        var spawnTransform = _spawnPoints[index];
        
        var translationMod = Random.Range(-_translationLeeway, _translationLeeway);
        var rotationMod = Random.Range(-_rotationLeeway, _rotationLeeway);

        float angle;
        Vector3 axis;
        spawnTransform.rotation.ToAngleAxis(out angle, out axis);       
        angle += rotationMod;

        Instantiate(_flower, spawnTransform.position + (new Vector3(1.0f, 1.0f, 0.0f) * translationMod), Quaternion.AngleAxis(angle, axis));
    }
}
