Shared:

The shared project acts as a common folder between the server and the clients.
Since they use a lot of the same objects, like Location or Weather objects which are the same at both ends.

Think of this as DTO's, data transfer objects.
The server would populate from the database and then send to the clients what they ask for.
The shared project allows the data types to be the same on both sides easily.