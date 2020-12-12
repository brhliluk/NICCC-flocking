using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject Streak;
    public float spawnPeriod;
    public Light origin;

    private  GameObject[]  targets;

    IEnumerator Spawn(GameObject targetObject) {
        var target = targetObject.GetComponent<Light>();
        while (true) {
            
            GameObject sphere = Instantiate(Streak, origin.transform.position, Quaternion.identity) as GameObject;
            sphere.GetComponent<Cannonball>().Shoot(target, Random.Range(30.0f, 70.0f));
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
            if (origin.name == target.name)
            {
                continue;
            }
            StartCoroutine(Spawn(targetObject));
        }
    }


    void Update()
    {

    }
}
