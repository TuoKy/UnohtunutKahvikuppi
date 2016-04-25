using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

    public float speed = 10;
    public float lifeTime = 2;
    public ParticleSystem startBurstEffect;
    public ParticleSystem finishBurstEffect;

    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.TransformDirection(Vector3.forward * speed);

        /*
        if (startBurstEffect != null)
        {
            Instantiate(startBurstEffect, transform.position, transform.rotation);
        }           
        */
        StartCoroutine(DestroyProjectileCo());
    }

    IEnumerator DestroyProjectileCo()
    {
        yield return new WaitForSeconds(lifeTime);

        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        /*
        if (finishBurstEffect != null)
        {
            Instantiate(finishBurstEffect, transform.position, transform.rotation);
        }
        */
        Destroy(gameObject);
    }
}
