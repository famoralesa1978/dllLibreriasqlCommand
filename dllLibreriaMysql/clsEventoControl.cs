using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace dllLibreriaEvento
{
	public class clsEventoControl
	{
		// Boolean flag used to determine when a character other than a number is entered.
		private bool nonNumberEntered = false;
		private bool nonRutEntered = false;
		private bool nonDireccion = false;

		// Handle the KeyDown event to determine the type of character entered into the control.
		public void Numero_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// Initialize the flag to false.
			nonNumberEntered = false;

			// Determine whether the keystroke is a number from the top of the keyboard.
			if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
			{
				// Determine whether the keystroke is a number from the keypad.
				if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
				{
					// Determine whether the keystroke is a backspace.
					if ((e.KeyCode != Keys.Back) && (e.KeyCode != Keys.Enter))
					{
						// A non-numerical keystroke was pressed.
						// Set the flag to true and evaluate in KeyPress event.
						nonNumberEntered = true;
					}
				}
			}
			//If shift key was pressed, it's not a number.
			if (Control.ModifierKeys == Keys.Shift)
			{
				nonNumberEntered = true;
			}
		}

		public void Numero_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			// Check for the flag being set in the KeyDown event.
			if (nonNumberEntered == true)
			{
				// Stop the character from being entered into the control since it is non-numerical.
				e.Handled = true;
			}
			else
				if (e.KeyChar == (char)(Keys.Enter))
			{
				e.Handled = true;
				SendKeys.Send("{TAB}");
			}
		}

		public void Rut_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// Initialize the flag to false.
			nonRutEntered = false;

			// Determine whether the keystroke is a number from the top of the keyboard.
			if ((e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9) && (e.KeyCode != Keys.K && e.KeyValue != 75) && (e.KeyValue != 109) && (e.KeyValue != 189))
			{
				// Determine whether the keystroke is a number from the keypad.
				if ((e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9) && (e.KeyCode != Keys.K) && (e.KeyValue != 109) && (e.KeyValue != 189))
				{
					// Determine whether the keystroke is a backspace.
					if ((e.KeyCode != Keys.Back) && (e.KeyCode != Keys.Enter))
					{
						// A non-numerical keystroke was pressed.
						// Set the flag to true and evaluate in KeyPress event.
						nonRutEntered = true;
					}
				}
			}
			//If shift key was pressed, it's not a number.
			if (Control.ModifierKeys == Keys.Shift)
			{
				nonRutEntered = true;
			}
		}

		public void Rut_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			// Check for the flag being set in the KeyDown event.
			if (nonRutEntered == true)
			{
				// Stop the character from being entered into the control since it is non-numerical.
				e.Handled = true;
			}
			else
				if (e.KeyChar == (char)(Keys.Enter))
			{
				e.Handled = true;
				SendKeys.Send("{TAB}");
			}

		}

		public void Direccion_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			// Check for the flag being set in the KeyDown event.
			if ((e.KeyChar.ToString() == "/") || (e.KeyChar.ToString() == "\\") )
			{
				// Stop the character from being entered into the control since it is non-numerical.
				e.Handled = true;
			}
			else
				if (e.KeyChar == (char)(Keys.Enter))
			{
				e.Handled = true;
				SendKeys.Send("{TAB}");
			}
		}

		public void run_Leave(object sender, EventArgs e)
		{
			string strRun;
			strRun = (sender as TextBox).Text;
			try
			{

				if(!String.IsNullOrEmpty( strRun)){

					strRun = strRun.ToUpper();
					strRun = strRun.Replace(".", "");
					strRun = strRun.Replace("-", "");
					int rutAux = int.Parse(strRun.Substring(0, strRun.Length - 1));

					char dv = char.Parse(strRun.Substring(strRun.Length - 1, 1));

					(sender as TextBox).Text = rutAux + "-" + dv;
				}
			
			}
			catch (Exception)
			{
			}

		}

		public void validarut_Validated(object sender, System.EventArgs e)
		{
			string strRun;
			strRun = (sender as TextBox).Text;
			try
			{
				strRun = strRun.ToUpper();
				strRun = strRun.Replace(".", "");
				strRun = strRun.Replace("-", "");
				int rutAux = int.Parse(strRun.Substring(0, strRun.Length - 1));

				char dv = char.Parse(strRun.Substring(strRun.Length - 1, 1));

				int m = 0, s = 1;
				for (; rutAux != 0; rutAux /= 10)
				{
					s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
				}
				if (dv != (char)(s != 0 ? s + 47 : 75))
				{
					(sender as TextBox).Text = "";
					MessageBox.Show("El rut o digitador es incorrecto");
					(sender as TextBox).Focus();
				}
			}
			catch (Exception)
			{
			}
		}

		public void validaVariosEmail_Validated(object sender, System.EventArgs e)
		{
			string strEmail;
			strEmail = (sender as TextBox).Text;
			try
			{
				string[] stringSeparators = new string[] { ";" };
				string[] result = strEmail.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
				String expresion;
				expresion = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
				foreach (string s in result)
				{

					if (System.Text.RegularExpressions.Regex.IsMatch(s, expresion))
					{
						if (System.Text.RegularExpressions.Regex.Replace(s, @"(@)(.+)$", String.Empty).Length == 0)
						{
							(sender as TextBox).Text = "";
							MessageBox.Show("El formato de correo esta incorrecto");
							(sender as TextBox).Focus();
						}
						else
						{
							(sender as TextBox).Text = (sender as TextBox).Text;
							//	SendKeys.Send("{TAB}");
						}

					}
					else
					{
						(sender as TextBox).Text = "";
						MessageBox.Show("El formato de correo esta incorrecto");
						(sender as TextBox).Focus();
					}
				}


			}
			catch (Exception)
			{
			}
		}


		public void validaEmail_Validated(object sender, System.EventArgs e)
		{
			string strEmail;
			strEmail = (sender as TextBox).Text;
			try
			{
				String expresion;
				expresion = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
		 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
				if (System.Text.RegularExpressions.Regex.IsMatch(strEmail, expresion))
				{
					if (System.Text.RegularExpressions.Regex.Replace(strEmail, @"(@)(.+)$", String.Empty).Length == 0)
					{
						(sender as TextBox).Text = "";
						MessageBox.Show("El formato de correo esta incorrecto");
						(sender as TextBox).Focus();
					}
					else
					{
						(sender as TextBox).Text = strEmail;
						//	SendKeys.Send("{TAB}");
					}

				}
				else
				{
					(sender as TextBox).Text = "";
					MessageBox.Show("El formato de correo esta incorrecto");
					(sender as TextBox).Focus();
				}
			}
			catch (Exception)
			{
			}
		}

		public bool validarRut(string rut)
		{

			bool validacion = false;
			try
			{
				rut = rut.ToUpper();
				rut = rut.Replace(".", "");
				rut = rut.Replace("-", "");
				int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

				char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

				int m = 0, s = 1;
				for (; rutAux != 0; rutAux /= 10)
				{
					s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
				}
				if (dv == (char)(s != 0 ? s + 47 : 75))
				{
					validacion = true;
				}
			}
			catch (Exception)
			{
			}
			return validacion;
		}

		public void Avanzar_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)(Keys.Enter))
			{
				e.Handled = true;
				SendKeys.Send("{TAB}");
			}

		}


	}
}
