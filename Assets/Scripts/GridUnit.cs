using UnityEngine;

public class GridUnit : MonoBehaviour
{
    [Header("åªç›Ç¢ÇÈÉ}ÉX")]
    public Vector2Int currentCell;

    [HideInInspector] public Vector2Int startCell;
    [HideInInspector] public Vector2Int targetCell;

    [HideInInspector] public bool isMoving;
    [HideInInspector] public bool switchedCell;

    public void Init(Vector2 worldPos)
    {
        currentCell = new Vector2Int(
            Mathf.RoundToInt(worldPos.x),
            Mathf.RoundToInt(worldPos.y)
        );
    }
}