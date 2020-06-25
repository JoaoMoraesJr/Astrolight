using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    private SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
