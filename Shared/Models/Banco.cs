﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Banco
    {

        public static List<Produto> Produtos { get; set; } = new ();
        public static List<Carro> Carros { get; set; } = new();
    }
}
