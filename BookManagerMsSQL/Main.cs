using System;
using System.Windows.Forms;

namespace BookManager
{
    public partial class Main : Form
    {
        enum enumBook
        {
            Isbn, BookName
        }

        enum enumUser
        {
            UserId, UserName
        }

        public Main()
        {
            InitializeComponent();
            Form1 f1= new Form1();

            f1.TopLevel = false;
            panelcontrol.Controls.Add(f1);
            f1.Show();
            panelcontrol.Controls.Clear();

            Form2 f2 = new Form2();

            f2.TopLevel = false;
            panelcontrol.Controls.Add(f2);
            f2.Show();
            panelcontrol.Controls.Clear();

            Form3 f3 = new Form3();

            f3.TopLevel = false;
            panelcontrol.Controls.Add(f3);
            f3.Show();
            panelcontrol.Controls.Clear();



        }
      
       
        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            panelcontrol.Controls.Clear();
            try
            {
                Form1 f1 = new Form1();
                f1.TopLevel = false;
                panelcontrol.Controls.Add(f1);
                f1.Show();
            }
            catch (IndexOutOfRangeException ie)
            {

            }
            catch(Exception ex)
            {

            }
        }

        private void buttonBook_Click(object sender, EventArgs e)
        {
            panelcontrol.Controls.Clear();
            try
            {
                Form2 f2 = new Form2();

                f2.TopLevel = false;
                panelcontrol.Controls.Add(f2);
                f2.Show();
            }
            catch (IndexOutOfRangeException ie)
            {

            }
            catch (Exception ex)
            {

            }
        }

        private void buttonUser_Click(object sender, EventArgs e)
        {
            panelcontrol.Controls.Clear();
            try
            {
                Form3 f3 = new Form3();
                f3.TopLevel = false;
                panelcontrol.Controls.Add(f3);
                f3.Show();
            }
            catch (IndexOutOfRangeException ie)
            {

            }
            catch (Exception ex)
            {

            }

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
