namespace AcklenAvenue.Testing
{
    public static class CastingExtensions
    {
        public static T As<T>(this object obj)
        {
            return (T) obj;
        }
    }
}