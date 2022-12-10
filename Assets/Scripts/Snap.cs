using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour
{
    public GameObject snapThanos;

    public float speed = 5.0f;

    public int direction { get; private set; } = -10;
    public Vector3 ToTheLeft { get; private set; }
    public Vector3 ToTheRight { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        Vector3 right = transform.position;
        right.x = rightEdge.x + 10f;
        this.ToTheRight = right;

        Vector3 left = transform.position;
        left.x = leftEdge.x - 10f;
        this.ToTheLeft = left;

        transform.position = this.ToTheLeft;

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.lives < 1)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (transform.position.x >= this.ToTheRight.x)
        {
            transform.position = this.ToTheRight;
        }
    }
}
