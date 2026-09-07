using System;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Calculator
{
	public sealed partial class ForeignExchangeCurrencyCalculator : Page
	{
		public ForeignExchangeCurrencyCalculator()
		{
			this.InitializeComponent();

			// Constants
			fromCurrencyComboBox.SelectedIndex = 0; // USD
			toCurrencyComboBox.SelectedIndex = 1;   // EUR
		}

		/// <summary>
		/// Gets the conversion rate between two currencies.
		/// </summary>
		/// <param name="from">The currency to convert from.</param>
		/// <param name="to">The currency to convert to.</param>
		/// <returns>The conversion rate.</returns>
		private double GetConversionRate(string from, string to)
		{
			// Same currency
			if (from == to)
			{
				return 1.0;
			}

			// USD
			if (from == "USD")
			{
				if (to == "EUR") return 0.85189982;
				if (to == "GBP") return 0.72872436;
				if (to == "INR") return 74.257327;
			}

			// EUR
			if (from == "EUR")
			{
				if (to == "USD") return 1.1739732;
				if (to == "GBP") return 0.8556672;
				if (to == "INR") return 87.00755;
			}

			// GBP
			if (from == "GBP")
			{
				if (to == "USD") return 1.371907;
				if (to == "EUR") return 1.1686692;
				if (to == "INR") return 101.68635;
			}

			// INR
			if (from == "INR")
			{
				if (to == "USD") return 0.011492628;
				if (to == "EUR") return 0.013492774;
				if (to == "GBP") return 0.0098339397;
			}

			return 1.0;
		}
		/// <summary>
		/// Handles the click event for the calculate currency button.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The event data.</param>
		private void calculateCurrencyButton_Click(
			object sender,
			RoutedEventArgs e)
		{
			try
			{
				// Get amount
				string cleanAmount = amountTextBox.Text
					.Replace("$", "")
					.Replace("€", "")
					.Replace("£", "")
					.Replace("₹", "")
					.Trim();

				double amount;

				if (!double.TryParse(cleanAmount, out amount))
				{
					resultTextBlock.Text =
						"Please enter a valid numeric amount.";

					rateTextBlock.Text = "";
					return;
				}

				// Get selected currencies
				ComboBoxItem fromItem =
					fromCurrencyComboBox.SelectedItem as ComboBoxItem;

				ComboBoxItem toItem =
					toCurrencyComboBox.SelectedItem as ComboBoxItem;

				if (fromItem == null || toItem == null)
				{
					resultTextBlock.Text =
						"Please select both currencies.";

					rateTextBlock.Text = "";
					return;
				}

				// Get currency codes from Tag
				string fromCurrency = fromItem.Tag.ToString();
				string toCurrency = toItem.Tag.ToString();

				// Get conversion rate
				double rate =
					GetConversionRate(fromCurrency, toCurrency);

				// Calculate result
				double result = amount * rate;

				// Currency names
				string fromName =
					GetCurrencyName(fromCurrency);

				string toName =
					GetCurrencyName(toCurrency);

				// Display result
				resultTextBlock.Text =
					$"{GetSymbol(toCurrency)}{result:F2} {toName}";

				// Display exchange rate
				rateTextBlock.Text =
					$"1 {fromCurrency} = {rate:G} {toName}\n\n" +
					$"1 {toName} = {GetConversionRate(toCurrency, fromCurrency):G} {fromCurrency}";
			}
			catch (Exception ex)
			{
				resultTextBlock.Text =
					$"Error: {ex.Message}";

				rateTextBlock.Text = "";
			}
		}

		/// <summary>
		/// Gets the name of a currency based on its code.
		/// </summary>
		/// <param name="currency">The currency code.</param>
		/// <returns>The currency name.</returns>
		private string GetCurrencyName(string currency)
		{
			switch (currency)
			{
				case "USD":
					return "US Dollar";

				case "EUR":
					return "Euro";

				case "GBP":
					return "British Pound";

				case "INR":
					return "Indian Rupee";

				default:
					return currency;
			}
		}

		/// <summary>
		/// Gets the symbol for a currency based on its code.
		/// </summary>
		/// <param name="currency">The currency code.</param>
		/// <returns>The currency symbol.</returns>
		private string GetSymbol(string currency)
		{
			switch (currency)
			{
				case "USD":
					return "$";

				case "EUR":
					return "€";

				case "GBP":
					return "£";

				case "INR":
					return "₹";

				default:
					return "";
			}
		}

		/// <summary>
		/// Handles the click event for the exit button, closing the application.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The event data.</param>
		private void exitButton_Click(object sender, RoutedEventArgs e)

		{
			CoreApplication.Exit();
		}
	}
}