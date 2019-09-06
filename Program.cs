using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DUT_Practical_Work_1 {
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
			double radius = helper.GetConsoleValue("Enter radius:");

			Console.WriteLine("Volume: " + GetVolumeSphere(radius));
			Console.WriteLine("Square: " + GetSquareSphere(radius) + "\n");

			Calculate();
		}

		private string GetVolumeSphere(double radius) {
			double result = (4 / 3) * Math.PI * Math.Pow(radius, 3);
			return result.ToString();
		}

		private string GetSquareSphere(double radius) {
			double result = 4 * Math.PI * Math.Pow(radius, 2);
			return result.ToString();
		}
	}

	class OutputProcessingHelper {

		public double GetConsoleValue(string description) {
			Console.WriteLine(description);
			return SafeGetDoubleValue(Console.ReadLine());
		}

		public double SafeGetDoubleValue(string value) {
			double outValue = double.MinValue;
			double.TryParse(PrepareConsoleValueToDouble(value), out outValue);

			return outValue;
		}

		public string PrepareConsoleValueToDouble(string value) {
			StringBuilder sb = new StringBuilder(value);
			sb.Replace(".", ",");
			return sb.ToString();
		}
	}

}
