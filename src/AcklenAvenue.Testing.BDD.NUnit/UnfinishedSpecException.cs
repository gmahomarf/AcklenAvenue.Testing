using System;

namespace AcklenAvenue.Testing.BDD.NUnit
{
    public class UnfinishedSpecException : Exception
    {
        public UnfinishedSpecException() : base("This spec is not finished.")
        {            
        }
    }
}