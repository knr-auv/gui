
using GUI_v2.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;


namespace GUI_v2.View.Converters
{
	public class MultiCommandConverter : IMultiValueConverter
	{
		private List<object> _value = new List<object>();
		private string key;
		private Dictionary<string, List<object>> commands= new Dictionary<string, List<object>>();
		/// <summary>
		/// dobbin of the converter
		/// </summary>
		/// <param name="value">commands binded by means of multibiniding</param>
		/// <returns>compound Relay command</returns>
		/// 

		public object Convert(object[] value, Type targetType,
			object parameter, CultureInfo culture)
		{
			if (parameter == null)
			{
				_value.AddRange(value);
				return new RelayCommand(GetCompoundExecute(null), GetCompoundCanExecute(null));
			}
			else
			{
				List<object> temp = new List<object>();
				temp.AddRange(value);
				key = (string)parameter;
				commands.Add(key, temp);
				return new RelayCommand(GetCompoundExecute(key), GetCompoundCanExecute(key));
			}
		}

		/// <summary>
		/// here - mandatory duty
		/// </summary>
		public object[] ConvertBack(object value, Type[] targetTypes,
			object parameter, CultureInfo culture)
		{
			return null;
		}

		/// <summary>
		/// for execution of all commands
		/// </summary>
		/// <returns>Action<object> that plays a role of the joint Execute</returns>
		private Action<object> GetCompoundExecute(string key)
		{
			return (parameter) =>
			{if(key!=null)
				foreach (RelayCommand command in commands[key])
				{
					if (command != default(RelayCommand))
						command.Execute(parameter);
				}
			else
					foreach (RelayCommand c in _value)
						if (c != default(RelayCommand))
							c.Execute(parameter);

			};
		}

		/// <summary>
		/// for check if execution of all commands is possible
		/// </summary>
		/// <returns>Predicate<object> that plays a role of the joint CanExecute</returns>
		private Predicate<object> GetCompoundCanExecute(string key)
		{
			return (parameter) =>
			{
				bool res = true;
				if(key!=null)
					foreach (RelayCommand command in commands[key])
						if (command != default(RelayCommand))
							res &= command.CanExecute(parameter);
				else
					foreach (RelayCommand c in _value)
						if (c != default(RelayCommand))
							res &= c.CanExecute(parameter);
				return res;
			};
		}
	}
}
