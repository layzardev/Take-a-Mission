using UnityEngine;
using System.Collections;
using EasyTransition;

public class RespawnOnTouch : MonoBehaviour
{
    [Header("Respawn Settings")]
    public Transform respawnPoint;

    [Header("EasyTransition Settings")]
    public TransitionSettings transition;
    public float startDelay = 0.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(RespawnWithTransition(collision.gameObject));
        }
    }

    private IEnumerator RespawnWithTransition(GameObject player)
    {
        // disable PlayerMovement sementara
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        if (movement != null) movement.enabled = false;

        // jalankan transisi EasyTransition
        TransitionManager.Instance().Transition(gameObject.scene.name, transition, startDelay);

        // tunggu durasi transisi + startDelay
        yield return new WaitForSeconds(startDelay + transition.transitionTime);

        // pindahkan player ke respawn
        player.transform.position = respawnPoint.position;

        // reset velocity jika ada Rigidbody2D
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        // aktifkan kembali PlayerMovement
        if (movement != null) movement.enabled = true;
    }
}
