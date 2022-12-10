using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
    public GameObject enemy;
    public System.Action killed;
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            this.gameObject.SetActive(false);
            this.killed.Invoke();
            GameManager.nbEnemy--;
            GameManager.playGame = true;
        }

    }
}
