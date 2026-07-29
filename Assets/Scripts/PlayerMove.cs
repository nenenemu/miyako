using UnityEngine;

public class Playermove : MonoBehaviour
{
    [System.Serializable]
    public class PlayerData
    {
        public Transform player;          // プレイヤーのオブジェクト
        public float moveSpeed = 5f;      // 移動速度
        public Vector2 offset;            // マスの中心からのズレ補正
        public KeyCode up;
        public KeyCode down;
        public KeyCode left;
        public KeyCode right;

        [HideInInspector] public bool isMoving = false;
        [HideInInspector] public Vector3 targetPos;
    }

    public Vector2 cellSize = new Vector2(1f, 1f); // 1マスの大きさ
    public PlayerData player1;
    public PlayerData player2;

    void Update()
    {
        HandlePlayer(player1);
        HandlePlayer(player2);
    }

    void HandlePlayer(PlayerData p)
    {
        if (p.isMoving)
        {
            p.player.position = Vector3.MoveTowards(p.player.position, p.targetPos, p.moveSpeed * Time.deltaTime);

            if (Vector3.Distance(p.player.position, p.targetPos) < 0.01f)
                p.isMoving = false;

            return;
        }

        Vector2Int dir = Vector2Int.zero;

        if (Input.GetKeyDown(p.up)) dir = Vector2Int.up;
        if (Input.GetKeyDown(p.down)) dir = Vector2Int.down;
        if (Input.GetKeyDown(p.left)) dir = Vector2Int.left;
        if (Input.GetKeyDown(p.right)) dir = Vector2Int.right;

        if (dir != Vector2Int.zero)
        {
            Vector3 move = new Vector3(dir.x * cellSize.x, dir.y * cellSize.y, 0);
            p.targetPos = p.player.position + move + (Vector3)p.offset;
            p.isMoving = true;
        }
    }
}
