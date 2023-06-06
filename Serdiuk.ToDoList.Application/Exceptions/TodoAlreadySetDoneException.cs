namespace Serdiuk.ToDoList.Application.Exceptions
{
    public class TodoAlreadySetDoneException : Exception
    {
        public TodoAlreadySetDoneException() : base("Done in todo already set, try again")
        {

        }

        public TodoAlreadySetDoneException(string message) : base(message)
        {

        }
    }
}
