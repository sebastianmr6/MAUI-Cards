// filepath: /Users/sebastian/Projects/MAUI/Cards/maui_cards/Views/ListPage.xaml.cs
using System;
using System.Threading.Tasks;
using maui_cards.ViewModels;
using maui_cards;

namespace maui_cards.Views;

public partial class ListPage : ContentPage
{
    public ListPage()
    {
        InitializeComponent();
        BindingContext = new ListViewModel();
    }
    
    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            if (e.SelectedItem == null)
                return;
                
            // Obtenemos el ListView
            var listView = (ListView)sender;
            
            // Desactivamos temporalmente el ListView para evitar múltiples selecciones
            listView.IsEnabled = false;
            
            var selectedCard = e.SelectedItem as Card;
            if (selectedCard != null)
            {
                // Usamos una operación asíncrona para no bloquear el hilo principal
                await DisplayAlert("Selección", $"Has seleccionado: {selectedCard.NombreComun}", "OK");
            }
            
            // Deseleccionar el item para permitir seleccionar de nuevo
            listView.SelectedItem = null;
            
            // Reactivamos el ListView después de procesar la selección
            listView.IsEnabled = true;
        }
        catch (Exception ex)
        {
            // Registramos cualquier error para diagnóstico
            Console.WriteLine($"Error en OnItemSelected: {ex.Message}");
        }
    }
    
    private async void OnCardTapped(object sender, TappedEventArgs e)
    {
        try
        {
            // Obtenemos el frame que fue tocado
            if (sender is Frame frame)
            {
                // Efecto visual de pulsación (cambiamos temporalmente el color)
                var originalColor = frame.BackgroundColor;
                frame.BackgroundColor = Colors.DarkSlateBlue;
                
                // Regresamos al color original después de un breve tiempo
                await Task.Delay(150);
                frame.BackgroundColor = originalColor;
            }
            
            if (e.Parameter == null)
                return;
                
            // Obtenemos la tarjeta desde el parámetro del comando
            var selectedCard = e.Parameter as Card;
            if (selectedCard != null)
            {
                // Usamos una operación asíncrona para no bloquear el hilo principal
                await DisplayAlert("Selección desde Tap", $"Has tocado: {selectedCard.NombreComun}", "OK");
            }
        }
        catch (Exception ex)
        {
            // Registramos cualquier error para diagnóstico
            Console.WriteLine($"Error en OnCardTapped: {ex.Message}");
        }
    }
}
