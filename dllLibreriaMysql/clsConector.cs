using System;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Specialized;
using System.Data.Sql; 
namespace dllConectorMysql
{
 
	 public class clsConectorSqlServer
	 {
		 private SqlConnection  conexion;
         private dllLibreriaMysql.clsUtiles clsUtiles1 = new dllLibreriaMysql.clsUtiles();
         //private SqlConnection conexion;

		 private void Conectar(String strPublicacion)  
		 {

            string bdProduccion = "SERVER=sql7001.site4now.net;UID=DB_A2B812_Control_admin;PWD=xray2017;DATABASE=DB_A2B812_Control;";//original 
            string bdDesarrollo = "SERVER=sql7001.site4now.net;UID=DB_A2B812_desarrollo_admin;PWD=xraydesa3377;DATABASE=DB_A2B812_desarrollo;";//original 

            string bd = strPublicacion == "Desarrollo" ? bdDesarrollo : strPublicacion == "Prod1" ? bdProduccion : "";

            //**********************
            conexion = new SqlConnection(bd);
			
			 conexion.Open();

		 }

		 private void Cerrar()
		 {

			 conexion.Close();
		 }

         public DataSet Listar(String strPublicacion,SqlCommand cmd)
		 {
			 Conectar(strPublicacion);
			 DataSet dt = new DataSet();
			 cmd.Connection = conexion;
             SqlDataAdapter reader = new SqlDataAdapter(cmd);
			 reader.Fill(dt);
			 Cerrar();
			 return dt;
		 }

         public void AgregarModificarEliminar(String strPublicacion,SqlCommand cmd)
		 {
			 Conectar(strPublicacion);

			 cmd.Connection = conexion;

			 cmd.ExecuteNonQuery();


			 Cerrar();

		 }



	 }
}
