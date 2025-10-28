using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour
{
    [Header("Elevator Settings")]
    public Transform pointA;       // Posisi bawah
    public Transform pointB;       // Posisi atas
    public float speed = 2f;
    public float waitTime = 0.5f;  // Jeda berhenti

    private Transform targetPoint;
    private bool isWaiting = false;

    [Header("Player Settings")]
    private Rigidbody2D playerRb;
    private GameObject player;
    private float originalGravity;

    private void Start()
    {
        targetPoint = pointB; // Awalnya ke atas
    }

    private void Update()
    {
        if (isWaiting) return; // kalau lagi nunggu, jangan gerak

        // Gerakkan elevator ke target
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // Jika sudah sampai ? ganti target (biar bolak-balik)
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.01f)
        {
            // Snap posisi biar rapi
            transform.position = new Vector3(
                targetPoint.position.x,
                targetPoint.position.y,
                transform.position.z
            );

            // targetPoint = (targetPoint == pointA) ? pointB : pointA;

            // Mulai jeda sebelum ganti target
            StartCoroutine(WaitAtPoint());
        }
    }

    private IEnumerator WaitAtPoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        // Ganti target setelah jeda
        targetPoint = (targetPoint == pointA) ? pointB : pointA;

        isWaiting = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            playerRb = player.GetComponent<Rigidbody2D>();
            originalGravity = playerRb.gravityScale;

            // Tempelkan player ke elevator
            player.transform.SetParent(transform);

            // Naikkan gravitasi agar lebih "nempel"
            playerRb.gravityScale = 50f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                player.transform.SetParent(null, true);
                playerRb.gravityScale = originalGravity;
            }

            player = null;
            playerRb = null;
        }
    }
}
