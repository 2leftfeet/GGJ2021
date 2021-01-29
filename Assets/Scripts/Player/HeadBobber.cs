using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HeadBobber : MonoBehaviour
{
    public float bobbingFrequency = 15f;
    public float bobbingAmount = 0.04f;

    Rigidbody body;

    float defaultY;
    float timer;

    void Start()
    {
        defaultY = transform.localPosition.y;
        body = GetComponentInParent<Rigidbody>();
    }

    void Update()
    {
        Debug.Log(body.velocity);
        if(body.velocity.sqrMagnitude > 0.01f)
        {
            timer += Time.deltaTime * bobbingFrequency;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
        }
        else
        {
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultY, Time.deltaTime * bobbingFrequency), transform.localPosition.z);
        }
    }
}
