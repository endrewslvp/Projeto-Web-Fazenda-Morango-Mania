//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Aula04Out24.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vendas
    {
        public int ID { get; set; }
        public Nullable<int> ID_Cliente { get; set; }
        public Nullable<int> ID_Produto { get; set; }
        public Nullable<int> Quantidade { get; set; }
        public Nullable<decimal> TotalCompra { get; set; }
        public string CodigoCompra { get; set; }
        public string MetodoPagamento { get; set; }
    
        public virtual Clientes Clientes { get; set; }
        public virtual Produtos Produtos { get; set; }
    }
}
