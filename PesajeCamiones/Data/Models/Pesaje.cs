using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PesajeCamiones.Data.Models;

public partial class Pesaje
{
    public int Id { get; set; }

    public DateOnly FechaPesaje { get; set; }

    public string PlacaCamion { get; set; } = null!;

    public float Peso { get; set; }

    public string Estacion { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<FotoPesaje> FotoPesajes { get; set; } = new List<FotoPesaje>();
    [JsonIgnore]
    public virtual Camion PlacaCamionNavigation { get; set; } = null!;
}
