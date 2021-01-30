using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HeadBobber : MonoBehaviour
{
    public float bobbingFrequency = 15f;
    public float bobbingAmount = 0.04f;

    Rigidbody body;
    AudioSource walk;

    float defaultY;
    float timer;

    void Start()
    {
        defaultY = transform.localPosition.y;
        body = GetComponentInParent<Rigidbody>();
        walk = GetComponentInParent<AudioSource>();
    }

    void Update()
    {
        if(body.velocity.sqrMagnitude > 0.01f && body.velocity.y < 0.01f)
        {
            if (!walk.isPlaying)
                walk.Play();
            timer += Time.deltaTime * bobbingFrequency;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
        }
        else
        {
            walk.Stop();
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultY, Time.deltaTime * bobbingFrequency), transform.localPosition.z);
        }
    }
}
