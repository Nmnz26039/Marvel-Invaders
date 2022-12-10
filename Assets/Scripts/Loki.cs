using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loki : MonoBehaviour
{
    public float speed = 5.0f;
    public float cycleTime = 20f;
    public GameObject loki;
    public System.Action killed;

    public Vector3 ToTheLeft { get; private set; }
    public Vector3 ToTheRight { get; private set; }
    public int direction { get; private set; } = -1;
    public bool spawned { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        //We position the destionations of loki to the right and left, outside the camera view point :
        Vector3 right = transform.position;
        right.x = rightEdge.x + 1.5f;
        this.ToTheRight = right;

        Vector3 left = transform.position;
        left.x = leftEdge.x - 1.5f;
        this.ToTheLeft = left;

        transform.position = this.ToTheLeft;
        Despawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawned)
        {
            return;
        }
        if (direction == 1)
        {
            MoveToTheRight();
        }
        else
        {
            MoveToTheLeft();
        }
    }

    private void MoveToTheLeft()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x <= this.ToTheLeft.x)
        {
            Despawn();
        }
    }
    private void MoveToTheRight()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
        if (transform.position.x >= this.ToTheRight.x)
        {
            Despawn();
        }
    }
    private void Spawn()
    {
        direction *= -1;

        if (direction == 1)
        {
            transform.position = this.ToTheLeft;
        }
        else
        {
            transform.position = this.ToTheRight;
        }

        spawned = true;
    }

    private void Despawn()
    {
        spawned = false;

        if (direction == 1)
        {
            transform.position = this.ToTheRight;
        }
        else
        {
            transform.position = this.ToTheLeft;
        }

        Invoke(nameof(Spawn), cycleTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            Despawn();

            //this.killed.Invoke();
        }

    }
}
