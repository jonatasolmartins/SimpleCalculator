using SimpleCalculator.Models;

namespace SimpleCalculator;
public partial class MainViewModel : ObservableObject
{

    private readonly IThemeService _themeService;

    public MainViewModel(IThemeService themeService)
    {
        _themeService = themeService;
        IsDark = _themeService.IsDark;
        _themeService.ThemeChanged += (_, _) => IsDark = _themeService.IsDark;
    }

    private bool _isDark;
    public bool IsDark
    {
        get => _isDark;
        set {
                if (SetProperty(ref _isDark, value))
                {
                    _themeService.SetThemeAsync(value ? AppTheme.Dark : AppTheme.Light);
                }
        }
    }

    [ObservableProperty]
    private Calculator _calculator = new();

    [RelayCommand]
    private void Input(string key) => Calculator = Calculator.Input(key);

}

