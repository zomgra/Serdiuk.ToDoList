namespace Serdiuk.ToDoList.Application.Exceptions
{
    public class TodoNotEnoughPermissionsException : Exception
    {
        public TodoNotEnoughPermissionsException() : base("you don't have enough rights")
        {

        }
        public TodoNotEnoughPermissionsException(string messsage) : base(messsage)
        {

        }
        public TodoNotEnoughPermissionsException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
