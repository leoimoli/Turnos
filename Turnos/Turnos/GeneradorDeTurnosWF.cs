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
    public partial class GeneradorDeTurnosWF : Form
    {
        public GeneradorDeTurnosWF()
        {
            InitializeComponent();
        }

        private void GeneradorDeTurnosWF_Load(object sender, EventArgs e)
        {
            CargarCombo();
        }

        private void CargarCombo()
        {
            List<string> CentroDeSalud = new List<string>();
            CentroDeSalud = Negocio.CentroDeSaludNeg.CargarComboCentroDeSalud();
            cmbCentro.Items.Clear();
            cmbCentro.Text = "Seleccione";
            cmbCentro.Items.Add("Seleccione");
            foreach (string item in CentroDeSalud)
            {
                cmbCentro.Text = "Seleccione";
                cmbCentro.Items.Add(item);
            }
        }
        private void cmbCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string var = cmbCentro.Text;
                if (var != "Seleccione")
                {
                    var split1 = var.Split(',')[1];
                    groupBox1.Text = split1;
                    split1 = split1.Trim();
                    string Centro = split1;
                    if (Centro != "Seleccione")
                    {
                        groupBox1.Visible = true;
                        btnCancelar.Visible = true;
                        btnGenerar.Visible = true;
                        List<CentroDeSalud> _centro = new List<CentroDeSalud>();
                        _centro = CentroDeSaludNeg.BuscarCentroDeSaludPorNombre(Centro);
                        var centro = _centro.First();
                        lblidCentro.Text = Convert.ToString(centro.idCentro);
                        lblDomicilioEdit.Text = centro.Domicilio;
                        CargarComboHora();
                        txtFraccionTurno.Text = Convert.ToString(centro.FraccionTurnos);
                        lblidCentro.Text = Convert.ToString(centro.idCentro);
                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarComboHora()
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
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                bool Exito = false;
                int idCentro = Convert.ToInt32(lblidCentro.Text);
                DateTime Fecha = dateTimePicker1.Value;
                string desde = cmbHoraDesde.Text;
                var VarDesde = desde.Split('h')[0];
                string hasta = cmbHoraHasta.Text;
                var VarHasta = hasta.Split('h')[0];
                int HoraDesde = Convert.ToInt32(VarDesde);
                int HoraHasta = Convert.ToInt32(VarHasta);
                int TotalHorasTrabajadas = HoraHasta - HoraDesde;
                int FraccionDeMinutos = Convert.ToInt32(txtFraccionTurno.Text);
                int TotalDeTurnos = CalcularTotalDeTurnos(TotalHorasTrabajadas, FraccionDeMinutos);
                Exito = TurnosNeg.GenerarTurnos(HoraDesde, HoraHasta, TotalDeTurnos, idCentro, Fecha, FraccionDeMinutos);

            }
            catch (Exception ex)
            { }
        }

        private int CalcularTotalDeTurnos(int totalHorasTrabajadas, int fraccionDeMinutos)
        {
            int totalDeTurnos = 0;
            totalDeTurnos = totalHorasTrabajadas * 60 / fraccionDeMinutos;
            return totalDeTurnos;
        }
    }
}
