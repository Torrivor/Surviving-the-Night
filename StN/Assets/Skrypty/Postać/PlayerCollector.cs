using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStats player;
    CircleCollider2D playerCollector;
    public float pullSpeed;
    private Transform collectibleTransform; // Referencja do przyci�ganego przedmiotu

    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        playerCollector = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        playerCollector.radius = player.CurrentMagnet;

        if (collectibleTransform != null)
        {
            // Przemieszczanie przedmiotu w kierunku gracza
            collectibleTransform.position = Vector2.MoveTowards(collectibleTransform.position,transform.position,pullSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Sprawdza czy inny obiekt ma interfejs ICollectible
        if (col.gameObject.TryGetComponent(out ICollectible collectible))
        {
            // Ustawia referencj� do przyci�ganego przedmiotu
            collectibleTransform = col.transform;
            collectible.Collect();
        }
    }
}
