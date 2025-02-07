using UnityEngine;

public class BossShield : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color activeColor = Color.blue;   // Skjoldets farve, når aktivt
    public Color inactiveColor = new Color(1f, 1f, 1f, 0f); // Gør skjold usynligt når inaktivt

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        DeactivateShield(); // Sørger for, at skjoldet starter usynligt
    }

    // Aktiver skjold
    public void ActivateShield()
    {
        spriteRenderer.color = activeColor;
        gameObject.SetActive(true);
    }

    // Deaktiver skjold
    public void DeactivateShield()
    {
        spriteRenderer.color = inactiveColor;
        gameObject.SetActive(false);
    }

    // Hvis skjoldet rammes af et angreb, skal det blokere skaden
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile")) // Antag, at spillerens angreb har dette tag
        {
            Debug.Log("Skjoldet blokerede angrebet!");
            Destroy(collision.gameObject); // Fjerner spillerens projektil
        }
    }
}

