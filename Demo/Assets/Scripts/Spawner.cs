using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject Streak;
    public int spawnPeriod;
    public Light target;
    public Light origin;
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Update()
    {
        time += Time.deltaTime;

        if (time >= spawnPeriod)
        {
            time = 0.0f;

            GameObject sphere = Instantiate(Streak, origin.transform.position, Quaternion.identity) as GameObject;
            sphere.GetComponent<Cannonball>().Shoot(target, Random.Range(30.0f,70.0f));
            // execute block of code here
        }
    }
}
