using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawn;

    //Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Genarate", 0, 1f);
    }

    //Update is called once per frame
    void Update()
    {

    }

    void Genarate()
    {
        Instantiate(spawn, transform.position, transform.rotation);
    }
}
