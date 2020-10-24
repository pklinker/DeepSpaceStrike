using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    AudioSource audioSource;
    bool isDead;
    Collider boxCollider;

    [Header("Effects")]
    [SerializeField] AudioClip deathExplosionAC;
    [SerializeField] GameObject deathExplosion;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        AddNonTriggerBoxCollider();
        isDead = false;
    }
    void AddNonTriggerBoxCollider()
    {
        boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnParticleCollision(GameObject other)
    {
        isDead = true;
        Instantiate(deathExplosion, transform.position, Quaternion.identity);
        //        print("Particles collided with enemy " + gameObject.name);
        Destroy(gameObject);

    }
}
