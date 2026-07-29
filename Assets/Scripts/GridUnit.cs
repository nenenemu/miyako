using UnityEngine;

public class GridUnit : MonoBehaviour
{
    [Header("現在いるマス")]
    public Vector2Int currentCell;

    [HideInInspector] public Vector2Int startCell;
    [HideInInspector] public Vector2Int targetCell;

    [HideInInspector] public bool isMoving;
    [HideInInspector] public bool switchedCell;

    // ★ マスを切り替えたフレーム
    [HideInInspector] public int switchedFrame = -1;

    public void Init(Vector2 worldPos)
    {
        currentCell = new Vector2Int(
            Mathf.RoundToInt(worldPos.x),
            Mathf.RoundToInt(worldPos.y)
        );
    }
}
