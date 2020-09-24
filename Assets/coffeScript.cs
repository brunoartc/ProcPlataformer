using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coffeScript : MonoBehaviour
{
    public AudioClip _getCoffee;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<AudioSource>().clip = _getCoffee;
;
        collision.gameObject.GetComponent<AudioSource>().Play();
        collision.gameObject.GetComponent<PlayerController>().RefreshSpeed();
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
