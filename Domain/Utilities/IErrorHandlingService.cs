
namespace DemoMvcApp.Handler
{
    public interface IErrorHandlingService<T>
    {
        void SetError(T error);
        T GetError();
    }

    public class ErrorHandlingService<T> : IErrorHandlingService<T>
    {
        private T _error;

        public void SetError(T error)
        {
            _error = error;
        }

        public T GetError() => _error;
    }

}
