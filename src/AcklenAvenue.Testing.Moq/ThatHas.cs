namespace AcklenAvenue.Testing.Moq
{
    public static class ThatHas
    {
        public static ExpressionComparisonBuilder<T> AnExpressionFor<T>()
        {
            return new ExpressionComparisonBuilder<T>();
        }

        public static FuncComparisonBuilder<T> AnFuncFor<T>()
        {
            return new FuncComparisonBuilder<T>();
        }       
    }
}