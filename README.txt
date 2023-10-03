Proof of Concept with two pages, services, viewmodels and a database. 
Startup page Creates a "Job" and saves it in DB, if not already there. 
Pressing the "Go To PhotoPage!" will navigate user to the PhotoPage.
Loading the photos attached to the "Job"

To re-create the bug/issue:
Add photos to the PhotoPage and navigate back and forth between JobPage and PhotoPage.
With 8-15 Photos added, the app will crash after 15-20 navigations back and forth. 
First the photos starts to turn white, and after few more tries, the app is closed by Os on real device, and just freezes on emulator.
Tested on Ipad Air 5th generation.  
The issue does NOT exist on Android device. 
