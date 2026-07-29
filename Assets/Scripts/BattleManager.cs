using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public GridUnit player1;
    public GridUnit player2;

    public int player1HP = 100;
    public int player2HP = 100;

    void Awake()
    {
        Instance = this;
    }

    public void CheckAttack(GridUnit attacker)
    {
        GridUnit defender;

        if (attacker == player1)
            defender = player2;
        else
            defender = player1;

        // “¯‚¶ƒ}ƒX‚¶‚á‚È‚¢‚È‚ç‰½‚à‚µ‚È‚¢
        if (attacker.currentCell != defender.currentCell)
            return;

        // UŒ‚
        if (attacker == player1)
        {
            player2HP -= 10;
            Debug.Log("P1UŒ‚I");
            Debug.Log("P2 HP : " + player2HP);
        }
        else
        {
            player1HP -= 10;
            Debug.Log("P2UŒ‚I");
            Debug.Log("P1 HP : " + player1HP);
        }
    }
}