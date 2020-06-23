using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalButton : MonoBehaviour
{
    public List<GameObject> activatedObjects;
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
        if (collision.gameObject.tag == "Interactable" || collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Interactable>().maxSize = 2;
            if (this.GetComponent<AudioSource>())
            {
                this.GetComponent<AudioSource>().Play();
            }
            //activatedObject.GetComponent<GoalInteractable>().ActivateGoal();
            foreach(GameObject activated in activatedObjects){

            activated.GetComponent<Animator>().SetBool("Activated", true);
            }
        }
    }
}
