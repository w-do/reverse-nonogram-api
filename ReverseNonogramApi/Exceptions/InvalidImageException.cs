namespace ReverseNonogramApi.Exceptions;

public class InvalidImageException : Exception
{
    public InvalidImageException(string message) : base(message) { }
}
