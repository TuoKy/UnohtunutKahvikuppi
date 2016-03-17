using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

    public float speed = 10;
    public float lifeTime = 2;

    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.TransformDirection(Vector3.forward * speed);

        StartCoroutine(DestroyProjectile());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }
}
