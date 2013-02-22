using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AcklenAvenue.Testing.BDD.MSTest
{
    public static class ShouldEqualExceptionExtensions
    {
        public static void ShouldEqualException(this Exception actualException, Exception expectedException)
        {
            var messages = new List<string>();
            try
            {
                Assert.IsInstanceOfType(actualException, expectedException.GetType());
            }
            catch (Exception exception)
            {
                messages.Add("Exception types don't match. " + exception.Message);
            }

            try
            {
                Assert.AreEqual(expectedException.Message, actualException.Message);
            }
            catch (Exception exception)
            {
                messages.Add("Exception messages don't match. " + exception.Message);
            }

            foreach (DictionaryEntry item in actualException.Data)
            {
                try
                {
                    Assert.AreEqual(expectedException.Data[item.Key], actualException.Data[item.Key]);                  
                }
                catch (Exception exception)
                {
                    messages.Add("Data dictionary item '" + item.Key +"' doesn't match. " + exception.Message);                    
                }
                
            }

            if (messages.Count > 0)
                Assert.Fail("\n" + string.Join("\n", messages.ToArray()));                
        }
    }
}