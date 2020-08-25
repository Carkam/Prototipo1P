using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prototipo1P
{
    public partial class frmProducto : Form
    {
        int codigo;
        public frmProducto()
        {
            InitializeComponent();
            procTipo();
            procCodigoA();
        }
        clsConexion conn = new clsConexion();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string Insertar = "INSERT INTO PRODUCTO VALUES(" + codigo + ",'" + textBox1.Text + "','" + textBox2.Text + "'," + Double.Parse(textBox3.Text) +","+Int32.Parse(textBox4.Text)+","+Int32.Parse(cmbCodigo.SelectedItem.ToString())+")";
                OdbcCommand comm = new OdbcCommand(Insertar, conn.Conexion());
                OdbcDataReader mostrarC = comm.ExecuteReader();
                MessageBox.Show("Se agregaron los datos correctamente");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                codigo = 0;
                procCodigoA();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }
        public void procTipo()
        /*Procedimiento para mostrar todos los formatos en los combobox*/
        {
            try
            {
                string Query = "SELECT * FROM TIPOPRODUCTO";
                OdbcDataReader Datos;
                OdbcCommand Consulta = new OdbcCommand();
                Consulta.CommandText = Query;
                Consulta.Connection = conn.Conexion();
                Datos = Consulta.ExecuteReader();
                while (Datos.Read())
                {
                    cmbCodigo.Items.Add(Datos.GetString(0));
                    cmbTIpo.Items.Add(Datos.GetString(1));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }

        public void procCodigoA()
        {
            int numero;
            try      

            {
                string contador = "SELECT COUNT(IDPRODUCTO) FROM PRODUCTO;";
                OdbcCommand comando = new OdbcCommand(contador, conn.Conexion());
                numero = Convert.ToInt32(comando.ExecuteScalar());
               
                if (numero == 0)
                {
                    codigo = 1;
                }
                else
                {
                   
                    codigo = numero + 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }

        private void cmbTIpo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCodigo.SelectedIndex = cmbTIpo.SelectedIndex;
        }
    }
}
