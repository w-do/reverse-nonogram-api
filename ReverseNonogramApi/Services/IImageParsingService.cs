namespace ReverseNonogramApi.Services;

public interface IImageParsingService
{
    public int[,] ParseImage(Stream imageFile);
}
