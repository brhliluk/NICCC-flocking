﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public float lifetime = 5.0f;

    public void SetLight(Light origin, Light target)
    {
        ParticleSystem ps = this.transform.GetChild(0).GetComponent<ParticleSystem>();
        var col = ps.colorOverLifetime;
        col.enabled = true;

        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] {
                            new GradientColorKey(target.color, 0.0f),
                            new GradientColorKey(origin.color, 45.6f/100.0f),
                            new GradientColorKey(origin.color, 1.0f) },
            new GradientAlphaKey[] {
                            new GradientAlphaKey(240.0f/255.0f, 0.0f),
                            new GradientAlphaKey(180.0f/255.0f, 68.5f/100.0f),
                            new GradientAlphaKey(0.0f, 1.0f) });
        col.color = grad;
    }

    public void Shoot(Light target_GO, float initialAngle) {
        var rigid = GetComponent<Rigidbody>();
        var dir = target_GO.transform.position - transform.position;  // get target direction
        var h = dir.y;  // get height difference
        dir.y = 0;  // retain only the horizontal direction
        var dist = dir.magnitude;  // get horizontal distance
        var a = initialAngle * Mathf.Deg2Rad;  // convert angle to radians
        dir.y = dist * Mathf.Tan(a);  // set dir to the elevation angle
        dist += h / Mathf.Tan(a);  // correct for small height differences
                                   // calculate the velocity magnitude
        var vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        Vector3 finalVelocity = vel * dir.normalized;

        // Fire!
        rigid.velocity = finalVelocity;

        // Alternative way:
        // rigid.AddForce(finalVelocity * rigid.mass, ForceMode.Impulse);

        rigid.useGravity = true;
        Destroy(this.gameObject, lifetime);
    }

    void Start()
    {
    }


}
