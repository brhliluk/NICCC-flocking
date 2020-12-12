using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Spawner : MonoBehaviour
{

    public GameObject Streak;
    private float spawnPeriod = 1.0f;
    public Light origin;

    // ------------------------------------

    private  GameObject[]  targets;
    string[] targetNames = {"Praha", "Cernosice", "Kralupy", "Kladno", "Ricany", "Brandys"};
    float[,] angles = {{ 0, 45,45,45, 45, 45},
                       {55, 0, 0, 0, 0, 0},
                       {55, 0, 0, 0, 0, 0},
                       {55, 0, 0, 0, 0, 0},
                       {55, 0, 0, 0, 0, 0},
                       {55, 0, 0, 0, 0, 0}};

    float[,] intensities = {{0.00f, 0.37f,0.02f,0.05f, 0.19f, 0.22f},
                            {0.97f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                            {0.09f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                            {0.28f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                            {0.32f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                            {0.65f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},

    };

    // ------------------------------------

    IEnumerator Spawn(GameObject targetObject, float angle, float intensity) {
        var target = targetObject.GetComponent<Light>();
        if (angle < 55)
        {
            yield return new WaitForSeconds(spawnPeriod/2);
        }

        while (true) {
            GameObject sphere = Instantiate(Streak, origin.transform.position, Quaternion.identity) as GameObject;
            sphere.GetComponent<Cannonball>().SetLight(intensity);
            sphere.GetComponent<Cannonball>().SetEmissions(intensity);
            sphere.GetComponent<Cannonball>().Shoot(target, angle);
            yield return new WaitForSeconds(spawnPeriod);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("UZEMI_TARGET");
        foreach (var targetObject in targets)
        {
            var target = targetObject.GetComponent<Light>();
            int i1 = Array.IndexOf(targetNames, origin.name);
            int i2 = Array.IndexOf(targetNames, target.name);
            float angle = angles[i1, i2];
            float intensity = (float) intensities[i1, i2];
            Debug.Log("Angle["+i1+","+i2+"] (" + targetNames[i1] +"," + targetNames[i2]+ ") = " + angle);


            if (origin.name != target.name && angle > 0)
            {
                StartCoroutine(Spawn(targetObject, angle, intensity));
            }
        }
    }


    void Update()
    {

    }
}
