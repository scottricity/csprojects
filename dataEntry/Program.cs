using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using static ReferenceManager;

public class Entry
{
    public string? EntryID { get; set; }
    public string? EntryType { get; set; }
    public bool? IsValid { get; set; }
    public ReferenceID? id;
}

public class EntryManager
{
    List<Entry> entries = new List<Entry>();

    public void SimulateTest()
    {
        entries.Add(new Entry { EntryID = "AA1", EntryType = "stagedOrder", IsValid = true, id = new ReferenceID {snowflake = 2 , snowflakeRange = 5233} } );
        Console.WriteLine(JsonSerializer.Serialize(entries));
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
        test.SimulateTest();
    }
    
}