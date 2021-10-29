using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text : MonoBehaviour
{
    private bool[,] moves;
    private bool[,] moves1;

    // Start is called before the first frame update
    void Start()
    {
        moves = new bool[8, 8];
        moves1 = new bool[8, 8];

        //moves = BoardManager.Instance.chessmens[0, 1].PossibleMove();
        //moves1 = BoardManager.Instance.chessmens[0, 1].GetPossibleMoves();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    moves = BoardManager.Instance.chessmens[2, 0].PossibleMove();
        //    //Debug.Log("Moves2");
        //    moves1 = BoardManager.Instance.chessmens[2, 0].GetPossibleMoves();

        //    Debug.Log("Moves1");
        //    for (int i = 0; i < 8; i++)
        //    {
        //        for (int j = 0; j < 8; j++)
        //        {
        //            Debug.Log(moves[i, j]);
        //        }
        //    }

        //    Debug.Log("Moves2");
        //    for (int i = 0; i < 8; i++)
        //    {
        //        for (int j = 0; j < 8; j++)
        //        {
        //            Debug.Log(moves1[i, j]);
        //        }
        //    }

        //}
    }
}
