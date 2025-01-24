using UnityEngine;

public class ConversationLocation : MonoBehaviour
{
    private int count;
    
    private void Update()
    {
        if (transform.childCount != 0)
        {
            //float the people in a random circular fashion
        }
    }

    public void AddCount()
    {
        count++;
    }

    public void RemoveCount()
    {
        if (count == 0)
            return;
        count--;
    }

    public int GetCount()
    {
        return count;
    }
}