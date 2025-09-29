using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    [SerializeField] float thrustForce = 100f;
    [SerializeField] float rotationForce = 100f;
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] AudioClip thrustSound;
    Rigidbody rb;

    AudioSource audioSource;

    void Start() {
      rb = GetComponent<Rigidbody>();
      audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable() {
      thrust.Enable();
      rotation.Enable();
  }

    void FixedUpdate () {
      ProcessThrust();
      ProcessRotation();
    }

  void ProcessThrust()
  {
    if (thrust.IsPressed())
    {
      rb.AddRelativeForce(Vector3.up * thrustForce);
      if(!audioSource.isPlaying) {
        audioSource.PlayOneShot(thrustSound);
      }
    }
    else {
      audioSource.Stop();
    }
  }

  void ProcessRotation()
  {
    if (rotation.IsPressed())
    {
      float rotationInput = rotation.ReadValue<float>();
      if(rotationInput < 0) {
        transform.Rotate(Vector3.forward * rotationForce * Time.fixedDeltaTime);
      }
      if(rotationInput > 0) {
        transform.Rotate(Vector3.back * rotationForce * Time.fixedDeltaTime);
      }
    }
  }
}
