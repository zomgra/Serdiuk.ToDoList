namespace Serdiuk.ToDoList.Application.Exceptions
{
    public class TodoUnathorizeException : Exception
    {
        public TodoUnathorizeException() : base("Authorization problem, try to log into your account")
        {

        }
        public TodoUnathorizeException(string message) : base(message)
        {

        }
    }
}
