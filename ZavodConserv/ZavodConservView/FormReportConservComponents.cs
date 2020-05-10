using ZavodConservbusinessLogic.BindingModels;
using ZavodConservbusinessLogic.BusinessLogics;
using System;
using System.Windows.Forms;
using Unity;
using System.Collections.Generic;
using System.Linq;

namespace ZavodConservView
{
    public partial class FormReportConservComponents : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;

        public FormReportConservComponents(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void ButtonMake_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var dict = logic.GetOrders(new ReportBindingModel { DateFrom = dateTimePickerFrom.Value.Date, DateTo = dateTimePickerTo.Value.Date });
                if (dict != null)
                {
                    dataGridView.Rows.Clear();

                    foreach (var date in dict)
                    {
                        decimal dateSum = 0;

                        dataGridView.Rows.Add(new object[] { date.Key, "", "" });

                        foreach (var order in date)
                        {
                            dataGridView.Rows.Add(new object[] { "", order.ConservName, order.Sum });
                            dateSum += order.Sum;
                        }

                        dataGridView.Rows.Add(new object[] { "Итого", "", dateSum });
                        dataGridView.Rows.Add(new object[] { });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonSaveToExcel_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveConservComponentToExcelFile(new ReportBindingModel { FileName = dialog.FileName, DateFrom = dateTimePickerFrom.Value.Date, DateTo = dateTimePickerTo.Value.Date });

                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }  
    }
}
