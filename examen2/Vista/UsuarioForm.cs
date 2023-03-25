using Clases;
using Datos;
using System;
using System.Windows.Forms;

namespace Vista
{
    public partial class UsuarioForm : Form
    {
        public UsuarioForm()
        {
            InitializeComponent();
        }
        string Operacion = string.Empty;
        UsuarioDatos userDatos = new UsuarioDatos();
        Usuario user = new Usuario();

        private void HabilitarControles()
        {
            CodigotextBox.Enabled = true;
            NombretextBox.Enabled = true;
            CorreotextBox.Enabled = true;
            ContraseñatextBox.Enabled = true;
        }

        private void DesabilitarControles()
        {
            CodigotextBox.Enabled = false;
            NombretextBox.Enabled = false;
            CorreotextBox.Enabled = false;
            ContraseñatextBox.Enabled = false;
        }

        private void LimpiarControles()
        {
            CodigotextBox.Clear();
            NombretextBox.Clear();
            CorreotextBox.Clear();
            ContraseñatextBox.Clear();
        }
        private async void LLenarDataGrid()
        {
            UsuariodataGridView.DataSource = await userDatos.DevolverUsuariosAsync();
        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            HabilitarControles();
            Operacion = "nuevo";
        }

        private void Cancelarbutton_Click(object sender, EventArgs e)
        {
            DesabilitarControles();
            LimpiarControles();
        }

        private void UsuarioForm_Load(object sender, EventArgs e)
        {
            LLenarDataGrid();
        }

        private async void Guardarbutton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CodigotextBox.Text))
            {
                errorProvider1.SetError(CodigotextBox, "Ingrese un código");
                CodigotextBox.Focus();
                return;
            }
            if (string.IsNullOrEmpty(NombretextBox.Text))
            {
                errorProvider1.SetError(NombretextBox, "Ingrese un nombre");
                NombretextBox.Focus();
                return;
            }
            user.Codigo = CodigotextBox.Text;
            user.Nombre = NombretextBox.Text;
            user.Correo = CorreotextBox.Text;
            user.Contraseña = ContraseñatextBox.Text;

            bool inserto = await userDatos.InsertarNuevoUsuarioAsync(user);

            if (inserto)
            {
                MessageBox.Show("Usuario Guardado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LLenarDataGrid();
                LimpiarControles();
                DesabilitarControles();

            }
            else
            {
                MessageBox.Show("Usuario no se pudo guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async void Eliminarbutton_Click(object sender, EventArgs e)
        {
            if (UsuariodataGridView.SelectedRows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("¿Desea eliminar el cliente?", "Atención", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    bool elimino = await userDatos.EliminarUsuarioAsync(UsuariodataGridView.CurrentRow.Cells["Codigo"].Value.ToString());
                    if (elimino)
                    {
                        MessageBox.Show("Usuario Eliminado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LLenarDataGrid();
                        LimpiarControles();
                        DesabilitarControles();
                    }
                    else
                    {
                        MessageBox.Show("Usuario no se pudo eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }
    }
}