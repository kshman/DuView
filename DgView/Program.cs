global using DgView.Chaek;
//global using DgView.Properties;
global using DgView.Dowa;
global using Gtk;
global using System;
global using System.Collections;
global using System.Collections.Generic;
global using System.IO;
global using System.Linq;
global using PixBitmap = Gdk.Pixbuf;

Application.Init();

var app = new Application("ksh.DgView", GLib.ApplicationFlags.None);
app.Register(GLib.Cancellable.Current);

var win = new DgView.Forms.TestComplex();
app.AddWindow(win);

win.ShowAll();

Application.Run();

