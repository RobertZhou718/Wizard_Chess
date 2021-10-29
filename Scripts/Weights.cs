using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weights : MonoBehaviour
{

    int[,] PawnBoardWeight = new int[,]
    {
        {-100,  -100,  -100,  -100,  -100,  -100,  -100,  -100},
       {50, 50, 50, 50, 50, 50, 50, 50},
        {10, 10, 20, 30, 30, 20, 10, 10},
        {5,  5, 10, 25, 25, 10,  5,  5},
        {0,  0,  0, 20, 20,  0,  0,  0},
        {5, -5,-10,  0,  0,-10, -5,  5},
        {5, 10, 10,-20,-20, 10, 10,  5},
        {100,  100,  100,  100,  100,  100,  100,  100}
    };

    int[,] PawnMirrorBoardWeight = new int[,]
    {
        {100,  100,  100,  100,  100,  100,  100,  100},
        {5, 10, 10,-20,-20, 10, 10,  5},
        {5, -5,-10,  0,  0,-10, -5,  5},
        {0,  0,  0, 20, 20,  0,  0,  0},
        {5,  5, 10, 25, 25, 10,  5,  5},
        {10, 10, 20, 30, 30, 20, 10, 10},
        {50, 50, 50, 50, 50, 50, 50, 50},
        {-100,  -100,  -100,  -100,  -100,  -100,  -100,  -100}
    };

    int[,] KnightBoardWeight = new int[,]
    {
        {-50,-40,-30,-30,-30,-30,-40,-50},
        {-40,-20,  0,  0,  0,  0,-20,-40},
        {-30,  0, 10, 15, 15, 10,  0,-30},
        {-30,  5, 15, 20, 20, 15,  5,-30},
        {-30,  0, 15, 20, 20, 15,  0,-30},
        {-30,  5, 10, 15, 15, 10,  5,-30},
        {-40,-20,  0,  5,  5,  0,-20,-40},
        {-50,-40,-30,-30,-30,-30,-40,-50}
    };

    int[,] KnightMirrorBoardWeight = new int[,]
    {
        {-50,-40,-30,-30,-30,-30,-40,-50},
        {-40,-20,  0,  5,  5,  0,-20,-40},
        {-30,  5, 10, 15, 15, 10,  5,-30},
        {-30,  0, 15, 20, 20, 15,  0,-30},
        {-30,  5, 15, 20, 20, 15,  5,-30},
        {-30,  0, 10, 15, 15, 10,  0,-30},
        {-40,-20,  0,  0,  0,  0,-20,-40},
        {-50,-40,-30,-30,-30,-30,-40,-50}
    };

    int[,] BishopBoardWeight = new int[,]
    {
        {-20,-10,-10,-10,-10,-10,-10,-20},
        {-10,  0,  0,  0,  0,  0,  0,-10},
        {-10,  0,  5, 10, 10,  5,  0,-10},
        {-10,  5,  5, 10, 10,  5,  5,-10},
        {-10,  0, 10, 10, 10, 10,  0,-10},
        {-10, 10, 10, 10, 10, 10, 10,-10},
        {-10,  5,  0,  0,  0,  0,  5,-10},
        {-20,-10,-10,-10,-10,-10,-10,-20}
    };

    int[,] BishopMirrowBoardWeight = new int[,]
    {
        {-20,-10,-10,-10,-10,-10,-10,-20},
        {-10,  5,  0,  0,  0,  0,  5,-10},
        {-10, 10, 10, 10, 10, 10, 10,-10},
        {-10,  0, 10, 10, 10, 10,  0,-10},
        {-10,  5,  5, 10, 10,  5,  5,-10},
        {-10,  0,  5, 10, 10,  5,  0,-10},
        {-10,  0,  0,  0,  0,  0,  0,-10},
        {-20,-10,-10,-10,-10,-10,-10,-20}
    };

    int[,] RookBoardWeight = new int[,]
    {
        {0,  0,  0,  0,  0,  0,  0,  0},
        {5, 10, 10, 10, 10, 10, 10,  5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {0,  0,  0,  5,  5,  0,  0,  0}
    };

    int[,] RookMirrorBoardWeight = new int[,]
    {
        {0,  0,  0,  5,  5,  0,  0,  0},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {5, 10, 10, 10, 10, 10, 10,  5},
        {0,  0,  0,  0,  0,  0,  0,  0}
    };

    int[,] QueenBoardWeight = new int[,]
    {
        {-20,-10,-10, -5, -5,-10,-10,-20},
        {-10,  0,  0,  0,  0,  0,  0,-10},
        {-10,  0,  5,  5,  5,  5,  0,-10},
        {-5,  0,  5,  5,  5,  5,  0, -5},
        {0,  0,  5,  5,  5,  5,  0, -5},
        {-10,  5,  5,  5,  5,  5,  0,-10},
        {-10,  0,  5,  0,  0,  0,  0,-10},
        {-20,-10,-10, -5, -5,-10,-10,-20}
    };

    int[,] QueenMirrorBoardWeight = new int[,]
    {
        {-20,-10,-10, -5, -5,-10,-10,-20},
        {-10,  0,  5,  0,  0,  0,  0,-10},
        {-10,  5,  5,  5,  5,  5,  0,-10},
        {0,  0,  5,  5,  5,  5,  0, -5},
        {-5,  0,  5,  5,  5,  5,  0, -5},
        {-10,  0,  5,  5,  5,  5,  0,-10},
        {-10,  0,  0,  0,  0,  0,  0,-10},
        {-20,-10,-10, -5, -5,-10,-10,-20}
    };

    int[,] KingBoardWeight =
    {
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-20,-30,-30,-40,-40,-30,-30,-20},
        {-10,-20,-20,-20,-20,-20,-20,-10},
        {20, 20,  0,  0,  0,  0, 20, 20},
        {20, 30, 10,  0,  0, 10, 30, 20}
    };

    int[,] KingMirrorBoardWeight =
    {
        {20, 30, 10,  0,  0, 10, 30, 20},
        {20, 20,  0,  0,  0,  0, 20, 20},
        {-10,-20,-20,-20,-20,-20,-20,-10},
        {-20,-30,-30,-40,-40,-30,-30,-20},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
    };

    int[,] KingEndBoardWeight =
    {
        {-50,-40,-30,-20,-20,-30,-40,-50},
        {-30,-20,-10,  0,  0,-10,-20,-30},
        {-30,-10, 20, 30, 30, 20,-10,-30},
        {-30,-10, 30, 40, 40, 30,-10,-30},
        {-30,-10, 30, 40, 40, 30,-10,-30},
        {-30,-10, 20, 30, 30, 20,-10,-30},
        {-30,-30,  0,  0,  0,  0,-30,-30},
        {-50,-30,-30,-30,-30,-30,-30,-50}
    };

    int[,] KingEndMirrorBoardWeight =
    {
        {-50,-30,-30,-30,-30,-30,-30,-50},
        {-30,-30,  0,  0,  0,  0,-30,-30},
        {-30,-10, 20, 30, 30, 20,-10,-30},
        {-30,-10, 30, 40, 40, 30,-10,-30},
        {-30,-10, 30, 40, 40, 30,-10,-30},
        {-30,-10, 20, 30, 30, 20,-10,-30},
        {-30,-20,-10,  0,  0,-10,-20,-30},
        {-50,-40,-30,-20,-20,-30,-40,-50}
    };

    

    public int GetBoardWeight(Chessman chessman, Vector2Int position)
    {
        if (chessman.isWhite)
        {
            if (chessman.GetType()==typeof(Pawn))
                return PawnBoardWeight[position.x, position.y];
            else if (chessman.GetType() == typeof(Rook))
                return RookBoardWeight[position.x, position.y];
            else if (chessman.GetType() == typeof(Bishop))
                return BishopBoardWeight[position.x, position.y];
            else if (chessman.GetType() == typeof(King))
                return KingBoardWeight[position.x, position.y];
            else if (chessman.GetType() == typeof(Queen))
                return QueenBoardWeight[position.x, position.y];
            else if (chessman.GetType() == typeof(Knight))
                return KnightBoardWeight[position.x, position.y];
            else
                return -1;
        }
        else
        {
            if ((chessman.GetType() == typeof(Pawn)))
                return PawnMirrorBoardWeight[position.x, position.y];
            else if ((chessman.GetType() == typeof(Rook)))
                return RookMirrorBoardWeight[position.x, position.y];
            else if ((chessman.GetType() == typeof(Bishop)))
                return BishopMirrowBoardWeight[position.x, position.y];
            else if ((chessman.GetType() == typeof(King)))
                return KingMirrorBoardWeight[position.x, position.y];
            else if ((chessman.GetType() == typeof(Queen)))
                return QueenMirrorBoardWeight[position.x, position.y];
            else if ((chessman.GetType() == typeof(Knight)))
                return KnightMirrorBoardWeight[position.x, position.y];
            else
                return -1;
        }
        
    }

    public int GetPieceWeight(Chessman chessman)
    {
        if (chessman.GetType() == typeof(Pawn))
            return 100;
        else if (chessman.GetType() == typeof(Rook))
            return 500;
        else if (chessman.GetType() == typeof(Bishop))
            return 300;
        else if (chessman.GetType() == typeof(Knight))
            return 300;
        else if (chessman.GetType() == typeof(Queen))
            return 900;
        else if (chessman.GetType() == typeof(King))
            return 90000;
        else
            return -1;
    }
}
