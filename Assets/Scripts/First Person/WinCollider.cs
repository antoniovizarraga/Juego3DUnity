using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCollider : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && player.secretEnding)
        {
            SceneManager.LoadScene("SecretEndingScene");
        } else if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("AnotherScene");
        }
    }
}
