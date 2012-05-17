namespace AcklenAvenue.Data
{
    public interface IContainerConfigurer<in TTypeOfContainer>
    {
        void Configure(TTypeOfContainer container);
    }
}