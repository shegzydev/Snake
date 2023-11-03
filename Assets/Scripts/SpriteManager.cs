using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    //Right, Down, Left, Up

    [SerializeField] Sprite[] headSprites;
    [SerializeField] Sprite[] cornerSprites;
    [SerializeField] Sprite[] bodySprites;
    [SerializeField] Sprite[] tailSprites;

    void Start()
    {

    }

    void Update()
    {

    }

    public Sprite GetSprite(int type, Vector3 pastPosition, Vector3 currentPosition, Vector3 futurePosition)
    {
        switch (type)
        {
            case 0://if head or tail
                {
                    Vector3 dir = futurePosition - currentPosition;

                    if (dir.x > 0) return headSprites[0];
                    if (dir.x < 0) return headSprites[2];
                    if (dir.y > 0) return headSprites[3];
                    if (dir.y < 0) return headSprites[1];

                    break;
                }
            case 1://if body
                {
                    if ((int)pastPosition.x == (int)futurePosition.x || (int)pastPosition.y == (int)futurePosition.y)//Straight
                    {
                        return ((int)currentPosition.x == (int)futurePosition.x) ? bodySprites[0] : bodySprites[1];
                    }
                    else//Curve
                    {
                        Vector3 dirA = futurePosition - currentPosition;
                        Vector3 dirB = pastPosition - currentPosition;

                        Vector3 dir = dirA + dirB;

                        if (dir.x > 0 && dir.y < 0) return cornerSprites[0];
                        if (dir.x < 0 && dir.y < 0) return cornerSprites[1];
                        if (dir.x > 0 && dir.y > 0) return cornerSprites[2];
                        if (dir.x < 0 && dir.y > 0) return cornerSprites[3];
                    }

                    break;
                }
            case 2://if tail
                {
                    Vector3 dir = futurePosition - currentPosition;

                    if (dir.x > 0) return tailSprites[0];
                    if (dir.x < 0) return tailSprites[2];
                    if (dir.y > 0) return tailSprites[3];
                    if (dir.y < 0) return tailSprites[1];

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
