using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace Management.Extras.CustomAddons;

public class Alerts
{
    public async void show_SnackBar(Color bgColor, Color txtColor, string Message, int timeSpan)
    {
        var snackbarOptions = new SnackbarOptions
        {
            BackgroundColor = bgColor,
            TextColor = txtColor,
            ActionButtonTextColor = Colors.Black,
            CornerRadius = new CornerRadius(10),
            Font = Microsoft.Maui.Font.SystemFontOfSize(16, FontWeight.Bold)
        };
        var snackbar = Snackbar.Make(Message, () => { /* Action on dismiss */ }, "OK", TimeSpan.FromSeconds(timeSpan), snackbarOptions);
        await snackbar.Show();
    }

    public async void show_ToastBar(string message)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        ToastDuration duration = ToastDuration.Short;
        double fontSize = 14;

        var toast = Toast.Make(message, duration, fontSize);

        await toast.Show(cancellationTokenSource.Token);
    }
}
