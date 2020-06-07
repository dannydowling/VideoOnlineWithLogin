The Identity Provider:

I've set up identity server 4 to provide verification of user credentials.
I'm using client secrets to validate.

All the clients make calls to the identity provider to log into the application.
Username and Passwords are stored in an IDPContext.

The identity provider is using Identity Framework for views.
It's using OAuth2 and Open ID connect to actually run the validation.