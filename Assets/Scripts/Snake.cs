using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class Snake : MonoBehaviour
{
    SpriteManager spriteManager;

    [SerializeField] GameObject chunk;
    [SerializeField] float speed;

    public Transform Food;

    public Transform Head;
    SpriteRenderer HeadSprite;

    public List<Transform> Body;
    public List<SpriteRenderer> BodySprite;

    Vector2 dir;
    float lasttime;

    int width = 17;
    int height = 9;

    int score = 0;

    bool gameOver;

    void Start()
    {
        spriteManager = FindObjectOfType<SpriteManager>();

        HeadSprite = Head.GetComponent<SpriteRenderer>();

        Head.position = new Vector2(Random.Range(-width, width + 1), Random.Range(-height, height + 1));
        Food.position = new Vector2(Random.Range(-width, width + 1), Random.Range(-height, height + 1));

        Body = new List<Transform>
        {
            Instantiate(chunk).transform
        };

        BodySprite = new List<SpriteRenderer>();
        BodySprite.Add(Body[0].GetComponent<SpriteRenderer>());

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
            score++;
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

        for (int i = Body.Count - 1; i > 0; i--)
        {
            Body[i].position = Body[i - 1].position;
        }
        Body[0].position = Head.position;
        Head.position += (Vector3)dir;

        LoopBoundaries();
        AssignSprites();
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
        Transform tmp = Instantiate(chunk).transform;
        Body.Add(tmp);
        BodySprite.Add(tmp.GetComponent<SpriteRenderer>());
    }

    void CheckGameOver()
    {
        foreach (Transform t in Body)
        {
            if (ToInt(Head.position.x) == ToInt(t.position.x) && ToInt(Head.position.y) == ToInt(t.position.y))
            {
                dir = Vector2.zero;
                gameOver = true;
                FindObjectOfType<GameManager>().ShowEndScreen();
                break;
            }
        }
    }

    void AssignSprites()
    {
        HeadSprite.sprite = spriteManager.GetSprite(0, Head.position, Head.position + (Vector3)dir);
        for (int i = 1; i < Body.Count - 1; i++)
        {
            BodySprite[i].sprite = spriteManager.GetSprite(1, Body[i + 1].position, Body[i - 1].position);
        }
    }

    public int GetScore
    {
        get
        {
            return score;
        }
    }

    int ToInt(float f)
    {
        return Mathf.RoundToInt(f);
    }
}
