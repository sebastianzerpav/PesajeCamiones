using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PesajeCamiones.Data.Models;

public partial class Camion
{
    public string Placa { get; set; } = null!;

    public string Marca { get; set; } = null!;

    public int NumeroEjes { get; set; }
    [JsonIgnore]
    public virtual ICollection<Pesaje> Pesajes { get; set; } = new List<Pesaje>();
}
