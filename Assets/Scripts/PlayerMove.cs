using UnityEngine;

public class Playermove : MonoBehaviour
{
    [System.Serializable]
    public class PlayerData
    {
        public Transform player;
        public GridUnit grid;

        public float moveSpeed = 5f;
        public Vector2 offset;

        public KeyCode up;
        public KeyCode down;
        public KeyCode left;
        public KeyCode right;

        [HideInInspector] public bool isMoving;
        [HideInInspector] public Vector3 startPos;
        [HideInInspector] public Vector3 targetPos;
    }

    public Vector2 cellSize = Vector2.one;

    public PlayerData player1;
    public PlayerData player2;

    void Start()
    {
        player1.grid.Init(player1.player.position);
        player2.grid.Init(player2.player.position);
    }

    void Update()
    {
        HandlePlayer(player1);
        HandlePlayer(player2);
    }

    void HandlePlayer(PlayerData p)
    {
        if (p.isMoving)
        {
            p.player.position = Vector3.MoveTowards(
                p.player.position,
                p.targetPos,
                p.moveSpeed * Time.deltaTime);

            float total = Vector3.Distance(p.startPos, p.targetPos);
            float now = Vector3.Distance(p.startPos, p.player.position);

            float t = total <= 0 ? 1 : now / total;

            if (!p.grid.switchedCell && t >= 0.5f)
            {
                p.grid.currentCell = p.grid.targetCell;
                p.grid.switchedCell = true;

                if (BattleManager.Instance != null)
                    BattleManager.Instance.CheckAttack(p.grid);
            }

            if (Vector3.Distance(p.player.position, p.targetPos) < 0.001f)
            {
                p.player.position = p.targetPos;

                p.grid.currentCell = p.grid.targetCell;
                p.grid.isMoving = false;

                p.isMoving = false;
            }

            return;
        }

        Vector2Int dir = Vector2Int.zero;

        if (Input.GetKeyDown(p.up))
            dir = Vector2Int.up;
        else if (Input.GetKeyDown(p.down))
            dir = Vector2Int.down;
        else if (Input.GetKeyDown(p.left))
            dir = Vector2Int.left;
        else if (Input.GetKeyDown(p.right))
            dir = Vector2Int.right;

        if (dir == Vector2Int.zero)
            return;

        p.grid.startCell = p.grid.currentCell;
        p.grid.targetCell = p.grid.currentCell + dir;

        p.grid.switchedCell = false;
        p.grid.isMoving = true;

        p.startPos = p.player.position;

        p.targetPos =
            new Vector3(
                p.grid.targetCell.x,
                p.grid.targetCell.y,
                0f)
            + (Vector3)p.offset;

        p.isMoving = true;
    }
}