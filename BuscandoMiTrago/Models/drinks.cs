using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuscandoMiTrago.Models
{
    public class drinks
    {
        [Key]
        [JsonProperty(PropertyName = "idDrink")]
        public int idDrink {get; set;}
        [JsonProperty(PropertyName = "strDrinkThumb")]
        public string strDrinkThumb { get; set; }
        [JsonProperty(PropertyName = "strDrink")]
        public string strDrink { get; set; }

    }
}
