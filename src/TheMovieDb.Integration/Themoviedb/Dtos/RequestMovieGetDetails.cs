using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMovieDb.Integration.Themoviedb.Dtos;

namespace TheMovieDb.Integration.Themoviedb.Dtos;




public class RequestMovieGetDetails : RequestBaseTheMovieDbModel
{
    public int Id { get; set; }
}