using JuliBack.Contexto;
using JuliBack.Models;
using Microsoft.EntityFrameworkCore;


namespace JuliBack.Repositories
{
    public interface IimageRepository
    {
        Task SaveChangesAsync();
        Task DeleteAsync(List<int> id);
        Task<List<Images>> GetByIdAsync(List<int> id);
        Task AddImagesAsync(Images image);
        Task<List<Images>> GetAllImagesAsync();
        

    }

    //Consultas para el Back
    public partial class ImageRep : IimageRepository
    {
        
        private readonly AppDbContext _Context;


        public ImageRep( AppDbContext context)
        {
           
            this._Context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _Context.SaveChangesAsync();
        }

        public async Task AddImagesAsync(Images image)
        {
             await _Context.Images.AddAsync(image);
             await _Context.SaveChangesAsync();
        }

    }

    //Consultas para el Front
    public partial class ImageRep
    {
        //Pido las imágenes por id para eliminar
        public async Task<List<Images>> GetByIdAsync(List<int> ids)
        {
            return await _Context.Images.Where(img => ids.Contains(img.Id)).ToListAsync();
        }

        //Lógica de borrado.
        public async Task DeleteAsync(List<int> id)
        {
            var img = await GetByIdAsync(id);

            _Context.Images.RemoveRange(img);

            await _Context.SaveChangesAsync();
        }

        public async Task<List<Images>> GetAllImagesAsync()
        {
            return await _Context.Images.ToListAsync();
        }
    }



}
