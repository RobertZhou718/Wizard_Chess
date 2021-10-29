using System.Collections;
using System.Collections.Generic;

using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class EasyLevel : MonoBehaviour
{
    public static EasyLevel Instance { set; get; }
    private List<Chessman> blackChessman;

    public int selectedX;
    public int selectedY;

    public int moveToX;
    public int moveToY;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        blackChessman = new List<Chessman>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addChessman(Chessman chessman)
    {
        blackChessman.Add(chessman);
    }

    public void removeChessman(Chessman chessman)
    {
        blackChessman.Remove(chessman);
    }

    public void selectChessman()
    {
        int index = Random.Range(0, blackChessman.Count - 1);
        bool[,] r = new bool[8, 8];
        r = blackChessman[index].PossibleMove();

    }

    public List<Vector2Int> selectPiece()
    {
        int index = Random.Range(0, blackChessman.Count);

        // Determine the selected piece
        List<Vector2Int> possibleMoves = countStep(blackChessman[index].PossibleMove());

        while (possibleMoves.Count == 0)
        {
            index = Random.Range(0, blackChessman.Count);
            possibleMoves = countStep(blackChessman[index].PossibleMove());
        }


        Vector2Int position = new Vector2Int(blackChessman[index].currentX, blackChessman[index].currentY);

        //Determine the selected step
        index = Random.Range(0, possibleMoves.Count);
        Vector2Int next_step = possibleMoves[index];

        List<Vector2Int> results = new List<Vector2Int>();
        results.Add(position);
        results.Add(next_step);

        return results;
    }

    private List<Vector2Int> countStep(bool[, ] moves)
    {
        List<Vector2Int> positions = new List<Vector2Int>();

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (moves[i,j])
                {
                    positions.Add(new Vector2Int(i, j));
                    
                }
                
            }
        }

        return positions;
    }
}
