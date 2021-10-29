using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rook : Chessman
{
    private void Start()
    {
        newStart();
        possible_moves = new bool[8, 8];
    }

    private void Update()
    {
        newUpdate();
        //UpdatePossibleMoves();
    }

    protected void UpdatePossibleMoves()
    {
        if (isChanged)
        {
            possible_moves = PossibleMove();
            //Debug.Log("change");
            isChanged = false;
        }
    }

    protected new void SetPosition(int x, int y)
    {
        base.SetPosition(x, y);
        //possible_moves = PossibleMove();
        PossibleMove();
    }

    public override void isSelected()
    {
        HittedMatEffect sc = GetComponent<HittedMatEffect>();
        if (null != sc)
        {
            sc.Active();
            if (isWhite)
                sc.SetColor(Color.red, 2);
            else
                sc.SetColor(Color.blue, 2);
        }
    }

    public override bool[,] PossibleMove()
    {
        //for (int x = 0; x < 8; x++) for (int y = 0; y < 8; y++) possible_moves[x, y] = false;

        bool[,] r = new bool[8, 8];
        Chessman c;
        int i;

        //Right
        i = currentX;
        while (true)
        {
            i++;
            if (i >= 8)
            {
                break;
            }
            c = BoardManager.Instance.chessmens[i, currentY];
            if (c == null)
            {
                r[i, currentY] = true;
                //possible_moves[i, currentY] = true;
            }
            else
            {
                if (c.isWhite != isWhite)
                {
                    r[i, currentY] = true;
                    //possible_moves[i, currentY] = true;
                }
                break;
            }
        }

        //Left
        i = currentX;
        while (true)
        {
            i--;
            if (i < 0)
            {
                break;
            }
            c = BoardManager.Instance.chessmens[i, currentY];
            if (c == null)
            {
                r[i, currentY] = true;
                //possible_moves[i, currentY] = true;
            }
            else
            {
                if (c.isWhite != isWhite)
                {
                    r[i, currentY] = true;
                    //possible_moves[i, currentY] = true;
                }
                break;
            }
        }

        //Up
        i = currentY;
        while (true)
        {
            i++;
            if (i >= 8)
            {
                break;
            }
            c = BoardManager.Instance.chessmens[currentX, i];
            if (c == null)
            {
                r[currentX, i] = true;
                //possible_moves[currentX, i] = true;
            }
            else
            {
                if (c.isWhite != isWhite)
                {
                    r[currentX, i] = true;
                    //possible_moves[currentX, i] = true;
                }
                break;
            }
        }

        //Down
        i = currentY;
        while (true)
        {
            i--;
            if (i < 0)
            {
                break;
            }
            c = BoardManager.Instance.chessmens[currentX, i];
            if (c == null)
            {
                r[currentX, i] = true;
                //possible_moves[currentX, i] = true;
            }
            else
            {
                if (c.isWhite != isWhite)
                {
                    r[currentX, i] = true;
                    //possible_moves[currentX, i] = true;
                }
                break;
            }
        }

        return r;
    }
}
