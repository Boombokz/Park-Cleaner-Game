using UnityEngine;

public abstract class Waste : MonoBehaviour
{
    protected Rigidbody wasteRb;
    private bool isJumped;
    private float JumpStrength = 5;
    protected int wasteMass = 3;

    public abstract void UseEffect();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag($"Ground") && !isJumped)
        {
            AddJump();
        }
    }

    private void AddJump()
    {
        wasteRb.AddForce(Vector3.up * JumpStrength, ForceMode.Impulse);
        isJumped = true;
    }
}