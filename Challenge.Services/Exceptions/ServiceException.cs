namespace Challenge.Services;

public class ServiceException : Exception
{
    internal List<string> _errors;
    public IReadOnlyCollection<string> Errors => _errors;

    public ServiceException()
    {
    }

    public ServiceException(string message) : base(message)
    {
    }

    public ServiceException(string message, List<string> errors) : base(message)
    {
        _errors = errors;
    }

    public ServiceException(string message, Exception innerException) : base(message, innerException)
    {
    }
}