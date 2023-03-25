using Clases;
using Datos;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class TicketForm : Form
    {
        TicketDatos ticketDatos = new TicketDatos();
        Cliente cliente;
        string Operacion;
        Tickets tickets;
        public TicketForm()
        {
            InitializeComponent();
        }
        private void HabilitarControles()
        {
            IdentidadmaskedTextBox.Enabled = true;
            IdtextBox.Enabled = true;
            CostotextBox.Enabled = true;
            TipoSoportetextBox.Enabled = true;
            DescripcionProblematextBox.Enabled = true;
            DescripcionSoluciontextBox.Enabled = true;

        }
        private void DesabilitarControles()
        {
            IdentidadmaskedTextBox.Enabled = false;
            IdtextBox.Enabled = false;
            CostotextBox.Enabled = false;
            TipoSoportetextBox.Enabled = false;
            DescripcionProblematextBox.Enabled = false;
            DescripcionSoluciontextBox.Enabled = false;
        }

        private void LimpiarControles()
        {
            IdentidadmaskedTextBox.Clear();
            IdtextBox.Clear();
            CostotextBox.Clear();
            TipoSoportetextBox.Clear();
            DescripcionProblematextBox.Clear();
            DescripcionSoluciontextBox.Clear();
        }


        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Operacion = "nuevo";
            HabilitarControles();
        }
        async void CargarTickets()

        {
            TicketdataGridView.DataSource = await ticketDatos.DevolverTicketsAsync();
        }

        private void TicketForm_Load(object sender, EventArgs e)
        {
            CargarTickets();
        }

        private void Modificarbutton_Click(object sender, EventArgs e)
        {
            Operacion = "modificar";
            HabilitarControles();
        }

        private void Cancelarbutton_Click(object sender, EventArgs e)
        {
            DesabilitarControles();
            LimpiarControles();
        }

        private async Task Guardarbutton_ClickAsync(object sender, EventArgs e)
        {
            if (IdentidadmaskedTextBox.Text == "")
            {
                errorProvider1.SetError(IdentidadmaskedTextBox, "Ingrese una identidad");
                IdentidadmaskedTextBox.Focus();
                return;
            }
            if (IdtextBox.Text == String.Empty)
            {
                errorProvider1.SetError(IdtextBox, "Ingrese un ID");
                IdtextBox.Focus();
                return;
            }


            tickets = new Tickets();
            tickets.IdentidadCliente = IdentidadmaskedTextBox.Text;
            tickets.Id = IdtextBox.Text;
            tickets.Costo = Convert.ToDecimal(CostotextBox.Text);
            tickets.TipoSoporte = TipoSoportetextBox.Text;
            tickets.DescripcionProblema = DescripcionProblematextBox.Text;
            tickets.DescripcionSolucion = DescripcionSoluciontextBox.Text;

            if (Operacion == "nuevo")
            {
                bool inserto = await ticketDatos.InsertarNuevoTicketAsync(tickets);
                if (inserto)
                {
                    MessageBox.Show("Ticket guardado");
                    CargarTickets();
                    LimpiarControles();
                    DesabilitarControles();
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el ticket");
                }
            }
            else if (Operacion == "modificar")
            {
                bool modifico = await ticketDatos.InsertarNuevoTicketAsync(tickets);
                if (modifico)
                {
                    MessageBox.Show("Ticket modificado");
                    CargarTickets();
                    LimpiarControles();
                    DesabilitarControles();
                }
                else
                {
                    MessageBox.Show("No se pudo modificar el ticket");
                }
            }
        }

        private async Task Eliminarbutton_ClickAsync(object sender, EventArgs e)
        {
            if (TicketdataGridView.SelectedRows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("¿Desea eliminar el ticket?", "Atención", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    bool elimino = await ticketDatos.EliminarTicketAsync(TicketdataGridView.CurrentRow.Cells["Id"].Value.ToString());

                    if (elimino)
                    {
                        MessageBox.Show("Ticket eliminado");
                        CargarTickets();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el ticket");
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un ticket");
            }
        }
    }
}

