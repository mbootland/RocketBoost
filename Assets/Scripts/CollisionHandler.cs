using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
   void OnCollisionEnter(Collision collision) {
    switch(collision.gameObject.tag) {
      case "Friendly":
        break;
      case "Finish":
        StartSuccessSequence();
        break;
      default:
        StartCrashSequence();
        break;
    }
  }

  void StartSuccessSequence()
  {
    // TODO: Add vfx and audio
    GetComponent<Movement>().enabled = false;
    Invoke("LoadNextLevel", 2f);
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
