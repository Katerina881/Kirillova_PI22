using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using ZavodConservbusinessLogic.BindingModels;
using ZavodConservbusinessLogic.BusinessLogics;
using ZavodConservbusinessLogic.Interfaces;
using ZavodConservbusinessLogic.ViewModels;

namespace ZavodConservView
{
    public partial class FormCreateOrder : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IConservLogic logicP;

        private readonly MainLogic logicM;

        public FormCreateOrder(IConservLogic logicP, MainLogic logicM)
        {
            InitializeComponent();
            this.logicP = logicP;
            this.logicM = logicM;
        }

        private void FormCreateOrder_Load(object sender, EventArgs e) 
        { 
            try 
            {
                var list = logicP.Read(null);
                comboBoxConserv.DataSource = list;
                comboBoxConserv.DisplayMember = "EquipmentName";
                comboBoxConserv.ValueMember = "Id";
            } 
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            } 
        }

        private void CalcSum() 
        { 
            if (comboBoxConserv.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text)) 
            { 
                try 
                {
                    int id = Convert.ToInt32(comboBoxConserv.SelectedValue); 
                    ConservViewModel Conserv = logicP.Read(new ConservBindingModel { Id = id })?[0];
                    int count = Convert.ToInt32(textBoxCount.Text); 
                    textBoxSum.Text = (count * Conserv?.Price ?? 0).ToString(); 
                } 
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
            } 
        }

        private void TextBoxCount_TextChanged(object sender, EventArgs e)
        { 
            CalcSum();
        }

        private void ComboBoxConserv_SelectedIndexChanged(object sender, EventArgs e)
        { 
            CalcSum();
        }

        private void ButtonSave_Click(object sender, EventArgs e) 
        { 
            if (string.IsNullOrEmpty(textBoxCount.Text)) 
            { 
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } 
            if (comboBoxConserv.SelectedValue == null) 
            { 
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            } 
            try
            { 
                logicM.CreateOrder(new CreateOrderBindingModel 
                {
                    ConservId = Convert.ToInt32(comboBoxConserv.SelectedValue), 
                    Count = Convert.ToInt32(textBoxCount.Text), 
                    Sum = Convert.ToDecimal(textBoxSum.Text)
                }); 
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        private void ButtonCancel_Click(object sender, EventArgs e) 
        { 
            DialogResult = DialogResult.Cancel; 
            Close(); 
        }
    }
}
