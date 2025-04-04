using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PesajeCamiones.Data.Models;

public partial class FotoPesaje
{
    public int IdFotoPesaje { get; set; }

    public string ImagenVehiculo { get; set; } = null!;

    public int IdPesaje { get; set; }
    [JsonIgnore]
    public virtual Pesaje IdPesajeNavigation { get; set; } = null!;
}
