using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHighlight : MonoBehaviour
{
    public static TileHighlight Instance { set; get; }

    public List<GameObject> boardHighlight;

    private void Start()
    {
        Instance = this;
    }

    public void HighlightAllowedMoves(bool[,] moves)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (moves[i, j])
                {
                    int index = i + j * 8;
                    SetHighlight(boardHighlight[index], Color.red);
                }
            }
        }
    
    }

    public void SetHighlight(GameObject gameObject, Color color)
    {
        HittedMatEffect hittedMat = gameObject.GetComponent<HittedMatEffect>();
        hittedMat.Active();
        hittedMat.SetColor(color, 1.5f);
    }

    public void CloseHighlight()
    {
        foreach (GameObject gameObject in boardHighlight)
        {
            HittedMatEffect hitted = gameObject.GetComponent<HittedMatEffect>();
            hitted.clossHighlight();
        }
    }
}
