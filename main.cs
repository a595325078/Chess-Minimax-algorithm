using System;
using System.Collections.Generic;

public class ChessAI
{
    public Move GetBestMove(Board board, int depth)
    {
        Move bestMove = null;
        int bestValue = int.MinValue;

        List<Move> possibleMoves = board.GetPossibleMoves();

        foreach (Move move in possibleMoves)
        {
            board.MakeMove(move);
            int boardValue = Minimax(board, depth - 1, false);
            board.UndoMove(move);

            if (boardValue > bestValue)
            {
                bestValue = boardValue;
                bestMove = move;
            }
        }

        return bestMove;
    }

    private int Minimax(Board board, int depth, bool isMaximizingPlayer)
    {
        if (depth == 0 || board.IsGameOver())
        {
            return EvaluateBoard(board);
        }

        if (isMaximizingPlayer)
        {
            int maxEval = int.MinValue;
            foreach (Move move in board.GetPossibleMoves())
            {
                board.MakeMove(move);
                int eval = Minimax(board, depth - 1, false);
                board.UndoMove(move);
                maxEval = Math.Max(maxEval, eval);
            }
            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            foreach (Move move in board.GetPossibleMoves())
            {
                board.MakeMove(move);
                int eval = Minimax(board, depth - 1, true);
                board.UndoMove(move);
                minEval = Math.Min(minEval, eval);
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
        if (board.IsCenterControlledByWhite()) positionalBonus += 5;
        return positionalBonus;
    }
}