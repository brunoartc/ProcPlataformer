using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addPoint : MonoBehaviour
{

    public AudioClip _getCoin;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<AudioSource>().clip = _getCoin;
        collision.gameObject.GetComponent<AudioSource>().Play();
        collision.gameObject.GetComponent<PlayerController>().AddPoint();
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
