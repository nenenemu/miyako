using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;

    public Image hpBar;

    void Start()
    {
        currentHP = maxHP;
        UpdateHPBar();
    }


    // TriggerÇ©ÇÁåƒÇŒÇÍÇÈ
    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        Debug.Log(
            gameObject.name +
            " É_ÉÅÅ[ÉW HP:" +
            currentHP
        );


        UpdateHPBar();


        if (currentHP <= 0)
        {
            Die();
        }
    }



    void UpdateHPBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount =
                (float)currentHP / maxHP;
        }
    }


    void Die()
    {
        Debug.Log(
            gameObject.name +
            " åÇîj"
        );

        gameObject.SetActive(false);
    }
}