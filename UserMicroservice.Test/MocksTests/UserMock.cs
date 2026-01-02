
namespace UserMicroservice.Test.DomainTests
{
    public class UserMock<T>
    {
        public UserMock()
        {
        }

        public T Setup(Func<T, T> func, T obj)
        {
            return func(obj);
        }

    }
}