using UnityEngine;

public class ThroughFloor : MonoBehaviour
{

    private PlatformEffector2D _platformEffector;

    void Awake()
    {
        _platformEffector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            _platformEffector.rotationalOffset = 180f;
        if (Input.GetKey(KeyCode.Space))
            _platformEffector.rotationalOffset = 0f;
    }
}