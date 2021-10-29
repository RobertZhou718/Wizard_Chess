using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Knight : Chessman
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


    public override void isSelected()
    {
        HittedMatEffect sc = GetComponent<HittedMatEffect>();
        if (null != sc)
        {
            sc.Active();

            //setIdle2();
            if (isWhite)
                sc.SetColor(Color.red, 2);
            else
                sc.SetColor(Color.blue, 2);
        }
    }

    public override bool[, ] PossibleMove()
    {
        //for (int x = 0; x < 8; x++) for (int y = 0; y < 8; y++) possible_moves[x, y] = false;

        bool[,] r = new bool[8, 8];

        //UpLeft
        KnightMove(currentX - 1, currentY + 2, ref r);

        //UpRight
        KnightMove(currentX + 1, currentY + 2, ref r);

        //DownLeft
        KnightMove(currentX + 2, currentY + 1, ref r);

        //DownLeft
        KnightMove(currentX - 2, currentY - 1, ref r);

        //UpLeft
        KnightMove(currentX - 1, currentY - 2, ref r);

        //UpRight
        KnightMove(currentX + 1, currentY - 2, ref r);

        //DownLeft
        KnightMove(currentX + 2, currentY - 1, ref r);

        //DownLeft
        KnightMove(currentX - 2, currentY + 1, ref r);

        //for (int i = 0; i < 8; i++)
        //{
        //    for (int j = 0; j < 8; j++)
        //    {
        //        possible_moves[i, j] = r[i, j];
        //    }
        //}

        return r;
    }

    protected new void SetPosition(int x, int y)
    {
        base.SetPosition(x, y);
        PossibleMove();
    }


    public void KnightMove(int x, int y, ref bool[,] r)
    {
        Chessman c;
        if (x >=0 && x< 8 && y >= 0 && y < 8)
        {
            c = BoardManager.Instance.chessmens[x, y];
            if (c == null)
            {
                r[x, y] = true;
            }else if (isWhite != c.isWhite)
            {
                r[x, y] = true;
            }
        }
    }
}
