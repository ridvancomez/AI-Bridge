using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float speed;
    private float horizontal;
    private float vertical;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;

    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Horizontal");
        horizontal = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontal * speed * Time.deltaTime, 0, vertical * -speed * Time.deltaTime));



    }


}
