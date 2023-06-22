using System;
using System.Collections.Generic;

namespace ApiLogin.Models;

public partial class Persona
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Password { get; set; } = null!;
}
