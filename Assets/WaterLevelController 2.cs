using UnityEngine;

public class WaterLevelController : MonoBehaviour
{
    public float riseSpeed = 0.3f; // Speed at which the water level rises

    private void Update()
    {
        transform.Translate(Vector3.up * riseSpeed * Time.deltaTime);
    }
}
