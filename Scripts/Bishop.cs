using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class Bishop : Chessman
{
   
    private void Start()
    {
        newStart();
        //possible_moves = new bool[8, 8];
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
            //possible_moves = PossibleMove();
            //Debug.Log("change");
            isChanged = false;
        }
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

    protected new void SetPosition(int x, int y)
    {
        base.SetPosition(x, y);
        PossibleMove();
    }

    public override bool[, ] PossibleMove()
    {
        //for (int x = 0; x < 8; x++) for (int y = 0; y < 8; y++) possible_moves[x, y] = false;

        bool[,] r = new bool[8, 8];
        Chessman c;
        int i, j;

        //Top left
        i = currentX;
        j = currentY;
        while (true)
        {
            i--;
            j++;
            if (i<0 || j >= 8)
            {
                break;
            }
            c = BoardManager.Instance.chessmens[i, j];
            if (c == null)
            {
                r[i, j] = true;
                //possible_moves[i, j] = true;
            }
            else
            {
                if(isWhite != c.isWhite)
                {
                    r[i, j] = true;
                    //possible_moves[i, j] = true;
                }
                break;
            }
        }

        i = currentX;
        j = currentY;
        while (true)
        {
            i--;
            j--;
            if (i < 0 || j < 0)
            {
                break;
            }
            c = BoardManager.Instance.chessmens[i, j];
            if (c == null)
            {
                r[i, j] = true;
                //possible_moves[i, j] = true;
            }
            else
            {
                if (isWhite != c.isWhite)
                {
                    r[i, j] = true;
                    //possible_moves[i, j] = true;
                }
                break;
            }
        }

        i = currentX;
        j = currentY;
        while (true)
        {
            i++;
            j--;
            if (i >= 8 || j < 0)
            {
                break;
            }
            c = BoardManager.Instance.chessmens[i, j];
            if (c == null)
            {
                r[i, j] = true;
                //possible_moves[i, j] = true;
            }
            else
            {
                if (isWhite != c.isWhite)
                {
                    r[i, j] = true;
                    //possible_moves[i, j] = true;
                }
                break;
            }
        }

        i = currentX;
        j = currentY;
        while (true)
        {
            i++;
            j++;
            if (i >= 8 || j >= 8)
            {
                break;
            }
            c = BoardManager.Instance.chessmens[i, j];
            if (c == null)
            {
                r[i, j] = true;
                //possible_moves[i, j] = true;
            }
            else
            {
                if (isWhite != c.isWhite)
                {
                    r[i, j] = true;
                    //possible_moves[i, j] = true;
                }
                break;
            }
        }

        return r;
    }
}
