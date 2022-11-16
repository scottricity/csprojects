using System;
using System.Numerics;

public class ReferenceManager
{
    public class ReferenceID
    {
        public int snowflake {get; set;}
        public double snowflakeRange {get; set;}
    }

    public string GenerateID()
    {
        ReferenceID id = new ReferenceID {snowflake = 2, snowflakeRange = 533};
        return id;
    }
}
