using System;
using System.Collections;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private bool canPop = false;

    [SerializeField] private float height = 5;
    [SerializeField] private float speed = 5;

    private Vector3 startPosition;
    private Vector3 originalPosition;

    private void Awake()
    {
        startPosition = transform.position;
        originalPosition = transform.position;
    }
    
    private void Update()
    {
        var moveBy = Mathf.PingPong(speed * Time.time, height);
        var moveByX = Mathf.PingPong((speed / 2) * Time.time, height);
        transform.position = new Vector3(startPosition.x + moveByX, startPosition.y + moveBy, transform.position.z);
    }

    public void UpdateBubble(Vector3 newPosition)
    {
        startPosition = newPosition;
    }

    public void ResetBubble()
    {
        startPosition = originalPosition;
    }
}
