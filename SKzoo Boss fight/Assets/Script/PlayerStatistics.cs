using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
    public int damageDealt; //Skade påført bossen
    public int damageTaken; // Skade taget fra bossen
    public int attacksDodged; // Angreb undgået
    public float reactionTime; // Reaktionstid

    public PlayerStatistics()
    {
        ResetStats();
    }
    // Start is called before the first frame update
   public void ResetStats()
   {
         damageDealt = 0;
         damageTaken = 0;
         attacksDodged = 0;
         reactionTime = 0f;
   }
}
