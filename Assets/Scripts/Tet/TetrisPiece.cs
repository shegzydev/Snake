using UnityEngine;
using System.Collections.Generic;
using System;

public class TetrisPiece:MonoBehaviour
{
    int xPos;
    int yPos;
    public List<Transform> chunks;

    void Start()
    {
        chunks = new List<Transform>();
        chunks.AddRange(transform.GetComponentsInChildren<Transform>());
        chunks.Remove(transform);
    }

    void Update()
    {

    }

    public void Drop()
    {
        if (ValidMove(Vector2.down))
        {
            transform.position += Vector3.down;
        }
    }

    bool ValidMove(Vector2 dir)
    {
        foreach (Transform t in chunks)
        {
            if(Int(t.position.x + dir.x) < 0 || Int(t.position.x + dir.x) > 9 || Int(t.position.y + dir.y) > 21)
            {
                return false;
            }
        }
        return true;
    }

    int Int(float f)
    {
        return Mathf.RoundToInt(f);
    }
}
