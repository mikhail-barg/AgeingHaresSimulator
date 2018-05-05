using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgeingHaresSimulator
{
    public sealed partial class ExceptionViewForm : Form
    {
        public static void Show(Exception exception)
        {
            using (ExceptionViewForm form = new ExceptionViewForm(exception))
            {
                form.ShowDialog();
            }
        }

        private readonly Exception m_exception;

        public ExceptionViewForm(Exception e)
        {
            InitializeComponent();
            
            this.m_exception = e;

            while (e != null)
            {
                stackTraceRichTextBox.Text += e.Message + "\n";
                e = e.InnerException;
            };

            stackTraceRichTextBox.Text += "\n\n=============================\n\n";

            e = this.m_exception;
            while (e != null)
            {
                stackTraceRichTextBox.Text += e.GetType().ToString() + " : " + e.Message + "\n" + e.StackTrace + "\n\n";
                e = e.InnerException;
            };
        }

        private void ExceptionViewForm_Load(object sender, EventArgs e)
        {
            //see http://stackoverflow.com/questions/2860026/richtextbox-autowordselection-broken
            stackTraceRichTextBox.AutoWordSelection = false;
        }
    }
}
