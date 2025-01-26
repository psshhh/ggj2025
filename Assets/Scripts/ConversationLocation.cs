using System.Collections.Generic;
using UnityEngine;

public class ConversationLocation : MonoBehaviour
{
    private int count;

    private List<IPerson> people = new List<IPerson>();

    public void AddCount(IPerson person)
    {
        count += person.NumberOfPeople;
        people.Add(person);
    }

    public void RemoveCount( IPerson person)
    {
        if (count == 0)
            return;
        count -= person.NumberOfPeople;
        people.Remove(person);
    }

    public int GetCount()
    {
        return count;
    }

    public void ClearCount()
    {
        count = 0;
        people.Clear();
    }
}