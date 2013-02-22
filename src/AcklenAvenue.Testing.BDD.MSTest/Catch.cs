using System;

namespace AcklenAvenue.Testing.BDD.MSTest
{
    public static class Catch
    {
        public static Exception Exception(Action action)
        {
            try
            {
                action.Invoke();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }            
        }
    }
}