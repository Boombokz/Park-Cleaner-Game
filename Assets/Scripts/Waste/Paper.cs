using UnityEngine;

public class Paper : Waste
{
    private new int wasteMass = 5;

    private void Start()
    {
        wasteRb = GetComponent<Rigidbody>();
        UseEffect();
    }

    public override void UseEffect()
    {
        Invoke(nameof(ChangePaperColor), 2f);
    }

    private void ChangePaperColor()
    {
        MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();

        foreach (var mesh in meshRenderers)
        {
            mesh.material.color = Color.blue;
        }
    }
}