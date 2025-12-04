using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform target;
    public Gozilla player;  // tu script del jugador
    public float baseAhead = 2f;
    public float aheadPerSpeed = 0.1f;
    public float smoothSpeed = 0.15f;

    void LateUpdate()
    {
        if (target == null || player == null) return;

        float aheadDistance = baseAhead * aheadPerSpeed;

        float targetX = target.position.x + aheadDistance;

        Vector3 desiredPosition = new Vector3(
            targetX,
            transform.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
