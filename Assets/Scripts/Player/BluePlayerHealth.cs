using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BluePlayerHealth : MonoBehaviour
{
    [SerializeField] private int _MaxHealth = 10000000;
    [SerializeField] private float _CurrentHealth = 10000000;
    [SerializeField] private TMP_Text _blueHealthText;
    [SerializeField] public float regeneration = 5;
    public bool isRegenHealth = false;

    AudioSource audioData;

    private void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //_blueHealthText.text = "Hit";
        
    }

    IEnumerator HitCoroutine() // Plays a hit notification in a text box
    {

        _blueHealthText.enabled = true;
        
        _blueHealthText.text = "Hit";

        yield return new WaitForSeconds(0.6f);

        _blueHealthText.enabled = false;  
    }

    IEnumerator RegenCoroutine() // Allows the player/ enemy to regenerate health after a few seconds
    {
        yield return new WaitForSeconds(2f);
        isRegenHealth = true;
        _CurrentHealth += 2;
        yield return new WaitForSeconds(4f);
        isRegenHealth = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        RedBlast red = other.gameObject.GetComponent<RedBlast>();

        HealthPack health = other.gameObject.GetComponent<HealthPack>();

        if (red != null)
        {
            _CurrentHealth -= 2;
            
            StartCoroutine(HitCoroutine());
            StartCoroutine(RegenCoroutine());                        

            Debug.Log("Hit");
            

            if (_CurrentHealth <= 0)
            {
                Destroy(gameObject);
            }

            else if (health != null)
            {
                _CurrentHealth += 3;
                audioData.Play();              

            }            
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