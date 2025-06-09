global using DgView.Chaek;
global using DgView.Dowa;
global using Gtk;
global using System;
global using System.IO;
global using System.Linq;
global using PixBitmap = Gdk.Pixbuf;
global using GdkKey = Gdk.Key;
using DgView;

if (!Configs.Initialize())
	return 1;

var filename = args.Length > 1 ? args[1] : string.Empty;
if (!Configs.GeneralRunOnce)
    Work(filename);
else
{
    using var mutex = new System.Threading.Mutex(true, "ksh.DgView.Unique", out var createdNew);
    if (!createdNew)
        return 1;
    Work(filename);
}

Configs.Dispose();
return 0;

static void Work(string filename)
{
    Configs.LoadCaches();

    Application.Init();
	ResL.LoadResources();

    var app = new Application("ksh.DgView", GLib.ApplicationFlags.None);
    app.Register(GLib.Cancellable.Current);

    var win = new DgView.Forms.ReadWindow();
    app.AddWindow(win);

    Application.Run();
	ResL.Dispose();
}
