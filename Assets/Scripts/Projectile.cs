using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public System.Action destroyed; //this is the event that we will invoke to say that the projectile collided with something
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.direction * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.destroyed != null)
        {
            this.destroyed.Invoke();
        } 
        Destroy(this.gameObject);

        switch (collision.gameObject.tag)
        {
            case "Ultron":
                GameManager.score += 10;
                break;
            case "Thanos":
                GameManager.score += 20;
                break;
            case "Redskull":
                GameManager.score += 20;
                break;
            case "Nebula":
                GameManager.score += 15; //For some reason, nebula conts double so I set her score to 15 to obtain 30
                break;
            case "Loki":
                GameManager.score += (int)Random.Range(40f,500f);
                break;
        }
    }
}
