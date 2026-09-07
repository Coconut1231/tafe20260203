using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Calculator
{
	public sealed partial class MainMenu : Page
	{
		public MainMenu()
		{
			this.InitializeComponent();
		}

		/// <summary>
		/// Navigates to the Math Calculator page when the MathCalculatorButton is clicked.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The event data.</param>
		private void MathCalculatorButton_Click(object sender, RoutedEventArgs e)
		{
			{ Frame.Navigate(typeof(MainPage)); }

		}

		/// <summary>
		/// Handles the click event for the exit button, closing the application.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The event data.</param>
		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			CoreApplication.Exit();
		}

		/// <summary>
		///	Navigates to the Foreign Exchange Currency Calculator page when the CurrencyCalculatorButton is clicked.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The event data.</param>
		private void CurrencyCalculatorButton_Click(object sender, RoutedEventArgs e)
		{
			{
				{ Frame.Navigate(typeof(ForeignExchangeCurrencyCalculator)); }
			}
		}

		/// <summary>
		/// Navigates to the Mortgage Calculator page when the MortgageCalculatorButton is clicked.	
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MortgageCalculatorButton_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}


