using System;
using System.Collections.Generic;
using System.Text;

namespace LokalnyTarg.Domain.Announcement
{
    public class Annoucment
    {
        public uint AnnoucementId { get; private set; }
        public string Title { get;private set; }
        public string Description { get;private set; }
        public uint SuppilerId { get;private set; }
        public string SuplierName { get; private set; }


        public Product Product { get; private set; }

        public Annoucment(uint annoucementId,uint productId,  string suplierName, uint suppilerId, string title, string description, string productName,
            string productDescription, float price, uint categoryId,string photo)
        {
            Title = title;
            Description = description;
            AnnoucementId = annoucementId;
            SuppilerId = suppilerId;
            SuplierName = suplierName;
            Product = new Product(productId, productName, productDescription, price, categoryId,photo);
       
        }
    }
}
