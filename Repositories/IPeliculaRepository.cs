using Microsoft.AspNetCore.Components.Web;
using ModeloParcial.Models;

namespace ModeloParcial.Repositories
{
    public interface IPeliculaRepository
    {

        List<Pelicula> GetAll();
        bool Create(Pelicula oPelicula);

        bool Update(int id);
        List<Pelicula> GetAllByYears(int anio1, int anio2);
        List<Pelicula> GetAllByGen(int idGenero,int anio1, int anio2);

        bool Delete(int id, string motivoBaja);

        Pelicula? GetById(int id);
        bool Delete(int id);


    }
}
