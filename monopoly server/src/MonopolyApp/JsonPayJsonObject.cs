using System;

namespace MonopolyApp
{
    public class ActionJsonObject
    {
    public override string ToString()
    {
        return $"type: {type}\nfrom: {from}\nto: {to}\nhowMany: {howMany}";
    }
    public string type {get; set;}
    public string from {get; set;}
    public string to {get; set;}
    public double howMany {get; set;}
    }
}