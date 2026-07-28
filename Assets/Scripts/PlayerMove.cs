using UnityEngine;

public class TwoPlayerGridMove : MonoBehaviour
{
    [System.Serializable]
    public class PlayerData
    {
        [HideInInspector] public bool arrivedThisFrame;
        [HideInInspector] public bool movedThisFrame;
        [HideInInspector] public bool pushOut;
        [HideInInspector] public Vector2Int previousCell;
        [HideInInspector] public Vector2Int moveDir;
        [HideInInspector] public bool isInsideSameCell;

        public Transform player;
        public float moveSpeed = 5f;
        public Vector2 offset;
        public KeyCode up;
        public KeyCode down;
        public KeyCode left;
        public KeyCode right;

        [HideInInspector] public bool isMoving = false;
        [HideInInspector] public Vector3 targetPos;
        [HideInInspector] public bool attacking;
    }

    public Vector2 cellSize = new Vector2(1f, 1f);
    public PlayerData player1;
    public PlayerData player2;


    void Update()
    {
        player1.arrivedThisFrame = false;
        player2.arrivedThisFrame = false;

        MovePlayer(player1);
        MovePlayer(player2);

        InputPlayer(player1);
        InputPlayer(player2);

        CheckGridCollision();
    }



    void InputPlayer(PlayerData p)
    {
        if (p.isMoving)
            return;


        Vector2Int dir = Vector2Int.zero;


        if (Input.GetKeyDown(p.up))
            dir = Vector2Int.up;

        if (Input.GetKeyDown(p.down))
            dir = Vector2Int.down;

        if (Input.GetKeyDown(p.left))
            dir = Vector2Int.left;

        if (Input.GetKeyDown(p.right))
            dir = Vector2Int.right;


        if (dir == Vector2Int.zero)
            return;



        p.moveDir = dir;


        // 移動前マス保存
        p.previousCell =
            new Vector2Int(
                Mathf.FloorToInt(p.player.position.x),
                Mathf.FloorToInt(p.player.position.y)
            );


        p.targetPos =
            p.player.position +
            new Vector3(
                dir.x * cellSize.x,
                dir.y * cellSize.y,
                0
            );

        p.isMoving = true;
        p.movedThisFrame = true;
        //p.enteredThisFrame = true;
    }

    void CheckGridCollision()
    {
        Vector2Int p1Cell =
            new Vector2Int(
                Mathf.RoundToInt(player1.player.position.x),
                Mathf.RoundToInt(player1.player.position.y)
            );


        Vector2Int p2Cell =
            new Vector2Int(
                Mathf.RoundToInt(player2.player.position.x),
                Mathf.RoundToInt(player2.player.position.y)
            );


        // ★同じマスじゃないなら終了
        if (p1Cell != p2Cell)
        {
            return;
        }


        // ★移動完了した人だけ攻撃権あり
        bool p1Attack = player1.arrivedThisFrame;
        bool p2Attack = player2.arrivedThisFrame;


        // 誰も入ってきてない
        if (!p1Attack && !p2Attack)
        {
            return;
        }



        // =====================
        // 同時侵入
        // =====================

        if (p1Attack && p2Attack)
        {
            Vector2Int p1Dir =
                p1Cell - player1.previousCell;

            Vector2Int p2Dir =
                p2Cell - player2.previousCell;


            // 正面衝突
            if (p1Dir == -p2Dir)
            {
                Debug.Log("相打ち");

                Damage(player1);
                Damage(player2);

                return;
            }
        }



        // =====================
        // P1が後から入った
        // =====================

        if (p1Attack)
        {
            Debug.Log("P1攻撃");

            Damage(player2);

            player1.pushOut = true;

            return;
        }



        // =====================
        // P2が後から入った
        // =====================

        if (p2Attack)
        {
            Debug.Log("P2攻撃");

            Damage(player1);

            player2.pushOut = true;

            return;
        }
    }

    void Damage(PlayerData p)
    {
        PlayerHP hp =
            p.player.GetComponent<PlayerHP>();

        if (hp != null)
        {
            hp.TakeDamage(1);
        }
    }

    void MovePlayer(PlayerData p)
    {
        if (!p.isMoving)
            return;


        p.player.position =
            Vector3.MoveTowards(
                p.player.position,
                p.targetPos,
                p.moveSpeed * Time.deltaTime
            );


        if (Vector3.Distance(p.player.position, p.targetPos) < 0.001f)
        {
            p.player.position = p.targetPos;

            p.isMoving = false;

            p.arrivedThisFrame = true;
        }
    }
}
