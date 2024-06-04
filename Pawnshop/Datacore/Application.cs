using System;
using System.ComponentModel;

namespace Pawnshop.Datacore
{
    public enum ApplicationType
    {
        Pledge,
        Sale,
        Refund
    }
    
    public abstract class Application
    {
        public static int ID = 0; 

        public int Id;
        public DateTime DateCreated { get; set; }
        public Client Client { get; set; }
        public Item Item { get; set; }
        public int Price { get; set; }
        public ApplicationType Type { get; set; }

        protected Application(DateTime dateCreated, Client client, Item item, int price, ApplicationType type)
        {
            Id = ID;
            ID++;
            DateCreated = dateCreated;
            Client = client;
            Item = item;
            Price = price;
            Type = type;

        }

        protected Application()
        {
            Client = new Client(null, null, null);
            Item = new Item(null,0,0);
            DateCreated = DateTime.Now;
        }
        
    }
    
    //Залог
    public class PledgeApplication : Application
    {
        public DateTime ExpiryDate { get; set; }

        
        public PledgeApplication(DateTime dateCreated, Client client, Item item, int price, DateTime expiryDate)
            : base(dateCreated, client, item, price, ApplicationType.Pledge)
        {
            ExpiryDate = expiryDate;
        }

        public PledgeApplication() : base() { }
    }
    
    //Продаж
    public class SaleApplication : Application
    {
        public bool IsSold { get; set; }

        // Конструктор класса
        public SaleApplication(DateTime dateCreated, Client client, Item item, int price, bool isSold)
            : base(dateCreated, client, item, price, ApplicationType.Sale)
        {
            IsSold = isSold;
        }

        public SaleApplication() : base() { }
    }
    
    //Заявка на повернення
    public class RefundApplication : Application
    {
        public bool IsApproved { get; set; }

        public RefundApplication(DateTime dateCreated, Client client, Item item, int price, bool isApproved)
            : base(dateCreated, client, item, price, ApplicationType.Refund)
        {
            IsApproved = isApproved;
        }   

        public RefundApplication() : base() { }
    }
}
