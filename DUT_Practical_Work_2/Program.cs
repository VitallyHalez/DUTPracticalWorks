using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DUT_Practical_Work_2 {
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
			int nn = helper.GetConsoleValue<int>("\nEnter nn:");
			int nk = helper.GetConsoleValue<int>("Enter nk:");
			
			if (ValidateLimits(nn, nk)) {
				double sum = GetSum(nn, nk);
				Console.WriteLine("Sum is: " + sum);
			}

			Calculate();
		}

		private bool ValidateLimits(int nn, int nk) {
			bool isValidate = nn < nk;

			if (!isValidate) {
				Console.WriteLine("Error: nk < nn!");
			}

			return isValidate;
		}

		private double GetSum(int nn, int nk) {
			double sum = double.MinValue;
			for (int i = nn; i <= nk; i++) {
				sum *= ((Math.Pow(i, 2) - 3) / (Math.Pow(i, 2) - Math.Pow(-1, i) * i + 3));
			}
			return sum;
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
