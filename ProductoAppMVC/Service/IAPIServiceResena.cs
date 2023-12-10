using ProductoAppMVC.Models;

namespace ProductoAppMVC.Service
{
    public interface IAPIServiceResena
    {
        public Task<Resena> GetProducto(int Id);
        public Task<Resena> PostResena(Resena resena);
        public Task<List<Resena>> GetResenas(int id);
        public Task<List<Resena>> GetListResenas();

        public Task<Boolean> DeleteResena(int IdResena);
    }
}
