namespace BukuAPI.Services.BukuService
{
    public interface IBukuService
    {
        List<object>? GetAllBukus(string? search);
        object? GetSingleBuku(int id);
        List<object>? AddBuku(Buku newBuku);
        List<object>? UpdateBuku(int id, Buku editBuku);
        List<object>? DeleteBuku(int id);
    }
}
