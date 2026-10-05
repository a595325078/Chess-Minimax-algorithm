using System;
using System.Collections.Generic;

public class ChessAI
{
    // Backward-compatible overload.
    // By default, the AI is assumed to play White.
    public Move GetBestMove(Board board, int depth)
    {
        return GetBestMove(board, depth, true);
    }

    // Evaluation convention:
    // positive score = White advantage
    // negative score = Black advantage
    public Move GetBestMove(Board board, int depth, bool aiIsWhite)
    {
        if (board == null)
            throw new ArgumentNullException(nameof(board));

        if (depth <= 0)
            throw new ArgumentOutOfRangeException(nameof(depth), "Depth must be greater than 0.");

        Move bestMove = null;
        int bestValue = aiIsWhite ? int.MinValue : int.MaxValue;

        List<Move> possibleMoves = board.GetPossibleMoves();

        foreach (Move move in possibleMoves)
        {
            board.MakeMove(move);

            int boardValue = Minimax(
                board,
                depth - 1,
                int.MinValue,
                int.MaxValue,
                !aiIsWhite
            );

            board.UndoMove(move);

            if (aiIsWhite)
            {
                if (boardValue > bestValue)
                {
                    bestValue = boardValue;
                    bestMove = move;
                }
            }
            else
            {
                if (boardValue < bestValue)
                {
                    bestValue = boardValue;
                    bestMove = move;
                }
            }
        }

        return bestMove;
    }

    private int Minimax(
        Board board,
        int depth,
        int alpha,
        int beta,
        bool isMaximizingPlayer)
    {
        if (depth <= 0 || board.IsGameOver())
        {
            return EvaluateBoard(board);
        }

        List<Move> possibleMoves = board.GetPossibleMoves();

        if (isMaximizingPlayer)
        {
            int maxEval = int.MinValue;

            foreach (Move move in possibleMoves)
            {
                board.MakeMove(move);

                int eval = Minimax(
                    board,
                    depth - 1,
                    alpha,
                    beta,
                    false
                );

                board.UndoMove(move);

                maxEval = Math.Max(maxEval, eval);
                alpha = Math.Max(alpha, eval);

                if (beta <= alpha)
                    break;
            }

            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;

            foreach (Move move in possibleMoves)
            {
                board.MakeMove(move);

                int eval = Minimax(
                    board,
                    depth - 1,
                    alpha,
                    beta,
                    true
                );

                board.UndoMove(move);

                minEval = Math.Min(minEval, eval);
                beta = Math.Min(beta, eval);

                if (beta <= alpha)
                    break;
            }

            return minEval;
        }
    }

    private int EvaluateBoard(Board board)
    {
        int score = 0;

        score += GetMaterialScore(board);
        score += GetPositionalScore(board);

        return score;
    }

    private int GetMaterialScore(Board board)
    {
        int whiteScore = board.WhitePieces.Count * 10;
        int blackScore = board.BlackPieces.Count * 10;

        return whiteScore - blackScore;
    }

    private int GetPositionalScore(Board board)
    {
        int positionalBonus = 0;

        if (board.IsCenterControlledByWhite())
            positionalBonus += 5;

        return positionalBonus;
    }
}
