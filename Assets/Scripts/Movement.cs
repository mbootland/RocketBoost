using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    [SerializeField] float thrustForce = 70f;
    [SerializeField] float rotationForce = 190f;
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] AudioClip thrustSound;
    [SerializeField] ParticleSystem thrusterParticles;
    [SerializeField] ParticleSystem leftSideThrusterParticles;
    [SerializeField] ParticleSystem rightSideThrusterParticles;
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
      engageThrusters();
    }
    else {
      audioSource.Stop();
      thrusterParticles.Stop();
    }
  }

  void ProcessRotation()
  {
    if (rotation.IsPressed())
    {
      Rotate();
    }
    else {
      leftSideThrusterParticles.Stop();
      rightSideThrusterParticles.Stop();
    }
  }



  void engageThrusters()
  {
    rb.AddRelativeForce(Vector3.up * thrustForce);
    thrusterParticles.Play();
    if (!audioSource.isPlaying)
    {
      audioSource.PlayOneShot(thrustSound);
    }
  }

  void Rotate() {
    float rotationInput = rotation.ReadValue<float>();
    if (rotationInput < 0)
    {
      transform.Rotate(Vector3.forward * rotationForce * Time.fixedDeltaTime);
      leftSideThrusterParticles.Play();
    }
    if (rotationInput > 0)
    {
      transform.Rotate(Vector3.back * rotationForce * Time.fixedDeltaTime);
      rightSideThrusterParticles.Play();
    }
  }
}
