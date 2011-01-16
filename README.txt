Dit is de sourcecode van de PwTouch App.

Om programma te starten:
- Installeer driver (x32 of x64): run "Install driver.cmd" als administrator.
- Start MultiTouch.Service.Console.exe
- Start MultiTouch.Configuration.WPF.exe
- PwTouch, dan op blauwe knop met pijl dan scherm sluiten
- Start MultiTouch.Driver.Console.exe
- Start AddIns/PwTouch/PwTouchConfiguration.exe of PwTouchConfiguration.lnk

Om te compilen:
- Extract EXTRACTME.rar (zodat er o.a. een Libraries map komt te staan in de zelfde map als deze README.txt)
- Installeer driver (x32 of x64): run "Install driver.cmd" als administrator.
- Open PwTouchApp.sln met Visual Studio 2010 (ik gebruik Ultimate, maar werkt misschien ook met gratis Express versie).
- Build - Rebuild Solution