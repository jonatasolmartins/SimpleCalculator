using SimpleCalculator.Styles;

namespace SimpleCalculator;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.Resources(r => r
                .Add(AppIcons.Dark)
                .Add(AppIcons.Light))
            .Background(Theme.Brushes.Background.Default)
            .DataContext(new MainViewModel(this.GetThemeService()), (page, vm) => page
                .Content(
                new Border()
                    .SafeArea(SafeArea.InsetMask.VisibleBounds)
                    .Background(Theme.Brushes.Secondary.Container.Default)
                .Child
                (
                    new Grid()
                    .RowDefinitions<Grid>("Auto,*,Auto,Auto")
                    .MaxWidth(700)
                    .VerticalAlignment(VerticalAlignment.Stretch)
                    .Children
                    (
                        Header(vm),
                        Output(vm),
                        Keypad(vm)
                    )
                ))
            );
    }

    private static UIElement Header(MainViewModel vm) =>
    new ToggleButton()
        .Grid(row: 0)
        .Margin(8, 24, 8, 0)
        .CornerRadius(20)
        .VerticalAlignment(VerticalAlignment.Top)
        .HorizontalAlignment(HorizontalAlignment.Center)
        .Background(Theme.Brushes.Secondary.Container.Default)
        .Style(Theme.ToggleButton.Styles.Icon)
        .IsChecked(x => x.Bind(() => vm.IsDark).TwoWay())
        .Content
        (
            new PathIcon()
                .Data(AppIcons.Light)
                .Foreground(Theme.Brushes.Primary.Default)
        )
        .ControlExtensions
        (
            alternateContent:
            new PathIcon()
                .Data(AppIcons.Dark)
                .Foreground(Theme.Brushes.Primary.Default)
        );


    private static UIElement Output(MainViewModel vm) =>
        new StackPanel()
            .Grid(row: 2)
            .Spacing(16)
            .Padding(16, 8)
            .HorizontalAlignment(HorizontalAlignment.Stretch)
            .Children
            (
                new TextBlock()
                    .Text(() => vm.Calculator.Equation)
                    .HorizontalAlignment(HorizontalAlignment.Right)
                    .Foreground(Theme.Brushes.OnSecondary.Container.Default)
                    .Style(Theme.TextBlock.Styles.DisplaySmall),
                 new TextBlock()
                    .Text(() => vm.Calculator.Output)
                    .HorizontalAlignment(HorizontalAlignment.Right)
                    .Foreground(Theme.Brushes.OnBackground.Default)
                    .Style(Theme.TextBlock.Styles.DisplayLarge)
            );


    private static Button KeyPadButton(
        MainViewModel vm,
        int gridRow,
        int gridColumn,
        object content,
        string? parameter = null) =>
        new Button()
        .Command(() => vm.InputCommand)
        .CommandParameter(parameter ?? content)
        .Content(content)
        .Grid(row: gridRow, column: gridColumn)
        .FontSize(32)
        .Height(72)
        .HorizontalAlignment(HorizontalAlignment.Stretch)
        .VerticalAlignment(VerticalAlignment.Stretch)
        .ControlExtensions(elevation: 0)
        .Style(Theme.Button.Styles.Elevated);


    private static Button KeyPadPrimaryButton(
        MainViewModel vm,
        int gridRow,
        int gridColumn,
        object content,
        string? parameter = null) =>
        new Button()
        .Command(() => vm.InputCommand)
        .CommandParameter(parameter ?? content)
        .Content(content)
        .Grid(row: gridRow, column: gridColumn)
        .FontSize(32)
        .Height(72)
        .HorizontalAlignment(HorizontalAlignment.Stretch)
        .VerticalAlignment(VerticalAlignment.Stretch)
        .Style(Theme.Button.Styles.Filled);

    private static Button KeyPadSecondaryButton(
            MainViewModel vm,
            int gridRow,
            int gridColumn,
            object content,
            Action<IDependencyPropertyBuilder<Brush>>? background = null,
            string? parameter = null) =>
            new Button()
            .Command(() => vm.InputCommand)
            .CommandParameter(parameter ?? content)
            .Content(content)
            .Grid(row: gridRow, column: gridColumn)
            .FontSize(32)
            .Height(72)
            .Background(background ?? Theme.Brushes.OnSurface.Inverse.Default)
            .HorizontalAlignment(HorizontalAlignment.Stretch)
            .VerticalAlignment(VerticalAlignment.Stretch)
            .Style(Theme.Button.Styles.FilledTonal);

    private static UIElement Keypad(MainViewModel vm) =>
    new Grid()
        .Grid(row: 3)
        .RowSpacing(16)
        .ColumnSpacing(16)
        .Padding(16)
        .MaxHeight(500)
        .ColumnDefinitions<Grid>("*,*,*,*")
        .RowDefinitions<Grid>("*,*,*,*,*")
        .Children
        (
            // Row 0
            KeyPadSecondaryButton(vm, 0, 0, "C"),
            KeyPadSecondaryButton(vm, 0, 1, "±"),
            KeyPadSecondaryButton(vm, 0, 2, "%"),
            KeyPadPrimaryButton(vm, 0, 3, "÷"),

            // Row 1
            KeyPadButton(vm, 1, 0, "7"),
            KeyPadButton(vm, 1, 1, "8"),
            KeyPadButton(vm, 1, 2, "9"),
            KeyPadPrimaryButton(vm, 1, 3, "×"),

            // Row 2
            KeyPadButton(vm, 2, 0, "4"),
            KeyPadButton(vm, 2, 1, "5"),
            KeyPadButton(vm, 2, 2, "6"),
            KeyPadPrimaryButton(vm, 2, 3, "−"),

            //Row 3
            KeyPadButton(vm, 3, 0, "1"),
            KeyPadButton(vm, 3, 1, "2"),
            KeyPadButton(vm, 3, 2, "3"),
            KeyPadPrimaryButton(vm, 3, 3, "+"),

            //Row 4
            KeyPadButton(vm, 4, 0, "."),
            KeyPadButton(vm, 4, 1, "0"),
            KeyPadButton(vm, 4, 2, new FontIcon().Glyph("\uE926").FontSize(36), parameter: "back"),
            KeyPadPrimaryButton(vm, 4, 3, "=")
        );
}

