namespace MauiDashboardApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            if (Maximize.IsVisible)
            {
                var animation = new Animation((current) =>
                {
                    FlyoutWidth = current;
                }, 65, 250, null);

                animation.Commit(this, "expond", finished: (value, cancelled) =>
                {
                    Maximize.IsVisible = false;
                    Minimize.IsVisible = true;
                });
            }
            else
            {
                var animation = new Animation((current) =>
                {
                    FlyoutWidth = current;
                }, 250, 65, null);

                animation.Commit(this, "minimize", finished: (value, cancelled) =>
                {
                    Maximize.IsVisible = true;
                    Minimize.IsVisible = false;
                });
            }
        }
    }
}
