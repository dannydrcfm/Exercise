using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Exercise.Models;

namespace Exercise
{

    public partial class Form1 : Form
    {

        Graphics drawArea;
        List<Line> lines;
        int lineCount = 0;
        int idLine = 0;
        bool isLineSelected = false;
        bool isButtonEnabled = false;
        public Form1()
        {
            InitializeComponent();
            numericUpDown1.Maximum = 1000;
            numericUpDown2.Maximum = 50;
            numericUpDown2.Minimum = -50;
            drawArea = drawingArea.CreateGraphics();
            lines = new List<Line>();
            
          
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            bool band = false;
            
            
            for (int i = 0; i < lines.Count; i++)
            {
                Line line = lines[i];
                if ((e.X >= line.squareX && e.X <= line.squareX + line.squareWidth) && (e.Y >= line.squareY - line.weight && e.Y <= line.squareY + line.squareHeight - line.weight))
                {

                    
                    if (isButtonEnabled == false)
                    {
                        isLineSelected = true;
                        idLine = i;
                        numericUpDown1.Value = line.startY;
                        numericUpDown2.Value = line.weight;
                        numericUpDown3.Value = line.squareHeight;
                    }
                    band = true;


                }
            }
            
            if (band == false && isButtonEnabled == true)
            {
                Pen pen = new Pen(Color.Black);
                lineCount++;
                Line line = new Line()
                {
                    id = lineCount,
                    startX = 0,
                    startY = e.Y,
                    endX = 50,
                    endY = e.Y,
                    squareX = 50,
                    squareY = e.Y - 15,
                    squareHeight = 30,
                    squareWidth = 30,
                    weight = 0
                };

                //lines.Add(line);
                lines.Add(line);
                lines = lines.OrderBy(i => i.squareY).ToList<Line>();

                mostrarLista(lines);
                drawArea.DrawLine(pen, 0, e.Y, 50, e.Y);
                drawArea.DrawRectangle(pen, 50, e.Y - 15, 30, 30);

                adjustGraphics();
                
            }
         
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (isLineSelected == true)
            {
                

                Pen whitePen = new Pen(Color.White);
                Pen blackPen = new Pen(Color.Black);

                drawArea.DrawRectangle(whitePen, lines[idLine].squareX, lines[idLine].squareY - lines[idLine].weight, lines[idLine].squareHeight, lines[idLine].squareWidth);
                drawArea.DrawLine(whitePen, lines[idLine].startX, lines[idLine].startY, lines[idLine].endX, lines[idLine].endY);

                lines[idLine].weight = (int)numericUpDown2.Value;

                drawArea.DrawRectangle(blackPen, lines[idLine].squareX, lines[idLine].squareY - lines[idLine].weight, lines[idLine].squareHeight, lines[idLine].squareWidth);
                lines[idLine].endY = lines[idLine].squareY + (lines[idLine].squareHeight / 2) - lines[idLine].weight;
                drawArea.DrawLine(blackPen, lines[idLine].startX, lines[idLine].startY, lines[idLine].endX, lines[idLine].endY);

                if (lines.Count > 1)
                {
                    adjustGraphics();
                }
                



            }
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (isLineSelected == true) {
                Pen whitePen = new Pen(Color.White);
                Pen blackPen = new Pen(Color.Black);

                drawArea.DrawLine(whitePen, 0, lines[idLine].startY, 50, lines[idLine].endY);
                drawArea.DrawLine(blackPen, 0, (int)numericUpDown1.Value, 50, lines[idLine].endY);

                lines[idLine].startY = (int)numericUpDown1.Value;


                if (lineCount > 1)
                {
                    if (idLine > 0 && idLine < lines.Count - 1)
                    {
                        for (int i = idLine; i < lines.Count - 1; i++)
                        {

                            if (lines[idLine].startY > lines[idLine + 1].startY)
                            {

                                moveRight();

                            }
                        }
                        for (int i = idLine; i > 0; i--)
                        {
                            if (lines[idLine].startY < lines[idLine - 1].startY)
                            {
                                moveLeft();
                            }
                        }
                    }
                    if (idLine == 0)
                    {
                        for (int i = 0; i < lines.Count - 1; i++)
                        {
                            if (lines[idLine].startY > lines[idLine + 1].startY)
                            {
                                moveRight();
                            }
                        }
                    }
                    if (idLine == lineCount - 1) {
                        for (int i = idLine; i > 0; i--) {
                            if (lines[idLine].startY < lines[idLine - 1].startY) {
                                moveLeft();
                            }
                        }
                    }
                }


            }

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (isLineSelected == true)
            {
                Pen whitePen = new Pen(Color.White);
                Pen blackPen = new Pen(Color.Black);
                drawArea.DrawRectangle(whitePen, lines[idLine].squareX, lines[idLine].squareY - lines[idLine].weight, lines[idLine].squareHeight, lines[idLine].squareWidth);
                drawArea.DrawRectangle(blackPen, lines[idLine].squareX, lines[idLine].squareY - lines[idLine].weight, (int)numericUpDown3.Value, (int)numericUpDown3.Value);
          
                drawArea.DrawLine(whitePen, 0, lines[idLine].startY, 50, lines[idLine].endY);
                
                lines[idLine].endY = lines[idLine].squareY + (lines[idLine].squareHeight / 2) - lines[idLine].weight;
                drawArea.DrawLine(blackPen, 0, lines[idLine].startY, 50, lines[idLine].endY);
                

                lines[idLine].squareWidth = (int)numericUpDown3.Value;
                lines[idLine].squareHeight = (int)numericUpDown3.Value;

                if (lines.Count > 1)
                {
                    adjustGraphics();
                }

                
                


            }
        }
        public void drawRightSide(int i) {
            Pen whitePen = new Pen(Color.White);
            Pen blackPen = new Pen(Color.Black);
           
            drawArea.DrawRectangle(whitePen, lines[i + 1].squareX, lines[i + 1].squareY - lines[i + 1].weight, lines[i + 1].squareHeight, lines[i + 1].squareWidth);
            drawArea.DrawLine(whitePen, 0, lines[i + 1].startY, 50, lines[i + 1].endY);

            lines[i + 1].squareY = lines[i].squareY + lines[i].squareHeight - lines[i].weight + 1 + lines[i + 1].weight;
           

            drawArea.DrawRectangle(blackPen, lines[i + 1].squareX, lines[i + 1].squareY - lines[i + 1].weight, lines[i + 1].squareHeight, lines[i + 1].squareWidth);

            lines[i + 1].endY = lines[i + 1].squareY + (lines[i + 1].squareHeight / 2) - lines[i + 1].weight;

            drawArea.DrawLine(blackPen, 0, lines[i + 1].startY, 50, lines[i + 1].endY);
        }
        public void drawLeftSide(int i) {
            Pen whitePen = new Pen(Color.White);
            Pen blackPen = new Pen(Color.Black);
            drawArea.DrawRectangle(whitePen, lines[i - 1].squareX, lines[i - 1].squareY - lines[i - 1].weight, lines[i - 1].squareHeight, lines[i - 1].squareWidth);
            drawArea.DrawLine(whitePen, 0, lines[i - 1].startY, 50, lines[i - 1].endY);

            lines[i - 1].squareY = lines[i].squareY - lines[i - 1].squareHeight - 1 - lines[i].weight + lines[i - 1].weight;

            drawArea.DrawRectangle(blackPen, lines[i - 1].squareX, lines[i - 1].squareY - lines[i - 1].weight, lines[i - 1].squareHeight, lines[i - 1].squareWidth);

            lines[i - 1].endY = lines[i - 1].squareY + (lines[i - 1].squareHeight / 2) - lines[i - 1].weight;

            drawArea.DrawLine(blackPen, 0, lines[i - 1].startY, 50, lines[i - 1].endY);
        }

        public void mostrarLista(List<Line> lista) {
            foreach (Line x in lista) {
                Console.WriteLine(x.id + " " + x.squareY);        
            }
        }
        public void adjustGraphics() {
            for (int i = idLine; i < lines.Count; i++)
            {

                if (i < lines.Count - 1)
                {
                    if ((lines[i].squareY + lines[i].squareHeight - lines[i].weight) > lines[i + 1].squareY - lines[i + 1].weight)
                    {
                        drawRightSide(i);
                    }
                }
                if (i > 0)
                {
                    if (lines[i].squareY < (lines[i - 1].squareY + lines[i - 1].squareHeight))
                    {
                        drawLeftSide(i);
                    }
                }

            }
            for (int i = idLine; i >= 0; i--)
            {
                if (i > 0)
                {
                    if (lines[i].squareY - lines[i].weight < (lines[i - 1].squareY + lines[i - 1].squareHeight - lines[i - 1].weight))
                    {
                        drawLeftSide(i);
                    }
                }
            }
        }
        public void moveRight() {
            Pen whitePen = new Pen(Color.White);
            Pen blackPen = new Pen(Color.Black);

            drawArea.DrawRectangle(whitePen, lines[idLine].squareX, lines[idLine].squareY - lines[idLine].weight, lines[idLine].squareWidth, lines[idLine].squareHeight);
            drawArea.DrawRectangle(whitePen, lines[idLine + 1].squareX, lines[idLine + 1].squareY - lines[idLine + 1].weight, lines[idLine + 1].squareWidth, lines[idLine + 1].squareHeight);

            drawArea.DrawLine(whitePen, 0, lines[idLine].startY, 50, lines[idLine].endY);
            drawArea.DrawLine(whitePen, 0, lines[idLine + 1].startY, 50, lines[idLine + 1].endY);

            var aux = lines[idLine];
            lines[idLine] = lines[idLine + 1];
            lines[idLine].squareY = aux.squareY;
            aux.squareY = lines[idLine].squareY + lines[idLine].squareHeight + 1;
            lines[idLine + 1] = aux;

            drawArea.DrawRectangle(blackPen, lines[idLine].squareX, lines[idLine].squareY - lines[idLine].weight, lines[idLine].squareWidth, lines[idLine].squareHeight);
            drawArea.DrawRectangle(blackPen, lines[idLine + 1].squareX, lines[idLine + 1].squareY - lines[idLine + 1].weight, lines[idLine + 1].squareWidth, lines[idLine + 1].squareHeight);

            lines[idLine].endY = lines[idLine].squareY + (lines[idLine].squareHeight / 2) - lines[idLine].weight;
            lines[idLine + 1].endY = lines[idLine + 1].squareY + (lines[idLine + 1].squareHeight / 2) - lines[idLine + 1].weight;

            drawArea.DrawLine(blackPen, 0, lines[idLine].startY, 50, lines[idLine].endY);
            drawArea.DrawLine(blackPen, 0, lines[idLine + 1].startY, 50, lines[idLine + 1].endY);
            adjustGraphics();
            idLine++;
        }

        public void moveLeft() {
            Pen whitePen = new Pen(Color.White);
            Pen blackPen = new Pen(Color.Black);

            drawArea.DrawRectangle(whitePen, lines[idLine].squareX, lines[idLine].squareY - lines[idLine].weight, lines[idLine].squareWidth, lines[idLine].squareHeight);
            drawArea.DrawRectangle(whitePen, lines[idLine - 1].squareX, lines[idLine - 1].squareY - lines[idLine - 1].weight, lines[idLine - 1].squareWidth, lines[idLine - 1].squareHeight);

            drawArea.DrawLine(whitePen, 0, lines[idLine].startY, 50, lines[idLine].endY);
            drawArea.DrawLine(whitePen, 0, lines[idLine - 1].startY, 50, lines[idLine - 1].endY);

            var aux = lines[idLine - 1];
            lines[idLine - 1] = lines[idLine];
            lines[idLine - 1].squareY = aux.squareY;
            aux.squareY = lines[idLine - 1].squareY + lines[idLine - 1].squareHeight + 1;
            lines[idLine] = aux;

            drawArea.DrawRectangle(blackPen, lines[idLine].squareX, lines[idLine].squareY - lines[idLine].weight, lines[idLine].squareWidth, lines[idLine].squareHeight);
            drawArea.DrawRectangle(blackPen, lines[idLine - 1].squareX, lines[idLine - 1].squareY - lines[idLine - 1].weight, lines[idLine - 1].squareWidth, lines[idLine - 1].squareHeight);

            lines[idLine].endY = lines[idLine].squareY + (lines[idLine].squareHeight / 2) - lines[idLine].weight;
            lines[idLine - 1].endY = lines[idLine - 1].squareY + (lines[idLine - 1].squareHeight / 2) - lines[idLine - 1].weight;
            

            drawArea.DrawLine(blackPen, 0, lines[idLine].startY, 50, lines[idLine].endY);
            drawArea.DrawLine(blackPen, 0, lines[idLine - 1].startY, 50, lines[idLine - 1].endY);
            idLine--;
            adjustGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isButtonEnabled == false)
            {
                button1.Text = "Cancel";
                isButtonEnabled = true;
            }
            else {
                button1.Text = "Add New";
                isButtonEnabled = false;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isLineSelected == true) { 
                Pen whitePen = new Pen(Color.White);
                drawArea.DrawRectangle(whitePen, lines[idLine].squareX, lines[idLine].squareY - lines[idLine].weight, lines[idLine].squareWidth, lines[idLine].squareHeight);
                drawArea.DrawLine(whitePen, 0, lines[idLine].startY, 50, lines[idLine].endY);

                lines.RemoveAt(idLine);
                lines = lines.OrderBy(i => i.squareY).ToList<Line>();
            }


        }
    }
}
