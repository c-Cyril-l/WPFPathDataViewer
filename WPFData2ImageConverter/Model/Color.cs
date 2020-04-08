using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using WPFPathDataViewer.Properties;

namespace WPFPathDataViewer.Model
{
  public class ColorDetails : INotifyPropertyChanged
  {

    #region Declarations

    private Brush _colorBrush;

    private Color _color;

    private string _name;

    #endregion

    #region Properties

    /// <summary>
    /// Color.
    /// </summary>
    public Color Color
    {
      get => _color;
      set
      {
        _color = value;
        RaisePropertyChanged(nameof(Color));
      }
    }

    /// <summary>
    /// Brush from Color property.
    /// </summary>
    public Brush ColorBrush
    {
      get => _colorBrush;
      set
      {
        _colorBrush = value;
        RaisePropertyChanged(nameof(ColorBrush));
      }
    }

    /// <summary>
    /// Brush from Color property.
    /// </summary>
    public string Name
    {
      get => _name;
      set
      {
        _name = value;
        RaisePropertyChanged(nameof(Name));
      }
    }

    #endregion

    #region InotifyPropertyChanged Implementation

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

  }
}