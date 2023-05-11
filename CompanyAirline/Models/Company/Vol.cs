using System;
using System.Collections.Generic;

namespace CompanyAirline.Models.Company;

public partial class Vol
{
    public int Id { get; set; }

    public string VilleDepart { get; set; } = null!;

    public string VilleArrive { get; set; } = null!;

    public string Tarif { get; set; } = null!;

    public string Avion { get; set; } = null!;

    public int PiloteId { get; set; }

    public virtual Pilote? Pilote { get; set; } = null!;
}
