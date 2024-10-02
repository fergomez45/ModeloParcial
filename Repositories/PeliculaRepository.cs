using ModeloParcial.Models;
using System.Data;

namespace ModeloParcial.Repositories
{
    public class PeliculaRepository : IPeliculaRepository
    {
        private CineDbContext _context;


        public PeliculaRepository(CineDbContext context)
        {
            _context = context;
        }
        public bool Create(Pelicula oPelicula)
        {
            _context.Peliculas.Add(oPelicula);
            return _context.SaveChanges() == 1;
        }

        public List<Pelicula> GetAll()
        {
            return _context.Peliculas.Where(x => x.Estreno != false).ToList();

            //CON ADO.NET

            //List<Pelicula> lst = new List<Pelicula>();
            //var helper = DataHelper.GetInstance();
            //DataTable t = helper.ExecuteSPQuery("SP_CONSULTAR_PELICULAS", null);

            //foreach(DataRow dr in t)
            //{
            //    var pelicula = new Pelicula;
            //    pelicula.Id = Int32.Parse(row["id"].ToString());
            //    pelicula.Titulo = row["titulo"].ToString();
            //    pelicula.Director = row["director"].ToString();
            //    pelicula.Anio = Int32.Parse(row["anio"].ToString());
            //    pelicula.Estreno = Boolean.Parse(row["estreno"].ToString());
            //    pelicula.IdGenero = Int32.Parse(row["id_genero"].ToString());
            //    pelicula.GeneroNavigation = new Genero()
            //    {
            //        Id = Int32.Parse(row["id_genero"].ToString());
            //        Nombre = row["nombre"].ToString();

                    
            //    }
            //      lst.Add(pelicula)
            //}
            // return lst;
        }

        public List<Pelicula> GetAllByYears(int anio1, int anio2)
        {
            return _context.Peliculas.Where(x => x.Anio >= anio1 && x.Anio <= anio2).ToList();
        }

        public bool Update(int id)
        {
            var peliculaActualizada = _context.Peliculas.Find(id);
            if (peliculaActualizada != null)
            {
                peliculaActualizada.Estreno = false;
                return _context.SaveChanges() > 0;
            }
            return false;

        }

        public bool Delete(int id, string motivoBaja)
        {
            var p = _context.Peliculas.Find(id);
            if (p != null)
            {
                p.Estreno = false;
                p.FechaBaja = DateTime.Now;
                p.MotivoBaja = motivoBaja;

                _context.Peliculas.Update(p);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Pelicula> GetAllByGen(int idGenero, int anio1, int anio2)
        {
            return _context.Peliculas.Where(x => x.Anio >= anio1 && x.Anio <= anio2 && x.IdGenero == idGenero).ToList();
        }

        public Pelicula? GetById(int id)
        {
            return _context.Peliculas.Find(id);
            
        }

        public bool Delete(int id)
        {
            var peliculaDelete = GetById(id);
            if(peliculaDelete != null)
            {
                _context.Peliculas.Remove(peliculaDelete);
                return _context.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
            
        }
    }
}
