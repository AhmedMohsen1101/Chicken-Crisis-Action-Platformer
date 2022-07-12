using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage { get; private set; }
    public ParticleSystem explode;
    public float LifeTime = 1;
    public float force = 10;
    public float speed = 10;
    private bool isFired;
    private float duration;
    private Vector3 direction;
    private Rigidbody getRigidbody;

    private void Update()
    {
        if (!isFired)
            return;
        duration += Time.deltaTime;
        //transform.Translate(direction * Time.deltaTime * speed, Space.Self);
        if (duration >= LifeTime)
            StartCoroutine(Disable());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isFired)
            return;
        StopAllCoroutines();
        StartCoroutine(Disable());
    }

    public void SetBulletInfo(float damage, Transform spawnPoint)
    {
        duration = 0;
        this.damage = damage;
        isFired = true;
        direction = spawnPoint.forward;
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        if(getRigidbody == null)
            getRigidbody = gameObject.GetComponent<Rigidbody>();
        getRigidbody.velocity = Vector3.zero;
        gameObject.SetActive(true);
        getRigidbody.AddForce(transform.forward * force, ForceMode.Force);
    }

    private IEnumerator Disable()
    {
        isFired = false;
        if (explode != null)
            explode.Play();
        yield return new WaitForSecondsRealtime(explode == null ? 0 : explode.time);
        gameObject.SetActive(false);
    }
}
