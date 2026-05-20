using System;
public class Myservice : IMyService
{
    private readonly int _serviceId;
    public Myservice()
    {
        _serviceId = new Random().Next(100000, 999999);
    }

    public void logCreation(string message)
    {
        Console.WriteLine($"Service ID: {_serviceId}, Message:  {message}");
    }

}