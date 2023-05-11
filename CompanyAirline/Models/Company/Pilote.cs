using System;
using System.Collections.Generic;

namespace CompanyAirline.Models.Company;

public partial class Pilote
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Nationality { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual ICollection<Vol> Vols { get; set; } = new List<Vol>();
}
