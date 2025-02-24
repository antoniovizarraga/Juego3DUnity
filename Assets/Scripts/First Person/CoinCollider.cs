using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinCollider : MonoBehaviour
{

    [SerializeField] private PlayerController player;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.AddPoints();
            Destroy(gameObject);
        }
    }
}
