using System;
using System.Collections.Generic;

namespace Desafio.Model
{
    public class MovieResponse
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Active { get; set; }
        public DateTime Create_at { get; set; }
        public List<GenreResponse> GenreList { get; set; }
    }
}
