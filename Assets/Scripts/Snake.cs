using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] GameObject chunk;
    [SerializeField] float speed;

    public Transform Food;
    public Transform Head;
    public List<Transform> body;

    Vector2 dir;
    float lasttime;

    int width = 17;
    int height = 9;

    bool gameOver;
    void Start()
    {
        Head.position = new Vector2(Random.Range(-width, width + 1), Random.Range(-height, height + 1));
        Food.position = new Vector2(Random.Range(-width, width + 1), Random.Range(-height, height + 1));

        body = new List<Transform>
        {
            Instantiate(chunk).transform
        };

        dir = Vector2.right;
    }

    void Update()
    {
        ChangeDirectionWithInput();

        if (Time.time - lasttime >= 1f / speed)
        {
            Move();
            lasttime = Time.time;
        }

        if (ToInt(Head.position.x) == ToInt(Food.position.x) && ToInt(Head.position.y) == ToInt(Food.position.y))
        {
            Food.position = new Vector2(Random.Range(-17, 18), Random.Range(-9, 10));
            Elongate();
        }

        CheckGameOver();
    }

    void ChangeDirectionWithInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (dir != Vector2.down)
            {
                dir = Vector2.up;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (dir != Vector2.up)
            {
                dir = Vector2.down;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (dir != Vector2.right)
            {
                dir = Vector2.left;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (dir != Vector2.left)
            {
                dir = Vector2.right;
            }
        }
    }

    void Move()
    {
        if (gameOver) return;

        for (int i = body.Count - 1; i > 0; i--)
        {
            body[i].position = body[i - 1].position;
        }
        body[0].position = Head.position;
        Head.position += (Vector3)dir;

        LoopBoundaries();
    }

    void LoopBoundaries()
    {
        if (Head.position.x < -width) Head.position += Vector3.right * (width * 2 + 1);
        if (Head.position.x > +width) Head.position -= Vector3.right * (width * 2 + 1);

        if (Head.position.y < -height) Head.position += Vector3.up * (height * 2 + 1);
        if (Head.position.y > +height) Head.position -= Vector3.up * (height * 2 + 1);
    }

    void Elongate()
    {
        body.Add(Instantiate(chunk).transform);
    }

    void CheckGameOver()
    {
        foreach (Transform t in body)
        {
            if(ToInt(Head.position.x) == ToInt(t.position.x) && ToInt(Head.position.y) == ToInt(t.position.y))
            {
                dir = Vector2.zero;
                gameOver = true;
                break;
            }
        }
    }

    int ToInt(float f)
    {
        return Mathf.RoundToInt(f);
    }
}
