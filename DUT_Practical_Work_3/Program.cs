using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DUT_Practical_Work_3 {
	class Program {
		static void Main(string[] args) {
			CalculateHelper helper = new CalculateHelper();
			helper.Calculate();
		}
	}

	class CalculateHelper {
		private OutputProcessingHelper _helper;

		private OutputProcessingHelper helper {
			get {
				return _helper ?? (
					_helper = new OutputProcessingHelper()
				);
			}
		}

		public void Calculate() {
			double x = helper.GetConsoleValue<double>("\nEnter x:");
			double y = helper.GetConsoleValue<double>("Enter y:");

			if (ValidateParameters(x, y)) {
				Console.WriteLine("Parameters in second part");
			}

			Calculate();
		}

		private bool ValidateParameters(double x, double y) {
			bool isValidate = x < 0 && y > 0;

			if (!isValidate) {
				Console.WriteLine("Parameters isn`t in second part");
			}

			return isValidate;
		}
	}

	class OutputProcessingHelper {

		public T GetConsoleValue<T>(string description) {
			Console.WriteLine(description);
			return SafeGetTypedValue<T>(Console.ReadLine());
		}

		public T SafeGetTypedValue<T>(string value) {
			try {
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
				if (converter != null) {
					return (T)converter.ConvertFromString(value);
				}
				return default(T);
			} catch (NotSupportedException) {
				return default(T);
			}
		}
	}
}
