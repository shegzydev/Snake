using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    //Right, Down, Left, Up

    [SerializeField] Sprite[] headSprites;
    [SerializeField] Sprite[] cornerSprites;
    [SerializeField] Sprite[] bodySprites;

    void Start()
    {

    }

    void Update()
    {

    }

    /// <summary>
    /// 0 = head, 1 = body, 2 = tail
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public Sprite GetSprite(int type, Vector3 currentPosition, Vector3 futurePosition)
    {
        switch (type)
        {
            case 0:
                {
                    Vector3 dir = futurePosition - currentPosition;
                    if (dir.x > 0) return headSprites[0];
                    if (dir.x < 0) return headSprites[2];
                    if (dir.y > 0) return headSprites[3];
                    if (dir.y < 0) return headSprites[1];
                    break;
                }
            case 1:
                {
                    Debug.Log("body");
                    if ((int)currentPosition.x == (int)futurePosition.x || (int)currentPosition.y == (int)futurePosition.y)//Straight
                    {
                        return ((int)currentPosition.x == (int)futurePosition.x) ? bodySprites[0] : bodySprites[1];
                    }
                    else//Curve
                    {
                        Vector3 dir = futurePosition - currentPosition;

                        if (dir.x > 0 && dir.y > 0) return cornerSprites[0];
                        if (dir.x < 0 && dir.y > 0) return cornerSprites[1];

                        if (dir.x > 0 && dir.y < 0) return cornerSprites[2];
                        if (dir.x < 0 && dir.y < 0) return cornerSprites[3];
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }
        return null;
    }
}
