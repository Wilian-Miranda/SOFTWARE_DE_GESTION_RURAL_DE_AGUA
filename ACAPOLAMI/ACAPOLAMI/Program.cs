﻿using ACAPOLAMI.VISTA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WilianMiranda01;
using WilianMiranda01.VISTA;

namespace ACAPOLAMI
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmPagosPorAnioPorConsumidor());
        }
    }
}
