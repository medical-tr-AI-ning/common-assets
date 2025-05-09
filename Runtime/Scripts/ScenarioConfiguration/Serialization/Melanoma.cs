using UnityEngine;

public class Melanoma : Lesion
{
    public float Spreading = 0f;
    public float Limitation = 0f;
    public float Elevation = 0f;
    public float Brightness = 0f;
    public float Contrast = 0f;
    public Vector2 ColorPosition = new Vector2(0f, 0f);
    public Texture2D BaseShape;
    public bool Placeable = true;
    public Vector3 Position;
    public string Placement;
    public float SizeUI;
    public GameObject collider;
    
    public Melanoma()
    {
        Size = 0.0234375f;
    }
    
    void Start()
    {
        //SphereCollider sc = gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;
        //sc.radius = 0.1f;
        //Rigidbody rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        //rb.useGravity = false;
        //rb.isKinematic = true;
        //int layer = LayerMask.NameToLayer("Melanoma");
        //gameObject.layer = layer;
    }

    private void OnCollisionStay(Collision other)
    {
        Placeable = false;
    }

    private void OnCollisionExit(Collision other)
    {
        Placeable = true;
    }
}
