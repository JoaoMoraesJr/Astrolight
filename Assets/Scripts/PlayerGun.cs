using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{

    public GameObject shotPrefab;
    public GameObject trailPrefab;
    public float maxRange = 5;
    public float shotVelocity = 0.5f;
    public Color growColor;
    public Color shrinkColor;
    public GameObject gunParticle;
    public AudioClip growthSound;
    public AudioClip shrinkSound;
    public AudioClip useSound;

    private GameObject currentShot;
    private List<GameObject> trails = new List<GameObject>();
    private bool isShooting = false;
    private Vector3 shotDirection;
    public float currentDistance = 1;
    private bool shotType = false; //false for shrink / true for grow
    private Color shotColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentShot != null)
        {
            createTrails();
        }
    }

    public void setShotDirection(Vector3 direction)
    {
        shotDirection = direction;
    }

    public void Shot(bool shoot1, bool shoot2)
    {
        if (shoot1) shotType = true;
        else if (shoot2) shotType = false;
        changeShotColor();

        if (!isShooting && currentShot != null)
        {
            GetComponent<AudioSource>().Stop();
            if (currentDistance <= 0.3f) {
                Destroy(currentShot.gameObject);
                gunParticle.SetActive(false);
            }
            else
            {
                Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                currentShot.transform.position = Vector2.MoveTowards(transform.position, targetPosition, currentDistance - (shotVelocity * Time.deltaTime));
                currentDistance = Vector2.Distance(currentShot.transform.position, transform.position);
                currentShot.transform.rotation = transform.rotation;
            }
        }
        if (!isShooting && (shoot1 || shoot2) && currentShot == null)
        {
            currentShot = Instantiate(shotPrefab, transform.position, transform.rotation);
        }

        isShooting = (shoot1 || shoot2) ? true : false;

        if (isShooting && currentShot != null)
        {

            gunParticle.SetActive(true);
            Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, shotDirection, currentDistance);
            if (hits != null)
            {
                bool isInteracting = false;
                foreach (RaycastHit2D hit in hits)
                {
                    //if (hit.collider.tag != "Player")
                    //{
                    //    currentDistance = Vector3.Distance(transform.position, hit.collider.transform.position);
                    //}
                    if (hit.collider.tag == "Interactable")
                    {
                        isInteracting = true;
                        currentDistance = Vector2.Distance(transform.position, hit.collider.transform.position);
                        if (shoot1 == true)
                        {
                            GetComponent<AudioSource>().clip = growthSound;
                            if (!GetComponent<AudioSource>().isPlaying)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                            hit.collider.GetComponent<Interactable>().Grow();
                        }else if (shoot2 == true)
                        {
                            GetComponent<AudioSource>().clip = shrinkSound;
                            if (!GetComponent<AudioSource>().isPlaying)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                            hit.collider.GetComponent<Interactable>().Shrink();
                        }
                    }
                }
                if (!isInteracting)
                {
                    GetComponent<AudioSource>().clip = useSound;
                    if (!GetComponent<AudioSource>().isPlaying)
                    {
                        GetComponent<AudioSource>().Play();
                    }
                }
            }

            currentShot.transform.position = Vector2.MoveTowards(transform.position, targetPosition, currentDistance + (shotVelocity * Time.deltaTime));
            currentShot.transform.rotation = transform.rotation;
            if (maxRange > currentDistance)
            {
            currentDistance = Vector2.Distance(currentShot.transform.position, transform.position);
            }
        }

    }

    public void createTrails()
    {
        float scaleSegment = Vector2.Distance(transform.position, currentShot.transform.position);
        int segmentsToCreate = Mathf.RoundToInt(Vector2.Distance(transform.position, currentShot.transform.position));
        scaleSegment = (scaleSegment - segmentsToCreate)/segmentsToCreate;
        if (segmentsToCreate == 0 && trails.Count == 1)
        {
            Destroy(trails[0]);
            trails.RemoveRange(0, 1);
        }
        if (segmentsToCreate > 0 )
        {
            if (trails.Count > segmentsToCreate)
            {
                for (int i = segmentsToCreate-1; i< ((segmentsToCreate-1) + (trails.Count - segmentsToCreate)); i++)
                {
                    Destroy(trails[i]);
                }
                trails.RemoveRange(segmentsToCreate - 1, trails.Count - segmentsToCreate);
            }
            float distance = 1f / (float)segmentsToCreate;
            float lerpValue = 0;
            Vector2 positionToInstantiate;
            for (int i = 0; i < segmentsToCreate; i++)
            {
                lerpValue += distance;
                positionToInstantiate = Vector2.Lerp(transform.position, currentShot.transform.position, lerpValue);
                if (i < trails.Count)
                {
                    trails[i].transform.position = positionToInstantiate;
                    trails[i].transform.rotation = transform.rotation;
                    trails[i].transform.localScale = new Vector2(1 + scaleSegment, 1);
                }
                else
                {
                    trails.Add(Instantiate(trailPrefab, positionToInstantiate, transform.rotation));
                }
                trails[i].GetComponent<SpriteRenderer>().color = shotColor;
            }

        }
    }

    public void changeShotColor()
    {

        if (shotType)
        {
            shotColor = growColor;
        }
        else
        {
            shotColor = shrinkColor;
        }
        if (currentShot != null)
        {
            currentShot.GetComponent<SpriteRenderer>().color = shotColor;
        }
    }
}
