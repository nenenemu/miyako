using UnityEngine;

public class PlayerDamageTrigger : MonoBehaviour
{
    public PlayerHP hp;


    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerDamageTrigger enemy =
            other.GetComponent<PlayerDamageTrigger>();


        if (enemy == null)
            return;


        // ‘Šè‚Éƒ_ƒ[ƒW
        enemy.hp.TakeDamage(1);


        Debug.Log(
            gameObject.name +
            " ‚ªUŒ‚‚µ‚½"
        );
    }
}