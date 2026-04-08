using Microsoft.Extensions.Logging;
using Unhosted_Device_side.Data;
using Unhosted_Device_side.Services;
using MediatR;


namespace Unhosted_Device_side;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});


		builder.Services.AddSingleton(sp =>
            new HttpClient
            {
                //BaseAddress = new Uri("http://localhost:5031")
				//BaseAddress = new Uri("http://10.14.177.33:5031")
				BaseAddress = new Uri("http://127.0.0.1:5031")

            });	

		builder.Services.AddMauiBlazorWebView();
	 	builder.Services.AddSingleton<AppDatabase>();
		builder.Services.AddSingleton<RentService>();
		builder.Services.AddSingleton<RentServiceAPI>();
		builder.Services.AddSingleton<GameService>();
		builder.Services.AddSingleton<GameServiceAPI>();
		builder.Services.AddSingleton<AuthorizeService>();
		builder.Services.AddSingleton<AuthorizeServiceAPI>();
		builder.Services.AddSingleton<UserServiceAPI>();
		builder.Services.AddSingleton<UserService>();


		
		

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
