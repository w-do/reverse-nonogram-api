namespace ReverseNonogramApi.Services
{
    public interface IImageParsingService
    {
        public int[,] ParseImageFile(IFormFile imageFile);
    }
}
