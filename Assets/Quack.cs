using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quack : MonoBehaviour
{
    private int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            counter++;
            if (counter == 2)
            {
                GetComponent<AudioSource>().pitch = 1.5f;
            }
            GetComponent<AudioSource>().Play();
        }
    }
}
