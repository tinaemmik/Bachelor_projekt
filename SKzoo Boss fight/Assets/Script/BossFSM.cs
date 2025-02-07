using UnityEngine;
using System.Collections;

public enum BossState
{
    Idle,       
    PhaseOne,   // Skyder ildkugler
    PhaseTwo,   // Kører med skjold
    PhaseThree  // Teleporterer
}

public class BossFSM : MonoBehaviour
{
    public BossState currentState = BossState.Idle;
    public Transform player;
    public float detectionRange = 10f;  // Afstand før bossen aktiveres
    private bool isActive = false;

    public GameObject fireballPrefab;  // Reference til ildkugler
    public Transform fireballSpawnPoint;
    public float fireballSpeed = 5f;
    public float chargeSpeed = 10f;
    public float phaseDuration = 5f;
    public GameObject shieldObject;  // Beskyttende skjold
    public Transform[] teleportPoints;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shieldObject.SetActive(false);  // Skjold starter inaktivt
    }

    void Update()
    {
        if (!isActive)
        {
            DetectPlayer();
            return;
        }

        switch (currentState)
        {
            case BossState.PhaseOne:
                ShootFireballs();
                break;
            case BossState.PhaseTwo:
                ChargeWithShield();
                break;
            case BossState.PhaseThree:
                TeleportRandomly();
                break;
        }
    }

    // Trigger: Spilleren træder ind i scenen
    void DetectPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < detectionRange)
        {
            isActive = true;
            StartCoroutine(PhaseCycle());
        }
    }

    // Skift mellem faser
    IEnumerator PhaseCycle()
    {
        while (isActive)
        {
            currentState = (BossState)Random.Range(1, 4);
            Debug.Log($"Boss skifter til: {currentState}");

            shieldObject.GetComponent<BossShield>().DeactivateShield();  // Deaktiver skjold ved fase-skift

            yield return new WaitForSeconds(phaseDuration);
        }
    }
    // Fase 1: Skyder ildkugler
    void ShootFireballs()
    {
        if (fireballPrefab != null && fireballSpawnPoint != null)
        {
            GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);
            Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
            Vector2 direction = (player.position - fireballSpawnPoint.position).normalized;
            rb.velocity = direction * fireballSpeed;
        }
    }

    // Fase 2: Kører med skjold
    void ChargeWithShield()
    {
        shieldObject.GetComponent<BossShield>().ActivateShield();  // Aktiver skjold
        float direction = (player.position.x > transform.position.x) ? 1f : -1f;
        rb.velocity = new Vector2(direction * chargeSpeed, 0f);
    }


    // Fase 3: Teleporterer
    void TeleportRandomly()
    {
        if (teleportPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, teleportPoints.Length);
            transform.position = teleportPoints[randomIndex].position;
        }
    }
}
