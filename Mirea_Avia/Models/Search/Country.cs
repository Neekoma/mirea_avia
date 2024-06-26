﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mirea_Avia.Models.Search
{
    public sealed class Country
    {
        // Такое наименование свойств обусловлено наименованием полей в БД
        [Key]
        public int country_id { get; set; }
        public string country_name { get; set; }
        public List<City> Cities { get; set; }

    }
}
