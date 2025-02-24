using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Venezolano"))
        {
            SceneManager.LoadScene("DeathScene");

            Debug.Log("Venezolanoooo");
        } else
        {
            Debug.Log("No venezolano");
        }
        
    }
}
