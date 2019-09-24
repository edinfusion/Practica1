using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica1
{
    public partial class Form1 : Form
    {
        public int contartab = 2;
        public String contenido;
        public String contenido2;

        public Form1()
        {
            InitializeComponent();
            

        }
        int noerror = 0;
        int nolexema = 0;
        int NoColumna = 0; ///NO COLUMNA ERRROR
        int NoFila = 0;
        int fil = 0;       //contaddor filas vector
        int filas = 0;     //contaddor filas vector
        int columnas = 0; //contaddor columnas vector
        int p = -1;       //contador nodos treeview
        int q = -1;         //contador nodos treeview   
        int r = -1;         //contador nodos treeview               
        int s = -1;         //contador nodos treeview
        int t = -1;         //contador nodos treeview
        int u = -1;         //contador nodos treeview
        int[,] fechas;        //vector para guardar fechas
        String[,] descripcion;   // vector para guardar descripcion e imagenes                                          
        string[,] tree;          //vector para guardar informacion de nodo seleccionado(treeview)
        string ReporteLexema="";
        string ReporteError="";
        int nopagina = 0;
        //*************************//
        //---Boton Nueva Pestaña---//
        //*************************//
        private void NuevaPestañaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AgregarPestaña();//se llama al metodo desde boton nueva pestaña

        }

        //*****************************//
        //---Metodo Agregar Pestañas---//
        //*****************************//
        private void AgregarPestaña()
        {
            TabPage nuevapestaña = new TabPage("Pestaña" + contartab);
            nuevapestaña.Size = new Size(625, 475);/// defino tamaño de pestaña
            //TextBox nuevotxt = new TextBox();
            /*nuevotxt.Name = "txtEntrada1";//+contartab;////verificar si es necesario correlativo
            nuevotxt.Multiline = true;// que funcione como vector
            nuevotxt.Size = new Size(440, 330);
            nuevotxt.Location = new Point(9, 18); // locacion de txt
            nuevapestaña.Controls.Add(nuevotxt);// agrego txt a pestaña nueva*/
            nuevapestaña.UseVisualStyleBackColor = true;
            tab1.TabPages.Add(nuevapestaña);
            contartab++;
            tab1.SelectedTab = nuevapestaña;//selecciona la pestaña actual
        }


        //**************************//
        //---Metodo Abrir Archivo---//
        //**************************//
        public void AbrirArchivo()
        {
            String ruta;
            string fila = "";
            string texto = "";
            openFileDialog1.Title = "Archivo de planificacion";
            openFileDialog1.Filter = "planificacion(*.ly) | *.ly";
            openFileDialog1.FileName = "";
            openFileDialog1.InitialDirectory = "Escritorio";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                ruta = openFileDialog1.FileName;
                StreamReader leer = new StreamReader(ruta, System.Text.Encoding.UTF8);
                string nombreC = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                while ((fila = leer.ReadLine()) != null)
                {
                    texto += fila + System.Environment.NewLine;
                }
                TextBox nuevotxt = new TextBox();
                nuevotxt.Name = "txtplanificacion";//+contartab;////verificar si es necesario correlativo
                nuevotxt.Multiline = true;// que funcione como vector
                nuevotxt.Size = new Size(440, 330);
                nuevotxt.Location = new Point(9, 18); // locacion de txt
                nuevotxt.ScrollBars = ScrollBars.Vertical;                                      //nuevotxt.Text = contenido;
                nuevotxt.Text = texto;
                leer.Close();

                tab1.SelectedTab.Controls.Add(nuevotxt);

                // contenido = "";
                //txtEntrada1.Text = contenido;


            }
        }

        //**************************//
        //---Boton Cargar Archivo---//
        //**************************//
        private void CargarArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirArchivo();
        }



        //************************//
        //--Metodo de Analizador--//
        //************************// 

        public void Analizador(string cadena)
        {
            int inicio = 0;
            int estadoprincipal = 0;
            char cadenaconcatenar;
            string token = "";
            string error = "";




            for (inicio = 0; inicio < cadena.Length; inicio++)
            {
                cadenaconcatenar = cadena[inicio];
                switch (estadoprincipal)
                {
                    case 0:
                        switch (cadenaconcatenar)
                        {
                            case '\n':
                                NoFila++;                                    ////nuevo
                                NoColumna = 0;                          /////////////nuevo
                                estadoprincipal = 0;
                                break;
                            case ' ':
                                NoColumna++;
                                estadoprincipal = 0;
                                break;
                            case '\t':
                                NoColumna++;
                                estadoprincipal = 0;
                                break;
                            case '\r':
                            case '\b':
                            case '\f':
                                estadoprincipal = 0; //si es espacio o salto de linea o tab sigue en el estado 0
                                break;

                            case 'p':
                                token += cadenaconcatenar;
                                estadoprincipal = 1;
                                break;

                            case 'P':
                                token += cadenaconcatenar;
                                estadoprincipal = 1;
                                break;
                            case ':':
                                //token += cadenaconcatenar;
                                DescripciondelosToken(":");
                                estadoprincipal = 4;
                                //estadoprincipal = 8;
                                //  inicio = inicio - 1;
                                break;
                            case 'A':
                                token += cadenaconcatenar;
                                estadoprincipal = 7;
                                break;
                            case 'a':
                                token += cadenaconcatenar;
                                estadoprincipal = 7;
                                break;
                            case 'M':
                                token += cadenaconcatenar;
                                estadoprincipal = 9;
                                break;
                            case 'm':
                                token += cadenaconcatenar;
                                estadoprincipal = 9;
                                break;
                            case 'D':
                                token += cadenaconcatenar;
                                estadoprincipal = 11;
                                break;
                            case 'd':
                                token += cadenaconcatenar;
                                estadoprincipal = 11;
                                break;
                            case 'i':
                                token += cadenaconcatenar;
                                estadoprincipal = 16;
                                break;
                            case 'I':
                                token += cadenaconcatenar;
                                estadoprincipal = 16;
                                break;
                            case ']':
                                // DescripciondelosToken("]");
                                token += cadenaconcatenar;
                                estadoprincipal = 25;
                                inicio = inicio - 1;
                                break;
                            case ')':
                                //DescripciondelosToken(")");
                                token += cadenaconcatenar;
                                estadoprincipal = 25;
                                inicio = inicio - 1;
                                break;
                            case '>':
                                //DescripciondelosToken(">");
                                token += cadenaconcatenar;
                                estadoprincipal = 25;
                                inicio = inicio - 1;
                                break;
                            case '}':
                                //DescripciondelosToken("}");
                                token += cadenaconcatenar;
                                estadoprincipal = 25;
                                inicio = inicio - 1;
                                break;
                            default:
                                token += cadenaconcatenar;
                                estadoprincipal = 25;
                                inicio = inicio - 1;
                                break;
                        }
                        break;

                    case 1:
                        if (cadenaconcatenar == 'p' || cadenaconcatenar == 'P')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 1;
                            NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('l') || cadenaconcatenar.Equals('L'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 1;       //Sigue en el estado 1
                            NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('a') || cadenaconcatenar.Equals('A'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 1;       //Sigue en el estado 1
                            NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('n') || cadenaconcatenar.Equals('N'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 1;       //Sigue en el estado 1
                            NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('i') || cadenaconcatenar.Equals('I'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 1;       //Sigue en el estado 1
                            NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('f') || cadenaconcatenar.Equals('F'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                            NoColumna++;
                        }

                        else if (!Char.IsLetter(cadenaconcatenar))
                        {

                            error = error + cadenaconcatenar;  //cancatena si es espacio u
                            DescripciondelosToken(token.ToUpper());                 ////////////////////////hacer lo mismo en los demas
                            DescripciondelosToken(error);
                            NoColumna++;
                            error = "";
                            token = "";                                     ////////////////////////hacer lo mismo en los demas
                            //error = "";
                            estadoprincipal = 1;       //Sigue en el estado 1
                        }
                        else if (Char.IsLetter(cadenaconcatenar))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                            NoColumna++;
                        }
                        else if (Char.IsSeparator(cadenaconcatenar) || Char.IsWhiteSpace(cadenaconcatenar))
                        {
                            estadoprincipal = 1;
                        }
                        break;

                    case 2:
                        if (cadenaconcatenar == 'i' || cadenaconcatenar.Equals('I'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 2;
                            NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('c') || cadenaconcatenar.Equals('C'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                            NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('a') || cadenaconcatenar.Equals('A'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                            NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('d') || cadenaconcatenar.Equals('D'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                            NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('o') || cadenaconcatenar.Equals('O'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                            NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('r') || cadenaconcatenar.Equals('R'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 3;
                            inicio = inicio - 1;
                            NoColumna++;
                        }
                        else if (Char.IsLetter(cadenaconcatenar))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                            NoColumna++;
                        }
                        else if (Char.IsSeparator(cadenaconcatenar) || Char.IsWhiteSpace(cadenaconcatenar))
                        {
                            estadoprincipal = 2;
                        }
                        else if (cadenaconcatenar.Equals(':'))
                        {
                           // token += cadenaconcatenar;
                            estadoprincipal = 3;
                            inicio = inicio - 2;
                            NoColumna++;
                        }
                        else if (!Char.IsLetter(cadenaconcatenar))
                        {
                            error = error + cadenaconcatenar;  //cancatena si es espacio u                            
                            DescripciondelosToken(token.ToUpper());
                            DescripciondelosToken(error);
                            NoColumna++;
                            error = "";
                            token = "";
                            //error = "";
                            estadoprincipal = 2;       //Sigue en el estado 1
                        }
                        break;
                    case 3:                                     //Estado de aceptacion
                        DescripciondelosToken(token.ToUpper());           //Enviar al data de token
                        //TokenValidos(token);                   //Token validos en el data
                        token = "";                           //Vacia la cadena 
                        estadoprincipal = 0;                 //regresa al estado 0
                        //posicion++;
                        break;

                    case 4:
                        if (cadenaconcatenar == ':' || Char.IsSeparator(cadenaconcatenar) || Char.IsWhiteSpace(cadenaconcatenar))
                        {
                            estadoprincipal = 4;
                            NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('"'))
                        {
                            estadoprincipal = 20;
                            NoColumna++;
                        }

                        else if (Char.IsDigit(cadenaconcatenar) )
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 5;
                            NoColumna++;
                        }
                        else if (!Char.IsLetter(cadenaconcatenar))
                        {
                            error = error + cadenaconcatenar;  //cancatena si es espacio u
                            DescripciondelosToken(token);                 ////////////////////////hacer lo mismo en los demas
                            DescripciondelosToken(error);
                            NoColumna++;
                            error = "";
                            token = "";
                            estadoprincipal = 4;       //Sigue en el estado 4
                           
                        }

                        break;

                    case 5:
                        if (Char.IsDigit(cadenaconcatenar))
                        {
                            token += cadenaconcatenar;
                            NoColumna++;
                            estadoprincipal = 5;
                        }
                        else if (cadenaconcatenar.Equals('{') || cadenaconcatenar.Equals('(') || cadenaconcatenar.Equals('<'))
                        {

                            estadoprincipal = 24;
                            inicio = inicio - 1;
                        }
                        else if (!Char.IsLetter(cadenaconcatenar))
                        {
                            error = error + cadenaconcatenar;  //cancatena si es espacio u
                           // DescripciondelosToken(token);                 ////////////////////////hacer lo mismo en los demas
                            DescripciondelosToken(error);
                            NoColumna++;
                            error = "";
                            token = "";
                            estadoprincipal = 5;       //Sigue en el estado 4

                        }
                        break;
                    case 6:
                        //MessageBox.Show("error simbolo en CADENA", "Lenguajes Proyecto Fase 1", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DescripciondelosToken(token);
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 7:
                        if (cadenaconcatenar == 'a' || cadenaconcatenar == 'A')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 7;
                            NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('ñ') || cadenaconcatenar.Equals('Ñ'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 7;
                            NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('o') || cadenaconcatenar.Equals('O'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 8;
                            inicio = inicio - 1;
                            NoColumna++;
                        }
                        else if (Char.IsLetter(cadenaconcatenar))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 7;       //Sigue en el estado 1
                            NoColumna++;
                        }
                        else if (Char.IsSeparator(cadenaconcatenar) || Char.IsWhiteSpace(cadenaconcatenar))
                        {
                            estadoprincipal = 7;
                        }
                        else if (cadenaconcatenar.Equals(':'))
                        {
                            // token += cadenaconcatenar;
                            estadoprincipal = 8;
                            inicio = inicio - 2;
                            NoColumna++;
                        }
                        else if (!Char.IsLetter(cadenaconcatenar))
                        {
                            error = error + cadenaconcatenar;  //cancatena si es espacio u
                            DescripciondelosToken(token.ToUpper());                 ////////////////////////hacer lo mismo en los demas
                            DescripciondelosToken(error);
                            NoColumna++;
                            error = "";
                            token = "";
                            estadoprincipal = 7;       //Sigue en el estado 1
                            
                        }
                        break;

                    case 8:
                        DescripciondelosToken(token.ToUpper());
                        token = "";
                        estadoprincipal = 0;
                        break;

                    case 9:
                        if (cadenaconcatenar == 'm' || cadenaconcatenar == 'M')
                        {
                            token += cadenaconcatenar;
                            NoColumna++;
                            estadoprincipal = 9;
                        }
                        else if (cadenaconcatenar.Equals('e') || cadenaconcatenar.Equals('E'))
                        {
                            token += cadenaconcatenar;
                            NoColumna++;
                            estadoprincipal = 9;
                        }
                        else if (cadenaconcatenar.Equals('s') || cadenaconcatenar.Equals('S'))
                        {
                            token += cadenaconcatenar;
                            NoColumna++;
                            estadoprincipal = 10;
                            inicio = inicio - 1;
                        }
                        else if (!Char.IsLetter(cadenaconcatenar))
                        {
                            error = error + cadenaconcatenar;  //cancatena si es espacio u
                            DescripciondelosToken(token.ToUpper());                 ////////////////////////hacer lo mismo en los demas
                            DescripciondelosToken(error);
                            NoColumna++;
                            error = "";
                            token = "";
                            estadoprincipal = 9;       //Sigue en el estado 1
                        }
                        else if (Char.IsLetter(cadenaconcatenar))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 9;       //Sigue en el estado 1
                            NoColumna++;
                        }
                        else if (Char.IsSeparator(cadenaconcatenar) || Char.IsWhiteSpace(cadenaconcatenar))
                        {
                            estadoprincipal = 9;
                        }
                        else if (cadenaconcatenar.Equals(':'))
                        {
                            // token += cadenaconcatenar;
                            estadoprincipal = 10;
                            inicio = inicio - 2;
                            NoColumna++;
                        }
                        break;
                    case 10:
                        DescripciondelosToken(token.ToUpper());
                        token = "";
                        estadoprincipal = 0;
                        break;

                    case 11:
                        if (cadenaconcatenar == 'd' || cadenaconcatenar == 'D')
                        {
                            token += cadenaconcatenar;
                            NoColumna++;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('i') || cadenaconcatenar.Equals('I'))
                        {
                            token += cadenaconcatenar;
                            NoColumna++;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('a') || cadenaconcatenar.Equals('A'))
                        {
                            token += cadenaconcatenar;
                            NoColumna++;
                            estadoprincipal = 12;
                            inicio = inicio - 1;
                        }
                        else if (!Char.IsLetter(cadenaconcatenar))
                        {
                            error = error + cadenaconcatenar;  //cancatena si es espacio u
                            DescripciondelosToken(token.ToUpper());                 ////////////////////////hacer lo mismo en los demas
                            DescripciondelosToken(error);
                            NoColumna++;
                            error = "";
                            token = "";
                            estadoprincipal = 11;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('e') || cadenaconcatenar.Equals('E'))
                        {
                            token += cadenaconcatenar;
                            NoColumna++;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('s') || cadenaconcatenar.Equals('S'))
                        {
                            token += cadenaconcatenar;
                            NoColumna++;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('c') || cadenaconcatenar.Equals('C'))
                        {
                            token += cadenaconcatenar;
                            NoColumna++;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('r') || cadenaconcatenar.Equals('R'))
                        {
                            token += cadenaconcatenar;
                            NoColumna++;
                            estadoprincipal = 13;
                        }
                        else if (Char.IsLetter(cadenaconcatenar))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 13;       //Sigue en el estado 1
                            NoColumna++;
                        }
                        else if (Char.IsSeparator(cadenaconcatenar) || Char.IsWhiteSpace(cadenaconcatenar))
                        {
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals(':'))
                        {
                            // token += cadenaconcatenar;
                            estadoprincipal = 12;
                            inicio = inicio - 2;
                            NoColumna++;
                        }
                        break;

                    case 12:
                        DescripciondelosToken(token.ToUpper());
                        fil++;
                        token = "";
                        estadoprincipal = 0;
                        break;
                    //case 7:
                    //    DescripciondelosToken(token);
                    //    token = "";
                    //    estadoprincipal = 0;
                    //    break;
                    case 13:
                        if (cadenaconcatenar == 'i' || cadenaconcatenar == 'I')
                        {
                            token += cadenaconcatenar;
                            NoColumna++;
                            estadoprincipal = 13;
                        }
                        else if (cadenaconcatenar.Equals('p') || cadenaconcatenar.Equals('P'))
                        {
                            token += cadenaconcatenar;
                            NoColumna++;
                            estadoprincipal = 13;
                        }
                        else if (cadenaconcatenar.Equals('c') || cadenaconcatenar.Equals('C'))
                        {
                            token += cadenaconcatenar;
                            NoColumna++;
                            estadoprincipal = 14;
                        }
                        else if (!Char.IsLetter(cadenaconcatenar))
                        {
                            error = error + cadenaconcatenar;  //cancatena si es espacio u
                            DescripciondelosToken(token.ToUpper());                 ////////////////////////hacer lo mismo en los demas
                            DescripciondelosToken(error);
                            NoColumna++;
                            error = "";
                            token = "";
                            estadoprincipal = 13;       //Sigue en el estado 1
                        }
                        else if (Char.IsLetter(cadenaconcatenar))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 14;       //Sigue en el estado 1
                            NoColumna++;
                        }

                        break;
                    case 14:
                        if (cadenaconcatenar == 'i' || cadenaconcatenar == 'I')
                        {
                            token += cadenaconcatenar;
                            NoColumna++;
                            estadoprincipal = 14;
                        }
                        else if (cadenaconcatenar.Equals('o') || cadenaconcatenar.Equals('ó') || cadenaconcatenar.Equals('O'))
                        {
                            token += cadenaconcatenar;
                            NoColumna++;
                            estadoprincipal = 14;
                        }
                        else if (cadenaconcatenar.Equals('n') || cadenaconcatenar.Equals('N'))
                        {
                            token += cadenaconcatenar;
                            NoColumna++;
                            estadoprincipal = 15;
                            inicio = inicio - 1;
                        }
                        else if (!Char.IsLetter(cadenaconcatenar))
                        {
                            error = error + cadenaconcatenar;  //cancatena si es espacio u
                            DescripciondelosToken(token.ToUpper());                 ////////////////////////hacer lo mismo en los demas
                            DescripciondelosToken(error);
                            NoColumna++;
                            error = "";
                            token = "";
                            estadoprincipal = 14;       //Sigue en el estado 1
                        }
                        else if (Char.IsLetter(cadenaconcatenar))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 14;       //Sigue en el estado 1
                            NoColumna++;
                        }
                        else if (Char.IsSeparator(cadenaconcatenar) || Char.IsWhiteSpace(cadenaconcatenar))
                        {
                            estadoprincipal = 14;
                        }
                        else if (cadenaconcatenar.Equals(':'))
                        {
                            // token += cadenaconcatenar;
                            estadoprincipal = 15;
                            inicio = inicio - 2;
                            NoColumna++;
                        }
                        break;
                    case 15:
                        DescripciondelosToken(token.ToUpper());
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 16:
                        if (cadenaconcatenar == 'i' || cadenaconcatenar == 'I')
                        {
                            token += cadenaconcatenar;
                            NoColumna++;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('m') || cadenaconcatenar.Equals('M'))
                        {
                            token += cadenaconcatenar;
                            NoColumna++;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('a') || cadenaconcatenar.Equals('A'))
                        {
                            token += cadenaconcatenar;
                            NoColumna++;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('g') || cadenaconcatenar.Equals('G'))
                        {
                            token += cadenaconcatenar;
                            NoColumna++;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('e') || cadenaconcatenar.Equals('E'))
                        {
                            token += cadenaconcatenar;
                            NoColumna++;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('n') || cadenaconcatenar.Equals('N'))
                        {
                            token += cadenaconcatenar;
                            NoColumna++;
                            estadoprincipal = 17;
                            inicio = inicio - 1;
                        }
                        else if (!Char.IsLetter(cadenaconcatenar))
                        {
                            error = error + cadenaconcatenar;  //cancatena si es espacio u
                            DescripciondelosToken(token.ToUpper());                 ////////////////////////hacer lo mismo en los demas
                            DescripciondelosToken(error);
                            NoColumna++;
                            error = "";
                            token = "";
                            estadoprincipal = 16;       //Sigue en el estado 1
                        }
                        else if (Char.IsLetter(cadenaconcatenar))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 16;       //Sigue en el estado 1
                            NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals(':'))
                        {
                            // token += cadenaconcatenar;
                            estadoprincipal = 17;
                            inicio = inicio - 2;
                            NoColumna++;
                        }
                        else if (Char.IsSeparator(cadenaconcatenar) || Char.IsWhiteSpace(cadenaconcatenar))
                        {
                            estadoprincipal = 16;
                        }
                        break;
                    case 17:
                        DescripciondelosToken(token.ToUpper());
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 18:
                        if (cadenaconcatenar == '{')
                        {

                            estadoprincipal = 18;
                        }
                        else if (char.IsLetter(cadenaconcatenar))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 18;
                        }
                        else if (cadenaconcatenar.Equals('}'))
                        {
                            estadoprincipal = 19;
                            inicio = inicio - 1;
                        }
                        break;
                    case 19:
                        DescripciondelosToken(token);
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 20:  //////*****NOMBRE CALENDARIO*******//////
                        //if (cadenaconcatenar == '"')
                        //{
                        //    estadoprincipal = 20;
                        //}
                        if (cadenaconcatenar != ';' && cadenaconcatenar != '[' && cadenaconcatenar != '"')
                        {
                            if (cadenaconcatenar == '\n')
                            {
                                NoFila++;
                                estadoprincipal = 20;                      ////////////////////////////63
                            }
                            token += cadenaconcatenar;
                            NoColumna++;
                            estadoprincipal = 20;
                        }
                        else if (cadenaconcatenar.Equals(';'))
                        {
                            estadoprincipal = 23;
                            inicio = inicio - 1;
                        }
                        else if (cadenaconcatenar.Equals('['))
                        {
                            estadoprincipal = 22;
                            inicio = inicio - 1;
                        }
                        break;
                    case 21:
                        if (cadenaconcatenar == '"')
                        {
                            estadoprincipal = 21;
                        }
                        else if (cadenaconcatenar.Equals('['))
                        {

                            estadoprincipal = 22;
                            //inicio = inicio - 1;
                        }
                        else if (cadenaconcatenar.Equals(';'))
                        {

                            estadoprincipal = 23;
                            //inicio = inicio - 1;
                        }

                        break;
                    case 22:
                        Cadenas(token);
                        NoColumna++;
                        DescripciondelosToken("[");                                              
                       // Threeview(token);                       
                        token = "";
                        estadoprincipal = 0;
                        //  elementos = 0;
                        break;

                    case 23:
                        Cadenas(token);
                        NoColumna++;
                        DescripciondelosToken(";");
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 24:
                        if (cadenaconcatenar == '{')
                        {
                            // elementos = 0;
                            Numeros(token);
                            // AgregarNodoAño(token);
                            NoColumna++;
                            DescripciondelosToken("{");
                            // elementos++;
                            //Subnodo(token);
                            token = "";                           //Vacia la cadena 
                            estadoprincipal = 0;
                        }
                        else if (cadenaconcatenar == '(')
                        {
                            // elementos = 1;
                            Numeros(token);
                            NoColumna++;
                            DescripciondelosToken("(");
                            // elementos++;
                            //Subnodo(token);
                            token = "";                           //Vacia la cadena 
                            estadoprincipal = 0;
                        }
                        else if (cadenaconcatenar == '<')
                        {
                            //  elementos = 2;
                            Numeros(token);
                            NoColumna++;
                            DescripciondelosToken("<");

                            //Subnodo(token);

                            token = "";                           //Vacia la cadena 
                            estadoprincipal = 0;
                        }
                        break;
                    case 25:
                        NoColumna++;
                        DescripciondelosToken(token);
                        token = "";
                        estadoprincipal = 0;
                        break;
                }



            }
        }



        //*******************************************//    
        //--------METODO PARA DESCRIBIR TOKEN--------//
        //*******************************************//  
        public void DescripciondelosToken(string tokeniguala)
        {
            switch (tokeniguala)
            {
                case "PLANIFICADOR":
                    nolexema++;
                    ReporteLexema = ReporteLexema + "<tr>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + nolexema + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "Planificador" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "1" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "PALABRA RESERVADA" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "</tr>\r\n";
                    break;
                case "AÑO":
                    nolexema++;
                    ReporteLexema = ReporteLexema + "<tr>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + nolexema + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "Año" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "2" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "PALABRA RESERVADA" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "</tr>\r\n";
                    break;
                case "MES":
                    nolexema++;
                    ReporteLexema = ReporteLexema + "<tr>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + nolexema + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "Mes" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "3" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "PALABRA RESERVADA" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "</tr>\r\n";
                    break;
                case "DIA":
                    nolexema++;
                    ReporteLexema = ReporteLexema + "<tr>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + nolexema + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "Dia" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "4" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "PALABRA RESERVADA" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "</tr>\r\n";
                    break;
                case "DESCRIPCION":
                    nolexema++;
                    ReporteLexema = ReporteLexema + "<tr>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + nolexema + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "Descripcion" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "5" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "PALABRA RESERVADA" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "</tr>\r\n";
                    break;
                case "IMAGEN":
                    nolexema++;
                    ReporteLexema = ReporteLexema + "<tr>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + nolexema + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "Imagen" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "6" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "PALABRA RESERVADA" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "</tr>\r\n";

                    break;
                case ":":
                    nolexema++;
                    ReporteLexema = ReporteLexema + "<tr>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + nolexema + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + ":" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "7" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "SIGNO DOS PUNTOS" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "</tr>\r\n";
                    break;
                case "{":
                    nolexema++;
                    ReporteLexema = ReporteLexema + "<tr>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + nolexema + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "{" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "8" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "LLAVE APERTURA" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "</tr>\r\n";
                    break;
                case "}":
                    nolexema++;
                    ReporteLexema = ReporteLexema + "<tr>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + nolexema + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "}" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "9" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "LLAVE CERRADURA" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "</tr>\r\n";
                    break;
                case "[":
                    nolexema++;
                    ReporteLexema = ReporteLexema + "<tr>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + nolexema + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "[" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "10" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "CORCHETE APERTURA" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "</tr>\r\n";
                    break;
                case "]":
                    nolexema++;
                    ReporteLexema = ReporteLexema + "<tr>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + nolexema + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "]" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "11" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "CORCHETE CERRADURA" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "</tr>\r\n";
                    break;
                case "(":
                    nolexema++;
                    ReporteLexema = ReporteLexema + "<tr>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + nolexema + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "(" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "12" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "PARENTESIS APERTURA" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "</tr>\r\n";
                    break;
                case ")":
                    nolexema++;
                    ReporteLexema = ReporteLexema + "<tr>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + nolexema + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + ")" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "13" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "PARENTESIS CERRADURA" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "</tr>\r\n";
                    break;
                case "<":
                    nolexema++;
                    ReporteLexema = ReporteLexema + "<tr>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + nolexema + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "<" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "14" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "MENOR QUE" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "</tr>\r\n";
                    break;
                case ">":
                    nolexema++;
                    ReporteLexema = ReporteLexema + "<tr>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + nolexema + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + ">" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "15" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "MAYOR QUE" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "</tr>\r\n";
                    break;
                case ";":
                    nolexema++;
                    ReporteLexema = ReporteLexema + "<tr>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + nolexema + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + ";" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "16" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "<td>" + "SIGNO PUNTO Y COMA" + "</td>\r\n";
                    ReporteLexema = ReporteLexema + "</tr>\r\n";
                    break;
                    
                default:
                    ErroresToken(tokeniguala);    //error en el texto
                    //noerror += 1;      //numero de error
                    break;


            }

        }


        //*******************************************//    
        //--------METODO PARA ERRORES TOKEN--------//
        //*******************************************//  
        public void ErroresToken(string error)
        {
            if (Numero(error) == true)
            {
                Numeros(error);
            }
            else
            {
                noerror++;
                ReporteError = ReporteError + "<tr>\r\n";
                ReporteError = ReporteError + "<td>" + noerror + "</td>\r\n";
                ReporteError = ReporteError + "<td>" + NoFila + "</td>\r\n";
                ReporteError = ReporteError + "<td>" + NoColumna + "</td>\r\n";
                ReporteError = ReporteError + "<td>" + error + "</td>\r\n";
                ReporteError = ReporteError + "<td>" + "PALABRA/SIGNO DESCONOCIDO" + " </td>\r\n";
                ReporteError = ReporteError + "</tr>\r\n";
            }
        }


        //****************************************//
        //----METODO PARA RECONOCER CADENAS-------//
        //****************************************//
        public void Cadenas(string token)
        {
            nolexema++;
            ReporteLexema = ReporteLexema + "<tr>\r\n";
            ReporteLexema = ReporteLexema + "<td>" + nolexema + "</td>\r\n";
            ReporteLexema = ReporteLexema + "<td>" + token + "</td>\r\n";
            ReporteLexema = ReporteLexema + "<td>" + "17" + "</td>\r\n";
            ReporteLexema = ReporteLexema + "<td>" + "CADENA" + "</td>\r\n";
            ReporteLexema = ReporteLexema + "</tr>\r\n";
        }

        //****************************************//
        //-----METODO PARA RECONOCER NUMEROS------//
        //****************************************//
        public void Numeros(string token)
        {
            nolexema++;
            ReporteLexema = ReporteLexema + "<tr>\r\n";
            ReporteLexema = ReporteLexema + "<td>" + nolexema + "</td>\r\n";
            ReporteLexema = ReporteLexema + "<td>" + token + "</td>\r\n";
            ReporteLexema = ReporteLexema + "<td>" + "18" + "</td>\r\n";
            ReporteLexema = ReporteLexema + "<td>" + "NUMERO" + "</td>\r\n";
            ReporteLexema = ReporteLexema + "</tr>\r\n";
        }

        //***********************************************//
        //----------------METODO HTML TOKENS-------------//
        //***********************************************//
        private void GenerarHtml()
        {
            nopagina++;
            string ruta = "c:\\Users\\edinf\\Desktop\\tokensreporte";
            if (Directory.Exists(ruta) == false)
            {
                Directory.CreateDirectory(ruta);
            }
            string info = "<!DOCTYPE html>\r\n" +
                "<html lang=\"en\">\r\n" +
              "<head>\r\n" +
                            "<meta charset =" + "\"UTF-8\"" + ">\r\n" +
                            " <title> LISTADO DE TOKENS </title>\r\n" +
                            "<link rel =" + "\"stylesheet\"" + " href =" + "\"tabla.css\"" + ">\r\n" +
              "</head>\r\n" +
              "<body>\r\n" +
                   //" < div id = \"main-container\">\r\n"+
                   "<table>\r\n" +
                   "<thead>\r\n" +
                     "<tr>\r\n" +
                           "<th>No</th>\r\n" +
                           "<th>LEXEMA</th>\r\n" +
                           "<th>ID</th>\r\n" +
                           "<th>TOKEN</th>\r\n" +
                     "</tr>\r\n" +
                  "</thead >\r\n" +
                           ReporteLexema +
                      "</table>\r\n" +
               //" </div>\r\n" +
               "</body>\r\n" +
               "</html>";
            File.WriteAllText("c:\\Users\\edinf\\Desktop\\tokensreporte\\" + "LISTADOTOKENS" + nopagina + ".html", info);

        }

        //***********************************************//
        //----------------METODO HTML ERRORES------------//
        //***********************************************//
        private void GenerarHtmlErrores()
        {
            // nopagina++;
            string ruta = "c:\\Users\\edinf\\Desktop\\erroresreporte";
            if (Directory.Exists(ruta) == false)
            {
                Directory.CreateDirectory(ruta);
            }
            string info = "<!DOCTYPE html>\r\n" +
                "<html lang=\"en\">\r\n" +
              "<head>\r\n" +
                            "<meta charset =" + "\"UTF-8\"" + ">\r\n" +
                            " <title> LISTADO DE ERRORES </title>\r\n" +
                            "<link rel =" + "\"stylesheet\"" + " href =" + "\"tabla.css\"" + ">\r\n" +
              "</head>\r\n" +
              "<body>\r\n" +
                   //" < div id = \"main-container\">\r\n"+
                   "<table>\r\n" +
                   "<thead>\r\n" +
                     "<tr>\r\n" +
                           "<th>No</th>\r\n" +
                           "<th>FILA</th>\r\n" +
                           "<th>COLUMNA</th>\r\n" +
                           "<th>CARACTER</th>\r\n" +
                           "<th>DESCRIPCION</th>\r\n" +
                     "</tr>\r\n" +
                  "</thead >\r\n" +
                           ReporteError +
                      "</table>\r\n" +
               //" </div>\r\n" +
               "</body>\r\n" +
               "</html>";
            File.WriteAllText("c:\\Users\\edinf\\Desktop\\erroresreporte\\" + "LISTADOERRORES" + nopagina + ".html", info);

        }

        //**********************//
        //----Boton Analizar----//
        //**********************//
        public void BtnAnalizar_Click(object sender, EventArgs e)
        {
            //txtSalida.Clear();
            treeView.Nodes.Clear();
            txtDesc.Clear();
            picDes.ImageLocation = "";
            noerror = 0;
            nolexema = 0;
            NoFila = 0;
            NoColumna = 0;
            fil = 0;
            filas = 0;
            columnas = 0;
            p = -1;
            q = -1;
            r = -1;
            s = -1;
            t = -1;
            u = -1;

            foreach (TextBox t in tab1.SelectedTab.Controls.OfType<TextBox>())
            {
                t.Refresh();
                //PSubnodo(t.Text);
                ReporteLexema ="";
                ReporteError ="";
                Analizador(t.Text);
                GenerarHtml();
                GenerarHtmlErrores();
                fechas = new int[fil, 3];
                descripcion = new string[fil, 5];                                  //////////////////////**************//NUEVO///*****************////////////
                if (noerror == 0)
                {
                    NombreTreeview(t.Text);
                    Asubnodo(t.Text);
                    Msubnodo(t.Text);
                    Dsubnodo(t.Text);                   
                    Calendario();
                    ConcatenarDias();
                }
                else
                {
                    MessageBox.Show("No se pudo generar la planificacion, \r\n"+
                        "debido a que hay palabras/signos no reconocidos, \r\n"+
                        "verifique en el reporte de errores, para corregir."
                        , "Informacion Sistema de Planificacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        //********************************//
        //--Metodo corrobar si es numero--//
        //********************************//

        public Boolean Numero(String prueba)
        {
            Boolean valor = true;
            foreach (Char c in prueba.ToCharArray())
            {
                valor = valor && Char.IsDigit(c);
            }

            return valor;
        }

          //*****************************************//
         //---Metodo para mandar nombre a treeview--//
        //*****************************************//

        public void NombreTreeview(string cadena)
        {
            int inicio = 0;
            int estadoprincipal = 0;
            char cadenaconcatenar;
            string token = "";
            //string error = "";




            for (inicio = 0; inicio < cadena.Length; inicio++)
            {
                cadenaconcatenar = cadena[inicio];
                switch (estadoprincipal)
                {
                    case 0:
                        switch (cadenaconcatenar)
                        {
                            case '\n':                                                        
                            case  ' ':
                            case '\r':
                            case '\t':
                            case '\b':
                            case '\f':
                                estadoprincipal = 0; //si es espacio o salto de linea o tab sigue en el estado 0
                                break;

                            case 'p':
                                token += cadenaconcatenar;
                                estadoprincipal = 1;
                                break;

                            case 'P':
                                token += cadenaconcatenar;
                                estadoprincipal = 1;
                                break;
                            case ':':
                                estadoprincipal = 4;
                                break;
                            case 'A':
                                token += cadenaconcatenar;
                                estadoprincipal = 7;
                                break;
                            case 'a':
                                token += cadenaconcatenar;
                                estadoprincipal = 7;
                                break;
                            case 'M':
                                token += cadenaconcatenar;
                                estadoprincipal = 9;
                                break;
                            case 'm':
                                token += cadenaconcatenar;
                                estadoprincipal = 9;
                                break;
                            case 'D':
                                token += cadenaconcatenar;
                                estadoprincipal = 11;
                                break;
                            case 'd':
                                token += cadenaconcatenar;
                                estadoprincipal = 11;
                                break;
                            case 'i':
                                token += cadenaconcatenar;
                                estadoprincipal = 16;
                                break;
                            case 'I':
                                token += cadenaconcatenar;
                                estadoprincipal = 16;
                                break;
                            case ']':
                                // DescripciondelosToken("]");
                                token += cadenaconcatenar;
                                estadoprincipal = 25;
                                inicio = inicio - 1;
                                break;
                            case ')':
                                //DescripciondelosToken(")");
                                token += cadenaconcatenar;
                                estadoprincipal = 25;
                                inicio = inicio - 1;
                                break;
                            case '>':
                                //DescripciondelosToken(">");
                                token += cadenaconcatenar;
                                estadoprincipal = 25;
                                inicio = inicio - 1;
                                break;
                            case '}':
                                //DescripciondelosToken("}");
                                token += cadenaconcatenar;
                                estadoprincipal = 25;
                                inicio = inicio - 1;
                                break;
                            default:
                                token += cadenaconcatenar;
                                estadoprincipal = 25;
                                inicio = inicio - 1;
                                break;
                        }
                        break;

                    case 1:
                        if (cadenaconcatenar == 'p' || cadenaconcatenar == 'P')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 1;
                           // NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('l') || cadenaconcatenar.Equals('L'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 1;       //Sigue en el estado 1
                            //NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('a') || cadenaconcatenar.Equals('A'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 1;       //Sigue en el estado 1
                            //NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('n') || cadenaconcatenar.Equals('N'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 1;       //Sigue en el estado 1
                            //NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('i') || cadenaconcatenar.Equals('I'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 1;       //Sigue en el estado 1
                            //NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('f') || cadenaconcatenar.Equals('F'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                            //NoColumna++;
                        }

                        else if (!Char.IsLetter(cadenaconcatenar))
                        {
                            //error = error + cadenaconcatenar;  //cancatena si es espacio u
                            //DescripciondelosToken(error);
                            //error = "";
                            //error = "";
                            estadoprincipal = 1;       //Sigue en el estado 1
                        }
                        break;

                    case 2:
                        if (cadenaconcatenar == 'i' || cadenaconcatenar.Equals('I'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 2;
                            //NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('c') || cadenaconcatenar.Equals('C'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                            //NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('a') || cadenaconcatenar.Equals('A'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                            //NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('d') || cadenaconcatenar.Equals('D'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                            //NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('o') || cadenaconcatenar.Equals('O'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                            //NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('r') || cadenaconcatenar.Equals('R'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 3;
                            inicio = inicio - 1;
                            // NoColumna++;

                        }
                        else if (!Char.IsLetter(cadenaconcatenar))
                        {
                            // error = error + cadenaconcatenar;  //cancatena si es espacio u
                            //DescripciondelosToken(error);
                            //error = "";
                            //error = "";
                            estadoprincipal = 2;       //Sigue en el estado 1
                        }
                        break;


                    case 3:                                     //Estado de aceptacion
                        //DescripciondelosToken(token.ToUpper());           //Enviar al data de token
                        //TokenValidos(token);                   //Token validos en el data
                        token = "";                           //Vacia la cadena 
                        estadoprincipal = 0;                 //regresa al estado 0
                        //posicion++;
                        break;

                    case 4:
                        if (cadenaconcatenar == ':')
                        {
                            estadoprincipal = 4;
                            // NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('"'))
                        {
                            estadoprincipal = 20;
                            //NoColumna++;
                        }

                        else if (Char.IsDigit(cadenaconcatenar) || Char.IsSeparator(cadenaconcatenar))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 5;
                            //NoColumna++;
                        }
                        else if (!Char.IsLetter(cadenaconcatenar))
                        {
                            //error = error + cadenaconcatenar;  //cancatena si es espacio u
                            //DescripciondelosToken(error);
                            //error = "";
                            ////error = "";
                            estadoprincipal = 4;       //Sigue en el estado 4
                            //NoColumna++;
                        }

                        break;

                    case 5:
                        if (Char.IsDigit(cadenaconcatenar))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 5;
                        }
                        else if (cadenaconcatenar.Equals('{') || cadenaconcatenar.Equals('(') || cadenaconcatenar.Equals('<'))
                        {

                            estadoprincipal = 24;
                            inicio = inicio - 1;
                        }
                        break;
                    case 6:
                        //MessageBox.Show("error simbolo en CADENA", "Lenguajes Proyecto Fase 1", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //DescripciondelosToken(token);
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 7:
                        if (cadenaconcatenar == 'a' || cadenaconcatenar == 'A')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 7;
                            // NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('ñ') || cadenaconcatenar.Equals('Ñ'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 7;
                            //NoColumna++;
                        }
                        else if (cadenaconcatenar.Equals('o') || cadenaconcatenar.Equals('O'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 8;
                            inicio = inicio - 1;
                            //NoColumna++;
                        }
                        else if (!Char.IsLetter(cadenaconcatenar))
                        {
                            //  error = error + cadenaconcatenar;  //cancatena si es espacio u
                            //NoColumna++;
                            //DescripciondelosToken(error);
                            //error = "";
                            //error = "";
                            estadoprincipal = 7;       //Sigue en el estado 1

                        }

                        break;

                    case 8:
                        //DescripciondelosToken(token.ToUpper());
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 9:
                        if (cadenaconcatenar == 'm' || cadenaconcatenar == 'M')
                        {
                            token += cadenaconcatenar;
                            //      NoColumna++;
                            estadoprincipal = 9;
                        }
                        else if (cadenaconcatenar.Equals('e') || cadenaconcatenar.Equals('E'))
                        {
                            token += cadenaconcatenar;
                            //NoColumna++;
                            estadoprincipal = 9;
                        }
                        else if (cadenaconcatenar.Equals('s') || cadenaconcatenar.Equals('S'))
                        {
                            token += cadenaconcatenar;
                            //  NoColumna++;
                            estadoprincipal = 10;
                            inicio = inicio - 1;
                        }
                        else if (!Char.IsLetter(cadenaconcatenar))
                        {
                            //error = error + cadenaconcatenar;  //cancatena si es espacio u
                            //NoColumna++;
                            //DescripciondelosToken(error);
                            //error = "";
                            //error = "";
                            estadoprincipal = 9;       //Sigue en el estado 1
                        }
                        break;
                    case 10:
                        // DescripciondelosToken(token.ToUpper());
                        token = "";
                        estadoprincipal = 0;
                        break;

                    case 11:
                        if (cadenaconcatenar == 'd' || cadenaconcatenar == 'D')
                        {
                            token += cadenaconcatenar;
                            // NoColumna++;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('i') || cadenaconcatenar.Equals('I'))
                        {
                            token += cadenaconcatenar;
                            //NoColumna++;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('a') || cadenaconcatenar.Equals('A'))
                        {
                            token += cadenaconcatenar;
                            //NoColumna++;
                            estadoprincipal = 12;
                            inicio = inicio - 1;
                        }
                        else if (!Char.IsLetter(cadenaconcatenar))
                        {
                            //error = error + cadenaconcatenar;  //cancatena si es espacio u
                            //NoColumna++;
                            //DescripciondelosToken(error);
                            //error = "";
                            //error = "";
                            estadoprincipal = 11;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('e') || cadenaconcatenar.Equals('E'))
                        {
                            token += cadenaconcatenar;
                            //NoColumna++;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('s') || cadenaconcatenar.Equals('S'))
                        {
                            token += cadenaconcatenar;
                            //NoColumna++;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('c') || cadenaconcatenar.Equals('C'))
                        {
                            token += cadenaconcatenar;
                            //NoColumna++;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('r') || cadenaconcatenar.Equals('R'))
                        {
                            token += cadenaconcatenar;
                            //NoColumna++;
                            estadoprincipal = 13;
                        }

                        break;

                    case 12:
                        //DescripciondelosToken(token.ToUpper());
                        //fil++;
                        token = "";
                        estadoprincipal = 0;
                        break;
                    //case 7:
                    //    DescripciondelosToken(token);
                    //    token = "";
                    //    estadoprincipal = 0;
                    //    break;
                    case 13:
                        if (cadenaconcatenar == 'i' || cadenaconcatenar == 'I')
                        {
                            token += cadenaconcatenar;
                            //    NoColumna++;
                            estadoprincipal = 13;
                        }
                        else if (cadenaconcatenar.Equals('p') || cadenaconcatenar.Equals('P'))
                        {
                            token += cadenaconcatenar;
                            //NoColumna++;
                            estadoprincipal = 13;
                        }
                        else if (cadenaconcatenar.Equals('c') || cadenaconcatenar.Equals('C'))
                        {
                            token += cadenaconcatenar;
                            //NoColumna++;
                            estadoprincipal = 14;
                        }
                        else if (!Char.IsLetter(cadenaconcatenar))
                        {
                            ////error = error + cadenaconcatenar;  //cancatena si es espacio u
                            ////NoColumna++;
                            ////DescripciondelosToken(error);
                            ////error = "";
                            //error = "";
                            estadoprincipal = 13;       //Sigue en el estado 1
                        }
                        break;
                    case 14:
                        if (cadenaconcatenar == 'i' || cadenaconcatenar == 'I')
                        {
                            token += cadenaconcatenar;
                            //NoColumna++;
                            estadoprincipal = 14;
                        }
                        else if (cadenaconcatenar.Equals('o') || cadenaconcatenar.Equals('ó') || cadenaconcatenar.Equals('O'))
                        {
                            token += cadenaconcatenar;
                            //NoColumna++;
                            estadoprincipal = 14;
                        }
                        else if (cadenaconcatenar.Equals('n') || cadenaconcatenar.Equals('N'))
                        {
                            token += cadenaconcatenar;
                            //NoColumna++;
                            estadoprincipal = 15;
                            inicio = inicio - 1;
                        }
                        else if (!Char.IsLetter(cadenaconcatenar))
                        {
                            //error = error + cadenaconcatenar;  //cancatena si es espacio u
                            //NoColumna++;
                            //DescripciondelosToken(error);
                            //error = "";
                            ////error = "";
                           estadoprincipal = 14;       //Sigue en el estado 1
                        }
                        break;
                    case 15:
                        //DescripciondelosToken(token.ToUpper());
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 16:
                        if (cadenaconcatenar == 'i' || cadenaconcatenar == 'I')
                        {
                            token += cadenaconcatenar;
                            //     NoColumna++;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('m') || cadenaconcatenar.Equals('M'))
                        {
                            token += cadenaconcatenar;
                            //NoColumna++;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('a') || cadenaconcatenar.Equals('A'))
                        {
                            token += cadenaconcatenar;
                            //NoColumna++;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('g') || cadenaconcatenar.Equals('G'))
                        {
                            token += cadenaconcatenar;
                            // NoColumna++;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('e') || cadenaconcatenar.Equals('E'))
                        {
                            token += cadenaconcatenar;
                            // NoColumna++;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('n') || cadenaconcatenar.Equals('N'))
                        {
                            token += cadenaconcatenar;
                            //NoColumna++;
                            estadoprincipal = 17;
                            inicio = inicio - 1;
                        }
                        else if (!Char.IsLetter(cadenaconcatenar))
                        {
                            //error = error + cadenaconcatenar;  //cancatena si es espacio u
                            //NoColumna++;
                            //DescripciondelosToken(error);
                            //error = "";
                            ////error = "";
                            estadoprincipal = 16;       //Sigue en el estado 1
                        }
                        break;
                    case 17:
                        //DescripciondelosToken(token.ToUpper());
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 18:
                        if (cadenaconcatenar == '{')
                        {

                            estadoprincipal = 18;
                        }
                        else if (char.IsLetter(cadenaconcatenar))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 18;
                        }
                        else if (cadenaconcatenar.Equals('}'))
                        {
                            estadoprincipal = 19;
                            inicio = inicio - 1;
                        }
                        break;
                    case 19:
                        //DescripciondelosToken(token);
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 20:  //////*****NOMBRE CALENDARIO*******//////
                        //if (cadenaconcatenar == '"')
                        //{
                        //    estadoprincipal = 20;
                        //}
                        if (cadenaconcatenar != ';' && cadenaconcatenar != '[' && cadenaconcatenar != '"')
                        {
                            if (cadenaconcatenar == '\n')
                            {
                                //        NoFila++;
                                estadoprincipal = 20;                      ////////////////////////////63
                            }
                            token += cadenaconcatenar;
                            //NoColumna++;
                            estadoprincipal = 20;
                        }
                        else if (cadenaconcatenar.Equals(';'))
                        {
                            estadoprincipal = 23;
                            inicio = inicio - 1;
                        }
                        else if (cadenaconcatenar.Equals('['))
                        {
                            estadoprincipal = 22;
                            inicio = inicio - 1;
                        }
                        break;
                    case 21:
                        if (cadenaconcatenar == '"')
                        {
                            estadoprincipal = 21;
                        }
                        else if (cadenaconcatenar.Equals('['))
                        {

                            estadoprincipal = 22;
                            //inicio = inicio - 1;
                        }
                        else if (cadenaconcatenar.Equals(';'))
                        {

                            estadoprincipal = 23;
                            //inicio = inicio - 1;
                        }

                        break;
                    case 22:
                        //Cadenas(token);
                        //NoColumna++;
                        //DescripciondelosToken("[");
                        Threeview(token);                       
                        token = "";
                        estadoprincipal = 0;
                        //  elementos = 0;
                        break;

                    case 23:
                        //Cadenas(token);
                        //NoColumna++;
                        //DescripciondelosToken(";");
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 24:
                        if (cadenaconcatenar == '{')
                        {
                            // elementos = 0;
                            //Numeros(token);
                            // AgregarNodoAño(token);
                            //NoColumna++;
                            //DescripciondelosToken("{");
                            // elementos++;
                            //Subnodo(token);
                            token = "";                           //Vacia la cadena 
                            estadoprincipal = 0;
                        }
                        else if (cadenaconcatenar == '(')
                        {
                            // elementos = 1;
                            //Numeros(token);
                            //NoColumna++;
                            //DescripciondelosToken("(");
                            // elementos++;
                            //Subnodo(token);
                            token = "";                           //Vacia la cadena 
                            estadoprincipal = 0;
                        }
                        else if (cadenaconcatenar == '<')
                        {
                            //  elementos = 2;
                            //Numeros(token);
                            //NoColumna++;
                            //DescripciondelosToken("<");

                            //Subnodo(token);

                            token = "";                           //Vacia la cadena 
                            estadoprincipal = 0;
                        }
                        break;
                    case 25:
                        //NoColumna++;
                        //DescripciondelosToken(token);
                        token = "";
                        estadoprincipal = 0;
                        break;
                }



            }
        }
        

        //**********************************************//
        //----------METODOS PARA NOMBRES----------------//
        //------DE PLANIFICACION EN TREEVIEW------------//
        //**********************************************//
        public void Threeview(String NodoPrincipal)
        {
            treeView.Nodes.Add(NodoPrincipal); ////*******NOMBRE_CALENDARIO********////////
            // elementos = 0;

        }

        //*******************************************//
        //-----------METODOS PARA AÑOS---------------//
        //------DE PLANIFICACION EN TREEVIEW---------//
        //*******************************************//
        private void Asubnodo(String Sub)    /////****** AÑO**********///////
        {
            int inicio = 0;
            int estadoprincipal = 0;
            char cadenaconcatenar;
            string token = "";
            int año = 0;
            string a = "";
            int filaux = -1;
            columnas = 0;



            for (inicio = 0; inicio < Sub.Length; inicio++)
            {
                cadenaconcatenar = Sub[inicio];
                switch (estadoprincipal)
                {
                    case 0:
                        switch (cadenaconcatenar)
                        {
                            case ' ':
                            case '\r':
                            case '\t':
                            case '\n':
                            case '\b':
                            case '\f':
                                estadoprincipal = 0; //si es espacio o salto de linea o tab sigue en el estado 0
                                break;

                            case 'p':

                                token += cadenaconcatenar;
                                estadoprincipal = 1;
                                break;

                            case 'P':
                                token += cadenaconcatenar;
                                estadoprincipal = 1;
                                break;
                            case ':':
                                //token += cadenaconcatenar;
                                // DescripciondelosToken(":");
                                estadoprincipal = 4;
                                //estadoprincipal = 8;
                                //  inicio = inicio - 1;
                                break;
                            case 'A':
                                token += cadenaconcatenar;
                                estadoprincipal = 7;
                                break;
                            case 'a':
                                token += cadenaconcatenar;
                                estadoprincipal = 7;
                                break;
                            case 'M':
                                token += cadenaconcatenar;
                                estadoprincipal = 9;
                                break;
                            case 'm':
                                token += cadenaconcatenar;
                                estadoprincipal = 9;
                                break;
                            case 'D':
                                token += cadenaconcatenar;
                                estadoprincipal = 11;
                                break;
                            case 'd':
                                token += cadenaconcatenar;
                                estadoprincipal = 11;
                                break;
                            case 'i':
                                token += cadenaconcatenar;
                                estadoprincipal = 16;
                                break;
                            case 'I':
                                token += cadenaconcatenar;
                                estadoprincipal = 16;
                                break;
                            case ']':
                                // DescripciondelosToken("]");
                                token += cadenaconcatenar;
                                estadoprincipal = 25;
                                inicio = inicio - 1;
                                break;
                            case ')':
                                //DescripciondelosToken(")");
                                token += cadenaconcatenar;
                                estadoprincipal = 25;
                                inicio = inicio - 1;
                                break;
                            case '>':
                                //DescripciondelosToken(">");
                                token += cadenaconcatenar;
                                estadoprincipal = 25;
                                inicio = inicio - 1;
                                break;
                            case '}':
                                //DescripciondelosToken("}");
                                token += cadenaconcatenar;
                                estadoprincipal = 25;
                                inicio = inicio - 1;
                                break;
                                //default:
                                //    token += cadenaconcatenar;
                                //    // estadoprincipal = 5;
                                //    //inicio = inicio - 1;
                                //    break;
                        }
                        break;

                    case 1:
                        if (cadenaconcatenar == 'p' || cadenaconcatenar == 'P')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 1;
                        }
                        else if (cadenaconcatenar.Equals('l') || cadenaconcatenar.Equals('L'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 1;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('a') || cadenaconcatenar.Equals('A'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 1;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('n') || cadenaconcatenar.Equals('N'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 1;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('i') || cadenaconcatenar.Equals('I'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 1;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('f') || cadenaconcatenar.Equals('F'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                        }
                        //else if (cadenaconcatenar.Equals('i'))
                        //{
                        //    token += cadenaconcatenar;  //cancatena si es espacio u
                        //    estadoprincipal = 2;       //Sigue en el estado 1
                        //}
                        break;

                    case 2:
                        if (cadenaconcatenar == 'i' || cadenaconcatenar.Equals('I'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 2;
                        }
                        else if (cadenaconcatenar.Equals('c') || cadenaconcatenar.Equals('C'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('a') || cadenaconcatenar.Equals('A'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('d') || cadenaconcatenar.Equals('D'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('o') || cadenaconcatenar.Equals('O'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('r') || cadenaconcatenar.Equals('R'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 3;
                            inicio = inicio - 1;

                        }
                        break;


                    case 3:                                     //Estado de aceptacion
                        //DescripciondelosToken(token);           //Enviar al data de token
                        //TokenValidos(token);                   //Token validos en el data
                        p++;
                        token = "";                           //Vacia la cadena 
                        estadoprincipal = 0;                 //regresa al estado 0
                        //posicion++;
                        break;

                    case 4:
                        if (cadenaconcatenar == ':')
                        {
                            estadoprincipal = 4;
                        }
                        else if (cadenaconcatenar.Equals('"'))
                        {
                            estadoprincipal = 20;
                        }

                        else if (Char.IsDigit(cadenaconcatenar) || Char.IsSeparator(cadenaconcatenar))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 5;
                        }

                        break;

                    case 5:
                        if (Char.IsDigit(cadenaconcatenar))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 5;
                        }
                        else if (cadenaconcatenar.Equals('{') || cadenaconcatenar.Equals('(') || cadenaconcatenar.Equals('<'))
                        {

                            estadoprincipal = 24;
                            inicio = inicio - 1;
                        }
                        break;
                    case 6:
                        //MessageBox.Show("error simbolo en CADENA", "Lenguajes Proyecto Fase 1", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DescripciondelosToken(token);
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 7:
                        if (cadenaconcatenar == 'a' || cadenaconcatenar == 'A')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 7;
                        }
                        else if (cadenaconcatenar.Equals('ñ') || cadenaconcatenar.Equals('Ñ'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 7;
                        }
                        else if (cadenaconcatenar.Equals('o') || cadenaconcatenar.Equals('O'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 8;
                            inicio = inicio - 1;
                        }
                        break;

                    case 8:
                        // DescripciondelosToken(token);
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 9:
                        if (cadenaconcatenar == 'm' || cadenaconcatenar == 'M')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 9;
                        }
                        else if (cadenaconcatenar.Equals('e') || cadenaconcatenar.Equals('E'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 9;
                        }
                        else if (cadenaconcatenar.Equals('s') || cadenaconcatenar.Equals('S'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 10;
                            inicio = inicio - 1;
                        }
                        break;
                    case 10:
                        // DescripciondelosToken(token);
                        token = "";
                        estadoprincipal = 0;
                        break;

                    case 11:
                        if (cadenaconcatenar == 'd' || cadenaconcatenar == 'D')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('i') || cadenaconcatenar.Equals('I'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('a') || cadenaconcatenar.Equals('A'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 12;
                            inicio = inicio - 1;
                        }
                        else if (cadenaconcatenar.Equals('e') || cadenaconcatenar.Equals('E'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('s') || cadenaconcatenar.Equals('S'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('c') || cadenaconcatenar.Equals('C'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('r') || cadenaconcatenar.Equals('R'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 13;
                        }

                        break;

                    case 12:
                        // DescripciondelosToken(token);
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 13:
                        if (cadenaconcatenar == 'i' || cadenaconcatenar == 'I')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 13;
                        }
                        else if (cadenaconcatenar.Equals('p') || cadenaconcatenar.Equals('P'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 13;
                        }
                        else if (cadenaconcatenar.Equals('c') || cadenaconcatenar.Equals('C'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 14;
                        }
                        break;
                    case 14:
                        if (cadenaconcatenar == 'i' || cadenaconcatenar == 'I')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 14;
                        }
                        else if (cadenaconcatenar.Equals('o') || cadenaconcatenar.Equals('ó') || cadenaconcatenar.Equals('O'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 14;
                        }
                        else if (cadenaconcatenar.Equals('n') || cadenaconcatenar.Equals('N'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 15;
                            inicio = inicio - 1;
                        }
                        break;
                    case 15:
                        //DescripciondelosToken(token);
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 16:
                        if (cadenaconcatenar == 'i' || cadenaconcatenar == 'I')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('m') || cadenaconcatenar.Equals('M'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('a') || cadenaconcatenar.Equals('A'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('g') || cadenaconcatenar.Equals('G'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('e') || cadenaconcatenar.Equals('E'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('n') || cadenaconcatenar.Equals('N'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 17;
                            inicio = inicio - 1;
                        }
                        break;
                    case 17:
                        //DescripciondelosToken(token);
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 18:
                        if (cadenaconcatenar == '{')
                        {

                            estadoprincipal = 18;
                        }
                        else if (char.IsLetter(cadenaconcatenar))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 18;
                        }
                        else if (cadenaconcatenar.Equals('}'))
                        {
                            estadoprincipal = 19;
                            inicio = inicio - 1;
                        }
                        break;
                    case 19:
                        //DescripciondelosToken(token);
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 20:  //////*****NOMBRE CALENDARIO*******//////
                        if (cadenaconcatenar == '"')
                        {
                            estadoprincipal = 20;
                        }
                        else if (Char.IsLetterOrDigit(cadenaconcatenar) || Char.IsSeparator(cadenaconcatenar))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 20;
                        }
                        else if (!Char.IsLetterOrDigit(cadenaconcatenar))
                        {
                            estadoprincipal = 21;
                            inicio = inicio - 1;
                        }

                        break;
                    case 21:
                        if (cadenaconcatenar == '"')
                        {
                            estadoprincipal = 21;
                        }
                        else if (cadenaconcatenar.Equals('['))
                        {

                            estadoprincipal = 22;
                            //inicio = inicio - 1;
                        }
                        else if (cadenaconcatenar.Equals(';'))
                        {

                            estadoprincipal = 23;
                            //inicio = inicio - 1;
                        }
                        break;
                    case 22:
                        //Cadenas(token);
                        //DescripciondelosToken("[");
                        //Threeview(token);
                        token = "";
                        estadoprincipal = 0;
                        //  elementos = 0;
                        break;

                    case 23:
                        //Cadenas(token);
                        //DescripciondelosToken(";");
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 24:
                        if (cadenaconcatenar == '{') ////AÑO***////
                        {
                            // filas++;
                            AgregarNodoAño(token);
                            a = token;
                            año = int.Parse(token);
                            //fechas[filas, columnas] = int.Parse(token);
                            token = "";                           //Vacia la cadena 
                            estadoprincipal = 0;
                        }
                        else if (cadenaconcatenar == '(')
                        {
                            token = "";                           //Vacia la cadena 
                            estadoprincipal = 0;

                        }
                        else if (cadenaconcatenar == '<')
                        {
                            filaux++;
                            fechas[filaux, columnas] = año;
                            descripcion[filaux, columnas] = a;                                            //////////////////////**************//NUEVO///*****************////////////
                            token = "";                           //Vacia la cadena 
                            estadoprincipal = 0;
                        }
                        break;
                    case 25:
                        token = "";
                        estadoprincipal = 0;
                        break;
                }



            }
        }

        //*******************************************//
        //-----------METODOS PARA MESES--------------//
        //------DE PLANIFICACION EN TREEVIEW---------//
        //*******************************************//
        private void Msubnodo(String QSub)
        {
            int inicio = 0;
            int estadoprincipal = 0;
            char cadenaconcatenar;
            string token = "";
            filas = -1;
            int filaux = -1;
            columnas = 1;
            int meses = 0;
            string m = "";
            for (inicio = 0; inicio < QSub.Length; inicio++)
            {
                cadenaconcatenar = QSub[inicio];
                switch (estadoprincipal)
                {
                    case 0:
                        switch (cadenaconcatenar)
                        {
                            case ' ':
                            case '\r':
                            case '\t':
                            case '\n':
                            case '\b':
                            case '\f':
                                estadoprincipal = 0; //si es espacio o salto de linea o tab sigue en el estado 0
                                break;

                            case 'p':
                                token += cadenaconcatenar;
                                estadoprincipal = 1;
                                break;

                            case 'P':
                                token += cadenaconcatenar;
                                estadoprincipal = 1;
                                break;
                            case ':':
                                //token += cadenaconcatenar;
                                //  DescripciondelosToken(":");
                                estadoprincipal = 4;
                                //estadoprincipal = 8;
                                //  inicio = inicio - 1;
                                break;
                            case 'A':
                                token += cadenaconcatenar;
                                estadoprincipal = 7;
                                break;
                            case 'a':
                                token += cadenaconcatenar;
                                estadoprincipal = 7;
                                break;
                            case 'M':
                                token += cadenaconcatenar;
                                estadoprincipal = 9;
                                break;
                            case 'm':
                                token += cadenaconcatenar;
                                estadoprincipal = 9;
                                break;
                            case 'D':
                                token += cadenaconcatenar;
                                estadoprincipal = 11;
                                break;
                            case 'd':
                                token += cadenaconcatenar;
                                estadoprincipal = 11;
                                break;
                            case 'i':
                                token += cadenaconcatenar;
                                estadoprincipal = 16;
                                break;
                            case 'I':
                                token += cadenaconcatenar;
                                estadoprincipal = 16;
                                break;
                            case ']':
                                // DescripciondelosToken("]");
                                token += cadenaconcatenar;
                                estadoprincipal = 25;
                                inicio = inicio - 1;
                                break;
                            case ')':
                                //DescripciondelosToken(")");
                                token += cadenaconcatenar;
                                estadoprincipal = 25;
                                inicio = inicio - 1;
                                break;
                            case '>':
                                //DescripciondelosToken(">");
                                token += cadenaconcatenar;
                                estadoprincipal = 25;
                                inicio = inicio - 1;
                                break;
                            case '}':
                                //DescripciondelosToken("}");
                                token += cadenaconcatenar;
                                estadoprincipal = 25;
                                inicio = inicio - 1;
                                break;
                                //default:
                                //    token += cadenaconcatenar;
                                //    // estadoprincipal = 5;
                                //    //inicio = inicio - 1;
                                //    break;
                        }
                        break;

                    case 1:
                        if (cadenaconcatenar == 'p' || cadenaconcatenar == 'P')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 1;
                        }
                        else if (cadenaconcatenar.Equals('l') || cadenaconcatenar.Equals('L'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 1;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('a') || cadenaconcatenar.Equals('A'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 1;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('n') || cadenaconcatenar.Equals('N'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 1;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('i') || cadenaconcatenar.Equals('I'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 1;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('f') || cadenaconcatenar.Equals('F'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                        }
                        //else if (cadenaconcatenar.Equals('i'))
                        //{
                        //    token += cadenaconcatenar;  //cancatena si es espacio u
                        //    estadoprincipal = 2;       //Sigue en el estado 1
                        //}
                        break;

                    case 2:
                        if (cadenaconcatenar == 'i' || cadenaconcatenar == 'I')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 2;
                        }
                        else if (cadenaconcatenar.Equals('c') || cadenaconcatenar.Equals('C'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('a') || cadenaconcatenar.Equals('A'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('d') || cadenaconcatenar.Equals('D'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('o') || cadenaconcatenar.Equals('O'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('r') || cadenaconcatenar.Equals('R'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 3;
                            inicio = inicio - 1;

                        }
                        break;


                    case 3:                                     //Estado de aceptacion
                        //DescripciondelosToken(token);           //Enviar al data de token
                        //TokenValidos(token);                   //Token validos en el data
                        q++;
                        r = -1;
                        token = "";                           //Vacia la cadena 
                        estadoprincipal = 0;                 //regresa al estado 0
                        //posicion++;
                        break;

                    case 4:
                        if (cadenaconcatenar == ':')
                        {
                            estadoprincipal = 4;
                        }
                        else if (cadenaconcatenar.Equals('"'))
                        {
                            estadoprincipal = 20;
                        }

                        else if (Char.IsDigit(cadenaconcatenar) || Char.IsSeparator(cadenaconcatenar))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 5;
                        }

                        break;

                    case 5:
                        if (Char.IsDigit(cadenaconcatenar))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 5;
                        }
                        else if (cadenaconcatenar.Equals('{') || cadenaconcatenar.Equals('(') || cadenaconcatenar.Equals('<'))
                        {

                            estadoprincipal = 24;
                            inicio = inicio - 1;
                        }
                        break;
                    case 6:
                        //MessageBox.Show("error simbolo en CADENA", "Lenguajes Proyecto Fase 1", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DescripciondelosToken(token);
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 7:
                        if (cadenaconcatenar == 'a' || cadenaconcatenar == 'A')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 7;
                        }
                        else if (cadenaconcatenar.Equals('ñ') || cadenaconcatenar.Equals('Ñ'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 7;
                        }
                        else if (cadenaconcatenar.Equals('o') || cadenaconcatenar.Equals('O'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 8;
                            inicio = inicio - 1;
                        }
                        break;

                    case 8:
                        // DescripciondelosToken(token);
                        r++;
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 9:
                        if (cadenaconcatenar == 'm' || cadenaconcatenar == 'M')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 9;
                        }
                        else if (cadenaconcatenar.Equals('e') || cadenaconcatenar.Equals('E'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 9;
                        }
                        else if (cadenaconcatenar.Equals('s') || cadenaconcatenar.Equals('S'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 10;
                            inicio = inicio - 1;
                        }
                        break;
                    case 10:
                        // DescripciondelosToken(token);
                        //    q++;
                        token = "";
                        estadoprincipal = 0;
                        break;

                    case 11:
                        if (cadenaconcatenar == 'd' || cadenaconcatenar == 'D')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('i') || cadenaconcatenar.Equals('I'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('a') || cadenaconcatenar.Equals('A'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 12;
                            inicio = inicio - 1;
                        }
                        else if (cadenaconcatenar.Equals('e') || cadenaconcatenar.Equals('E'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('s') || cadenaconcatenar.Equals('S'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('c') || cadenaconcatenar.Equals('C'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('r') || cadenaconcatenar.Equals('R'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 13;
                        }

                        break;

                    case 12:
                        // DescripciondelosToken(token);
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 13:
                        if (cadenaconcatenar == 'i' || cadenaconcatenar == 'I')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 13;
                        }
                        else if (cadenaconcatenar.Equals('p') || cadenaconcatenar.Equals('P'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 13;
                        }
                        else if (cadenaconcatenar.Equals('c') || cadenaconcatenar.Equals('C'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 14;
                        }
                        break;
                    case 14:
                        if (cadenaconcatenar == 'i' || cadenaconcatenar == 'I')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 14;
                        }
                        else if (cadenaconcatenar.Equals('o') || cadenaconcatenar.Equals('ó') || cadenaconcatenar.Equals('O'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 14;
                        }
                        else if (cadenaconcatenar.Equals('n') || cadenaconcatenar.Equals('N'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 15;
                            inicio = inicio - 1;
                        }
                        break;
                    case 15:
                        //DescripciondelosToken(token);
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 16:
                        if (cadenaconcatenar == 'i' || cadenaconcatenar == 'I')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('m') || cadenaconcatenar.Equals('M'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('a') || cadenaconcatenar.Equals('A'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('g') || cadenaconcatenar.Equals('G'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('e') || cadenaconcatenar.Equals('E'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('n') || cadenaconcatenar.Equals('N'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 17;
                            inicio = inicio - 1;
                        }
                        break;
                    case 17:
                        //DescripciondelosToken(token);
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 18:
                        if (cadenaconcatenar == '{')
                        {

                            estadoprincipal = 18;
                        }
                        else if (char.IsLetter(cadenaconcatenar))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 18;
                        }
                        else if (cadenaconcatenar.Equals('}'))
                        {
                            estadoprincipal = 19;
                            inicio = inicio - 1;
                        }
                        break;
                    case 19:
                        //DescripciondelosToken(token);
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 20:  //////*****NOMBRE CALENDARIO*******//////
                        if (cadenaconcatenar == '"')
                        {
                            estadoprincipal = 20;
                        }
                        else if (Char.IsLetterOrDigit(cadenaconcatenar) || Char.IsSeparator(cadenaconcatenar))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 20;
                        }
                        else if (!Char.IsLetterOrDigit(cadenaconcatenar))
                        {
                            estadoprincipal = 21;
                            inicio = inicio - 1;
                        }

                        break;
                    case 21:
                        if (cadenaconcatenar == '"')
                        {
                            estadoprincipal = 21;
                        }
                        else if (cadenaconcatenar.Equals('['))
                        {

                            estadoprincipal = 22;
                            //inicio = inicio - 1;
                        }
                        else if (cadenaconcatenar.Equals(';'))
                        {

                            estadoprincipal = 23;
                            //inicio = inicio - 1;
                        }
                        break;
                    case 22:
                        //Cadenas(token);
                        //DescripciondelosToken("[");
                        //Threeview(token);
                        token = "";
                        estadoprincipal = 0;
                        //  elementos = 0;
                        break;

                    case 23:
                        //Cadenas(token);
                        //DescripciondelosToken(";");
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 24:
                        if (cadenaconcatenar == '{') ////AÑO***////
                        {
                            token = "";                           //Vacia la cadena 
                            estadoprincipal = 0;
                        }
                        else if (cadenaconcatenar == '(')
                        {
                            //filas++;
                            AgregarNodoMes(token);
                            meses = int.Parse(token);
                            m = token;
                            // fechas[filas,columnas] = int.Parse(token);
                            token = "";                           //Vacia la cadena 
                            estadoprincipal = 0;
                        }
                        else if (cadenaconcatenar == '<')
                        {
                            filaux++;
                            fechas[filaux, columnas] = meses;
                            descripcion[filaux, columnas] = m;
                            token = "";                           //Vacia la cadena 
                            estadoprincipal = 0;
                        }
                        break;
                    case 25:
                        token = "";
                        estadoprincipal = 0;
                        break;
                }



            }
        }


        //*******************************************//
        //-----------METODOS PARA DIA----------------//
        //------DE PLANIFICACION EN TREEVIEW---------//
        //*******************************************//
        private void Dsubnodo(String DSub)
        {
            int inicio = 0;
            int estadoprincipal = 0;
            char cadenaconcatenar;
            string token = "";
            filas = -1;
            columnas = 2;
            int col = 3;                                    /////////////******NUEVO**************/////////

            for (inicio = 0; inicio < DSub.Length; inicio++)
            {
                cadenaconcatenar = DSub[inicio];
                switch (estadoprincipal)
                {
                    case 0:
                        switch (cadenaconcatenar)
                        {
                            case ' ':
                            case '\r':
                            case '\t':
                            case '\n':
                            case '\b':
                            case '\f':
                                estadoprincipal = 0; //si es espacio o salto de linea o tab sigue en el estado 0
                                break;

                            case 'p':
                                token += cadenaconcatenar;
                                estadoprincipal = 1;
                                break;

                            case 'P':
                                token += cadenaconcatenar;
                                estadoprincipal = 1;
                                break;
                            case ':':
                                //token += cadenaconcatenar;
                                //DescripciondelosToken(":");
                                estadoprincipal = 4;
                                //estadoprincipal = 8;
                                //  inicio = inicio - 1;
                                break;
                            case 'A':
                                token += cadenaconcatenar;
                                estadoprincipal = 7;
                                break;
                            case 'a':
                                token += cadenaconcatenar;
                                estadoprincipal = 7;
                                break;
                            case 'M':
                                token += cadenaconcatenar;
                                estadoprincipal = 9;
                                break;
                            case 'm':
                                token += cadenaconcatenar;
                                estadoprincipal = 9;
                                break;
                            case 'D':
                                token += cadenaconcatenar;
                                estadoprincipal = 11;
                                break;
                            case 'd':
                                token += cadenaconcatenar;
                                estadoprincipal = 11;
                                break;
                            case 'i':
                                token += cadenaconcatenar;
                                estadoprincipal = 16;
                                break;
                            case 'I':
                                token += cadenaconcatenar;
                                estadoprincipal = 16;
                                break;
                            case ']':
                                // DescripciondelosToken("]");
                                token += cadenaconcatenar;
                                estadoprincipal = 25;
                                inicio = inicio - 1;
                                break;
                            case ')':
                                //DescripciondelosToken(")");
                                token += cadenaconcatenar;
                                estadoprincipal = 25;
                                inicio = inicio - 1;
                                break;
                            case '>':
                                //DescripciondelosToken(">");
                                token += cadenaconcatenar;
                                estadoprincipal = 25;
                                inicio = inicio - 1;
                                break;
                            case '}':
                                //DescripciondelosToken("}");
                                token += cadenaconcatenar;
                                estadoprincipal = 25;
                                inicio = inicio - 1;
                                break;
                                //default:
                                //    token += cadenaconcatenar;
                                //    // estadoprincipal = 5;
                                //    //inicio = inicio - 1;
                                //    break;
                        }
                        break;

                    case 1:
                        if (cadenaconcatenar == 'p' || cadenaconcatenar == 'P')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 1;
                        }
                        else if (cadenaconcatenar.Equals('l') || cadenaconcatenar.Equals('L'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 1;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('a') || cadenaconcatenar.Equals('A'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 1;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('n') || cadenaconcatenar.Equals('N'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 1;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('i') || cadenaconcatenar.Equals('I'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 1;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('f') || cadenaconcatenar.Equals('F'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                        }
                        //else if (cadenaconcatenar.Equals('i'))
                        //{
                        //    token += cadenaconcatenar;  //cancatena si es espacio u
                        //    estadoprincipal = 2;       //Sigue en el estado 1
                        //}
                        break;

                    case 2:
                        if (cadenaconcatenar == 'i' || cadenaconcatenar == 'I')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 2;
                        }
                        else if (cadenaconcatenar.Equals('c') || cadenaconcatenar.Equals('C'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('a') || cadenaconcatenar.Equals('A'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('d') || cadenaconcatenar.Equals('D'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('o') || cadenaconcatenar.Equals('O'))
                        {
                            token += cadenaconcatenar;  //cancatena si es espacio u
                            estadoprincipal = 2;       //Sigue en el estado 1
                        }
                        else if (cadenaconcatenar.Equals('r') || cadenaconcatenar.Equals('R'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 3;
                            inicio = inicio - 1;

                        }
                        break;


                    case 3:                                     //Estado de aceptacion
                        //DescripciondelosToken(token);           //Enviar al data de token
                        //TokenValidos(token);                   //Token validos en el data
                        s++;
                        t = -1;
                        u = -1;
                        token = "";                           //Vacia la cadena 
                        estadoprincipal = 0;                 //regresa al estado 0
                        //posicion++;
                        break;

                    case 4:                                                                         //////////////NUEVO_REEMPLAZO_METODO//////////////////////////////
                        if (cadenaconcatenar == ':')
                        {
                            estadoprincipal = 4;
                        }
                        else if (cadenaconcatenar.Equals('"'))
                        {
                            estadoprincipal = 20;
                        }

                        else if (Char.IsDigit(cadenaconcatenar) || Char.IsSeparator(cadenaconcatenar))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 5;
                        }

                        break;

                    case 5:
                        if (Char.IsDigit(cadenaconcatenar))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 5;
                        }
                        else if (cadenaconcatenar.Equals('{') || cadenaconcatenar.Equals('(') || cadenaconcatenar.Equals('<'))
                        {

                            estadoprincipal = 24;
                            inicio = inicio - 1;
                        }
                        break;
                    case 6:
                        //MessageBox.Show("error simbolo en CADENA", "Lenguajes Proyecto Fase 1", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DescripciondelosToken(token);
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 7:
                        if (cadenaconcatenar == 'a' || cadenaconcatenar == 'A')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 7;
                        }
                        else if (cadenaconcatenar.Equals('ñ') || cadenaconcatenar.Equals('Ñ'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 7;
                        }
                        else if (cadenaconcatenar.Equals('o') || cadenaconcatenar.Equals('O'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 8;
                            inicio = inicio - 1;
                        }
                        break;

                    case 8:
                        // DescripciondelosToken(token);
                        t++;
                        u = -1;
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 9:
                        if (cadenaconcatenar == 'm' || cadenaconcatenar == 'M')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 9;
                        }
                        else if (cadenaconcatenar.Equals('e') || cadenaconcatenar.Equals('E'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 9;
                        }
                        else if (cadenaconcatenar.Equals('s') || cadenaconcatenar.Equals('S'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 10;
                            inicio = inicio - 1;
                        }
                        break;
                    case 10:
                        // DescripciondelosToken(token);
                        //    q++;
                        u++;
                        token = "";
                        estadoprincipal = 0;
                        break;

                    case 11:
                        if (cadenaconcatenar == 'd' || cadenaconcatenar == 'D')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('i') || cadenaconcatenar.Equals('I'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('a') || cadenaconcatenar.Equals('A'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 12;
                            inicio = inicio - 1;
                        }
                        else if (cadenaconcatenar.Equals('e') || cadenaconcatenar.Equals('E'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('s') || cadenaconcatenar.Equals('S'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('c') || cadenaconcatenar.Equals('C'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 11;
                        }
                        else if (cadenaconcatenar.Equals('r') || cadenaconcatenar.Equals('C'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 13;
                        }

                        break;

                    case 12:
                        // DescripciondelosToken(token);
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 13:
                        if (cadenaconcatenar == 'i' || cadenaconcatenar == 'I')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 13;
                        }
                        else if (cadenaconcatenar.Equals('p') || cadenaconcatenar.Equals('P'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 13;
                        }
                        else if (cadenaconcatenar.Equals('c') || cadenaconcatenar.Equals('C'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 14;
                        }
                        break;
                    case 14:
                        if (cadenaconcatenar == 'i' || cadenaconcatenar == 'I')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 14;
                        }
                        else if (cadenaconcatenar.Equals('o') || cadenaconcatenar.Equals('ó') || cadenaconcatenar.Equals('O'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 14;
                        }
                        else if (cadenaconcatenar.Equals('n') || cadenaconcatenar.Equals('N'))
                        {
                            token += cadenaconcatenar;
                            col = 3;                                                                    //////////////  NUEVOOOOOO************************
                            estadoprincipal = 15;
                            inicio = inicio - 1;

                        }
                        break;
                    case 15:
                        //DescripciondelosToken(token);
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 16:
                        if (cadenaconcatenar == 'i' || cadenaconcatenar == 'I')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('m') || cadenaconcatenar.Equals('M'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('a') || cadenaconcatenar.Equals('A'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('g') || cadenaconcatenar.Equals('G'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('e') || cadenaconcatenar.Equals('E'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 16;
                        }
                        else if (cadenaconcatenar.Equals('n') || cadenaconcatenar.Equals('N'))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 17;
                            inicio = inicio - 1;
                        }
                        break;
                    case 17:
                        //DescripciondelosToken(token);
                        col = 4;                                                                                ///////////////*****NUEVO********************//////////
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 18:
                        if (cadenaconcatenar == '{')
                        {

                            estadoprincipal = 18;
                        }
                        else if (char.IsLetter(cadenaconcatenar))
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 18;
                        }
                        else if (cadenaconcatenar.Equals('}'))
                        {
                            estadoprincipal = 19;
                            inicio = inicio - 1;
                        }
                        break;
                    case 19:
                        //DescripciondelosToken(token);
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 20:  //////*****NOMBRE CALENDARIO*******//////                                                 ////REEMPLAZO_DE_METODO_CON_METODO_ANALIZADOR_DEL 20 AL 23/////////
                        //if (cadenaconcatenar == '"')
                        //{
                        //    estadoprincipal = 20;
                        //}
                        if (cadenaconcatenar != ';' && cadenaconcatenar != '[' && cadenaconcatenar != '"')
                        {
                            token += cadenaconcatenar;
                            estadoprincipal = 20;
                        }
                        else if (cadenaconcatenar.Equals(';'))
                        {
                            estadoprincipal = 23;
                            inicio = inicio - 1;
                        }
                        else if (cadenaconcatenar.Equals('['))
                        {
                            estadoprincipal = 22;
                            inicio = inicio - 1;
                        }
                        break;
                    case 21:
                        if (cadenaconcatenar == '"')
                        {
                            estadoprincipal = 21;
                        }
                        else if (cadenaconcatenar.Equals('['))
                        {

                            estadoprincipal = 22;
                            //inicio = inicio - 1;
                        }
                        else if (cadenaconcatenar.Equals(';'))
                        {

                            estadoprincipal = 23;
                            //inicio = inicio - 1;
                        }
                        break;
                    case 22:
                        //Cadenas(token);
                        //DescripciondelosToken("[");
                        //Threeview(token);

                        token = "";
                        estadoprincipal = 0;
                        //  elementos = 0;
                        break;

                    case 23:
                        String info = token;                                                                ////////////***********NUEVO*************************//////////
                        descripcion[filas, col] = info;
                        //Cadenas(token);
                        //DescripciondelosToken(";");
                        token = "";
                        estadoprincipal = 0;
                        break;
                    case 24:
                        if (cadenaconcatenar == '{') ////AÑO***////
                        {
                            token = "";                           //Vacia la cadena 
                            estadoprincipal = 0;
                        }
                        else if (cadenaconcatenar == '(')
                        {
                            //AgregarNodoMes(token);
                            token = "";                           //Vacia la cadena 
                            estadoprincipal = 0;
                        }
                        else if (cadenaconcatenar == '<')
                        {
                            AgregarNodoDia(token);
                            filas++;
                            fechas[filas, columnas] = int.Parse(token);
                            descripcion[filas, 2] = token;                                                               //////////////////////**************//NUEVO///*****************////////////

                            token = "";                           //Vacia la cadena 
                            estadoprincipal = 0;
                        }
                        break;
                    case 25:
                        token = "";
                        estadoprincipal = 0;
                        break;
                }



            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //**************************//
        //-------TREEVIEW_AÑO-------//
        //**************************//
        private void AgregarNodoAño(String Año)
        {
            treeView.Nodes[p].Nodes.Add(Año);
        }

        //**************************//
        //-------TREEVIEW_MES-------//
        //**************************//
        private void AgregarNodoMes(String Mes)
        {
            treeView.Nodes[q].Nodes[r].Nodes.Add(Mes);
        }
        //**************************//
        //-------TREEVIEW_DIA-------//
        //**************************//
        private void AgregarNodoDia(String Dia)
        {
            treeView.Nodes[s].Nodes[t].Nodes[u].Nodes.Add(Dia);
        }





        //***************************//
        //----CALENDARIO------------//
        //**************************//
        private void Calendario()
        {
            //int a, d, m;
            //a = Int32.Parse(año);
            //d = Int32.Parse(mes);
                //m = Int32.Parse(dia);
            for (int filas = 0; filas < fechas.GetLength(0); filas++)
            {


                DateTime f = new DateTime(fechas[filas, 0], fechas[filas, 1], fechas[filas, 2]);
                monthCalendar1.AddBoldedDate(f);
            }


        }
       
        private void Label2_Click(object sender, EventArgs e)
        {

        }


        private void TreeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        //*****************************************//
        //-------CONCATENAR DIAS DUPLICADOS-------//
        //***************************************//
        public void ConcatenarDias()
        {
            int[] posiciones;

            int i = 0;
            for (int fila = 0; fila < descripcion.GetLength(0); fila++)
            {
                posiciones = new int[descripcion.GetLength(0)];
                string aux = descripcion[fila, 3];

                for (int fila2 = 0; fila2 < descripcion.GetLength(0); fila2++)
                {
                    if (descripcion[fila, 0].Trim() == descripcion[fila2, 0].Trim() && descripcion[fila, 1].Trim() == descripcion[fila2, 1].Trim() && descripcion[fila, 2].Trim() == descripcion[fila2, 2].Trim() && descripcion[fila, 3].Trim().Equals(descripcion[fila2, 3].Trim()) == false)
                    {
                        aux = aux + " y " + descripcion[fila2, 3];
                        i++;
                        posiciones[i] = fila2;
                       // posiciones[0] = fila;
                    }
                }
                if (i != 0)
                {
                    for (int filaaux = 0; filaaux < posiciones.Length; filaaux++)
                    {
                        if (posiciones[filaaux] != 0)
                        {
                            descripcion[fila, 3] = aux;
                            descripcion[posiciones[filaaux], 3] = aux;
                        }
                    }
                }
                i = 0;
            }
        }



        //**********************************//
        //-------INFORMACION_TREEVIEW-------//
        //**********************************//

        private void TreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // textBox1.Text = e.Node.FullPath;
            string fecha = e.Node.FullPath;
            string[] f = fecha.Split('\\');
            tree = new string[1, 3];

            if (f.Length >= 4)
            {
                for (int c = 0; c < tree.GetLength(1); c++)   /////////CICLO PARA QUITAR NOMBRE DE NODO PRINCIPAL
                {
                    tree[0, c] = f[c + 1];
                }

                for (int fila = 0; fila < descripcion.GetLength(0); fila++)   ////// CICLO PARA AGREGAR DESCRIPCION E IMAGEN DE DIA SELECCIONADO
                {
                    if (descripcion[fila, 0] == tree[0, 0] && descripcion[fila, 1] == tree[0, 1] && descripcion[fila, 2] == tree[0, 2])
                    {
                        txtDesc.Text = descripcion[fila, 3];
                        picDes.ImageLocation = descripcion[fila, 4].Trim();
                       
                    }

                }
            }
            else
            {
                MessageBox.Show("DEBE SELECCIONAR UN DIA", "Informacion Sistema de Planificacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

         //**********************************//
        //----------BOTON SALIR-------------//
       //**********************************//
        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

          //**********************************//
         //----------BOTON GUARDAR-----------//
        //**********************************//
        private void GuardarArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox t in tab1.SelectedTab.Controls.OfType<TextBox>())
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "planificacion (*.ly) | *.ly";
                //saveFileDialog1.OverwritePrompt = true;
                saveFileDialog1.Title = "Guardar Planificacion";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string ruta = saveFileDialog1.FileName;
                    StreamWriter fichero = new StreamWriter(ruta);
                    fichero.Write(t.Text);
                    fichero.Close();

                    MessageBox.Show("Planificacion Guardada", "Informacion Sistema de Planificacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        //*********************************************//
        //----------BOTON MANUAL DE USUARIO-----------//
        //*******************************************//
        private void ManualDeUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string pdfPath = Path.Combine(Application.StartupPath,"MANUAL DE USUARIO.pdf");
            Process.Start(pdfPath);
        }

        //*********************************************//
        //----------ACERCA DE              -----------//
        //*******************************************//
        private void AcercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("EDIN EMANUEL MONTENEGRO VASQUEZ\r\n"+
                "CARNET 201709311", "AUTOR DE PROGRAMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ManualTecnicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string pdfPath = Path.Combine(Application.StartupPath, "MANUAL TECNICO.pdf");
            Process.Start(pdfPath);
        }
    }

}
