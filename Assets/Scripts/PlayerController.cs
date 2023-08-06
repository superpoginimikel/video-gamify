using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public Transform playerTransform;
    private int hitPoints;
    private int boundarySize = 150;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0) * speed;
        Vector3 newPosition = playerTransform.localPosition + movement;
        float xBoundary = Mathf.Clamp(newPosition.x, -boundarySize, boundarySize);
        float yBoundary = Mathf.Clamp(newPosition.y, -boundarySize, boundarySize);
        newPosition.x = xBoundary;
        newPosition.y = yBoundary;
        transform.localPosition = newPosition;
    }

    public void DeductHp(int hp) {
        this.hitPoints = hitPoints - hp;
    }
}
