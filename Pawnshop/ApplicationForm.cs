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
    public partial class ApplicationForm : Form
    {
        public ApplicationForm()
        {
            InitializeComponent();
        }

        Control obj;
        bool start = false;

        private ApplicationType type;

        public Datacore.Application application;
        public PawnShop shop;

        private void getType()
        {
            if (obj != null)
            {
                obj.Hide();

                Controls.Remove(obj);
            }
            obj = null;

            if (type == ApplicationType.Pledge)
            {
                SpecialLabel.Text = "Дата закінчення залогу";
                obj = new DateTimePicker();
                (obj as DateTimePicker).Value = DateTime.Now;
                (obj as DateTimePicker).Format = DateTimePickerFormat.Short;
                (obj as DateTimePicker).MinDate = DateTime.Now.AddDays(1);
                if (application != null && !start)
                {
                    (obj as DateTimePicker).Value = (application as PledgeApplication).ExpiryDate;
                }
            }
            else if (type == ApplicationType.Refund)
            {
                SpecialLabel.Text = "Чи дозволено викупити предмет ?";
                obj = new CheckBox();
                (obj as CheckBox).Text = "Дозволено";
                if (application != null && !start)
                {
                    (obj as CheckBox).Checked = (application as RefundApplication).IsApproved;
                }
            }
            else if (type == ApplicationType.Sale)
            {
                SpecialLabel.Text = "Чи продано предмет ?";
                obj = new CheckBox();
                (obj as CheckBox).Text = "Продано";
                if (application != null && !start)
                {
                    (obj as CheckBox).Checked = (application as SaleApplication).IsSold;
                }
            }
            obj.Location = new Point(215, 167);
            obj.Size = new Size(132, 20);
            obj.Visible = true;
            obj.Enabled = true;
            Controls.Add(obj);

        }

        private void ApplicationForm_Load(object sender, EventArgs e)
        {
            ApplicationType[] types = (ApplicationType[])Enum.GetValues(typeof(ApplicationType));

            start = true;
            TypeComboBox.DataSource = types;
            start = false;

            if (application != null)
            {
                ItemNameTextBox.Text = application.Item.name;
                ItemPriceNumericUpDown.Value = application.Item.price;
                AgeNumericUpDown.Value = application.Item.age;

                TypeComboBox.SelectedItem = application.Type;
                ApplicationCostNumericUpDown.Value = application.Price;

                ClientNameTextBox.Text = application.Client.name;
                ClientAddressTextBox.Text = application.Client.address;
                ContactInformationTextBox.Text = application.Client.contactInfo;
            }
            else
            {
                TypeComboBox.SelectedIndex = 0;
            }

            getType();
            start = true;

            AgeNumericUpDown.Maximum = DateTime.Now.Year;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (ItemNameTextBox.Text == "")
            {
                MessageBox.Show("Поле 'Назва предмета' повинно бути заповнено");
                return;
            }
            if (ClientAddressTextBox.Text == "")
            {
                MessageBox.Show("Поле 'Адреса клієнта' повинно бути заповнено");
                return;
            }
            if (ClientNameTextBox.Text == "")
            {
                MessageBox.Show("Поле 'Ім'я клієнта' повинно бути заповнено");
                return;
            }
            if (ContactInformationTextBox.Text == "")
            {
                MessageBox.Show("Поле 'Контактна інформація' повинно бути заповнено");
                return;
            }

            if (application == null)
            {
                if (type == ApplicationType.Pledge)
                {
                    application = new PledgeApplication();
                }
                else if (type == ApplicationType.Refund)
                {
                    application = new RefundApplication();

                }
                else if (type == ApplicationType.Sale)
                {
                    application = new SaleApplication();
                }
                shop.applications.Add(application);
            }

            if (application != null)
            {
                shop.applications.Remove(application);
            }
            if (type == ApplicationType.Pledge)
            {
                application = new PledgeApplication();
                (application as PledgeApplication).ExpiryDate = (obj as DateTimePicker).Value;
            }
            else if (type == ApplicationType.Refund)
            {
                application = new RefundApplication();
                (application as RefundApplication).IsApproved = (obj as CheckBox).Checked;

            }
            else if (type == ApplicationType.Sale)
            {
                application = new SaleApplication();
                (application as SaleApplication).IsSold = (obj as CheckBox).Checked;
            }
            shop.applications.Add(application);

            application.Item.name = ItemNameTextBox.Text;
            application.Item.price = Convert.ToInt32(ItemPriceNumericUpDown.Value);
            application.Item.age = Convert.ToInt32(AgeNumericUpDown.Value);

            application.Client.address = ClientAddressTextBox.Text;
            application.Client.contactInfo = ContactInformationTextBox.Text;
            application.Client.name = ClientNameTextBox.Text;

            application.Type = type;
            application.Price = Convert.ToInt32(ApplicationCostNumericUpDown.Value);


            Close();

        }

        private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (type == (ApplicationType)TypeComboBox.SelectedItem)
            {
                return;
            }

            type = (ApplicationType)TypeComboBox.SelectedItem;
            getType();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}