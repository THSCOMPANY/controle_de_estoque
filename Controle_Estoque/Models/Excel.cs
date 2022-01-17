using System;
using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Controle_Estoque
{
    class Excel
    {
       //Esse procedimento, realiza a tarefa de exportar e deixar formatado
       //Como deixei comentado acima, esse cara aqui faz essa tarefa
       //kkkk. Verdade
       public void exportarExcel(ListView listView1)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xls", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                    Worksheet ws = (Worksheet)app.ActiveSheet;
                    app.Visible = false;
                    //CRIA O CABEÇALHO
                    //ws.Cells[1, 1] = "ID";
                    ws.Cells[1, 1] = "COD.PRODUTO";
                    ws.Cells[1, 2] = "PRODUTO";
                    ws.Cells[1, 3] = "MARCA";
                    ws.Cells[1, 4] = "ANO";
                    ws.Cells[1, 5] = "VL";
                    ws.Cells[1, 6] = "VF";
                    ws.Cells[1, 7] = "CLASS.";
                    ws.Cells[1, 8] = "QUANT.";

                    int i = 2;

                    foreach (ListViewItem item in listView1.Items)
                    {
                        ws.Cells[i, 1] = item.SubItems[0].Text;
                        ws.Cells[i, 1] = item.SubItems[1].Text;
                        ws.Cells[i, 2] = item.SubItems[2].Text;
                        ws.Cells[i, 3] = item.SubItems[3].Text;
                        ws.Cells[i, 4] = item.SubItems[4].Text;
                        ws.Cells[i, 5] = item.SubItems[5].Text;
                        ws.Cells[i, 6] = item.SubItems[6].Text;
                        ws.Cells[i, 7] = item.SubItems[7].Text;
                        //ws.Cells[i, 8] = item.SubItems[8].Text;
                        i++;
                    }

                    ws.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("DADOS EXPORTADOS COM ÊXITO!!!", "Message", MessageBoxButtons.OK);
                }
            }
        }

        //Esse procedimento, realiza a tarefa de exportar e deixar tudo na mesma célula
        private async void exportarExcel02(ListView listView1)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV|*.csv", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(new FileStream(sfd.FileName, FileMode.Create), Encoding.UTF8))
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine("ID PRODUTO , COD. PRODUTO , PRODUTO , MARCA , ANO , VALOR UNITÁRIO , VALOR FINAL , CLASSIFICACAO , QUANTIDADE");

                        foreach (ListViewItem item in listView1.Items)
                        {
                            sb.AppendLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", item.SubItems[0].Text, item.SubItems[1].Text, item.SubItems[2].Text, item.SubItems[3].Text, item.SubItems[4].Text, item.SubItems[5].Text, item.SubItems[6].Text, item.SubItems[7].Text, item.SubItems[8].Text));
                        }
                        await sw.WriteLineAsync(sb.ToString());
                        MessageBox.Show("Seus dados foram exportados com sucesso!!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
