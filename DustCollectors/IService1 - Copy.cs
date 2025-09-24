using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DustCollectors.Classes;


namespace DustCollectors
{
    [DataContract]
    public class InvoiceDTO
    {
        [DataMember] public int InvoiceID { get; set; }
        [DataMember] public int UserID { get; set; }
        [DataMember] public DateTime InvoiceDate { get; set; }
        [DataMember] public decimal Subtotal { get; set; }
        [DataMember] public decimal VAT { get; set; }
        [DataMember] public decimal DeliveryFee { get; set; }
        [DataMember] public decimal TotalAmount { get; set; }
        [DataMember] public string Status { get; set; }
    }

    [DataContract]
    public class InvoiceItemDTO
    {
        [DataMember] public int InvoiceItemID { get; set; }
        [DataMember] public int InvoiceID { get; set; }
        [DataMember] public string ProductName { get; set; }
        [DataMember] public int Quantity { get; set; }
        [DataMember] public decimal UnitPrice { get; set; }
        [DataMember] public decimal TotalPrice { get; set; }
    }



    [DataContract]
    public class UserDTO
    {
        [DataMember] public int UserID { get; set; }
        [DataMember] public string FullName { get; set; }
        [DataMember] public string Email { get; set; }
        [DataMember] public string Address { get; set; }
    }





}
