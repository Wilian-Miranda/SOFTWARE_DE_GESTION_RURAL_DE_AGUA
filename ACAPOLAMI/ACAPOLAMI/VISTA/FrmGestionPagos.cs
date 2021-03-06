﻿using ACAPOLAMI.MODELO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ACAPOLAMI.DOMINIO;
using ACAPOLAMI.NEGOCIO;
using ACAPOLAMI.DAO;
using System.Collections;
using WilianMiranda01.VISTA;

namespace ACAPOLAMI.VISTA
{
    public partial class FrmGestionPagos : Form
    {
        ClsDSucesos sucesos = new ClsDSucesos();
        ClsCalculosPagos calculos = new ClsCalculosPagos();
        public string Id;
        public FrmGestionPagos()
        {
            InitializeComponent();
            CargarEstados();
            CargarConsumidor();
            txtMontoBase.Text = FmrPrincipal.pagoBase;
            cbEstado.Text = estadoCB;
        }

        private void FrmGestioPagos_Load(object sender, EventArgs e)
        {
            DatosCBseleccionados();
            cbEstado.SelectedIndex = -1;
            cbEstado.Text = estadoCB;

            if (ValidacionCajasTexto() && btnEjecutar.Text.Equals("Actualizar"))
            {
                txtPendiente.ReadOnly = false;
                txtTotal.ReadOnly = false;
            }
        }

        public String estadoCB;
        public String nombreNonsumidor;
        private void DatosCBseleccionados()
        {
            cbConsumidor.Text = nombreNonsumidor;
            using (ACAPOLAMIEntities db = new ACAPOLAMIEntities())
            {
                var cargarDatos = (from a in db.sp_MostrarConsumidores()
                                   where (a.Nombres + " " + a.Apellidos) == nombreNonsumidor
                                   select a).ToList();
                foreach (var i in cargarDatos)
                {
                    txtIdConsumidor.Text = i.Id.ToString();
                    txtNombres.Text = i.Nombres;
                    txtApellidos.Text = i.Apellidos;
                }
            }

        }

        private void BuscarConsumidor()
        {
            if (!cbConsumidor.Text.Equals(""))
            {
                CargarConsumidor();
                if (txtCancelado.Text != "" && txtCancelado.Text != "0.0000")
                {
                    ClsCalculosPagos calculos = new ClsCalculosPagos();
                    calculos.CalculoDePago(Convert.ToDouble(txtMontoBase.Text, System.Globalization.CultureInfo.InvariantCulture), 
                        Convert.ToDouble(txtCancelado.Text, System.Globalization.CultureInfo.InvariantCulture),
                        Convert.ToDouble(txtPendiente.Text, System.Globalization.CultureInfo.InvariantCulture), 
                        Convert.ToDouble(txtImpuesto.Text, System.Globalization.CultureInfo.InvariantCulture), 
                        Convert.ToDouble(txtTotal.Text, System.Globalization.CultureInfo.InvariantCulture));
                }
            }
        }

        private void btnBuscarConsumidor_Click(object sender, EventArgs e)
        {
            Limpiar();
            BuscarConsumidor();
            ConsumidorCBSeleccionado();
            txtCancelado.Focus();
            txtIdConsumidor.ReadOnly = true;
            txtMontoBase.Text = FmrPrincipal.pagoBase;

            if (Convert.ToDouble(txtTotal.Text) < 0)
                txtTotal.Text = "0.0000";
        }
        private void ConsumidorCBSeleccionado()
        {
            if (cbConsumidor.SelectedIndex != -1)
            {
                DatosConsumidor();
            }
        }

        private void DatosConsumidor()
        {
            String id = cbConsumidor.SelectedValue.ToString();

            using (ACAPOLAMIEntities db = new ACAPOLAMIEntities())
            {
                var agregarDatos = (from a in db.sp_MostrarConsumidores()
                                    where a.Id.ToString() == id
                                    select new
                                    {
                                        a.Id,
                                        a.Nombres,
                                        a.Apellidos
                                    }).ToList();
                foreach (var i in agregarDatos)
                {
                    txtIdConsumidor.Text = i.Id.ToString();
                    txtNombres.Text = i.Nombres;
                    txtApellidos.Text = i.Apellidos;
                }

                var deuda = (from a in db.Pagos
                             where a.idConsumidor_FK.ToString() == id
                             select a.montoTotal).ToList();

                txtTotal.Text = deuda.Sum().ToString();
            }
        }

        private void CargarConsumidor()
        {
            using (ACAPOLAMIEntities db = new ACAPOLAMIEntities())
            {
                var consumidores = (from a in db.sp_MostrarConsumidores()
                                    where a.Nombres.ToLower().Contains(cbConsumidor.Text.ToLower())
                                    || a.Apellidos.ToLower().Contains(cbConsumidor.Text.ToLower())
                                    || (a.Nombres + " " + a.Apellidos).ToLower().Contains(cbConsumidor.Text.ToLower())
                                    select new
                                    {
                                        a.Id,
                                        nombres = a.Nombres + " " + a.Apellidos,
                                    }).ToList();

                cbConsumidor.DataSource = consumidores.ToList();
                cbConsumidor.DisplayMember = "nombres";
                cbConsumidor.ValueMember = "Id";
            }
        }

        private void txtImpuesto_Enter(object sender, EventArgs e)
        {
            if (txtImpuesto.Text.Equals("0.0000"))
            {
                txtImpuesto.Text = "";
                txtImpuesto.ForeColor = Color.RoyalBlue;
            }
        }

        private void txtImpuesto_Leave(object sender, EventArgs e)
        {
            if (txtImpuesto.Text.Equals(""))
            {
                txtImpuesto.Text = "0.0000";
                txtImpuesto.ForeColor = Color.Gray;
            }
        }

        private void txtMontoBase_Enter(object sender, EventArgs e)
        {
            if (txtMontoBase.Text.Equals("0.0000"))
            {
                txtMontoBase.Text = "";
                txtMontoBase.ForeColor = Color.RoyalBlue;
            }
        }

        private void txtMontoBase_Leave(object sender, EventArgs e)
        {
            if (txtMontoBase.Text.Equals(""))
            {
                txtMontoBase.Text = "0.0000";
                txtMontoBase.ForeColor = Color.Gray;
            }
        }

        private void txtCancelado_Enter(object sender, EventArgs e)
        {
            if (txtCancelado.Text.Equals("0.0000"))
            {
                txtCancelado.Text = "";
                txtCancelado.ForeColor = Color.RoyalBlue;
            }
        }

        private void txtCancelado_Leave(object sender, EventArgs e)
        {
            if (txtCancelado.Text.Equals(""))
            {
                txtCancelado.Text = "0.0000";
                txtCancelado.ForeColor = Color.Gray;
            }
        }

        private Boolean ValidacionCajasTexto()
        {
            Boolean permitir = true;

            if (txtIdConsumidor.Text.Equals("Codigo"))
            {
                ControlValidacion.SetError(txtIdConsumidor, "Este campo es obligatorio");
                permitir = false;
            }
            if (txtNombres.Text.Equals("Primero Segundo"))
            {
                ControlValidacion.SetError(txtNombres, "Este campo es obligatorio");
                permitir = false;
            }
            if (txtApellidos.Text.Equals("Primero Segundo"))
            {
                ControlValidacion.SetError(txtApellidos, "Este campo es obligatorio");
                permitir = false;
            }
            return permitir;
        }

        private void DesactivarValidacion()
        {
            ControlValidacion.SetError(txtIdConsumidor, "");
            ControlValidacion.SetError(txtNombres, "");
            ControlValidacion.SetError(txtApellidos, "");
            ControlValidacion.SetError(txtMontoBase, "");
            ControlValidacion.SetError(txtCancelado, "");
            ControlValidacion.SetError(txtImpuesto, "");
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            DesactivarValidacion();
            if (ValidacionCajasTexto()&&btnEjecutar.Text.Equals("Insertar"))
            {
                //Evaluando condiciones para evitar errores al ingresar los datos
                if (calculos.Pendiente < 0)
                    txtPendiente.Text = "0";

                if (calculos.Total < 0)
                    txtTotal.Text = "0";

                if (calculos.Impuesto < 0)
                    txtImpuesto.Text = "0";

                if (txtPendiente.Text == "0.0000" || txtImpuesto.Text == "0.0000")
                    CalculosPagos();

                //Agregar pago
                ClsDPagos pago = new ClsDPagos();

                String id = cbConsumidor.SelectedValue.ToString();
                int idconsumidor = Convert.ToInt32(id);

                try
                {
                    if (cbEstado.SelectedValue == null)
                    {
                        cbEstado.SelectedIndex = 1;
                    }

                    pago.AgregarPago(Convert.ToDecimal(txtMontoBase.Text), Convert.ToDecimal(txtCancelado.Text),
                        Convert.ToDecimal(txtPendiente.Text), Convert.ToDecimal(txtImpuesto.Text), Convert.ToDecimal(txtTotal.Text),
                        Convert.ToDateTime(dtpFechaPago.Text), Convert.ToInt32(cbEstado.SelectedValue.ToString()),
                        idconsumidor);
                }

                catch (Exception ex)
                {
                    FrmDialogoError.Error("Ha ocurrido el siguiente error: " + ex.Message);
                }

                FrmNotificaciones notificacion = new FrmNotificaciones(sucesos.CargarDatosSucesos().tipoSuceso,
                            sucesos.CargarDatosSucesos().descripcion, FrmNotificaciones.TipoAlerta.Realizado);
                notificacion.Show();

                Limpiar();

                txtMontoBase.Text = FmrPrincipal.pagoBase;
                btnBuscarConsumidor.PerformClick();
            }
            else if (ValidacionCajasTexto()&&btnEjecutar.Text.Equals("Actualizar"))
            {
                //Modificar pago
                ClsDPagos pago = new ClsDPagos();

                try
                {
                    pago.Modificarpago(Convert.ToInt32(txtIdRegistro.Text), Convert.ToDecimal(txtMontoBase.Text), Convert.ToDecimal(txtCancelado.Text),
                        Convert.ToDecimal(txtPendiente.Text), Convert.ToDecimal(txtImpuesto.Text), Convert.ToDecimal(txtTotal.Text),
                        Convert.ToDateTime(dtpFechaPago.Text), Convert.ToInt32(cbEstado.SelectedValue.ToString()),
                        Convert.ToInt32(txtIdConsumidor.Text));
                }

                catch(Exception ex)
                {
                    FrmDialogoError.Error("Ha ocurrido el siguiente error: " + ex.Message);
                }

                FrmNotificaciones notificacion = new FrmNotificaciones(sucesos.CargarDatosSucesos().tipoSuceso,
                            sucesos.CargarDatosSucesos().descripcion, FrmNotificaciones.TipoAlerta.Advertencia);
                notificacion.Show();

                Limpiar();

                this.Close();
            }
            else if (ValidacionCajasTexto() && btnEjecutar.Text.Equals("Eliminar"))
            {
                //Eliminar pago
                ClsDPagos pago = new ClsDPagos();

                try
                {
                    pago.EliminarPago(Convert.ToInt32(txtIdRegistro.Text));
                }

                catch(Exception ex)
                {
                    FrmDialogoError.Error("Ha ocurrido el siguiente error: " + ex.Message);
                }

                FrmNotificaciones notificacion = new FrmNotificaciones(sucesos.CargarDatosSucesos().tipoSuceso,
                        sucesos.CargarDatosSucesos().descripcion, FrmNotificaciones.TipoAlerta.Error);
                notificacion.Show();

                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCerrarGestionConsumidores_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #region CONTROL DE EVENTOS DE CAMBIO DE COLOR DE LOS BOTONES
        ButtonColor boton = new ButtonColor();
        ClsButtonColor color = new ClsButtonColor();
        private void btnCerrarGestionConsumidores_MouseHover(object sender, EventArgs e)
        {
            boton.BotonRojo = btnCerrarGestionConsumidores;
            color.Rojo(boton);
        }

        private void btnCerrarGestionConsumidores_MouseLeave(object sender, EventArgs e)
        {
            boton.BotonAzulOscuro = btnCerrarGestionConsumidores;
            color.AzulOscuro(boton);
        }

        private void btnCerrarGestionConsumidores_MouseMove(object sender, MouseEventArgs e)
        {
            boton.BotonRojo = btnCerrarGestionConsumidores;
            color.Rojo(boton);
        }

        private void btnLimpiar_MouseHover(object sender, EventArgs e)
        {
            boton.BotonAzulClaro = btnLimpiar;
            color.AzulClaro(boton);
            btnLimpiar.ForeColor = Color.White;
        }

        private void btnLimpiar_MouseLeave(object sender, EventArgs e)
        {
            btnLimpiar.BackColor = Color.White;
            btnLimpiar.ForeColor = Color.Black;
        }

        private void btnLimpiar_MouseMove(object sender, MouseEventArgs e)
        {
            boton.BotonAzulClaro = btnLimpiar;
            color.AzulClaro(boton);
            btnLimpiar.ForeColor = Color.White;
        }

        private void btnEjecutar_MouseHover(object sender, EventArgs e)
        {
            boton.BotonAzulClaro = btnEjecutar;
            color.AzulClaro(boton);
        }

        private void btnEjecutar_MouseLeave(object sender, EventArgs e)
        {
            boton.BotonAzulOscuro = btnEjecutar;
            color.AzulOscuro(boton);
        }

        private void btnEjecutar_MouseMove(object sender, MouseEventArgs e)
        {
            boton.BotonAzulClaro = btnEjecutar;
            color.AzulClaro(boton);
        }

        private void btnCancelar_MouseHover(object sender, EventArgs e)
        {
            boton.BotonAzulClaro = btnCancelar;
            color.AzulClaro(boton);
            btnCancelar.ForeColor = Color.White;
        }

        private void btnCancelar_MouseLeave(object sender, EventArgs e)
        {
            btnCancelar.BackColor = Color.White;
            btnCancelar.ForeColor = Color.Black;
        }

        private void btnCancelar_MouseMove(object sender, MouseEventArgs e)
        {
            boton.BotonAzulClaro = btnCancelar;
            color.AzulClaro(boton);
            btnCancelar.ForeColor = Color.White;
        }

        private void btnBuscarConsumidor_MouseHover(object sender, EventArgs e)
        {
            boton.BotonAzulClaro = btnBuscarConsumidor;
            color.AzulClaro(boton);

        }

        private void btnBuscarConsumidor_MouseLeave(object sender, EventArgs e)
        {
            btnBuscarConsumidor.BackColor = Color.White;
            btnBuscarConsumidor.ForeColor = Color.Black;
        }

        private void btnBuscarConsumidor_MouseMove(object sender, MouseEventArgs e)
        {
            boton.BotonAzulClaro = btnBuscarConsumidor;
            color.AzulClaro(boton);
        }
        #endregion

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()  
        {
            txtIdConsumidor.Text = "Codigo";
            txtNombres.Text = "Primero Segundo";
            txtApellidos.Text = "Primero Segundo";
            txtMontoBase.Text = FmrPrincipal.pagoBase;
            txtImpuesto.Text = "0.0000";
            txtTotal.Text = "0.0000";
            txtPendiente.Text = "0.0000";
            txtCancelado.Text = "0.0000";
        }

        //metodo para cargar los estados
        private void CargarEstados()
        {
            using(ACAPOLAMIEntities db = new ACAPOLAMIEntities())
            {
                var estados = (from a in db.Estados
                               select a).ToList();

                if (estados.Count > 0)
                {
                    cbEstado.DataSource = estados.ToList();
                    cbEstado.DisplayMember = "nombreEstado";
                    cbEstado.ValueMember = "idEstado";
                }
            }
        }
        //Evento para mover el formulario
        int posX = 0;
        int posY = 0;

        private void pnlEncabezado_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button!=MouseButtons.Left)
            {
                posX = e.X;
                posY = e.Y;
            }
            else
            {
                Left += e.X - posX;
                Top += e.Y - posY;
            }
        }

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                posX = e.X;
                posY = e.Y;
            }
            else
            {
                Left += e.X - posX;
                Top += e.Y - posY;
            }
        }

        private void cbConsumidor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConsumidorCBSeleccionado();
        }

        private void cbConsumidor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && cbConsumidor.Text != "")
            {
                e.Handled = true;
                btnBuscarConsumidor.PerformClick();
            }
        }

        private void cbConsumidor_Enter(object sender, EventArgs e)
        {
            cbConsumidor.SelectAll();
        }

        private void CalculosPagos()
        {
            if (txtCancelado.Text != "" && txtCancelado.Text != "0.0000")
            {
                calculos.CalculoDePago(Convert.ToDouble(txtMontoBase.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture), 
                    Convert.ToDouble(txtCancelado.Text.Replace(",","."), System.Globalization.CultureInfo.InvariantCulture),
                    Convert.ToDouble(txtPendiente.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture), 
                    Convert.ToDouble(txtImpuesto.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture), 
                    Convert.ToDouble(txtTotal.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture));

                txtPendiente.Text = calculos.Pendiente.ToString();
                txtImpuesto.Text = calculos.Impuesto.ToString();
                txtTotal.Text = calculos.Total.ToString();
                cbEstado.SelectedIndex = cbEstado.FindString(calculos.Estado);
            }
        }

        //Solo numeros en los texbox
        private void SoloNumeros(KeyPressEventArgs k)
        {
            if (char.IsDigit(k.KeyChar))
            {
                k.Handled = false;
            }

            else if (char.IsSeparator(k.KeyChar))
            {
                k.Handled = false;
            }

            else if (char.IsControl(k.KeyChar))
            {
                k.Handled = false;
            }

            else if (k.KeyChar.ToString().Equals("."))
            {
                k.Handled = false;
            }

            else
                k.Handled = true;
        }

        private void txtCancelado_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumeros(e);
        }

        private void txtMontoBase_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumeros(e);
        }

        private void txtImpuesto_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumeros(e);
            if (e.KeyChar == 13)
            {
                btnEjecutar.PerformClick();
                e.Handled = true;
            }
        }

        private void txtCancelado_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                CalculosPagos();
                e.Handled = true;
            }
        }

        private void txtImpuesto_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtCancelado.Text != "" && txtCancelado.Text != "0.0000")
            {
                CalculosPagos();
                e.Handled = true;
            }
        }
    }
}
