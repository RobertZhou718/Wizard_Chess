using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pawn : Chessman
{

    private void Start()
    {
        newStart();
        possible_moves = new bool[8, 8];
    }

    private void Update()
    {
        generationPiece();
        isMoving();
        isAttacking();
        lookAtTarget();
        close_up();
        //UpdatePossibleMoves();
    }

    private void checkPromotion()
    {
        if (currentY == 7 || currentY == 0)
        {
            BoardManager.Instance.PawnPromotion(this, currentX, currentY);
        }
    }

    protected new void isMoving()
    {
        if (!is_moving) return;

        if (Vector3.Distance(agent.destination, transform.position) < 0.05f)
        {
            is_moving = false;
            setIdle();
            BoardManager.Instance.pause = false;
            if (isWhite)
            {
                BoardManager.Instance.AITurn = true;
            }
            checkPromotion();
        }
    }

    protected new void SetPosition(int x, int y)
    {
        base.SetPosition(x, y);
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
        Chessman c, c2;

        //white team move
        if (isWhite)
        {
            //Diagonal left
            if (currentX != 0 && currentY != 7)
            {
                c = BoardManager.Instance.chessmens[currentX - 1, currentY + 1];
                if (c != null && !c.isWhite)
                {
                    //possible_moves[currentX - 1, currentY + 1] = true;
                    r[currentX - 1, currentY + 1] = true;
                }
                

            }
            //Diagonal right
            if (currentX != 7 && currentY != 7)
            {
                c = BoardManager.Instance.chessmens[currentX + 1, currentY + 1];
                if (c != null && !c.isWhite)
                {
                    //possible_moves[currentX + 1, currentY + 1] = true;
                    r[currentX + 1, currentY + 1] = true;
                }

            }
            //Middle
            if (currentY != 7)
            {
                c = BoardManager.Instance.chessmens[currentX, currentY + 1];
                if (c == null)
                {
                    //possible_moves[currentX, currentY + 1] = true;
                    r[currentX, currentY + 1] = true;
                }

            }
            //Middle on first move
            if (currentY == 1)
            {
                c = BoardManager.Instance.chessmens[currentX, currentY + 1];
                c2 = BoardManager.Instance.chessmens[currentX, currentY + 2];
                if (c == null & c2 == null)
                {
                    //possible_moves[currentX, currentY + 1] = true;
                    //possible_moves[currentX, currentY + 2] = true;

                    r[currentX, currentY + 1] = true;
                    r[currentX, currentY + 2] = true;
                }
            }
        }
        else
        {
            //Diagonal left
            if (currentX != 0 && currentY != 0)
            {
                c = BoardManager.Instance.chessmens[currentX - 1, currentY - 1];
                if (c != null && c.isWhite)
                {
                    //possible_moves[currentX - 1, currentY - 1] = true;
                    r[currentX - 1, currentY - 1] = true;
                }

            }
            //Diagonal right
            if (currentX != 7 && currentY != 0)
            {
                c = BoardManager.Instance.chessmens[currentX + 1, currentY - 1];
                if (c != null && c.isWhite)
                {
                    //possible_moves[currentX + 1, currentY - 1] = true;
                    r[currentX + 1, currentY - 1] = true;
                }

            }
            //Middle
            if (currentY != 0)
            {
                c = BoardManager.Instance.chessmens[currentX, currentY - 1];
                if (c == null)
                {
                    //possible_moves[currentX, currentY - 1] = true;
                    r[currentX, currentY - 1] = true;
                }

            }
            //Middle on first move
            if (currentY == 6)
            {
                c = BoardManager.Instance.chessmens[currentX, currentY - 1];
                c2 = BoardManager.Instance.chessmens[currentX, currentY - 2];
                if (c == null & c2 == null)
                {
                    //possible_moves[currentX, currentY - 1] = true;
                    //possible_moves[currentX, currentY - 2] = true;

                    r[currentX, currentY - 1] = true;
                    r[currentX, currentY - 2] = true;
                }
                //if (c2 == null)
                //{
                //    r[currentX, currentY - 2] = true;
                //}
            }
        }

        return r;
    }

    
}
