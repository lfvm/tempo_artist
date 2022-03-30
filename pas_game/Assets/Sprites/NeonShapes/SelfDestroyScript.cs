using UnityEngine;

public class SelfDestroyScript : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 3.0f);
    }
}