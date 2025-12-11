Unity 3D Chess AI (Minimax Implementation)
這是一個基於 Unity 開發的 3D 西洋棋遊戲專案中的 AI 決策模組。本模組使用 C# 實作 Minimax (極小化極大演算法)，透過遞迴建立賽局樹 (Game Tree)，讓電腦對手能夠預測未來步數並做出最佳決策。

功能特色 (Features)
Minimax 演算法：模擬雙方（AI 與玩家）的最佳策略，尋找全域最佳解。

遞迴決策 (Recursive Decision Making)：支援設定搜尋深度 (Depth)，平衡運算效能與 AI 智力。

盤面評估 (Heuristic Evaluation)：基於棋子數量 (Material) 與位置優勢 (Position) 計算當前局勢分數。

核心邏輯 (Core Logic)
1. 建立賽局樹 (Game Tree)
AI 不僅僅看當前的一步，而是透過 Minimax 函式進行深度優先搜尋 (DFS)。

Maximizing Player (AI)：嘗試尋找分數最高的步數。

Minimizing Player (Opponent)：假設對手會選擇對 AI 最不利（分數最低）的步數。

2. 評估函式 (Evaluation Function)
當遞迴達到指定深度 (Depth = 0) 或遊戲結束時，EvaluateBoard 函式會計算靜態分數：

Material Score：計算雙方剩餘棋子的價值總和。

Positional Score：根據棋盤控制權（如是否佔領中心格）給予額外加分。
