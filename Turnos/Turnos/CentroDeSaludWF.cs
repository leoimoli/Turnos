using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Turnos.Entidades;
using Turnos.Negocio;

namespace Turnos
{
    public partial class CentroDeSaludWF : MasterWF
    {
        public CentroDeSaludWF()
        {
            InitializeComponent();
        }

        private void btnHabilitarBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                FuncionesBotonHabilitarBuscar();
            }
            catch (Exception ex)
            {

            }
        }
        private void FuncionesBotonHabilitarBuscar()
        {
            btnHabilitarBuscar.Visible = false;
            groupBox3.Visible = true;
            txtBuscar.Focus();
            txtBuscar.AutoCompleteCustomSource = Clases_Maestras.PreCargarPorCentros.Autocomplete();
            txtBuscar.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtBuscar.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            try
            {
                FuncionesBotonNuevoCentro();

            }
            catch (Exception ex)
            {
            }
        }
        private void FuncionesBotonNuevoCentro()
        {
            txtBuscar.Clear();
            groupBox3.Visible = false;
            LimpiarCamposBotonNuevoCentro();
            groupBox1.Enabled = true;
            txtNombre.Focus();
            groupBox1.Text = "Nuevo Centro";
            btnHabilitarBuscar.Visible = true;
        }
        private void LimpiarCamposBotonNuevoCentro()
        {
            txtNombre.Clear();
            progressBar1.Value = Convert.ToInt32(null);
            progressBar1.Visible = false;
            groupBox1.Enabled = false;
            txtDomicilio.Clear();
            txtTelefono.Clear();
            txtCodArea.Clear();
            txtTelefono.Clear();
            CargarCombos();
            txtCantidadTurnos.Clear();
            txtFraccionTurno.Clear();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Entidades.CentroDeSalud _centro = CargarEntidad();
                bool Exito = CentroDeSaludNeg.GuardaCentroDeSalud(_centro);
                if (Exito == true)
                {
                    ProgressBar();
                    const string message2 = "Se registro el centro exitosamente.";
                    const string caption2 = "Éxito";
                    var result2 = MessageBox.Show(message2, caption2,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Asterisk);
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtDomicilio.Clear();
            txtCantidadTurnos.Clear();
            txtCodArea.Clear();
            txtTelefono.Clear();
            txtFraccionTurno.Clear();
            CargarCombos();
            progressBar1.Value = Convert.ToInt32(null);
            progressBar1.Visible = false;
            groupBox1.Enabled = false;
        }
        private void ProgressBar()
        {
            progressBar1.Visible = true;
            progressBar1.Maximum = 100000;
            progressBar1.Step = 1;

            for (int j = 0; j < 100000; j++)
            {
                Caluculate(j);
                progressBar1.PerformStep();
            }
        }
        private void Caluculate(int i)
        {
            double pow = Math.Pow(i, i);
        }
        private CentroDeSalud CargarEntidad()
        {
            CentroDeSalud _centro = new CentroDeSalud();
            _centro.Nombre = txtNombre.Text;
            _centro.Domicilio = txtDomicilio.Text;
            _centro.HorarioDesde = cmbHoraDesde.Text;
            _centro.Horariohasta = cmbHoraHasta.Text;
            string telefono = txtCodArea.Text + "-" + txtTelefono.Text;
            _centro.Telefono = telefono;
            _centro.CantidadDeTurnos = Convert.ToInt32(txtCantidadTurnos.Text);
            _centro.FraccionTurnos = Convert.ToInt32(txtFraccionTurno.Text);
            _centro.idUsuario = 1;
            return _centro;
        }

        private void CentroDeSaludWF_Load(object sender, EventArgs e)
        {
            CargarCombos();
        }
        private void CargarCombos()
        {
            string[] Horarios = Clases_Maestras.ValoresConstantes.Hora;
            cmbHoraDesde.Items.Add("Seleccione");
            cmbHoraDesde.Items.Clear();
            foreach (string item in Horarios)
            {
                cmbHoraDesde.Text = "Seleccione";
                cmbHoraDesde.Items.Add(item);
            }
            cmbHoraHasta.Items.Add("Seleccione");
            cmbHoraHasta.Items.Clear();
            foreach (string item in Horarios)
            {
                cmbHoraHasta.Text = "Seleccione";
                cmbHoraHasta.Items.Add(item);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            List<CentroDeSalud> _centro = new List<CentroDeSalud>();
            var NombreCentro = txtBuscar.Text;
            _centro = CentroDeSaludNeg.BuscarCentroDeSaludPorNombre(NombreCentro);
            if (_centro.Count > 0)
            {
                var centro = _centro.First();
                lblidCentro.Text = Convert.ToString(centro.idCentro);
                txtNombre.Text = centro.Nombre;
                txtDomicilio.Text = centro.Domicilio;
                var tel = centro.Telefono;
                string var = tel;
                var split1 = var.Split('-')[0];
                var split2 = var.Split('-')[1];
                split1 = split1.Trim();
                split2 = split2.Trim();
                txtCodArea.Text = split1;
                txtTelefono.Text = split2;
                cmbHoraDesde.Text = centro.HorarioDesde;
                cmbHoraHasta.Text = centro.Horariohasta;
                txtCantidadTurnos.Text = Convert.ToString(centro.CantidadDeTurnos);
                txtFraccionTurno.Text = Convert.ToString(centro.FraccionTurnos);
                btnEditar.Visible = true;
                btnEliminar.Visible = true;
                btnHistorial.Visible = true;
            }
            else
            {
                txtBuscar.Focus();
                const string message = "No existe ningun cliente con el nombre o razón social ingresado.";
                const string caption = "Atención";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                           MessageBoxIcon.Exclamation);
                throw new Exception();
            }
        }
    }
}
