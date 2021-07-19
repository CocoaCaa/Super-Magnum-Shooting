using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public enum Direction { LEFT = -1, RIGHT = 1 }

    public int score;
    public Direction direction;
    public float speed = 1.0f;

    void Update()
    {
        transform.position += new Vector3(((float)direction * speed), 0, 0) * Time.deltaTime;

        if (transform.position.x < -10 || transform.position.x > 10)
        {
            Destroy(gameObject);
        }
    }
}
