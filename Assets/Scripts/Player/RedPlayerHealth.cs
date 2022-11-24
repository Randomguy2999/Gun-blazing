using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RedPlayerHealth : MonoBehaviour
{
    [SerializeField] private int _MaxHealth = 10;
    [SerializeField] private int _CurrentHealth = 10;
    [SerializeField] private TMP_Text _healthText;
        
    private void Update()
    {
        _healthText.text = $"Health: {_CurrentHealth }" ;
    }

    private void OnTriggerEnter(Collider other)
    {
        BlueBlast blue = other.gameObject.GetComponent<BlueBlast>();

        HealthPack health = other.gameObject.GetComponent<HealthPack>();

        if (blue != null)
        {
            _CurrentHealth -= 2;

            Debug.Log("Hit");

            if (_CurrentHealth <= 0)
            {
                Destroy(gameObject);
            }

        }
        else if (health != null)
        {
            Debug.Log("Heal");
            _CurrentHealth += 3;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        DeathZone deathZone = col.gameObject.GetComponent<DeathZone>();

        HazardDamage hazard = col.gameObject.GetComponent<HazardDamage>();
                
        if (deathZone != null)
        {
            _CurrentHealth = 0;

            if (_CurrentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
        else if (hazard != null)
        {
            _CurrentHealth--;

            if (_CurrentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}