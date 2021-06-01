﻿using ACAPOLAMI.DAO;
using ACAPOLAMI.DOMINIO;
using ACAPOLAMI.MODELO;
using ACAPOLAMI.NEGOCIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WilianMiranda01.VISTA
{
    public partial class FrmRegistroUsuario : Form
    {
        public FrmRegistroUsuario()
        {
            InitializeComponent();
        }

        ClsButtonColor button = new ClsButtonColor();
        ButtonColor btn = new ButtonColor();

        private void btnCerrarAplicacion_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnMinimizarAplicacion_Click(object sender, EventArgs e)
        {

            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;
            }

            else if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void btnRegistroUsuario_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            ClsDUsuarios ClsUsuario = new ClsDUsuarios();
            //cls.SaveDatosUserList(txtNombre.Text, txtApellido.Text, Convert.ToInt32(txtEdad), Convert.ToInt32( txtPass));
            Usuarios usuario = new Usuarios();
            usuario.nombre = txtUsuario.Text;
            usuario.clave = txtPassword.Text;

            String comprobarPass = txtConfirmar.Text;
            if (comprobarPass.Equals(txtPassword.Text))
            {
                ClsUsuario.GuardarUsuario(usuario);

                MessageBox.Show("Registrado con exito");
                Form1 frm = new Form1();
                this.Hide();
                frm.Show();
            }
            else {
                MessageBox.Show("Error al registrar");
            }
        }

        int posX = 0;
        int posY = 0;
        private void pnlCabeceraRegistroUsuario_MouseMove(object sender, MouseEventArgs e)
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

        private void btnCerrarAplicacion_MouseHover(object sender, EventArgs e)
        {
            btn.BotonRojo = btnCerrarAplicacion;
            button.Rojo(btn);
        }

        private void btnCerrarAplicacion_MouseLeave(object sender, EventArgs e)
        {
            btn.BotonAzulOscuro = btnCerrarAplicacion;
            button.AzulOscuro(btn);
        }

        private void btnCerrarAplicacion_MouseMove(object sender, MouseEventArgs e)
        {
            btn.BotonRojo = btnCerrarAplicacion;
            button.Rojo(btn);
        }
        
        private void btnRegistrarUsuario_MouseHover(object sender, EventArgs e)
        {
            btn.BotonAzulClaro = btnRegistrarUsuario;
            button.AzulClaro(btn);
        }

        private void btnRegistrarUsuario_MouseLeave(object sender, EventArgs e)
        {
            btn.BotonAzulOscuro = btnRegistrarUsuario;
            button.AzulOscuro(btn);
        }

        private void btnRegistrarUsuario_MouseMove(object sender, MouseEventArgs e)
        {
            btn.BotonAzulClaro = btnRegistrarUsuario;
            button.AzulClaro(btn);
        }

        private void btnCancelarRegistro_MouseHover(object sender, EventArgs e)
        {
            btn.BotonAzulClaro = btnCancelarRegistro;
            button.AzulClaro(btn);
        }

        private void btnCancelarRegistro_MouseLeave(object sender, EventArgs e)
        {
            btn.BotonAzulOscuro = btnCancelarRegistro;
            button.AzulOscuro(btn);
        }

        private void btnCancelarRegistro_MouseMove(object sender, MouseEventArgs e)
        {
            btn.BotonAzulClaro = btnCancelarRegistro;
            button.AzulClaro(btn);
        }

        private void FrmRegistroUsuario_Load(object sender, EventArgs e)
        {

        }
    }
}
