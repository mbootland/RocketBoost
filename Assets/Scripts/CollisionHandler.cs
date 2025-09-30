using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
   void OnCollisionEnter(Collision collision) {
    switch(collision.gameObject.tag) {
      case "Friendly":
        break;
      case "Finish":
        Invoke("LoadNextLevel", 2f);
        break;
      default:
        StartCrashSequence();
        break;
    }
  }
  void StartCrashSequence()
  {
    GetComponent<Movement>().enabled = false;
    Invoke("ReloadLevel", 2f);
  }
  void LoadNextLevel()
  {
    int currentScene = SceneManager.GetActiveScene().buildIndex;
    int nextScene = currentScene + 1;

    if (nextScene == SceneManager.sceneCountInBuildSettings)
    {
      nextScene = 0;
    }

    SceneManager.LoadScene(nextScene);
  }

  void ReloadLevel()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}
