using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DUT_Practical_Work_4 {
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
			int arrLength = helper.GetConsoleValue<int>("\nEnter array size:");
			double[] arr = helper.GetConsoleArrayValue<double>("\nEnter array:", arrLength);
			
			Console.WriteLine("New positive array: " + PrepareArrayPositive(arr));
			Console.WriteLine("New negative array: " + PrepareArrayNegative(arr));
			
			Calculate();
		}

		private string PrepareArrayPositive(double[] arr) {
			int newArrLength = arr.Length + arr.Count(x => x > 0);
			double[] newArr = new double[newArrLength];

			for (int i = 0, j = 0; i < arr.Length; i++, j++) {
				newArr[j] = arr[i];

				if (arr[i] > 0) {
					newArr[j + 1] = 0;
					j++;
				}
			}

			return string.Join(", ", newArr);
		}

		private string PrepareArrayNegative(double[] arr) {
			int newArrLength = arr.Length + arr.Count(x => x < 0);
			double[] newArr = new double[newArrLength];

			for (int i = 0, j = 0; i < arr.Length; i++, j++) {
				newArr[j] = arr[i];

				if (arr[i] < 0) {
					newArr[j + 1] = 0;
					j++;
				}
			}

			return string.Join(", ", newArr);
		}
	}

	class OutputProcessingHelper {

		public T GetConsoleValue<T>(string description) {
			Console.WriteLine(description);
			return SafeGetTypedValue<T>(Console.ReadLine());
		}

		public T[] GetConsoleArrayValue<T>(string description, int arrLength) {
			Console.WriteLine(description);
			T[] arr = new T[arrLength];

			for (int i = 0; i < arrLength; i++) {
				arr[i] = SafeGetTypedValue<T>(Console.ReadLine());
			}

			return arr;
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
