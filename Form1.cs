using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Run_length
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Method to draw a square based on the x and y values, the color, and the number of boxes to draw based on the csv array value
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        /// <param name="csvArrayi"></param>
        private void DrawSquare(int x, int y, Color color, int csvArrayi)
        {
            Graphics paper = pictureBox1.CreateGraphics();
            SolidBrush brush = new SolidBrush(color);
            //Fill the rectangle, multiplhying the amount of reoccuring squares time the size of the box to get the correct size of the rectangle to draw
            paper.FillRectangle(brush, x, y, 10 * csvArrayi, 10);
        }
        /// <summary>
        /// Opens the file explorer and draws the image based on the csv file using black and white boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFile_Click(object sender, EventArgs e)
        {
            try
            {

                //The x and y values
                int x = 0;
                int y = 0;
                //The openfile dialog
                OpenFileDialog ofd = new OpenFileDialog();
                //If user selects a file and hits ok open it
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    //Read the file and split the first line to get the size of the image
                    StreamReader sReader = File.OpenText(ofd.FileName);
                    string[] sizeArray = sReader.ReadLine().Split(',');
                    while (!sReader.EndOfStream)
                    {
                        //Read the line and split it into an array to get the number of black and white boxes to draw
                        string line = sReader.ReadLine();
                        string[] csvArray = line.Split(',');
                        //Draw the boxes based on the csvarray length
                        for (int i = 0; i < csvArray.Length; i += 2)
                        {
                            //Draw a white square first based on the number provided times the size of the box
                            DrawSquare(x, y, Color.White, int.Parse(csvArray[i]));
                            //shift the x value to the right based on the number of boxes drawn
                            x += (10 * int.Parse(csvArray[i]));
                            //if the next value in the array is out of bounds break out of the loop
                            if (i >= csvArray.Length - 1)
                            {
                                break;
                            }
                            //Draw a black square second based on the number provided times the size of the box
                            DrawSquare(x, y, Color.Black, int.Parse(csvArray[i + 1]));
                            //shift the x value to the right based on the number of boxes drawn
                            x += (10 * int.Parse(csvArray[i + 1]));
                            //if the next value in the array is out of the intended size bounds shift the y down and reset the x value to 0
                            if (sizeArray[0] == x.ToString())
                            {
                                y += 10;
                                x = 0;
                            }
                        }
                        //Shift the y down and reset the x value to 0 after each line is drawn
                        y += 10;
                        x = 0;

                    }
                }

            }
            //Incase of an error show a message box to the user
            catch
            {
                MessageBox.Show("Please select a valid csv file");
            }
        }
        /// <summary>
        /// Clears the picture box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClear_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }
        /// <summary>
        /// Exits the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
