﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Logistica
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Logistica Gavoci"; // Imposta il nome della form
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Imposta il nome per la prima tab (TabPage)
            tabControl1.TabPages[0].Text = "Tabella";

            // Imposta il nome per la seconda tab (TabPage)
            tabControl1.TabPages[1].Text = "Metodi";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Ottieni il numero di righe e colonne inserite
            int numRows = (int)numericUpDownRighe.Value;
            int numCols = (int)numericUpDownColonne.Value + 2; // Aggiungi 2 per le colonne "TOTALE"

            // Crea una nuova tabella vuota
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.ColumnCount = numCols;
            int c = 0;

            for (int i = 1; i < numCols - 1; i++)
            {
                dataGridView1.Columns[i].Name = "D" + (c + 1); // Testo nella prima riga
                c++;
            }

            // Imposta le intestazioni per le colonne "TOTALE"
            dataGridView1.Columns[numCols - 1].Name = "TOTALE";


            for (int i = 0; i < numRows + 1; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = "UP" + (i + 1); // Testo nella prima colonna
            }



            // Imposta "TOTALE" nell'ultima cella della colonna vuota
            dataGridView1.Rows[numRows].Cells[numCols - numCols].Value = "TOTALE";
        }
        private void CalcolaTotali()
        {
            int numRows = dataGridView1.RowCount - 1; // Escludi la riga dei totali
            int numCols = dataGridView1.ColumnCount;

            // Calcola i totali delle colonne
            for (int col = 1; col < numCols - 1; col++)
            {
                int totalCol = 0;
                for (int row = 0; row < numRows; row++)
                {
                    int cellValue = 0;
                    if (dataGridView1.Rows[row].Cells[col].Value != null)
                    {
                        if (int.TryParse(dataGridView1.Rows[row].Cells[col].Value.ToString(), out cellValue))
                        {
                            totalCol += cellValue;
                        }
                    }
                }
                dataGridView1.Rows[numRows].Cells[col].Value = totalCol.ToString();
            }

            // Calcola i totali delle righe
            for (int row = 0; row < numRows; row++)
            {
                int totalRow = 0;
                for (int col = 1; col < numCols - 1; col++)
                {
                    int cellValue = 0;
                    if (dataGridView1.Rows[row].Cells[col].Value != null)
                    {
                        if (int.TryParse(dataGridView1.Rows[row].Cells[col].Value.ToString(), out cellValue))
                        {
                            totalRow += cellValue;
                        }
                    }
                }
                dataGridView1.Rows[row].Cells[numCols - 1].Value = totalRow.ToString();
            }

            // Calcola il totale totale
            int totalTotal = 0;
            for (int row = 0; row < numRows; row++)
            {
                int cellValue = 0;
                if (dataGridView1.Rows[row].Cells[numCols - 1].Value != null)
                {
                    if (int.TryParse(dataGridView1.Rows[row].Cells[numCols - 1].Value.ToString(), out cellValue))
                    {
                        totalTotal += cellValue;
                    }
                }
            }
            dataGridView1.Rows[numRows].Cells[numCols - 1].Value = totalTotal.ToString();
        }


        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se la cella è all'interno della tabella (non nelle intestazioni)
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Verifica se il valore inserito è un numero
                int cellValue;
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null &&
                    int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out cellValue))
                {
                    // Esegui il calcolo dei totali solo se il valore è un numero
                    CalcolaTotali();
                }
            }
        }





        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Impedisci la modifica delle celle "UP" e "D"
            if (e.ColumnIndex == 0 || e.ColumnIndex == dataGridView1.ColumnCount - 1)
            {
                e.Cancel = true;
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Verifica se il valore inserito è un numero
            if (e.ColumnIndex > 0 && e.ColumnIndex < dataGridView1.ColumnCount - 1)
            {
                string value = e.FormattedValue.ToString();
                int number;
                if (!int.TryParse(value, out number))
                {
                    e.Cancel = true;
                    MessageBox.Show("Inserire solo numeri nelle celle.");
                }
            }
        }





        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }




        private void buttonMetodi_Click(object sender, EventArgs e)
        {
            int numRows = dataGridView1.RowCount - 1;
            int numCols = dataGridView1.ColumnCount - 2;
            int[,] cellValues = new int[numRows, numCols];

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    string cellValue = "0";
                    if (dataGridView1.Rows[row].Cells[col + 1].Value != null)
                    {
                        cellValue = dataGridView1.Rows[row].Cells[col + 1].Value.ToString();
                    }

                    int parsedValue = 0;
                    if (cellValue != "0")
                    {
                        if (Int32.TryParse(cellValue, out parsedValue))
                        {
                            cellValues[row, col] = parsedValue;
                        }
                        else
                        {
                            cellValues[row, col] = 0;
                        }
                    }
                    else
                    {
                        cellValues[row, col] = 0;
                    }
                }
            }
            MetodoDelNordOvest();
            MinimiCostiAlgoritmo(cellValues);
        }



            private void MinimiCostiAlgoritmo(int[,] cellValues)
        {
            int numRows = cellValues.GetLength(0);
            int numCols = cellValues.GetLength(1);
            int[,] costMatrix = (int[,])cellValues.Clone();
            int[] rowCapacity = new int[numRows];
            int[] colDemand = new int[numCols];
            bool[,] assigned = new bool[numRows, numCols];
            int totalCost = 0;

            string fullOutput = "METODO DEI 'MINIMI COSTI'" + Environment.NewLine;
            fullOutput += "-----------------------------------" + Environment.NewLine;

            // Correggi la lettura dei valori
            for (int i = 0; i < numRows; i++)
            {
                if (dataGridView1.Rows[i].Cells[numCols + 1].Value != null)
                {
                    rowCapacity[i] = Convert.ToInt32(dataGridView1.Rows[i].Cells[numCols + 1].Value);
                }
                else
                {
                    rowCapacity[i] = 0;
                }
            }

            for (int j = 0; j < numCols; j++)
            {
                if (dataGridView1.Rows[numRows].Cells[j + 1].Value != null)
                {
                    colDemand[j] = Convert.ToInt32(dataGridView1.Rows[numRows].Cells[j + 1].Value);
                }
                else
                {
                    colDemand[j] = 0;
                }
            }
            while (true)
            {
                int minCost = int.MaxValue;
                int selectedRow = -1;
                int selectedCol = -1;

                for (int i = 0; i < numRows; i++)
                {
                    for (int j = 0; j < numCols; j++)
                    {
                        if (costMatrix[i, j] < minCost && !assigned[i, j])
                        {
                            minCost = costMatrix[i, j];
                            selectedRow = i;
                            selectedCol = j;
                        }
                    }
                }

                if (minCost == int.MaxValue)
                {
                    break;
                }

                int assignedValue = Math.Min(rowCapacity[selectedRow], colDemand[selectedCol]);

                string message = $"da produttore nr.{selectedRow + 1} a consumatore nr.{selectedCol + 1}: {assignedValue} unità a {costMatrix[selectedRow, selectedCol].ToString("C")} = {(assignedValue * costMatrix[selectedRow, selectedCol]).ToString("C")}";
                fullOutput += message + Environment.NewLine;

                totalCost += assignedValue * costMatrix[selectedRow, selectedCol];

                rowCapacity[selectedRow] -= assignedValue;
                colDemand[selectedCol] -= assignedValue;
                assigned[selectedRow, selectedCol] = true;

                if (rowCapacity[selectedRow] == 0)
                {
                    for (int j = 0; j < numCols; j++)
                    {
                        assigned[selectedRow, j] = true;
                    }
                }

                if (colDemand[selectedCol] == 0)
                {
                    for (int i = 0; i < numRows; i++)
                    {
                        assigned[i, selectedCol] = true;
                    }
                }
            }

            fullOutput += "-----------------------------------" + Environment.NewLine;
            fullOutput += $"TOTALE COSTI = {totalCost.ToString("C")}" + Environment.NewLine;
            fullOutput += "" + Environment.NewLine;
            fullOutput += "" + Environment.NewLine;

            textBoxMetodi.AppendText(fullOutput); // Aggiungi il risultato al testo esistente
        }




        private void MetodoDelNordOvest()
        {
            int numRows = dataGridView1.RowCount - 1;
            int numCols = dataGridView1.ColumnCount - 2;
            int[,] cellValues = new int[numRows, numCols];
            int[,] costMatrix = new int[numRows, numCols];
            string result = "Risultati del Metodo del Nord-Ovest:\r\n";
            result += "-----------------------------------\r\n";

            int[] rowCapacity = new int[numRows];
            int[] colDemand = new int[numCols];

            for (int i = 0; i < numRows; i++)
            {
                if (dataGridView1.Rows[i].Cells[numCols + 1].Value != null)
                {
                    rowCapacity[i] = Convert.ToInt32(dataGridView1.Rows[i].Cells[numCols + 1].Value);
                }
                else
                {
                    rowCapacity[i] = 0;
                }
            }

            for (int j = 0; j < numCols; j++)
            {
                if (dataGridView1.Rows[numRows].Cells[j + 1].Value != null)
                {
                    colDemand[j] = Convert.ToInt32(dataGridView1.Rows[numRows].Cells[j + 1].Value);
                }
                else
                {
                    colDemand[j] = 0;
                }
            }

            int[,] assigned = new int[numRows, numCols];

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j + 1].Value != null)
                    {
                        cellValues[i, j] = Convert.ToInt32(dataGridView1.Rows[i].Cells[j + 1].Value);
                    }
                    else
                    {
                        cellValues[i, j] = 0;
                    }
                }
            }

            // Copia i valori della matrice costMatrix dalla matrice cellValues
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    costMatrix[i, j] = cellValues[i, j];
                }
            }

            int totalCost = 0;

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    int allocation = Math.Min(rowCapacity[i], colDemand[j]);
                    assigned[i, j] = allocation;
                    rowCapacity[i] -= allocation;
                    colDemand[j] -= allocation;
                    totalCost += allocation * cellValues[i, j];

                    // Aggiungi questa parte per stampare il risultato
                    if (allocation > 0)
                    {
                        result += $"Da produttore nr{j + 1} a consumatore nr{i + 1}: {allocation} unità a {costMatrix[i, j].ToString("C")} = {(allocation * costMatrix[i, j]).ToString("C")}" + Environment.NewLine;
                    }
                }
            }

            result += "-----------------------------------" + Environment.NewLine;
            result += $"TOTALE COSTI = {totalCost.ToString("C")}" + Environment.NewLine;
            result += "" + Environment.NewLine;
            result += "" + Environment.NewLine;

            textBoxMetodi.AppendText(result); // Aggiungi il risultato al testo esistente
        }



        private void casuali_Click(object sender, EventArgs e)
        {
            int numRows = (int)numericUpDownRighe.Value;
            int numCols = (int)numericUpDownColonne.Value + 2;

            if (numRows <= 0 || numCols <= 0)
            {
                MessageBox.Show("Inserire il numero di righe e colonne prima di popolare la tabella.");
                return;
            }

            Random random = new Random();

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 1; col < numCols - 1; col++)
                {
                    int numeroCasuale = random.Next(1, 101); // Genera numeri casuali nell'intervallo da 1 a 100
                    dataGridView1.Rows[row].Cells[col].Value = numeroCasuale.ToString();
                }
            }

            CalcolaTotali();
        }
    }
}