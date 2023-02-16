namespace ReverseNonogramApi.Services
{
    public interface IImageParsingService
    {
        public int[,] ParseImageFile(Stream imageFile);
    }
}
