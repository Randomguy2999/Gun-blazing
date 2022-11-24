using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBlast : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 1f;
    [SerializeField] private float _speed = 0f;
    [SerializeField] private float _spawnTime = 0f;
    AudioSource audioData;

    private void OnEnable()
    {
        audioData = GetComponent<AudioSource>();
        _spawnTime = Time.time;
        audioData.Play(); //Plays audio
    }
    private void Update()
    {
        transform.position += transform.forward * (_speed * Time.deltaTime);

        if (Time.time > _spawnTime + _lifeTime)
        {
            Destroy(gameObject);
        }

    }
}
