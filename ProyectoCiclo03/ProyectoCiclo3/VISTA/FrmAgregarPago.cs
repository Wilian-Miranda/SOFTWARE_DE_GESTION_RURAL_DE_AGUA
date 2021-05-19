﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoCiclo3.DAO;
using ProyectoCiclo3.DOMINIO;
using ProyectoCiclo3.MODELO;
using ProyectoCiclo3.NEGOCIO;

namespace ProyectoCiclo3.VISTA
{
    public partial class FrmAgregarPago : Form
    {
        public FrmAgregarPago()
        {
            InitializeComponent();
            ListarClientesCB();
            ListarEstadosCB();
        }
        ClsButtonColor button = new ClsButtonColor();
        ButtonColor btn = new ButtonColor();

        int posX = 0;
        int posY = 0;
        private void pnlCabeceraEliminarCliente_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                posX = e.X;
                posY = e.Y;
            }
            else
            {
                Left += (e.X - posX);
                Top += (e.Y - posY);
            }
        }

        private void btnEliminar_MouseHover(object sender, EventArgs e)
        {
            btn.BotonAzulClaro = btnAgregar;
            button.AzulClaro(btn);
        }

        private void btnEliminar_MouseLeave(object sender, EventArgs e)
        {
            btn.BotonAzulOscuro = btnAgregar;
            button.AzulOscuro(btn);
        }

        private void btnEliminar_MouseMove(object sender, MouseEventArgs e)
        {
            btn.BotonAzulClaro = btnAgregar;
            button.AzulClaro(btn);
        }

        private void btnCancelar_MouseHover(object sender, EventArgs e)
        {
            btn.BotonAzulClaro = btnCancelar;
            button.AzulClaro(btn);
        }

        private void btnCancelar_MouseLeave(object sender, EventArgs e)
        {
            btn.BotonAzulOscuro = btnCancelar;
            button.AzulOscuro(btn);
        }

        private void btnCancelar_MouseMove(object sender, MouseEventArgs e)
        {
            btn.BotonAzulClaro = btnCancelar;
            button.AzulClaro(btn);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            using (ACAPOLAMIEntities db = new ACAPOLAMIEntities())
            {
                if (cbConsumidor.Text == "" && txtMonto.Text == "")
                {
                    MessageBox.Show("Los campos de Consumidor y el monto son obligatorios");
                }
                else
                {
                    ClsDPagos cls = new ClsDPagos();

                    Pagos pg = new Pagos();

                    pg.monto = Convert.ToInt32(txtMonto.Text);
                    pg.montoCancelado = Convert.ToInt32(txtCancelado.Text);
                    pg.impuesto = Convert.ToInt32(txtImpuesto.Text);

                    if (cbConsumidor.Text == "")
                    {
                        MessageBox.Show("Seleccione un consumidor");
                    }
                    else
                    {
                        cbConsumidor.Text = pg.idConsumidor_FK.ToString();
                    }

                    cls.AgregarPago(pg);
                }
            }
        }

        private string idConsumidor = "";
        private void cbConsumidor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbConsumidor.SelectedValue != null)
            {
                idConsumidor = cbConsumidor.SelectedValue.ToString();
            }
        }

        //método para listar los clientes en el comboBox
        private void ListarClientesCB()
        {
            using (ACAPOLAMIEntities db = new ACAPOLAMIEntities())
            {

                var lista = db.Consumidores.ToList();

                if (lista.Count > 0)
                {
                    cbConsumidor.DataSource = lista;
                    cbConsumidor.DisplayMember = "nombresConsumidor";
                    cbConsumidor.ValueMember = "idConsumidor";

                    if (cbConsumidor.Items.Count > 0)
                    {
                        cbConsumidor.SelectedIndex = -1;
                    }
                }
            }
        }

        //metodo para listar los pagos base en el combobox
        private void ListarEstadosCB()
        {
            using (ACAPOLAMIEntities db = new ACAPOLAMIEntities())
            {
                var lista = db.Estados.ToList();

                if (lista.Count > 0)
                {
                    cbEstado.DataSource = lista;
                    cbEstado.DisplayMember = "nombreEstado";
                    cbEstado.ValueMember = "idEstado";
                    if (cbEstado.Items.Count > 0)
                    {
                        cbEstado.SelectedIndex = -1;
                    }
                }
            }
        }
        private string idEstado = "";
        private void cbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEstado.SelectedValue != null)
            {
                idEstado = cbEstado.SelectedValue.ToString();
            }
        }
    }
}
