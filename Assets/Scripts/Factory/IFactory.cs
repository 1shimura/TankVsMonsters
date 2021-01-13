namespace Scripts
{
    public interface IFactory<T> {
        T Create();
    }
}