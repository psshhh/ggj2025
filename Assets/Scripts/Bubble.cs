using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Bubble : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool canPop = false;

    [SerializeField] private float height = 5;
    [SerializeField] private float speed = 5;
    [SerializeField] private AudioClip sound;

    private Vector3 startPosition;
    private Vector3 originalPosition;

    private AudioSource audioSource;

    private bool isPaused = false;
    
    private void Awake()
    {
        startPosition = transform.position;
        originalPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.volume = .5f;
            audioSource.clip = sound;
        }
    }
    
    private void Update()
    {
        if (isPaused)
            return;
        
        var moveBy = Mathf.PingPong(speed * Time.time, height);
        var moveByX = Mathf.PingPong((speed / 2) * Time.time, height);
        transform.position = new Vector3(startPosition.x + moveByX, startPosition.y + moveBy, transform.position.z);
    }

    public void UpdateBubble(Vector3 newPosition)
    {
        isPaused = false;
        startPosition = newPosition;
    }

    public void MovePausedBubble()
    {
        isPaused = true;
        audioSource?.Play(); 
    }

    public void IncreaseSpeed(int multiplier)
    {
        if (multiplier > 1)
            speed *= multiplier;
    }

    public void ResetBubble()
    {
        startPosition = originalPosition;
    }

    public void Talk()
    {
        audioSource.Play();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (canPop == false)
            return;
        
        audioSource?.Play();
        GetComponent<Image>().enabled = false;
    }
}
