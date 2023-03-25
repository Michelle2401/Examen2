using Datos;
using System;
using System.Windows.Forms;

namespace Vista
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private void Cancelarbutton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void Aceptarbutton_ClickAsync(object sender, EventArgs e)
        {
            if (UsuariotextBox.Text == string.Empty)
            {
                errorProvider1.SetError(UsuariotextBox, "Ingrese un usuario");
                UsuariotextBox.Focus();
                return;
            }
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(ContraseñatextBox.Text))
            {
                errorProvider1.SetError(ContraseñatextBox, "Ingrese una contraseña");
                ContraseñatextBox.Focus();
                return;
            }
            errorProvider1.Clear();

            UsuarioDatos usuarioDatos = new UsuarioDatos();
            bool usuarioValido = await usuarioDatos.ValidarUsuarioAsync(UsuariotextBox.Text, ContraseñatextBox.Text);

            if (usuarioValido)
            {
                Menu menuFormulario = new Menu();
                Hide();
                menuFormulario.Show();
            }
            else
            {
                MessageBox.Show("Datos de usuario incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void UsuariotextBox_TextChanged_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(UsuariotextBox.Text))
            {
                errorProvider1.Clear();
            }
        }

        private void ContraseñatextBox_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(ContraseñatextBox.Text))
            {
                errorProvider1.Clear();
            }
        }

        private void MostrarContraseñabutton_Click(object sender, EventArgs e)
        {
            if (ContraseñatextBox.PasswordChar == '*')
            {
                ContraseñatextBox.PasswordChar = '\0';
            }
            else
            {
                ContraseñatextBox.PasswordChar = '*';
            }
        }


    }
}
