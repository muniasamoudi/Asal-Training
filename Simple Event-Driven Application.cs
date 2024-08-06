using System;
using System.Collections;
using System.Collections.Generic;


public delegate void NotificationHandler(string msg);

public class Publisher
{
    public event NotificationHandler NotificationPublished;

    public void SendNotification(string msg)
    {
        if (NotificationPublished != null)
        {
            NotificationPublished(msg);
        }
    }
}

public class SubscriberOne
{
    public void HandleNotification(string msg)
    {
        Console.WriteLine("Subscriber One received: " + msg);
    }
}

public class SubscriberTwo
{
    public void HandleNotification(string msg)
    {
        Console.WriteLine("Subscriber Two received: " + msg);
    }
}

public class SubscriberThree
{
    public void HandleNotification(string msg)
    {
        Console.WriteLine("Subscriber Three received: " + msg);
    }
}

public class SubscriberCollection : IEnumerable<NotificationHandler>
{
    private List<NotificationHandler> subscribers = new List<NotificationHandler>();

    public void AddSubscriber(NotificationHandler subscriber)
    {
        subscribers.Add(subscriber);
    }

    public IEnumerator<NotificationHandler> GetEnumerator()
    {
        return new SubscriberEnumerator(subscribers);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class SubscriberEnumerator : IEnumerator<NotificationHandler>
{
    private List<NotificationHandler> subscribers;
     int index = -1;

    public SubscriberEnumerator(List<NotificationHandler> subscribers)
    {
        this.subscribers = subscribers;
    }

    public bool MoveNext()
    {
        index++;
        return (index < subscribers.Count);
    }

    public void Reset()
    {
        index = -1;
    }

    public NotificationHandler Current
    {
        get
        {
            try
            {
                return subscribers[index];
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
    }

    object IEnumerator.Current => Current;

    public void Dispose(){}
}

class Program
{
    static void Main(string[] args)
    {
        Publisher publisher = new Publisher();
        SubscriberOne subscriberOne = new SubscriberOne();
        SubscriberTwo subscriberTwo = new SubscriberTwo();
        SubscriberThree subscriberThree = new SubscriberThree();
        SubscriberCollection subscriberCollection = new SubscriberCollection();
        
        subscriberCollection.AddSubscriber(subscriberOne.HandleNotification);
        subscriberCollection.AddSubscriber(subscriberTwo.HandleNotification);
        subscriberCollection.AddSubscriber(subscriberThree.HandleNotification);

        foreach (NotificationHandler handler in subscriberCollection)
        {
            publisher.NotificationPublished += handler;
        }

        publisher.SendNotification("Notification message");
    }
}
