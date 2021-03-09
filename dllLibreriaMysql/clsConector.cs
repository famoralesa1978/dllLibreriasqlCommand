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

            string bdProduccion = "Pn6QdbLxN6zYhNuC0AGO9QzP8WL2RI9VHfd/l56YcLkZ1UdzuJNuXq3s7y9ZY3eq6QrxfamnP0GH0FDdEHA6bAWJdHonailm8a5b3eyUw5vuWLyX+mBmFPxKLHFVjRtYm0sjwb1KdqM=";//original 
            string bdDesarrollo = "qbz4h7qjqnp0/OO4YgugGwzP8WL2RI9VHfd/l56YcLkZ1UdzuJNuXhBXIBpMWzG0Ksz6XiJssjyyoaDnAW6D6nLUIj/AB5EjQC5owho+mOlJ/DherMPWxLE4XBiaFShK";//original 

            string bd =  strPublicacion == "Desarrollo" ? bdDesarrollo : strPublicacion == "Prod1" ? bdProduccion : "";

            //**********************
            conexion = new SqlConnection(clsUtiles1.DecryptTripleDES( bd));
			
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
