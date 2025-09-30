using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  [SerializeField] AudioClip crashSound;
  [SerializeField] AudioClip successSound;

  AudioSource audioSource;

  void Start()
  {
    audioSource = GetComponent<AudioSource>();
  }

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
    audioSource.PlayOneShot(successSound);
    GetComponent<Movement>().enabled = false;
    Invoke("LoadNextLevel", 2f);
  }
  void StartCrashSequence()
  {
    audioSource.PlayOneShot(crashSound);
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
