using UnityEngine;

public class Atom : Waste
{
    [SerializeField] private ParticleSystem _particleSystem;

    private void Start()
    {
        wasteRb = GetComponent<Rigidbody>();
        UseEffect();
    }

    public override void UseEffect()
    {
        Invoke(nameof(AddParticle), 1f);
    }

    private void AddParticle()
    {
        _particleSystem.Play();
    }
}