using Unhosted_Device_side.Data;

namespace Unhosted_Device_side;

public partial class App : Application
{
	public App(AppDatabase database)
	{
		InitializeComponent();
        MainPage = new MainPage();

        Task.Run(async () => await database.InitAsync());
	}

	

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new MainPage()) { Title = "Unhosted_Device_side" };
	}
}
