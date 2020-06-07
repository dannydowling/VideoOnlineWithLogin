Thanks for clicking the link and checking out the page.

I'm ambitious for this website. I own the Preflight.AI domain and I have some things I'm trying to solve.

1. Better Fuel Planning for trips. Like being able to put in a destination and how much weight has been added.
  Then having the website give better values for fuel quantity required. Making things easier than they are.
  To accomplish this, I'll need weather information, I'll need fuel weight and tracking of aircraft load and efficiency.
  
2. I'd like the website to offer collaborative NOTAMS. Notices to Airmen are charts that tell of features used to guide 
  approaches in flight, they're also spoken to pilots from the control tower to keep them aware of things. 
  I'd like to overlay google maps with NOTAM symbols.

3. I plan to hire a designer. I'm okay at the functionality side of things but the website isn't very fancy.
    In that I see real business type potential for the site, I plan to hire a designer when I can afford to soon.
    
    My email address is Danny.Dowling@gmail.com


How this app is sorted:

The Identity Provider:

I've set up identity server 4 to provide verification of user credentials.
I'm using client secrets to validate.

All the clients make calls to the identity provider to log into the application.
Username and Passwords are stored in an IDPContext.

The identity provider is using Identity Framework for views.
It's using OAuth2 and Open ID connect to actually run the validation.

The Server:

The server handles the logic for the application.
It runs the controller actions on the database that stores information from the models like Location and Weather.

it also runs the processing of distance calculations and the backend processing that are the main business logic of the app.

In general, if a client asks for information once they're logged into the app, the server should provide it.

Shared:

The shared project acts as a common folder between the server and the clients.
Since they use a lot of the same objects, like Location or Weather objects which are the same at both ends.

Think of this as DTO's, data transfer objects.
The server would populate from the database and then send to the clients what they ask for.
The shared project allows the data types to be the same on both sides easily.

Client:

The client project contains the views that each of the specific devices share in common.

it simplifies things because the views here aren't device specific.

Android:

The android app contains logic specific to compiling on android.
