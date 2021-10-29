using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class King : Chessman
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

    public new void die_destroy()
    {
        BoardManager.Instance.pause = true;
        BoardManager.Instance.Restart();
        //BoardManager.Instance.EndGame();
        Destroy(gameObject);
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
        //PossibleMove();
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
        int i, j;

        i = currentX - 1;
        j = currentY + 1;

        if (currentY != 7)
        {
            for (int k=0; k< 3; k++)
            {
                if (i>=0 && i < 8)
                {
                    c = BoardManager.Instance.chessmens[i, j];
                    if (c == null)
                    {
                        r[i, j] = true;
                        //possible_moves[i, j] = true;
                    }
                    else if (isWhite != c.isWhite)
                    {
                        r[i, j] = true;
                        //possible_moves[i, j] = true;
                    }
                }
                i++;
            }
        }

        i = currentX - 1;
        j = currentY - 1;

        if (currentY != 0)
        {
            for (int k = 0; k < 3; k++)
            {
                if (i >= 0 && i < 8)
                {
                    c = BoardManager.Instance.chessmens[i, j];
                    if (c == null)
                    {
                        r[i, j] = true;
                        //possible_moves[i, j] = true;
                    }
                    else if (isWhite != c.isWhite)
                    {
                        r[i, j] = true;
                        //possible_moves[i, j] = true;
                    }
                }
                i++;
            }
        }

        if (currentX!=0)
        {
            c = BoardManager.Instance.chessmens[currentX - 1, currentY];
            if (c == null)
            {
                r[currentX - 1, currentY] = true;
                //possible_moves[currentX - 1, currentY] = true;
            }
            else if (isWhite != c.isWhite)
            {
                r[currentX - 1, currentY] = true;
                ////possible_moves[currentX - 1, currentY] = true;
            }
        }

        if (currentX != 7)
        {
            c = BoardManager.Instance.chessmens[currentX + 1, currentY];
            if (c == null)
            {
                r[currentX + 1, currentY] = true;
                //possible_moves[currentX + 1, currentY] = true;
            }
            else if (isWhite != c.isWhite)
            {
                r[currentX + 1, currentY] = true;
                //possible_moves[currentX + 1, currentY] = true;
            }
        }


        return r;
    }
}
