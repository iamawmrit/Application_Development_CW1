using System;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace AD_CW1_20048632_Amrit_Adhikari_C2;

class Program : MauiApplication
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

	static void Main(string[] args)
	{
		var app = new Program();
		app.Run(args);
	}
}
