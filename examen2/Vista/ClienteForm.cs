using Clases;
using Datos;
using System;
using System.Windows.Forms;

namespace Vista
{
    public partial class ClienteForm : Form
    {
        public ClienteForm()
        {
            InitializeComponent();
        }
        ClienteDatos clienteDatos = new ClienteDatos();
        Cliente cliente;
        string Operacion;

        private void HabilitarControles()
        {
            IdentidadmaskedTextBox.Enabled = true;
            NombretextBox.Enabled = true;
            DirecciontextBox.Enabled = true;
            CorreotextBox.Enabled = true;
        }

        private void DesabilitarControles()
        {
            IdentidadmaskedTextBox.Enabled = false;
            NombretextBox.Enabled = false;
            DirecciontextBox.Enabled = false;
            CorreotextBox.Enabled = false;
        }

        private void LimpiarControles()
        {
            IdentidadmaskedTextBox.Clear();
            NombretextBox.Clear();
            DirecciontextBox.Clear();
            CorreotextBox.Clear();

        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Operacion = "nuevo";
            HabilitarControles();
        }
        async void CargarClientes()
        {
            ClientesdataGridView.DataSource = await clienteDatos.DevolverClientesAsync();
        }

        private void ClienteForm_Load(object sender, EventArgs e)
        {
            CargarClientes();
        }

        private void Cancelarbutton_Click(object sender, EventArgs e)
        {

            DesabilitarControles();
            LimpiarControles();
        }

        private async void Guardarbutton_Click(object sender, EventArgs e)
        {
            if (IdentidadmaskedTextBox.Text == "")
            {
                errorProvider1.SetError(IdentidadmaskedTextBox, "Ingrese una identidad");
                IdentidadmaskedTextBox.Focus();
                return;
            }
            if (NombretextBox.Text == String.Empty)
            {
                errorProvider1.SetError(NombretextBox, "Ingrese un nombre");
                NombretextBox.Focus();
                return;
            }


            cliente = new Cliente();
            cliente.Identidad = IdentidadmaskedTextBox.Text;
            cliente.Nombre = NombretextBox.Text;
            cliente.Direccion = DirecciontextBox.Text;
            cliente.Correo = CorreotextBox.Text;


            if (Operacion == "nuevo")
            {
                bool inserto = await clienteDatos.InsertarNuevoClienteAsync(cliente);
                if (inserto)
                {
                    MessageBox.Show("Cliente guardado");
                    CargarClientes();
                    LimpiarControles();
                    DesabilitarControles();
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el cliente");
                }
            }
        }

        private async void Eliminarbutton_Click(object sender, EventArgs e)
        {
            if (ClientesdataGridView.SelectedRows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("¿Desea eliminar el cliente?", "Atención", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    bool elimino = await clienteDatos.EliminarClienteAsync(ClientesdataGridView.CurrentRow.Cells["Identidad"].Value.ToString());

                    if (elimino)
                    {
                        MessageBox.Show("Cliente eliminado");
                        CargarClientes();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el cliente");
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un cliente");
            }
        }
    }
}
