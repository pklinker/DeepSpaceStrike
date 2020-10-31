using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Controls enemy vehicles.
 */
public class Enemy : MonoBehaviour
{

    AudioSource audioSource;
    bool isDead;
    Collider boxCollider;
    ScoreBoard scoreBoard;

    [Header("Effects")]
    [Tooltip("Death explose sound effect")] [SerializeField] AudioClip deathExplosionAC;
    [SerializeField] GameObject deathExplosion;
    [SerializeField] Transform parent;

    [Header("Scoring")]
    [Tooltip("Points to add to the player's score")] [SerializeField] int pointValue = 10;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        AddCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddCollider()
    {
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
        scoreBoard.ScoreHit(pointValue);
        GameObject fx = Instantiate(deathExplosion, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        //        print("Particles collided with enemy " + gameObject.name);
        Destroy(gameObject);

    }
}
