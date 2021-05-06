﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoCiclo3.VISTA;
using ProyectoCiclo3.MODELO;
using ProyectoCiclo3.NEGOCIO;
using ProyectoCiclo3.DOMINIO;

namespace WilianMiranda01.VISTA
{
    public partial class FrmPagos : Form
    {
        public FrmPagos()
        {
            InitializeComponent();
        }

        ClsButtonColor button = new ClsButtonColor();
        ButtonColor btn = new ButtonColor();
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnRegistrarPago_MouseHover(object sender, EventArgs e)
        {
            btn.BotonAzulOscuro = btnRegistrarPago;
            button.AzulOscuro(btn);
            btnRegistrarPago.ForeColor = Color.White;
        }

        private void btnRegistrarPago_MouseLeave(object sender, EventArgs e)
        {
            btnRegistrarPago.BackColor = Color.WhiteSmoke;
            btnRegistrarPago.ForeColor = Color.Black;
        }

        private void btnRegistrarPago_MouseMove(object sender, MouseEventArgs e)
        {
            btn.BotonAzulOscuro = btnRegistrarPago;
            button.AzulOscuro(btn);
            btnRegistrarPago.ForeColor = Color.White;
        }

        private void btnModificarPago_MouseHover(object sender, EventArgs e)
        {
            btn.BotonAzulOscuro = btnModificarPago;
            button.AzulOscuro(btn);
            btnModificarPago.ForeColor = Color.White;
        }

        private void btnModificarPago_MouseMove(object sender, MouseEventArgs e)
        {
            btn.BotonAzulOscuro = btnModificarPago;
            button.AzulOscuro(btn);
            btnModificarPago.ForeColor = Color.White;
        }

        private void btnModificarPago_MouseLeave(object sender, EventArgs e)
        {
            btnModificarPago.BackColor = Color.WhiteSmoke;
            btnModificarPago.ForeColor = Color.Black;
        }

        private void btnEliminarPago_MouseHover(object sender, EventArgs e)
        {
            btn.BotonRojo = btnEliminarPago;
            button.Rojo(btn);
            btnEliminarPago.ForeColor = Color.White;
        }

        private void btnEliminarPago_MouseLeave(object sender, EventArgs e)
        {
            btnEliminarPago.BackColor = Color.WhiteSmoke;
            btnEliminarPago.ForeColor = Color.Black;
        }

        private void btnEliminarPago_MouseMove(object sender, MouseEventArgs e)
        {
            btn.BotonRojo = btnEliminarPago;
            button.Rojo(btn);
            btnEliminarPago.ForeColor = Color.White;
        }

        private void btnBuscarConsumidor_MouseHover(object sender, EventArgs e)
        {
            btn.BotonAzulOscuro = btnBuscarConsumidor;
            button.AzulOscuro(btn);
            btnBuscarConsumidor.ForeColor = Color.White;
        }

        private void btnBuscarConsumidor_MouseMove(object sender, MouseEventArgs e)
        {
            btn.BotonAzulOscuro = btnBuscarConsumidor;
            button.AzulOscuro(btn);
            btnBuscarConsumidor.ForeColor = Color.White;
        }

        private void btnBuscarConsumidor_MouseLeave(object sender, EventArgs e)
        {
            btnBuscarConsumidor.BackColor = Color.WhiteSmoke;
            btnBuscarConsumidor.ForeColor = Color.Black;
        }

        private void btnModificarPago_Click(object sender, EventArgs e)
        {
            FrmModificarPago modificarPago = new FrmModificarPago();

            if (dtgPagos.SelectedRows.Count > 0)
            {
                //cargando datos al formulario del modificar pago
                modificarPago.txtIdPago.Text = dtgPagos.CurrentRow.Cells[0].Value.ToString();
                modificarPago.txtNombres.Text = dtgPagos.CurrentRow.Cells[1].Value.ToString();
                modificarPago.txtApellidos.Text = dtgPagos.CurrentRow.Cells[2].Value.ToString();
                modificarPago.txtMonto.Text = dtgPagos.CurrentRow.Cells[3].Value.ToString();
                modificarPago.txtCancelado.Text = dtgPagos.CurrentRow.Cells[4].Value.ToString();
                modificarPago.txtImpuesto.Text = dtgPagos.CurrentRow.Cells[5].Value.ToString();
                modificarPago.cbEstado.Text = dtgPagos.CurrentRow.Cells[6].Value.ToString();
                modificarPago.txtFechaDePago.Text = dtgPagos.CurrentRow.Cells[7].Value.ToString();

                modificarPago.ShowDialog();
            }
            else
            {
                MessageBox.Show("Primero debe sleccionar el registro que desea modificar");
            }


            
        }

        private void btnRegistrarPago_Click(object sender, EventArgs e)
        {
            FrmAgregarPago nuevoPago = new FrmAgregarPago();
            nuevoPago.ShowDialog();

        }

        private void btnEliminarPago_Click(object sender, EventArgs e)
        {
            FrmEliminarPago eliminarPago = new FrmEliminarPago();

            if (dtgPagos.SelectedRows.Count > 0)
            {
                //cargando datos al formulario del eliminar pago
                eliminarPago.txtIdPago.Text = dtgPagos.CurrentRow.Cells[0].Value.ToString();
                eliminarPago.txtNombres.Text = dtgPagos.CurrentRow.Cells[1].Value.ToString();
                eliminarPago.txtApellidos.Text = dtgPagos.CurrentRow.Cells[2].Value.ToString();
                eliminarPago.txtMonto.Text = dtgPagos.CurrentRow.Cells[3].Value.ToString();
                eliminarPago.txtCancelado.Text = dtgPagos.CurrentRow.Cells[4].Value.ToString();
                eliminarPago.txtImpuesto.Text = dtgPagos.CurrentRow.Cells[5].Value.ToString();
                eliminarPago.txtEstado.Text = dtgPagos.CurrentRow.Cells[6].Value.ToString();
                eliminarPago.txtFechaDePago.Text = dtgPagos.CurrentRow.Cells[7].Value.ToString();

                eliminarPago.ShowDialog();
            }
            else
            {
                MessageBox.Show("Primero debe seleccionar el registro que desea eliminar");
            }

            
        }

        private void btnRefrescar_MouseHover(object sender, EventArgs e)
        {
            btn.BotonAzulOscuro = btnRefrescar;
            button.AzulOscuro(btn);
            btnRefrescar.ForeColor = Color.White;
        }

        private void btnRefrescar_MouseLeave(object sender, EventArgs e)
        {
            btnRefrescar.BackColor = Color.WhiteSmoke;
            btnRefrescar.ForeColor = Color.Black;
        }

        private void btnRefrescar_MouseMove(object sender, MouseEventArgs e)
        {
            btn.BotonAzulOscuro = btnRefrescar;
            button.AzulOscuro(btn);
            btnRefrescar.ForeColor = Color.White;
        }
    }
}
