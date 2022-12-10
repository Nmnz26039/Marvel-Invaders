using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Invaders : MonoBehaviour
{
    public Enemy[] prefabs;
    public int rows = 5;
    public int columns = 11;
    public AnimationCurve speed; //A bit like a function with x axis indicates the percentage and y axes indicates the speed
    public Projectile missilePrefab;
    public float missileAttackRate = 1.0f; 

    public int nbEnemyKilled { get; private set; }
    public int nbEnemyAlive => this.totalEnemy - this.nbEnemyKilled;
    public int totalEnemy => this.rows * this.columns;
    public float percentKilled => (float)this.nbEnemyKilled / (float)this.totalEnemy;

    private Vector3 _direction = Vector2.right;

    private void Awake()
    {
        for(int row = 0; row<this.rows; row++)
        {
            float width = 2.0f * (this.columns - 1);
            float height = 2.0f * (this.rows - 1);
            Vector3 centering = new Vector2(-width/2,-height/2);
            Vector3 rowPOsition = new Vector3(centering.x, (row * 2.0f) + centering.y, 0.0f);
            for(int col=0; col < this.columns; col++)
            {
                Enemy enemy = Instantiate(this.prefabs[row], this.transform);
                enemy.killed += EnemyKilled;
                Vector3 position = rowPOsition;
                position.x += col * 2.0f;
                enemy.transform.position = position;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = this.transform.position;
        position.y = 16.0f; 
        this.transform.position = position;
        if ((GameManager.playGame == true) && (GameManager.lives > 0))
            InvokeRepeating(nameof(MissileAttack), this.missileAttackRate, this.missileAttackRate);
    }

    // Update is called once per frame
    void Update()
    {
        if ((GameManager.playGame == true) && (GameManager.lives > 0))
        {
            this.transform.position += _direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime;
            Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
            Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

            foreach (Transform enemy in this.transform)
            {
                if (!enemy.gameObject.activeInHierarchy) //to see if each enemy is active, so still alive
                {
                    continue;
                }
                if (_direction == Vector3.right && enemy.position.x >= (rightEdge.x - 1.0f)) //-1 to give a little bit of patting
                {
                    AdvanceRow();
                }
                else if (_direction == Vector3.left && enemy.position.x <= (leftEdge.x + 1.0f))
                {
                    AdvanceRow();
                }
            }
        } 
    }
    private void AdvanceRow()
    {
        _direction.x *= -1.0f; //we changethe direction

        Vector3 position = this.transform.position;
        position.y -= 1.0f; //Down of one row
        //GameManager.touchPlayer++;
        this.transform.position = position;
    }
    private void MissileAttack()
    {
        foreach (Transform enemy in this.transform)
        {
            if (!enemy.gameObject.activeInHierarchy) //to see if each enemy is active, so still alive
            {
                continue;
            }

            if(Random.value < (1.0f / (float)this.nbEnemyAlive))
            {
                Instantiate(this.missilePrefab, enemy.position, Quaternion.identity);
                break; //it guarentees that only one missile will shoot at once
            }
        }
    }
    private void EnemyKilled()
    {
        this.nbEnemyKilled++;
        //if (this.nbEnemyKilled >= this.totalEnemy)
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //}
    }
}
