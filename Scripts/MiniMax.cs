using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class MiniMax : MonoBehaviour
{
    public static MiniMax Instance { set; get; }

    //private List<Chessman> blackChessman;

    int maxDepth = 3;

    static int threadCount = 0;

    Weights weights = new Weights();

    int white_score = 0;
    int black_score = 0;

    int bestScore = -10000000;
    int currentScore = -1000000;

    int humanBestScore = 10000000;
    int humanCurrentScore = 10000000;

    //List<Chessman> activeChessman;

    List<Chessman> blackChessman;
    List<Chessman> whiteChessman;

    List<Vector2Int> black_best_step;
    List<Vector2Int> white_best_step;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        //activeChessman = new List<Chessman>();
        whiteChessman = new List<Chessman>();
        blackChessman = new List<Chessman>();

        black_best_step = new List<Vector2Int>();
        white_best_step = new List<Vector2Int>();
    }
    

    public List<Vector2Int> UpdateStep(bool black)
    {
        //activeChessman.Clear();
        whiteChessman.Clear();
        blackChessman.Clear();

        InitializeChessmanLists();

        if (black)
        {
            bestScore = -10000000;
            currentScore = -10000000;
        }
        else
        {
            humanBestScore = 10000000;
            currentScore = 10000000;
        }
        //best_step.Clear();
        black_best_step.Clear();
        white_best_step.Clear();
        Alpha_Beta(maxDepth, -10000000, 10000000, black);

        if (black)
        {
            if (black_best_step == null)
            {
                Chessman piece = blackChessman[Random.Range(0, blackChessman.Count)];
                black_best_step.Add(new Vector2Int(piece.currentX, piece.currentY));
                Vector2Int step = possibleMoves(piece)[Random.Range(0, possibleMoves(piece).Count)];
                black_best_step.Add(step);
            }
            return black_best_step;
        }
        else
        {
            if (white_best_step == null)
            {
                Chessman piece = whiteChessman[Random.Range(0, whiteChessman.Count)];
                black_best_step.Add(new Vector2Int(piece.currentX, piece.currentY));
                Vector2Int step = possibleMoves(piece)[Random.Range(0, possibleMoves(piece).Count)];
                black_best_step.Add(step);
            }

            return white_best_step;
        }

        
    }

    private void InitializeChessmanLists()
    {
        var go = BoardManager.Instance.GetActiveChessman().GetEnumerator();
        while (go.MoveNext())
        {
            Chessman piece = go.Current.GetComponent<Chessman>();
            //activeChessman.Add(piece);
            if (piece.isWhite)
            {
                whiteChessman.Add(piece);
            }
            else
            {
                blackChessman.Add(piece);
            }
        }
        go.Dispose();
    }


    int Alpha_Beta(int depth, int alpha, int beta, bool max)
    {
        white_score = 0;
        black_score = 0;
        if (depth == 0)
        {
            return _Evaluate();
        }

        if (max)
        {
            int score = -10000000;
            Dictionary<Chessman, List<Vector2Int>> all_moves = new Dictionary<Chessman, List<Vector2Int>>();

            var go = blackChessman.GetEnumerator();
            while (go.MoveNext())
            {
                all_moves.Add(go.Current, possibleMoves(go.Current));
            }
            go.Dispose();


            var seletedChessman = all_moves.GetEnumerator();
            while (seletedChessman.MoveNext())
            {
                bool prune = false;
                Vector2Int chessman_position = new Vector2Int(seletedChessman.Current.Key.currentX, seletedChessman.Current.Key.currentY);

                List<Vector2Int> destinations = new List<Vector2Int>();
                destinations = seletedChessman.Current.Value;

                var positions = destinations.GetEnumerator();
                while (positions.MoveNext())
                {
                    Chessman move_to_chessman = BoardManager.Instance.chessmens[positions.Current.x, positions.Current.y];
                    Vector2Int move_to_position = positions.Current;

                    AssumeSwap(seletedChessman.Current.Key, chessman_position, move_to_chessman, move_to_position);

                    //IEnumerator compute()
                    //{
                    //    yield return null;

                    //    score = Alpha_Beta(depth - 1, alpha, beta, false);  
                        
                    //}

                    //StartCoroutine(compute());
                    


                    score = Alpha_Beta(depth - 1, alpha, beta, false);

                    UndoAssumption(seletedChessman.Current.Key, chessman_position, move_to_chessman, move_to_position);
                    if (score > alpha)
                    {
                        currentScore = score;
                        if (currentScore > bestScore && depth == maxDepth)
                        {
                            black_best_step.Clear();
                            black_best_step.Add(chessman_position);
                            black_best_step.Add(move_to_position);
                        }
                        alpha = score;
                    }

                    if (score >= beta)
                    {
                        prune = true;
                        break;
                    }
                }
                positions.Dispose();
                if (prune)
                    break;
            }
            seletedChessman.Dispose();
            return alpha;
        }
        else
        {
            int score = 10000000;
            Dictionary<Chessman, List<Vector2Int>> all_moves = new Dictionary<Chessman, List<Vector2Int>>();

            var go = whiteChessman.GetEnumerator();
            while (go.MoveNext())
            {
                all_moves.Add(go.Current, possibleMoves(go.Current));   
            }
            go.Dispose();

            var seletedChessman = all_moves.GetEnumerator();
            while (seletedChessman.MoveNext())
            {
                bool prune = false;
                Vector2Int chessman_position = new Vector2Int(seletedChessman.Current.Key.currentX, seletedChessman.Current.Key.currentY);

                List<Vector2Int> destinations = new List<Vector2Int>();
                destinations = seletedChessman.Current.Value;

                var positions = destinations.GetEnumerator();
                while (positions.MoveNext())
                {
                    Chessman move_to_chessman = BoardManager.Instance.chessmens[positions.Current.x, positions.Current.y];
                    Vector2Int move_to_position = positions.Current;

                    AssumeSwap(seletedChessman.Current.Key, chessman_position, move_to_chessman, move_to_position);

                    //IEnumerator compute()
                    //{
                    //    yield return null;
                        
                    //    score = Alpha_Beta(depth - 1, alpha, beta, true);
                        

                    //}
                    score = Alpha_Beta(depth - 1, alpha, beta, true);

                    //StartCoroutine(compute());
                    

                    UndoAssumption(seletedChessman.Current.Key, chessman_position, move_to_chessman, move_to_position);
                    if (score < beta)
                    {
                        beta = score;
                        humanCurrentScore = score;
                        if (humanCurrentScore < humanBestScore && depth == maxDepth)
                        {
                            white_best_step.Clear();
                            white_best_step.Add(chessman_position);
                            white_best_step.Add(move_to_position);
                        }

                    }
                    if (score <= alpha)
                    {
                        prune = true;
                        break;
                    }
                }
                positions.Dispose();
                if (prune)
                    break;
            }
            seletedChessman.Dispose();
            return beta;
        }
    }


    private void AssumeSwap(Chessman seleted_chessman, Vector2Int start_position, Chessman moved_chessman, Vector2Int end_position)
    {
        if (moved_chessman != null)
        {
            if (moved_chessman.isWhite)
            {
                whiteChessman.Remove(moved_chessman);
            }
            else
            {
                blackChessman.Remove(moved_chessman);
            }
            //activeChessman.Remove(moved_chessman);
        }
        
        seleted_chessman.SetPosition(end_position.x, end_position.y);
        
        BoardManager.Instance.chessmens[start_position.x, start_position.y] = null;
        BoardManager.Instance.chessmens[end_position.x, end_position.y] = seleted_chessman;
    }


    private void UndoAssumption(Chessman seleted_chessman, Vector2Int start_position, Chessman moved_chessman, Vector2Int end_position)
    {
        seleted_chessman.SetPosition(start_position.x, start_position.y);
        if (moved_chessman != null)
        {
            if (moved_chessman.isWhite)
            {
                whiteChessman.Add(moved_chessman);
            }
            else
            {
                blackChessman.Add(moved_chessman);
            }

        }
        BoardManager.Instance.chessmens[start_position.x, start_position.y] = seleted_chessman;
        BoardManager.Instance.chessmens[end_position.x, end_position.y] = moved_chessman;
    }


    private List<Vector2Int> possibleMoves(Chessman chessman)
    {
        bool[,] moves = new bool[8, 8];
        //moves = chessman.GetPossibleMoves();
        moves = chessman.PossibleMove();

        List<Vector2Int> positions = new List<Vector2Int>();

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (moves[i, j])
                {
                    positions.Add(new Vector2Int(i, j));
                }
            }
        }
        return positions;
    }

    private int _Evaluate()
    {
        float piece_diff = 0;
        float white_weight = 0;
        float black_weight = 0;

        var pieces = whiteChessman.GetEnumerator();
        while (pieces.MoveNext())
        {
            Vector2Int position = new Vector2Int(pieces.Current.currentX, pieces.Current.currentY);
            white_weight += weights.GetBoardWeight(pieces.Current, position);
            white_score += weights.GetPieceWeight(pieces.Current);
            
        }
        pieces.Dispose();

        pieces = blackChessman.GetEnumerator();
        while (pieces.MoveNext())
        {
            Vector2Int position = new Vector2Int(pieces.Current.currentX, pieces.Current.currentY);
            black_score += weights.GetBoardWeight(pieces.Current, position);
            black_score += weights.GetPieceWeight(pieces.Current);

        }
        pieces.Dispose();

        piece_diff = (black_score + (black_weight / 100)) - (white_score + (white_weight / 100));
        
        return Mathf.RoundToInt(piece_diff * 100);
    }

}
