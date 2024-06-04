using Pawnshop.Properties;
using Pawnshop.Datacore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pawnshop
{
    static class Engine
    {

        public static PawnShop GetShop()
        {
            PawnShop shop = new PawnShop();

                string text = "";
                try
                {
                    text = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "PawhShop.txt");
                }
                catch
                {
                    return shop;

                }
            string[] texts = text.Split('|');

            shop.name = texts[0];
            shop.Address = texts[1];
            shop.PhoneNumber = texts[2];

            for (int i = 3; i < texts.Length; i++)
            {
                string[] applicationStr = texts[i].Split('*');
                ApplicationType type = (ApplicationType)Enum.Parse(typeof(ApplicationType),applicationStr[2]);
                Datacore.Application application = null;

                DateTime dateTimeCreated = Convert.ToDateTime(applicationStr[0]);
                int price = Convert.ToInt32(applicationStr[1]);
                string comment = applicationStr[3];

                string clientName = applicationStr[4];
                string address = applicationStr[5];
                string contactInfo = applicationStr[6];


                string itemName = applicationStr[7];
                int itemPrice = Convert.ToInt32(applicationStr[8]);
                int age = Convert.ToInt32(applicationStr[9]);

                if (type == ApplicationType.Pledge)
                {

                    DateTime expiryDate = Convert.ToDateTime(applicationStr[10]);

                    application = new PledgeApplication(dateTimeCreated,new Client(clientName,address,contactInfo), new Item(itemName,itemPrice,age),price,expiryDate);
                }
                else if (type == ApplicationType.Refund)
                {
                    bool isApproved = Convert.ToBoolean(applicationStr[10]);
                    application = new RefundApplication(dateTimeCreated, new Client(clientName, address, contactInfo), new Item(itemName, itemPrice, age), price, isApproved);
                }
                else if (type == ApplicationType.Sale)
                {
                    bool isSold = Convert.ToBoolean(applicationStr[10]);
                    application = new SaleApplication(dateTimeCreated, new Client(clientName, address, contactInfo), new Item(itemName, itemPrice, age), price, isSold);
                }
                shop.applications.Add(application);

            }

            return shop;
        }

        public static void SaveShop(PawnShop shop)
        {
            string text = "";
            text += $"{shop.name}|{shop.Address}|{shop.PhoneNumber}";

            foreach (var application in shop.applications)
            {

                /*
        public DateTime DateCreated { get; set; }
        public Client Client { get; set; }
        public Item Item { get; set; }
        public int Price { get; set; }
        public ApplicationType Type { get; set; }
        public string comment;*/
                text += $"|{application.DateCreated}*{application.Price}*{application.Type}*" +
                    $"{application.Client.GetString()}{application.Item.GetString()}";
                if (application.Type == ApplicationType.Pledge)
                {
                    text += "*" + (application as PledgeApplication).ExpiryDate;
                }
                else if (application.Type == ApplicationType.Refund)
                {
                    text += "*" + (application as RefundApplication).IsApproved;

                }
                else if (application.Type == ApplicationType.Sale)
                {
                    text += "*" + (application as SaleApplication).IsSold;
                }

            }

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "PawhShop.txt", text);
        }


    }
}
