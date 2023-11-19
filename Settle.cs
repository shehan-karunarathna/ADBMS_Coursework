using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
namespace KSmarketing_system
{
    public partial class Settle : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        Cashier cashier;
        public Settle(Cashier cash)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            this.KeyPreview = true;
            cashier = cash;
        }

       
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCash.Clear();
            txtCash.Focus();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                if ((double.Parse(txtChange.Text) < 0) || (txtCash.Text.Equals("")))
                {
                    MessageBox.Show("Insufficient amount, Please enter the corret amount!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    for (int i = 0; i < cashier.dgvCash.Rows.Count; i++)
                    {
                        // procedure and trigger 1 use to update the product table and after that update the tb cart table 
                        cm = new SqlCommand("UpdateProductQuantity", cn);
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.Parameters.AddWithValue("@pcode", cashier.dgvCash.Rows[i].Cells[2].Value.ToString());
                        cm.Parameters.AddWithValue("@qtyToDeduct", int.Parse(cashier.dgvCash.Rows[i].Cells[5].Value.ToString()));

                        cn.Open();
                        cm.ExecuteNonQuery();

                    }
                    Recept recept = new Recept(cashier);
                    recept.LoadRecept(txtCash.Text, txtChange.Text);
                    recept.ShowDialog();

                    MessageBox.Show("Payment successfully saved!", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cashier.GetTranNo();
                    cashier.LoadCart();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double sale = double.Parse(txtSale.Text);
                double cash = double.Parse(txtCash.Text);
                double charge = cash - sale;
                txtChange.Text = charge.ToString("#,##0.00");
            }
            catch (Exception)
            {
                txtChange.Text = "0.00";
            }
        }

        private void Settle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Dispose();
            else if (e.KeyCode == Keys.Enter) btnEnter.PerformClick();
        }

        private void Settle_Load(object sender, EventArgs e)
        {

        }

        private void txtSale_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
