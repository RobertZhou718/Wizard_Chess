using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { set; get; }
    private bool[,] allowedMoves { set; get; }
    public Chessman[,] chessmens { set; get; }
    private Chessman selectedChessman;

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;

    public List<GameObject> chessmanPrefabs;
    private List<GameObject> activeChessman;

    private Quaternion orientation = Quaternion.Euler(0, 180, 0);

    public bool isWhiteTurn = true;

    public bool AITurn { set; get; }

    public bool WhiteFirst { set; get; }

    public bool pause { set; get; }

    public bool playWithAI;
    public bool Highlevel;

    public bool restart = false;
    bool isComputing;

    bool doubleAI = false;

    public GameObject text1;
    public GameObject text2;
    // Start is called before the first frame update
    private void Awake()
    {
        restart = true;
    }

    void Start()
    {
        
        Instance = this;
        SpawnAllChessmans();
        pause = true;
        AITurn = false;
        WhiteFirst = false;
        playWithAI = false;
        Highlevel = true;
        isComputing = true;
        doubleAI = false;
        text1.SetActive(false);
        text2.SetActive(false);
    }





    // Update is called once per frame
    void Update()
    {
        Highlevel = Attribute.High_Level;
        playWithAI = Attribute.AI;
        doubleAI = Attribute.DoubleAI;

        if (Input.GetMouseButtonDown(0))
        {
            
            if (restart)
            {
                WhiteFirst = true;
                //pause = false;
                restart = false;
            }
        
            text1.SetActive(false);
            text2.SetActive(false);
        }
        UpdateSelection();
        //DrawChessboard();
        AIPlayer();
        DoublePlayers();
        DoubleAI();
       // Restart();
    }

    private void DoublePlayers()
    {
        if (doubleAI) return;
        if (playWithAI) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (selectionX >= 0 && selectionY >= 0)
            {
                if (selectedChessman == null)
                {
                    // select the chessman
                    SelectChessman(selectionX, selectionY);
                }
                else if (selectedChessman.isWhite == isWhiteTurn)
                {
                    MoveChessman(selectionX, selectionY);
                    SelectChessman(selectionX, selectionY);
                }
                else
                {
                    // move the chessman
                    MoveChessman(selectionX, selectionY);
                }
            }
        }
    }

    private void DoubleAI()
    {
        if (!doubleAI) return;

        //Debug.Log(doubleAI);
        if (WhiteFirst && !pause)
        {
            if (isComputing)
            {
                isComputing = false;

                List<Vector2Int> aiStep = new List<Vector2Int>();
                //IEnumerator compute()
                //{
                //    yield return new WaitForEndOfFrame();
                //    aiStep = MiniMax.Instance.UpdateStep(true);

                //    //yield return 
                //}

                //StartCoroutine(compute());
                aiStep = MiniMax.Instance.UpdateStep(true);

                if (aiStep.Count != 0)
                {
                    if (selectedChessman == null)
                    {
                        SelectChessman(aiStep[0].x, aiStep[0].y);
                        MoveChessman(aiStep[1].x, aiStep[1].y);
                        WhiteFirst = !WhiteFirst;
                        isComputing = true;
                        //isComputing = false;
                    }
                }
            }
        }
        else
        {
            if (pause) return;
            
            if (isComputing)
            {
                isComputing = false;
                List<Vector2Int> aiStep = new List<Vector2Int>();
                //aiStep = EasyLevel.Instance.selectPiece();
                aiStep = MiniMax.Instance.UpdateStep(false);
                if (aiStep.Count != 0)
                {
                    
                    if (selectedChessman == null)
                    {
                        
                        SelectChessman(aiStep[0].x, aiStep[0].y);
                        //Debug.Log(selectedChessman);
                        MoveChessman(aiStep[1].x, aiStep[1].y);
                        WhiteFirst = !WhiteFirst;
                        isComputing = true;
                    }

                }
            }

        }
        
    }

    private void AIPlayer()
    {
        if (doubleAI) return;
        if (!playWithAI) return;
        if (!AITurn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (selectionX >= 0 && selectionY >= 0)
                {
                    if (selectedChessman == null)
                    {
                        // select the chessman
                        SelectChessman(selectionX, selectionY);
         
                    }
                    else if (selectedChessman.isWhite == isWhiteTurn)
                    {
                        MoveChessman(selectionX, selectionY);
                        SelectChessman(selectionX, selectionY);
                    }
                    else
                    {
                        // move the chessman
                        MoveChessman(selectionX, selectionY);
                    }
                }
            }
        }
        else
            AI_turn();
    }


    public List<GameObject> GetActiveChessman()
    {
        return activeChessman;
    }


    private void SelectChessman(int x, int y)
    {
        if (pause) return;

        if (chessmens[x, y] == null)
        {
            return;
        }
        if (chessmens[x, y].isWhite != isWhiteTurn)
        {
            return;
        }
        
        selectedChessman = chessmens[x, y];
        selectedChessman.isSelected();

        bool hasAtleastOneMove = false;
        allowedMoves = chessmens[x, y].PossibleMove();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (allowedMoves[i, j])
                    hasAtleastOneMove = true;
            }
        }
        //Debug.Log(allowedMoves.Length);

        if (!hasAtleastOneMove)
            return;
        
        TileHighlight.Instance.HighlightAllowedMoves(allowedMoves);
        
    }


    private void MoveChessman(int x, int y)
    {
        //if (pause) return;
        //Debug.Log(new Vector2Int(x, y));
        if (allowedMoves[x, y])
        {
            Chessman c = chessmens[x, y];

            selectedChessman.moveToTarget(GetTileCenter(x, y), ref c);

            if (c != null && c.isWhite != isWhiteTurn)
            {
                activeChessman.Remove(c.gameObject);
            }

            if (c!=null && !c.isWhite && Highlevel == false && playWithAI == true)
            {
                EasyLevel.Instance.removeChessman(c);
            }

            chessmens[selectedChessman.currentX, selectedChessman.currentY] = null;
            
            selectedChessman.SetPosition(x, y);
            chessmens[x, y] = selectedChessman;
            
            isWhiteTurn = !isWhiteTurn;
        }

        HittedMatEffect sc = selectedChessman.GetComponent<HittedMatEffect>();
        if (null != sc)
        {
            sc.clossHighlight();
        }
        TileHighlight.Instance.CloseHighlight();
        
        //BoardHighlight.Instance.Hidehighlights();
        selectedChessman = null;
    }


    private void AI_turn()
    {
        if (!pause && AITurn)
        {
            
            List<Vector2Int> aiStep = new List<Vector2Int>();
            if (Highlevel)
            {
                
                //isComputing = true;
                aiStep = MiniMax.Instance.UpdateStep(true);

                
                
                //IEnumerator tt()
                //{

                //    yield return StartCoroutine(H());

                //}

                //IEnumerator H()
                //{
                //    aiStep = MiniMax.Instance.UpdateStep();
                //    yield return null;
                //}
                //StartCoroutine(tt());
            }
            else
                aiStep = EasyLevel.Instance.selectPiece();

            //Debug.Log("fkladjlfk");

            if (aiStep.Count != 0)
            {
                if (selectedChessman == null)
                {
                    SelectChessman(aiStep[0].x, aiStep[0].y);

                    MoveChessman(aiStep[1].x, aiStep[1].y);
                    AITurn = false;
                    //isComputing = false;
                }
            }
            //if (selectedChessman != null)
            //    MoveChessman(aiStep[1].x, aiStep[1].y);
        }
    }

    private void UpdateSelection()
    {
        if (!Camera.main)
            return;

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50.0f, LayerMask.GetMask("ChessPlane"))){
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }

    private void SpawnChessman(int index, int x, int y)
    {
        GameObject go;
        if (index <=5)
            go = Instantiate(chessmanPrefabs[index], GetTileCenter(x, y), Quaternion.identity) as GameObject;
        else
            go = Instantiate(chessmanPrefabs[index], GetTileCenter(x, y), Quaternion.Euler(0, 180, 0)) as GameObject;
        go.transform.SetParent(transform);
        chessmens[x, y] = go.GetComponent<Chessman>();
        chessmens[x, y].SetPosition(x, y);
        activeChessman.Add(go);

        if (chessmens[x, y].isWhite == false)
        {
            EasyLevel.Instance.addChessman(chessmens[x, y]);

        }

    }

    private void SpawnAllChessmans()
    {
        pause = false;

        activeChessman = new List<GameObject>();
        chessmens = new Chessman[8, 8];
        //Black Team
        //King
        SpawnChessman(0, 4, 0);

        //Queen
        SpawnChessman(1, 3, 0);

        //Rooks
        SpawnChessman(2, 0, 0);
        SpawnChessman(2, 7, 0);

        //Bishop
        SpawnChessman(3, 2, 0);
        SpawnChessman(3, 5, 0);

        //Knight
        SpawnChessman(4, 1, 0); 
        SpawnChessman(4, 6, 0);

        //Pawns
        for (int i=0;i<8; i++)
        {
            SpawnChessman(5, i, 1);
        }

        //White Team
        //King
        SpawnChessman(6, 4, 7);

        //Queen
        SpawnChessman(7, 3, 7);

        //Rooks
        SpawnChessman(8, 0, 7);
        SpawnChessman(8, 7, 7);

        //Bishop
        SpawnChessman(9, 2, 7);
        SpawnChessman(9, 5, 7);

        //Knight
        SpawnChessman(10, 1, 7);
        SpawnChessman(10, 6, 7);

        //Pawns
        for (int i = 0; i < 8; i++)
        {
            SpawnChessman(11, i, 6);
        }

        
    }

    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        return origin;
    }

    private void DrawChessboard()
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heighthLine = Vector3.forward * 8;

        for (int i =0; i <= 8; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
            for (int j = 0; j <= 8; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heighthLine);
            }
        }

        //Draw the selection
        if(selectionX >=0 && selectionY >= 0)
        {
            Debug.DrawLine(Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));
            Debug.DrawLine(Vector3.forward * (selectionY+1) + Vector3.right * (selectionX),
                Vector3.forward * (selectionY) + Vector3.right * (selectionX + 1));
        }

    }

    public void EndGame()
    {
        if (isWhiteTurn)
            text1.SetActive(true);
        else
            text2.SetActive(true);

        foreach (GameObject go in activeChessman)
            Destroy(go);

        //BoardManager.Instance.pause = true;

        isWhiteTurn = true;
        //BoardHighlight.Instance.Hidehighlights();
        TileHighlight.Instance.CloseHighlight();
        SpawnAllChessmans();
        if (doubleAI)
        {
            doubleAI = false;
           // UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        
    }

    public void PawnPromotion(Chessman piece, int x, int y)
    {
        activeChessman.Remove(piece.gameObject);
        if (piece.isWhite)
            SpawnChessman(1, x, y);
        else
            SpawnChessman(7, x, y);
        Destroy(piece.gameObject);
    }
    public void Restart() {
        foreach (GameObject go in activeChessman)
            Destroy(go);

        //BoardManager.Instance.pause = true;

        isWhiteTurn = true;
        //BoardHighlight.Instance.Hidehighlights();
        TileHighlight.Instance.CloseHighlight();
        SpawnAllChessmans();
        pause = false;

        AITurn = false;
        WhiteFirst = false;
        playWithAI = false;
        Highlevel = true;
        isComputing = true;
        doubleAI = true;
    }

}
