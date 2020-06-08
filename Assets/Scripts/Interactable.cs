using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float maxSize = 5;
    public float minSize = 0.5f;
    public float sizeVelocity = 0.01f;
    public bool affectMass = false;
    public float massVelocity = 0.01f;
    public bool onlyGrowY = false;
    // Start is called before the first frame update

    public float growthVelocity;
    public float shrinkVelocity;
    private float massGrowthVelocity;
    private float massShrinkVelocity;
    void Start()
    {
        massGrowthVelocity = 1 + massVelocity;
        massShrinkVelocity = 1 - massVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Grow()
    {
        if (transform.localScale.y < maxSize) {
            if (onlyGrowY)
            {
                transform.localScale = transform.localScale + new Vector3(0, sizeVelocity * Time.deltaTime, 0);
            }
            else
            {
                transform.localScale = transform.localScale + new Vector3(sizeVelocity * Time.deltaTime, sizeVelocity * Time.deltaTime, 0) ;
            }
        }
        if (affectMass)
        {
            this.GetComponent<Rigidbody2D>().mass = this.GetComponent<Rigidbody2D>().mass + (massGrowthVelocity * Time.deltaTime);
        }
    }

    public void Shrink()
    {
        if (transform.localScale.y > minSize)
        {
            transform.localScale = transform.localScale - new Vector3(sizeVelocity * Time.deltaTime, sizeVelocity * Time.deltaTime, 0);
        }
        if (affectMass)
        {
            this.GetComponent<Rigidbody2D>().mass = this.GetComponent<Rigidbody2D>().mass + (massShrinkVelocity * Time.deltaTime);
        }
    }
}
