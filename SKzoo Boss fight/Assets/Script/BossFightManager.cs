using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BossFightManager : MonoBehaviour
{
    public PlayerStatistics playerStats = new PlayerStatistics();
    private float lastAttackTime;

    public void OnPlayerDealsDamage(int damage)
    {
        playerStats.damageDealt += damage;
    }
    public void OnPlayerTakesDamage(int damage)
    {
        playerStats.damageTaken += damage;
    }
    public void OnPlayerDodgesAttack()
    {
        playerStats.attacksDodged++;

        float currentTime = Time.time;
        float timeSinceLastAttack = currentTime - lastAttackTime;
        playerStats.reactionTime = (playerStats.reactionTime + timeSinceLastAttack) / 2;
        lastAttackTime = currentTime;
    }
    public void EndBossFight(){

        Debug.Log("Boss-kampen slut. Statistikker:");
        Debug.Log($"Skade pårført: {playerStats.damageDealt}");
        Debug.Log($"Skade taget: {playerStats.damageTaken}");
        Debug.Log($"Angreb undgået: {playerStats.attacksDodged}");
        Debug.Log($"Reaktionstid: {playerStats.reactionTime}");
    }
}
