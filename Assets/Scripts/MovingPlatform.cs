using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public Transform posA, posB;
    public float speed = 2f;
    public float waitTime = 0.5f;

    private Vector3 targetPos;
    private bool isWaiting = false;

    private void Start()
    {
        targetPos = posB.position;
    }

    private void Update()
    {
        if (!isWaiting)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.01f)
            {
                // snap supaya persis
                transform.position = targetPos;
                StartCoroutine(WaitAndSwitchTarget());
            }
        }
    }

    private IEnumerator WaitAndSwitchTarget()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        targetPos = (targetPos == posA.position) ? posB.position : posA.position;
        isWaiting = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(transform, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null, true);
        }
    }
}
