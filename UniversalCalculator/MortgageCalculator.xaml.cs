using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Calculator
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class Mortgage : Page
	{
		public Mortgage()
		{
			InitializeComponent();
		}

		// Calculates monthly mortgage repayment using:
		// M = P [ i(1 + i)^n ] / [ (1 + i)^n - 1 ]
		//   P = principal loan amount
		//   i = monthly interest rate (annual rate / 12 / 100)
		//   n = number of monthly payments (years * 12)
		private void btnCalculate_Click(object sender, RoutedEventArgs e)
		{
			resultCard.Visibility = Visibility.Visible;

			if (!double.TryParse(txtPrincipal.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out double principal)
				|| principal <= 0)
			{
				ShowError("Enter a valid loan amount greater than 0.");
				return;
			}

			if (!double.TryParse(txtAnnualRate.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out double annualRatePercent)
				|| annualRatePercent < 0)
			{
				ShowError("Enter a valid annual interest rate (0 or higher).");
				return;
			}

			if (!int.TryParse(txtTermYears.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out int termYears)
				|| termYears <= 0)
			{
				ShowError("Enter a valid loan term in years greater than 0.");
				return;
			}

			double monthlyRate = annualRatePercent / 100.0 / 12.0;
			int numberOfPayments = termYears * 12;

			double monthlyRepayment;

			if (monthlyRate == 0)
			{
				// No interest - simple straight-line division to avoid divide-by-zero
				monthlyRepayment = principal / numberOfPayments;
			}
			else
			{
				double factor = Math.Pow(1 + monthlyRate, numberOfPayments);
				monthlyRepayment = principal * (monthlyRate * factor) / (factor - 1);
			}

			double totalRepayment = monthlyRepayment * numberOfPayments;
			double totalInterest = totalRepayment - principal;

			ShowSuccess(
				$"Monthly repayment: ${monthlyRepayment:N2}\n" +
				$"Total repayment: ${totalRepayment:N2}\n" +
				$"Total interest: ${totalInterest:N2}");
		}

		private void ShowSuccess(string message)
		{
			resultCard.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 239, 246, 236));
			resultCard.BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 127, 184, 120));
			txtResult.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 46, 91, 39));
			txtResult.Text = message;
		}

		private void ShowError(string message)
		{
			resultCard.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 253, 236, 234));
			resultCard.BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 217, 92, 84));
			txtResult.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 158, 42, 33));
			txtResult.Text = message;
		}

		private void btnBack_Click(object sender, RoutedEventArgs e)
		{
			if (Frame.CanGoBack)
			{
				Frame.GoBack();
			}
			else
			{
				Frame.Navigate(typeof(MainMenu));
			}
		}
	} // test
}
