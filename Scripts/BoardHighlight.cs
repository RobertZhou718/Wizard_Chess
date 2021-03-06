using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlight : MonoBehaviour
{
    public static BoardHighlight Instance { set; get; }

    public GameObject highlightPrefab;
    private List<GameObject> highlights;

    private float tile_size = 1f;

    private void Start()
    {
        Instance = this;
        highlights = new List<GameObject>();
    }

    private GameObject GetHighlightObject()
    {
        GameObject go = highlights.Find(g => !g.activeSelf);

        if (go == null)
        {
            go = Instantiate(highlightPrefab);
            highlights.Add(go);
        }
        return go;
    }

    public void HighlightAllowedMoves(bool [,] moves)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (moves[i, j])
                {
                    GameObject go = GetHighlightObject();
                    go.SetActive(true);
                    //go.GetComponent<HittedMatEffect>().Active();
                    //go.GetComponent<HittedMatEffect>().SetColor(Color.green);
                    go.transform.position = new Vector3(i + tile_size / 2, 0f, j + tile_size / 2);
                }
            }
        }
    }

    public void Hidehighlights()
    {
        foreach(GameObject go in highlights)
        {
            go.SetActive(false);
        }
    }
}
