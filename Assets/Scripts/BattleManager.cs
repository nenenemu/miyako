using UnityEngine;
using UnityEngine.UI;


public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public GridUnit player1;
    public GridUnit player2;

    public int player1HP = 100;
    public int player2HP = 100;

    [Header("HPバー")]
    public Image player1HPBar;
    public Image player2HPBar;

    public int maxHP = 100;

    bool battleProcessed = false;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        player1HPBar.fillAmount = Mathf.Clamp01((float)player1HP / maxHP);
        player2HPBar.fillAmount = Mathf.Clamp01((float)player2HP / maxHP);
    }

    public void ResolveBattle()
    {
        // 同じマスじゃなければ戦闘解除
        if (player1.currentCell != player2.currentCell)
        {
            battleProcessed = false;
            return;
        }

        // すでに処理済みなら何もしない
        if (battleProcessed)
            return;

        bool p1Moved = player1.switchedFrame == Time.frameCount;
        bool p2Moved = player2.switchedFrame == Time.frameCount;

        Vector2Int p1Dir = player1.targetCell - player1.startCell;
        Vector2Int p2Dir = player2.targetCell - player2.startCell;

        // ============================
        // ① 完全同時 → 相打ち
        // ============================
        if (player1.isMoving &&
            player2.isMoving &&
            p1Moved &&
            p2Moved)
        {
            // 正面衝突 or すれ違い（両者が動いている時だけ）
            if (p1Dir != Vector2Int.zero &&
                p2Dir != Vector2Int.zero &&
                p1Dir == -p2Dir)
            {
                Debug.Log("相打ち！（正面衝突 or すれ違い）");
                player1HP -= 10;
                player2HP -= 10;
                battleProcessed = true;
                return;
            }

            // 完全同時（方向違いでも相打ち）
            Debug.Log("相打ち！（完全同時）");
            player1HP -= 10;
            player2HP -= 10;
            battleProcessed = true;
            return;
        }

        // ============================
        // ② 正面衝突（両者とも移動中のみ）
        // ============================

        if (player1.isMoving &&
            player2.isMoving &&
            p1Dir != Vector2Int.zero &&
            p2Dir != Vector2Int.zero &&
            p1Dir == -p2Dir)
        {
            Debug.Log("相打ち！（正面衝突）");

            player1HP -= 10;
            player2HP -= 10;

            battleProcessed = true;
            return;
        }

        // ============================
        // ③ マス交換（すれ違い）
        // ============================
        if (player1.startCell == player2.targetCell &&
            player2.startCell == player1.targetCell)
        {
            Debug.Log("相打ち！（マス交換）");
            player1HP -= 10;
            player2HP -= 10;
            battleProcessed = true;
            return;
        }

        // ============================
        // ④ 後入り攻撃（基本ルール）
        // ============================
        if (p1Moved && !p2Moved)
        {
            Debug.Log("P1攻撃！（後入り）");
            player2HP -= 10;
            battleProcessed = true;
            return;
        }

        if (!p1Moved && p2Moved)
        {
            Debug.Log("P2攻撃！（後入り）");
            player1HP -= 10;
            battleProcessed = true;
            return;
        }
    }
}
