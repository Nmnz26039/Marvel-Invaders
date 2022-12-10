using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public Projectile laserPrefab;
    private bool _laserActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        Vector3 position = this.transform.position;
        if (GameManager.lives > 0)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.position += Vector3.left * this.speed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.position += Vector3.right * this.speed * Time.deltaTime;
            }
            if(this.transform.position.x >= (rightEdge.x - 1.0f))
            {
                position.x -= 0.1f; 
                this.transform.position = position;
            }
            else if(this.transform.position.x <= (leftEdge.x + 1.0f))
            {
                position.x += 0.1f; 
                this.transform.position = position;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        } 
        if (GameManager.nbEnemy == 0)
        {
            this.transform.position += Vector3.up * this.speed * Time.deltaTime;
        }
    }
    private void Shoot()
    {
        if (!_laserActive)
        {
            Projectile projectile = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
            projectile.destroyed += LaserDestroyed;
            _laserActive = true;
        }
    }
    private void LaserDestroyed()
    {
        _laserActive = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy")||
            collision.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            GameManager.playGame = false;
            GameManager.lives--;
            //if(GameManager.lives == 0)
            //{
            //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //}
        }
    }

}
