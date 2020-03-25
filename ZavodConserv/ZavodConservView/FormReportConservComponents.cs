using ZavodConservbusinessLogic.BindingModels;
using ZavodConservbusinessLogic.BusinessLogics;
using System;
using System.Windows.Forms;
using Unity;

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

        private void FormReportConservComponents_Load(object sender, EventArgs e)
        {
            try
            {
                var dict = logic.GetConservComponent();
                if (dict != null)
                {
                    dataGridView.Rows.Clear(); foreach (var elem in dict)
                    {
                        dataGridView.Rows.Add(new object[] { elem.ComponentName, "", ""
});
                        foreach (var listElem in elem.Conservs)
                        {
                            dataGridView.Rows.Add(new object[] { "", listElem.Item1, listElem.Item2 });
                        }
                        dataGridView.Rows.Add(new object[] { "Итого", "", elem.TotalCount
});
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
            using (var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveConservComponentToExcelFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName
                        });
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
