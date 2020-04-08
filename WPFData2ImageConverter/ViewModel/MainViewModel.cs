using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using WPFPathDataViewer.Misc;
using WPFPathDataViewer.Model;

namespace WPFPathDataViewer.ViewModel
{
  public class MainViewModel : INotifyPropertyChanged
  {

    #region Declarations

    private ColorDetails _selectedColor;

    private string _text;

    #endregion

    #region Constructor

    public MainViewModel()
    {
      Colors = new ObservableCollection<ColorDetails>();
      _selectedColor = new ColorDetails()
      {
        Color = System.Windows.Media.Colors.Black,
        ColorBrush = Brushes.Black,
        Name = System.Windows.Media.Colors.Black.ToString()
      };
      PopulateListColors();
      InitializeCommands();
    }

    #endregion

    #region Properties

    /// <summary>
    /// List of available colors.
    /// </summary>
    public ObservableCollection<ColorDetails> Colors { get; set; }

    /// <summary>
    /// Selected Color from (UI ListBox)".
    /// </summary>
    public ColorDetails SelectedColor
    {
      get => _selectedColor;
      set
      {
        _selectedColor = value;
        RaisePropertyChanged(nameof(SelectedColor));
      }
    }


    /// <summary>
    /// Data Text.
    /// </summary>
    public string Text
    {
      get => _text;
      set
      {
        _text = value;
        RaisePropertyChanged(nameof(Text));
      }
    }


    #endregion

    #region Commands

    #region Declarations

    public RelayCommand CloseApplication { get; private set; }

    public RelayCommand ClearText { get; private set; }

    public RelayCommand CutText { get; private set; }

    public RelayCommand CutTextAsGeometry { get; private set; }

    public RelayCommand CopyText { get; private set; }

    public RelayCommand PasteText { get; private set; }

    public RelayCommand CopyTextAsGeometry { get; private set; }


    #endregion

    #region Initialization

    private void InitializeCommands()
    {

      CloseApplication = new RelayCommand((action) =>
        Application.Current.Shutdown()
      );

      CopyText = new RelayCommand((action) =>
      {
        Clipboard.SetText(Text ?? "");
      });

      CopyTextAsGeometry = new RelayCommand((action) =>
      {
        Clipboard.SetText(GeometryTemplate(Text ?? ""));
      });

      CutText = new RelayCommand((action) =>
      {
        Clipboard.SetText(Text ?? "");
        Text = "";
      });

      CutTextAsGeometry = new RelayCommand((action) =>
      {
        Clipboard.SetText(GeometryTemplate(Text ?? ""));
        Text = "";
      });

      ClearText = new RelayCommand((action) => Text = "");

      PasteText = new RelayCommand((action) => Text = Clipboard.GetText());

    }

    #endregion

    #endregion

    #region Methods

    /// <summary>
    /// Put data into geometry template.
    /// </summary>
    /// <param name="data">data to put in geometry template.</param>
    /// <returns>Geometry template of the data.</returns>
    private static string GeometryTemplate(string data)
    {
      return $@"
              <Geometry x:Key="""">
                {data}
              </Geometry>";
    }

    /// <summary>
    /// Populate colors into colors observable collection.
    /// </summary>
    private void PopulateListColors()
    {

      // Getting available colors.
      var availableColors = GetColors();

      // Clearing the Colors collection (just to prevent unexpected errors).
      Colors.Clear();

      // looping through availableColors we got from above and put them into Colors collection.
      foreach (var color in availableColors)
      {
        Colors.Add(color);
      }

    }

    /// <summary>
    /// Getting list of ColorDetails from available colors.
    /// </summary>
    /// <returns>List of ColorDetails from available colors.</returns>
    private static IEnumerable<ColorDetails> GetColors()
    {
      return
        // Getting colors from media colors.
        typeof(Colors).GetProperties()

          // Ordering colors by name.
          .OrderBy(property => property.Name)

          // filter the list to only color types (Just to make sure we getting the right ones).
          .Where(property => property.PropertyType == typeof(Color))

          // put them into list of ColorDetails class.
          .Select(property => new ColorDetails
          {
            Name = property.Name,
            Color = (Color)property.GetValue(null, null),
            ColorBrush = new SolidColorBrush((Color)property.GetValue(null, null))
          });
    }


    #endregion

    #region InotifyPropertyChanged Implementation

    public event PropertyChangedEventHandler PropertyChanged;

    private void RaisePropertyChanged(string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
  }
}
