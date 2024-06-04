using Pawnshop.Datacore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pawnshop
{
    public partial class PawnShopForm : Form
    {
        PawnShop shop;

        public PawnShopForm()
        {
            InitializeComponent();
        }

        private void Reload()
        {
            if (shop != null)
            {
                Engine.SaveShop(shop);
            }
            //else
            //{
            //    shop = new PawnShop();
            //    shop.applications.Add(new SaleApplication(DateTime.Now, new Client("Antonio", "Bakerstreet 226", "Missis Hadson"), new Item("clock", 1000, 5), 2000, true));
            //    shop.name = "Мій Ломбард";
            //    shop.PhoneNumber = "+1234567890";
            //    shop.Address = "Харків, вулиця Наукова, дім 1";
            //
            //}
            shop = Engine.GetShop();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = shop.applications;
            nameTextBox.Text = shop.name;
            PhoneTextBox.Text = shop.PhoneNumber;
            adressTextBox.Text = shop.Address;
        }

        private void PawnShopForm_Load(object sender, EventArgs e)
        {
            Reload();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            ApplicationForm applicationForm = new ApplicationForm();
            applicationForm.shop = shop;
            applicationForm.ShowDialog();
            Reload();
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }
            ApplicationForm applicationForm = new ApplicationForm();
            applicationForm.shop = shop;
            applicationForm.application = dataGridView1.CurrentRow.DataBoundItem as Datacore.Application;
            applicationForm.ShowDialog();
            Reload();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;
            Datacore.Application application = dataGridView1.CurrentRow.DataBoundItem as Datacore.Application;
            if (MessageBox.Show(
            $"Заява {application.Client.name} про {application.Type} {application.Item.name} буде видалена. Ви впевнені ?",
            "Видалення заяви",
            MessageBoxButtons.YesNo
            ) != DialogResult.Yes
            )
                return;

            shop.applications.Remove(application);
            Datacore.Application.list.Remove(application);
            Reload();
        }

       


        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            shop.name = nameTextBox.Text;
        }

        private void adressTextBox_TextChanged(object sender, EventArgs e)
        {
            shop.Address = adressTextBox.Text;
        }

        private void PhoneTextBox_TextChanged(object sender, EventArgs e)
        {
            shop.PhoneNumber = PhoneTextBox.Text;
        }

        private void PawnShopForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Reload();
        }
        private void SearchButton_Click(object sender, EventArgs e)
        {
            List<Datacore.Application> list = new List<Datacore.Application>();

            string search = SearchTextBox.Text;
            
            foreach (var item in Datacore.Application.list)
            {
                string name = item.Item.name;
                bool check = true;
                if (search.Length > name.Length)
                {
                    check = false;
                }
                else
                {
                    for (int i = 0; i < search.Length; i++)
                    {
                        if (search[i] != name[i])
                        {
                            check = false;
                            break;
                        }
                    }
                }

                if (check)
                {
                    list.Add(item);
                }


            }

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = list;
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
