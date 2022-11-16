using System;
using System.Collections.Generic;

public class Entry
{
    public string EntryID { get; set; }
    public string EntryType { get; set; }
}

public class EntryManager
{
    List<Entry> entries = new List<Entry>();

    public void GetEntries()
    {
        if (entries != null)
        {
            return entries.Count;
        }else{
            entries.Add("Ok");
            return entries.Count;
        }
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Scott's Data Entry System");
        Console.WriteLine("Please state your ID: ");
        string inputId = Console.ReadLine();
        Console.WriteLine("Validating {0}", inputId);
        EntryManager test = new EntryManager();
    }
    
}