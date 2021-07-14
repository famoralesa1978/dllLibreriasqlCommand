﻿using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace dllLibreriaMysql

{

	public class clsSqlComunSqlserver
	{
		dllConectorMysql.clsConectorSqlServer Conectar = new dllConectorMysql.clsConectorSqlServer();
		private SqlConnection conexion;
		public void Listar_Estado(string strPublicacion, ref ComboBox cbxEstadodescrion, ref ComboBox cbxEstadoId)
		{
			DataSet ds = new DataSet();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT Descripcion,Id_Estado from glo_estado ORDER BY orden";
			cmd.CommandType = CommandType.Text;

			ds = Conectar.Listar(strPublicacion, cmd);
			cbxEstadodescrion.DisplayMember = ds.Tables[0].Columns[0].Caption.ToString();
			cbxEstadodescrion.ValueMember = ds.Tables[0].Columns[1].Caption.ToString();
			cbxEstadodescrion.DataSource = ds.Tables[0];
		}

		public void Listar_EstadoBusqueda(string strPublicacion, ref ComboBox cbxEstadodescrion, ref ComboBox cbxEstadoId)
		{
			DataSet ds = new DataSet();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select 'Todos'as Descripcion, -1 as Id_Estado union  SELECT Descripcion,Id_Estado from glo_estado ORDER BY orden";
			cmd.CommandType = CommandType.Text;

			ds = Conectar.Listar(strPublicacion, cmd);
			cbxEstadodescrion.DisplayMember = ds.Tables[0].Columns[0].Caption.ToString();
			cbxEstadodescrion.ValueMember = ds.Tables[0].Columns[1].Caption.ToString();
			cbxEstadodescrion.DataSource = ds.Tables[0];
		}

		public void Listar_Region(string strPublicacion, ref ComboBox cbxGlosa, ref ComboBox cbxId)
		{
			DataSet ds = new DataSet();
			SqlCommand cmd = new SqlCommand();
			//SqlCommand cmd = new SqlCommand();
			//MessageBox.Show("Conectado al servidor");


			cmd.CommandText = "select  cast(id_region as varchar(2)) + '- ' +  region,id_region from glo_region order by orden ";
			cmd.CommandType = CommandType.Text;

			ds = Conectar.Listar(strPublicacion, cmd);
			cbxGlosa.DisplayMember = ds.Tables[0].Columns[0].Caption.ToString();
			cbxGlosa.ValueMember = ds.Tables[0].Columns[1].Caption.ToString();
			cbxGlosa.DataSource = ds.Tables[0];
		}

		public void Listar_Provincia(string strPublicacion, ref ComboBox cbxGlosa, ref ComboBox cbxId, int idRegion)
		{
			DataSet ds = new DataSet();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select provincia,id_provincia from glo_provincia " +
									 "where id_region=" + idRegion.ToString() + " order by id_provincia";
			cmd.CommandType = CommandType.Text;

			ds = Conectar.Listar(strPublicacion, cmd);

			cbxGlosa.DisplayMember = ds.Tables[0].Columns[0].Caption.ToString();
			cbxGlosa.ValueMember = ds.Tables[0].Columns[1].Caption.ToString();
			cbxGlosa.DataSource = ds.Tables[0];
		}

		public void Listar_Comuna(string strPublicacion, ref ComboBox cbxGlosa, ref ComboBox cbxId, int idRegion, int idprovincia)
		{
			DataSet ds = new DataSet();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select comuna,id_comuna from glo_comuna " +
									 "where id_provincia=" + idprovincia.ToString() + " and Id_region= " + idRegion.ToString() +
									 " order by comuna";
			cmd.CommandType = CommandType.Text;

			ds = Conectar.Listar(strPublicacion, cmd);
			cbxGlosa.DisplayMember = ds.Tables[0].Columns[0].Caption.ToString();
			cbxGlosa.ValueMember = ds.Tables[0].Columns[1].Caption.ToString();
			cbxGlosa.DataSource = ds.Tables[0];
		}

		public void Listar_Parametro(string strPublicacion, ref ComboBox cbxGlosa, ref ComboBox cbxId, int id)
		{
			DataSet ds = new DataSet();
			SqlCommand cmd = new SqlCommand();
			// SqlCommand cmd = new SqlCommand();
			//MessageBox.Show("Conectado al servidor");


			cmd.CommandText = "select Glosa,Id_DetParametro from conf_detparametro " +
									 "where Id_Parametro=" + id.ToString() + " and id_padre=0 and id_estado=1 order by orden";
			cmd.CommandType = CommandType.Text;

			ds = Conectar.Listar(strPublicacion, cmd);

			cbxGlosa.DisplayMember = ds.Tables[0].Columns[0].Caption.ToString();
			cbxGlosa.DataSource = ds.Tables[0];

			cbxId.DisplayMember = ds.Tables[0].Columns[1].Caption.ToString();
			cbxId.DataSource = ds.Tables[0];
		}

		public void Listar_Parametro(string strPublicacion, ref ComboBox cbxGlosa, int id)
		{
			DataSet ds = new DataSet();
			SqlCommand cmd = new SqlCommand();
			// SqlCommand cmd = new SqlCommand();
			//MessageBox.Show("Conectado al servidor");


			cmd.CommandText = "select  cast(Orden as varchar(max)) +'-'+Glosa as Glosa,Id_DetParametro from conf_detparametro " +
																 "where Id_Parametro=" + id.ToString() + " and id_padre=0 and id_estado=1 order by orden";
			cmd.CommandType = CommandType.Text;

			ds = Conectar.Listar(strPublicacion, cmd);

			cbxGlosa.DisplayMember = ds.Tables[0].Columns[0].Caption.ToString();
			cbxGlosa.ValueMember = ds.Tables[0].Columns[1].Caption.ToString();
			cbxGlosa.DataSource = ds.Tables[0];

		}

		public void Insertar(string strPublicacion, GroupBox tabla, ref Boolean bolResult)
		{
			DataSet ds = new DataSet();
			SqlCommand cmd = new SqlCommand();
			// SqlCommand cmd = new SqlCommand();

			cmd.CommandText = " SELECT is_identity,VALUE,is_nullable,columns.NAME AS COLUMNA,columns.NAME AS COLUMNA,CASE WHEN pk.COLUMN_NAME IS NOT NULL THEN 'PRI' ELSE '' END AS KeyType " +
			 "FROM sys.schemas INNER JOIN sys.tables ON schemas.schema_id = tables.schema_id " +
		 "INNER JOIN sys.columns  " +
			"ON tables.object_id = columns.object_id " +
		 "LEFT JOIN (SELECT ku.TABLE_CATALOG,ku.TABLE_SCHEMA,ku.TABLE_NAME,ku.COLUMN_NAME   " +
		 "FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS tc INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ku  ON  tc.CONSTRAINT_TYPE = 'PRIMARY KEY' AND " +
			"tc.CONSTRAINT_NAME = ku.CONSTRAINT_NAME) " +
		 "pk ON  pk.COLUMN_NAME=columns.NAME AND tables.NAME = pk.TABLE_NAME  " +
			"left JOIN  sys.extended_properties  ON tables.object_id = extended_properties.major_id   AND columns.column_id = extended_properties.minor_id " +
			" where tables.NAME= '" + tabla.Name.ToString() + "';";

			//"SELECT * FROM " +tabla.Name.ToString();// WHERE TABLE_SCHEMA = 'bd_sistema' AND "// +
			// "TABLE_NAME = '" + tabla.Name.ToString()    + "'";
			cmd.CommandType = CommandType.Text;

			ds = Conectar.Listar(strPublicacion, cmd);

			int intfila;
			string strnombre_control;
			string strtipo;
			string strrequerido;
			Control control;
			Label controllabel;
			string strvalor;
			string strinsert;
			string strvalues;
			string strcontrol_key;
			string strcontrol_extra;
			string strpaso;
			string strMensaje;
			strMensaje = "";
			strvalues = "";
			strpaso = "";
			strrequerido = "";
			strcontrol_key = "";
			strcontrol_extra = "";
			strinsert = "insert into " + tabla.Name.ToString() + "(";
			//auto_increment										
			for (intfila = 0; intfila <= ds.Tables[0].Rows.Count - 1; intfila++)
			{
				strnombre_control = ds.Tables[0].Rows[intfila]["COLUMNA"].ToString();
				strtipo = ds.Tables[0].Rows[intfila]["value"].ToString();
				strrequerido = ds.Tables[0].Rows[intfila]["is_nullable"].ToString();
				strcontrol_key = ds.Tables[0].Rows[intfila]["KeyType"].ToString();
				strcontrol_extra = ds.Tables[0].Rows[intfila]["is_identity"].ToString();
				strvalor = "";

				if ((strtipo != "") && (strcontrol_extra != "True"))
				{
					if (strtipo == "lbl")
					{
						control = tabla.Controls.Find(strtipo + "_" + strnombre_control, true).FirstOrDefault() as Label;
						strvalor = control.Text.ToString();
					}
					if (strtipo == "txt")
					{
						control = tabla.Controls.Find(strtipo + "_" + strnombre_control, true).FirstOrDefault() as TextBox;
						strvalor = control.Text.ToString();
						controllabel = tabla.Controls.Find("lbl_" + strnombre_control, true).FirstOrDefault() as Label;
						if (controllabel.Font.Underline == true)
						{
							if (String.IsNullOrEmpty(strvalor) )
							{
								strMensaje = strMensaje + "-" + controllabel.Text + System.Environment.NewLine;
							}
						}
					}
					if (strtipo == "rtb")
					{
						control = tabla.Controls.Find(strtipo + "_" + strnombre_control, true).FirstOrDefault() as RichTextBox;
						strvalor = control.Text.ToString();
					}
					if (strtipo == "dtp")
					{
						control = tabla.Controls.Find(strtipo + "_" + strnombre_control, true).FirstOrDefault() as DateTimePicker;
						controllabel = tabla.Controls.Find("lbl_" + strnombre_control, true).FirstOrDefault() as Label;
						strvalor = control.Text.ToString();

						if (controllabel.Font.Underline == true)
						{
							if (strvalor=="01-01-1900")
							{
								strMensaje = strMensaje + "-" + controllabel.Text + System.Environment.NewLine;
							}
						}
					}
					if (strtipo == "cbx")
					{
						controllabel = tabla.Controls.Find("lbl_" + strnombre_control, true).FirstOrDefault() as Label;
						strvalor = (tabla.Controls.Find(strtipo + "_" + strnombre_control, true).FirstOrDefault() as ComboBox).SelectedValue.ToString();
						if (controllabel.Font.Underline == true)
						{
							if(String.IsNullOrEmpty(strvalor) && strvalor=="0")
							{
								strMensaje = strMensaje + "-"  + controllabel.Text + System.Environment.NewLine;
							}
						}
					}
					if (strrequerido == "False")
					{
						if (strvalor == "")
							strMensaje += (tabla.Controls.Find("lbl_" + strnombre_control, true).FirstOrDefault() as Label).Text + "\n";
					}

					if (strpaso == "")
					{
						strpaso = "paso";
						strinsert = strinsert + strnombre_control;
						strvalues = "values('" + strvalor + "'";
					}
					else
					{
						strinsert = strinsert + "," + strnombre_control;
						strvalues = strvalues + ",'" + strvalor + "'";
					}
				}




			}//for

			if (strMensaje == "")
			{
				strinsert = strinsert + ")";
				strvalues = strvalues + ")";
				cmd.CommandText = strinsert + strvalues;

				//"SELECT * FROM " +tabla.Name.ToString();// WHERE TABLE_SCHEMA = 'bd_sistema' AND "// +
				// "TABLE_NAME = '" + tabla.Name.ToString()    + "'";
				cmd.CommandType = CommandType.Text;

				Conectar.AgregarModificarEliminar(strPublicacion, cmd);
				bolResult = true;
			}
			else
			{
				strMensaje = "Faltan campos por ingresar:" + "\n" + strMensaje;
				bolResult = false;
				MessageBox.Show(strMensaje);
			}

		}

		public void Modificar(string strPublicacion, GroupBox tabla, ref Boolean bolResult)
		{
			DataSet ds = new DataSet();
			SqlCommand cmd = new SqlCommand();
			// SqlCommand cmd = new SqlCommand();
			//MessageBox.Show("Conectado al servidor");



			cmd.CommandText = " SELECT is_identity,VALUE,is_nullable,columns.NAME AS COLUMNA,columns.NAME AS COLUMNA,CASE WHEN pk.COLUMN_NAME IS NOT NULL THEN 'PRI' ELSE '' END AS KeyType " +
			 "FROM sys.schemas INNER JOIN sys.tables ON schemas.schema_id = tables.schema_id " +
		 "INNER JOIN sys.columns  " +
			"ON tables.object_id = columns.object_id " +
		 "LEFT JOIN (SELECT ku.TABLE_CATALOG,ku.TABLE_SCHEMA,ku.TABLE_NAME,ku.COLUMN_NAME   " +
		 "FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS tc INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ku  ON  tc.CONSTRAINT_TYPE = 'PRIMARY KEY' AND " +
			"tc.CONSTRAINT_NAME = ku.CONSTRAINT_NAME) " +
		 "pk ON  pk.COLUMN_NAME=columns.NAME AND tables.NAME = pk.TABLE_NAME  " +
			"left JOIN  sys.extended_properties  ON tables.object_id = extended_properties.major_id   AND columns.column_id = extended_properties.minor_id " +
			" where tables.NAME= '" + tabla.Name.ToString() + "';";

			//"SELECT * FROM " +tabla.Name.ToString();// WHERE TABLE_SCHEMA = 'bd_sistema' AND "// +
			// "TABLE_NAME = '" + tabla.Name.ToString()    + "'";
			cmd.CommandType = CommandType.Text;

			ds = Conectar.Listar(strPublicacion, cmd);

			int intfila;
			string strnombre_control;
			string strcontrol_key;
			string strtipo;
			Control control;
			Label controllabel;
			string strvalor;
			string strinsert;
			string strvalues;
			string strwhere;
			string strMensaje;
			string strrequerido;
			strrequerido = "";
			strMensaje = "";
			strvalues = "";
			strwhere = "";
			//data_type
			strinsert = "update " + tabla.Name.ToString() + " ";
			for (intfila = 0; intfila <= ds.Tables[0].Rows.Count - 1; intfila++)
			{
				strcontrol_key = "";
				strnombre_control = ds.Tables[0].Rows[intfila]["COLUMNA"].ToString();
				strtipo = ds.Tables[0].Rows[intfila]["value"].ToString();
				strrequerido = ds.Tables[0].Rows[intfila]["is_nullable"].ToString();
				strcontrol_key = ds.Tables[0].Rows[intfila]["KeyType"].ToString();
				// strcontrol_extra = ds.Tables[0].Rows[intfila]["is_identity"].ToString();
				strvalor = "";

				if (strtipo != "")
				{
					if (strtipo == "lbl")
					{
						control = tabla.Controls.Find(strtipo + "_" + strnombre_control, true).FirstOrDefault() as Label;
						strvalor = control.Text.ToString();
					}
					if (strtipo == "txt")
					{
						control = tabla.Controls.Find(strtipo + "_" + strnombre_control, true).FirstOrDefault() as TextBox;
						strvalor = control.Text.ToString();
						controllabel = tabla.Controls.Find("lbl_" + strnombre_control, true).FirstOrDefault() as Label;
						if (controllabel.Font.Underline == true)
						{
							if (String.IsNullOrEmpty(strvalor))
							{
								strMensaje = strMensaje + "-" + controllabel.Text + System.Environment.NewLine;
							}
						}
					}
					if (strtipo == "rtb")
					{
						control = tabla.Controls.Find(strtipo + "_" + strnombre_control, true).FirstOrDefault() as RichTextBox;
						strvalor = control.Text.ToString();
					}
					if (strtipo == "dtp")
					{
						control = tabla.Controls.Find(strtipo + "_" + strnombre_control, true).FirstOrDefault() as DateTimePicker;
						strvalor = control.Text.ToString();
						controllabel = tabla.Controls.Find("lbl_" + strnombre_control, true).FirstOrDefault() as Label;
						if (controllabel.Font.Underline == true)
						{
							if (strvalor == "01-01-1900")
							{
								strMensaje = strMensaje + "-" + controllabel.Text + System.Environment.NewLine;
							}
						}
					}
					if (strtipo == "cbx")
					{
						//control = tabla.Controls.Find(strtipo + "_" + strnombre_control, true).FirstOrDefault() as ComboBox;
						//strvalor = control.Text.ToString();
						strvalor = (tabla.Controls.Find(strtipo + "_" + strnombre_control, true).FirstOrDefault() as ComboBox).SelectedValue.ToString();
						controllabel = tabla.Controls.Find("lbl_" + strnombre_control, true).FirstOrDefault() as Label;
						if (controllabel.Font.Underline == true)
						{
							if (String.IsNullOrEmpty(strvalor) && strvalor == "0")
							{
								strMensaje = strMensaje + "-" + controllabel.Text + System.Environment.NewLine;
							}
						}
					}

					if (strrequerido == "0")
					{
						if (strvalor == "")
							strMensaje += (tabla.Controls.Find("lbl_" + strnombre_control, true).FirstOrDefault() as Label).Text + "\n";
					}

					if (strcontrol_key == "PRI")
					{
						if (strwhere == "")
							strwhere = " where (" + strnombre_control + "='" + strvalor + "')";
						else
							strwhere = "and (" + strnombre_control + "=" + strvalor + ")";
					}
					else
					{
						if (strvalues == "")
						{
							strvalues = "paso";
							strinsert = strinsert + "set " + strnombre_control + "='" + strvalor + "'";
						}
						else
						{

							strinsert = strinsert + "," + strnombre_control + "='" + strvalor + "'";
						}
					}
				} //if(strtipo != ""){
			}//for
			 //  strinsert = strinsert + " ";

			if (strMensaje == "")
			{
				cmd.CommandText = strinsert + strwhere;

				//"SELECT * FROM " +tabla.Name.ToString();// WHERE TABLE_SCHEMA = 'bd_sistema' AND "// +
				// "TABLE_NAME = '" + tabla.Name.ToString()    + "'";
				cmd.CommandType = CommandType.Text;

				Conectar.AgregarModificarEliminar(strPublicacion, cmd);

				bolResult = true;
			}
			else
			{
				strMensaje = "Faltan campos por ingresar:" + "\n" + strMensaje;
				bolResult = false;
				MessageBox.Show(strMensaje);
			}

		}

		public void ModificarDatos(string strPublicacion, GroupBox tabla, ref Boolean bolResult)
		{
			DataSet ds = new DataSet();
			SqlCommand cmd = new SqlCommand();
			// SqlCommand cmd = new SqlCommand();
			//MessageBox.Show("Conectado al servidor");



			cmd.CommandText = " SELECT is_identity,VALUE,is_nullable,columns.NAME AS COLUMNA,columns.NAME AS COLUMNA,CASE WHEN pk.COLUMN_NAME IS NOT NULL THEN 'PRI' ELSE '' END AS KeyType " +
				 "FROM sys.schemas INNER JOIN sys.tables ON schemas.schema_id = tables.schema_id " +
		 "INNER JOIN sys.columns  " +
			"ON tables.object_id = columns.object_id " +
		 "LEFT JOIN (SELECT ku.TABLE_CATALOG,ku.TABLE_SCHEMA,ku.TABLE_NAME,ku.COLUMN_NAME   " +
		 "FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS tc INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ku  ON  tc.CONSTRAINT_TYPE = 'PRIMARY KEY' AND " +
			"tc.CONSTRAINT_NAME = ku.CONSTRAINT_NAME) " +
		 "pk ON  pk.COLUMN_NAME=columns.NAME AND tables.NAME = pk.TABLE_NAME  " +
			"left JOIN  sys.extended_properties  ON tables.object_id = extended_properties.major_id   AND columns.column_id = extended_properties.minor_id " +
			" where tables.NAME= '" + tabla.Name.ToString() + "';";

			//"SELECT * FROM " +tabla.Name.ToString();// WHERE TABLE_SCHEMA = 'bd_sistema' AND "// +
			// "TABLE_NAME = '" + tabla.Name.ToString()    + "'";
			cmd.CommandType = CommandType.Text;

			ds = Conectar.Listar(strPublicacion, cmd);

			int intfila;
			string strnombre_control;
			string strcontrol_key;
			string strtipo;
			Control control;
			string strvalor;
			string strinsert;
			string strvalues;
			string strwhere;
			string strMensaje;
			string strrequerido;
			strrequerido = "";
			strMensaje = "";
			strvalues = "";
			strwhere = "";
			//data_type
			strinsert = "update " + tabla.Name.ToString() + " ";
			for (intfila = 0; intfila <= ds.Tables[0].Rows.Count - 1; intfila++)
			{
				strcontrol_key = "";
				strnombre_control = ds.Tables[0].Rows[intfila]["COLUMNA"].ToString();
				strtipo = ds.Tables[0].Rows[intfila]["value"].ToString();
				strrequerido = ds.Tables[0].Rows[intfila]["is_nullable"].ToString();
				strcontrol_key = ds.Tables[0].Rows[intfila]["KeyType"].ToString();
				// strcontrol_extra = ds.Tables[0].Rows[intfila]["is_identity"].ToString();
				strvalor = "";

				if (strtipo != "")
				{
					if (strtipo == "lbl")
					{
						control = tabla.Controls.Find(strtipo + "_" + strnombre_control, true).FirstOrDefault() as Label;

						if (control != null)
							strvalor = control.Text.ToString();
					}
					if (strtipo == "txt")
					{
						control = tabla.Controls.Find(strtipo + "_" + strnombre_control, true).FirstOrDefault() as TextBox;
						if (control != null)
							strvalor = control.Text.ToString();
					}
					if (strtipo == "rtb")
					{
						control = tabla.Controls.Find(strtipo + "_" + strnombre_control, true).FirstOrDefault() as RichTextBox;
						if (control != null)
							strvalor = control.Text.ToString();
					}
					if (strtipo == "dtp")
					{
						control = tabla.Controls.Find(strtipo + "_" + strnombre_control, true).FirstOrDefault() as DateTimePicker;
						if (control != null)
							strvalor = control.Text.ToString();

					}
					if (strtipo == "cbx")
					{
						control = tabla.Controls.Find(strtipo + "_" + strnombre_control, true).FirstOrDefault() as ComboBox;
						//strvalor = control.Text.ToString();
						if (control != null)
							strvalor = (tabla.Controls.Find(strtipo + "_" + strnombre_control, true).FirstOrDefault() as ComboBox).SelectedValue.ToString();

					}

					if (strrequerido == "0")
					{
						if (strvalor == "")
							strMensaje += (tabla.Controls.Find("lbl_" + strnombre_control, true).FirstOrDefault() as Label).Text + "\n";
					}

					if (strcontrol_key == "PRI")
					{
						if (strwhere == "")
							strwhere = " where (" + strnombre_control + "='" + strvalor + "')";
						else
							strwhere = "and (" + strnombre_control + "=" + strvalor + ")";
					}
					else
					{
						if (strvalues == "")
						{
							if (strvalor != "")
							{
								strvalues = "paso";
								strinsert = strinsert + "set " + strnombre_control + "='" + strvalor + "'";
							}
						}
						else
						{
							if (strvalor != "")
								strinsert = strinsert + "," + strnombre_control + "='" + strvalor + "'";
						}
					}
				} //if(strtipo != ""){
			}//for
			 //  strinsert = strinsert + " ";

			if (strMensaje == "")
			{
				cmd.CommandText = strinsert + strwhere;

				//"SELECT * FROM " +tabla.Name.ToString();// WHERE TABLE_SCHEMA = 'bd_sistema' AND "// +
				// "TABLE_NAME = '" + tabla.Name.ToString()    + "'";
				cmd.CommandType = CommandType.Text;

				Conectar.AgregarModificarEliminar(strPublicacion, cmd);

				bolResult = true;
			}
			else
			{
				strMensaje = "Faltan campos por ingresar:" + "\n" + strMensaje;
				bolResult = false;
				MessageBox.Show(strMensaje);
			}

		}

		public bool ValidarFormulario(string strPublicacion, GroupBox tabla, ref Boolean bolResult, ref string strMensaje)
		{
			bool bolValidar = false;
			DataSet ds = new DataSet();
			SqlCommand cmd = new SqlCommand();
			// SqlCommand cmd = new SqlCommand();

			cmd.CommandText = " SELECT is_identity,VALUE,is_nullable,columns.NAME AS COLUMNA,columns.NAME AS COLUMNA,CASE WHEN pk.COLUMN_NAME IS NOT NULL THEN 'PRI' ELSE '' END AS KeyType " +
			 "FROM sys.schemas INNER JOIN sys.tables ON schemas.schema_id = tables.schema_id " +
		 "INNER JOIN sys.columns  " +
			"ON tables.object_id = columns.object_id " +
		 "LEFT JOIN (SELECT ku.TABLE_CATALOG,ku.TABLE_SCHEMA,ku.TABLE_NAME,ku.COLUMN_NAME   " +
		 "FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS tc INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ku  ON  tc.CONSTRAINT_TYPE = 'PRIMARY KEY' AND " +
			"tc.CONSTRAINT_NAME = ku.CONSTRAINT_NAME) " +
		 "pk ON  pk.COLUMN_NAME=columns.NAME AND tables.NAME = pk.TABLE_NAME  " +
			"left JOIN  sys.extended_properties  ON tables.object_id = extended_properties.major_id   AND columns.column_id = extended_properties.minor_id " +
			" where tables.NAME= '" + tabla.Name.ToString() + "';";

			//"SELECT * FROM " +tabla.Name.ToString();// WHERE TABLE_SCHEMA = 'bd_sistema' AND "// +
			// "TABLE_NAME = '" + tabla.Name.ToString()    + "'";
			cmd.CommandType = CommandType.Text;

			ds = Conectar.Listar(strPublicacion, cmd);
			strMensaje = "";
			int intfila;
			string strnombre_control;
			string strtipo;
			string strrequerido;
			Control control;
			Label controllabel;
			string strvalor;
			string strinsert;
			string strvalues;
			string strcontrol_key;
			string strcontrol_extra;
			string strpaso;
			strvalues = "";
			strpaso = "";
			strrequerido = "";
			strcontrol_key = "";
			strcontrol_extra = "";
			strinsert = "insert into " + tabla.Name.ToString() + "(";
			//auto_increment										
			for (intfila = 0; intfila <= ds.Tables[0].Rows.Count - 1; intfila++)
			{
				strnombre_control = ds.Tables[0].Rows[intfila]["COLUMNA"].ToString();
				strtipo = ds.Tables[0].Rows[intfila]["value"].ToString();
				strrequerido = ds.Tables[0].Rows[intfila]["is_nullable"].ToString();
				strcontrol_key = ds.Tables[0].Rows[intfila]["KeyType"].ToString();
				strcontrol_extra = ds.Tables[0].Rows[intfila]["is_identity"].ToString();
				strvalor = "";

				if ((strtipo != "") && (strcontrol_extra != "True"))
				{
					if (strtipo == "lbl")
					{
						control = tabla.Controls.Find(strtipo + "_" + strnombre_control, true).FirstOrDefault() as Label;
						strvalor = control.Text.ToString();
					}
					if (strtipo == "txt")
					{
						control = tabla.Controls.Find(strtipo + "_" + strnombre_control, true).FirstOrDefault() as TextBox;
						strvalor = control.Text.ToString();
						controllabel = tabla.Controls.Find("lbl_" + strnombre_control, true).FirstOrDefault() as Label;
						if (controllabel.Font.Underline == true)
						{
							if (String.IsNullOrEmpty(strvalor))
							{
								strMensaje = strMensaje + "-" + controllabel.Text + System.Environment.NewLine;
							}
						}
					}
					if (strtipo == "rtb")
					{
						control = tabla.Controls.Find(strtipo + "_" + strnombre_control, true).FirstOrDefault() as RichTextBox;
						strvalor = control.Text.ToString();
					}
					if (strtipo == "dtp")
					{
						control = tabla.Controls.Find(strtipo + "_" + strnombre_control, true).FirstOrDefault() as DateTimePicker;
						controllabel = tabla.Controls.Find("lbl_" + strnombre_control, true).FirstOrDefault() as Label;
						strvalor = control.Text.ToString();

						if (controllabel.Font.Underline == true)
						{
							if (strvalor == "01-01-1900")
							{
								strMensaje = strMensaje + "-" + controllabel.Text + System.Environment.NewLine;
							}
						}
					}
					if (strtipo == "cbx")
					{
						controllabel = tabla.Controls.Find("lbl_" + strnombre_control, true).FirstOrDefault() as Label;
						strvalor = (tabla.Controls.Find(strtipo + "_" + strnombre_control, true).FirstOrDefault() as ComboBox).SelectedValue.ToString();
						if (controllabel.Font.Underline == true)
						{
							if (String.IsNullOrEmpty(strvalor) && strvalor == "0")
							{
								strMensaje = strMensaje + "-" + controllabel.Text + System.Environment.NewLine;
							}
						}
					}

				}
			}//for

			if (strMensaje == "")
			{
				bolValidar = true;
			}
			else
			{

				strMensaje = "Faltan campos por ingresar:" + "\n" + strMensaje;
				bolValidar = false;
			}

			return bolValidar;

		}

		public DataSet RptDosimetria(string strPublicacion, Int64 id_cliente, Int64 id_region, String run, Int64 anno)
		{
			DataSet ds = new DataSet();
			SqlCommand cmd = new SqlCommand();

			cmd.CommandText = "RptDosimetro_filtro " + anno.ToString() + "," + id_cliente.ToString() + "," + id_region.ToString() + ",'" + run + "'";

			cmd.CommandType = CommandType.Text;

			ds = Conectar.Listar(strPublicacion, cmd);
			return ds;
		}

		public DataSet RptDosimetriaPa(string strPublicacion, Int64 id_cliente, Int64 id_region, String run, Int64 anno)
		{
			DataSet ds = new DataSet();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "rptDosimetroPorRut " + anno.ToString() + "," + id_cliente.ToString() + "," + id_region.ToString() + ",'" + run + "'";

			cmd.CommandType = CommandType.Text;

			ds = Conectar.Listar(strPublicacion, cmd);
			return ds;
		}
	}
}
