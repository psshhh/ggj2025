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

    public void AddCount(int multiplier)
    {
        count += multiplier;
    }

    public void RemoveCount(int multiplier)
    {
        if (count == 0)
            return;
        count -= multiplier;
    }

    public int GetCount()
    {
        return count;
    }

    public void ClearCount()
    {
        count = 0;
    }
}