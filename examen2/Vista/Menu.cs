using System;
using System.Windows.Forms;

namespace Vista
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        UsuarioForm usuarioForm = null;
        ClienteForm clienteForm = null;
        TicketForm ticketForm = null;

        private void ListaUsuariotoolStripButton_Click(object sender, EventArgs e)
        {
            if (usuarioForm == null)
            {
                usuarioForm = new UsuarioForm();
                usuarioForm.MdiParent = this;
                usuarioForm.FormClosed += UsuarioForm_FormClosed;
                usuarioForm.Show();
            }
            else
            {
                usuarioForm.Activate();
            }

        }
        private void UsuarioForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            usuarioForm = null;
        }
        private void Menu__FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void ClientestoolStripButton_Click(object sender, EventArgs e)
        {
            if (clienteForm == null)
            {
                clienteForm = new ClienteForm();
                clienteForm.MdiParent = this;
                clienteForm.FormClosed += ClienteForm_FormClosed;
                clienteForm.Show();
            }
            else
            {
                clienteForm.Activate();
            }

        }
        private void ClienteForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            clienteForm = null;
        }

        private void TickettoolStripButton_Click(object sender, EventArgs e)
        {
            if (ticketForm == null)
            {
                ticketForm = new TicketForm();
                ticketForm.MdiParent = this;
                ticketForm.FormClosed += TicketForm_FormClosed;
                ticketForm.Show();
            }
            else
            {
                ticketForm.Activate();
            }
        }
        private void TicketForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ticketForm = null;
        }
    }
}
